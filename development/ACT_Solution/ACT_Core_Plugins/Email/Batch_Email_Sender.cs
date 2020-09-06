using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Extensions;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.DataAccess;

namespace ACT.Plugins.Email
{
	public class Batch_Email_Sender : ACT.Core.Interfaces.Common.I_Execute
    {
		/// <summary>
		/// Static Information Class
		/// </summary>
		public static class Information
		{
#if CLUSTER
			//TODO MARK BUILD CLUSTER
			public static bool IsRunning { get { } }
#else
			public static bool IsRunning = false;
#endif
		}

		private bool _NeedsExecute;

		private string _ACTCloud_Connection = ACT.Core.SystemSettings.GetSettingByName( "ACT_Cloud_ConnectionString" ).Value;
		private string _Connection = ACT.Core.SystemSettings.GetSettingByName( "Stamp_ConnectionString" ).Value;
		private string _Environment = ACT.Core.SystemSettings.GetSettingByName( "Environment" ).Value;
        private static string _actCloudDbConnection = "";
        private static string _stampDbConnection = "";
        private static string _environment = "";
        private static int _ticksBeforeEmail = 0;
        private static int _ticksSinceLastEmail = 0;
        private static bool _verboseDebugging = ACT.Core.SystemSettings.GetSettingByName( "VerboseDebugging" ).Value.ToBool();
        private static int _useMailProvider = ACT.Core.SystemSettings.GetSettingByName( "USE_MAIL_TYPE" ).Value.ToInt();
        private static string _applicationId = ACT.Core.SystemSettings.GetSettingByName( "EMAIL_APPLICATION_ID" ).Value;

        /// <summary>
        /// Whether or not the service needs to execute
        /// </summary>
        public bool NeedsExecute
		{
			get
			{
				//ACT CORE LOGIC GATE
				if ( Information.IsRunning ) {
					return false;
				}

				var _data = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent();
				_data.Open( ACT.Core.SystemSettings.GetSettingByName( "ACT_Cloud_ConnectionString" ).Value );
				var results = _data.RunCommand( "BATCHEMAIL_NEEDSEXECUTE", null, true, System.Data.CommandType.StoredProcedure ).FirstDataTable_WithRows();

				if ( results != null )
				{
					_NeedsExecute = true;
				}
				else {
					_NeedsExecute = false;
				}

				return _NeedsExecute;
			}
			set
			{
				_NeedsExecute = value;
			}
		}

        /// <summary>
        /// Executes the ParseEmail function
        /// </summary>
        /// <returns>ACT.Core.Interfaces.Common.I_TestResult</returns>
		public I_TestResult Execute()
		{
			Information.IsRunning = true;
			var testResult = ParseEmail( _ACTCloud_Connection, _Connection, _Environment );
			Information.IsRunning = false;
			return testResult;
		}

		/// <summary>
		/// Get the configuration info from ACTCloud
		/// </summary>
		/// <returns>A datarow with the configuration info</returns>
		private static I_QueryResult GetConfigurationSettings(string setting, DataTable tblParams )
		{

			return null;
			// GET THE SQLCommandText FROM ACT_Application_BATCHEMAIL_Configuration
			//var configuration = ACTCloud.Database.BATCHEMAIL.GET.CONFIGURATION.Execute.Proc( setting ).FirstDataTable_WithRows().Rows[0];

			//// USE THE SQLCommandText
			//using ( var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent() )
			//{
			//	I_QueryResult results;
			//	var commandText = configuration["SQLCommandText"].ToString();
			//	var isProc = configuration["IsProc"].ToBool();
			//	var connection = configuration["ConnectionName"].ToString();

			//	DataAccess.Open( ACT.Core.SystemSettings.GetSettingByName( connection ).Value );

			//	if ( isProc == true )
			//	{
			//		// TODO Mark: We need to figure how to pass dynamic parameters
			//		List<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();

			//		if ( tblParams != null )
			//		{
			//			_Params.Add( new System.Data.SqlClient.SqlParameter( "@MANY_NVARCHARS", tblParams ) );
			//		}

			//		results = DataAccess.RunCommand( commandText, _Params, true, System.Data.CommandType.StoredProcedure );
			//	}
			//	else
			//	{
			//		results = DataAccess.RunCommand( commandText, null, true, System.Data.CommandType.Text );
			//	}

			//	return results;
			//}
		}

        /// <summary>
        /// Parses and sends out emails
        /// </summary>
        /// <param name="actCloudDbConnection">ACTCloud database connection</param>
        /// <param name="stampDbConnection">Stamp database connection</param>
        /// <param name="environment">Current environment</param>
        /// <returns>ACT.Core.Interfaces.Common.I_TestResult</returns>
		public static I_TestResult ParseEmail( string actCloudDbConnection, string stampDbConnection, string environment )
		{
			_actCloudDbConnection = actCloudDbConnection;
			_stampDbConnection = stampDbConnection;
			_environment = environment;

			var testResult = ACT.Core.CurrentCore<I_TestResult>.GetCurrent();
			testResult.Success = true;

			testResult = SetServiceRunningState( testResult );

			if ( testResult.Success == false )
			{
				testResult = ResetServiceRunningState( testResult );
				return testResult;
			}

			WriteToEventLog( "Executing UpdateTickCount" );

			// WE DON'T WANT TO TRY TO SEND SMTP ON EVERY TIMER TICK BECAUSE IT SENDS THE ADMIN AN EMAIL IF SMTP FAILS
			// SO UPDATE THE TICK AND RUN THE SMTP SEND IF THE CURRENT TICK MATCHES TICKS IN THE SETTINGS
			UpdateTickCount();

			I_QueryResult emailBatches = GetAllBatchesReadyToSend();

			if ( emailBatches == null )
			{
				testResult = ResetServiceRunningState( testResult );
				return testResult;
			}

			int emailSentCount = 0;

			// LOOP THROUGH BATCHES
			foreach ( DataTable emailSubBatches in emailBatches.Tables )
			{
				if ( emailSubBatches.Rows.Count == 0 )
				{
					break;
				}

				emailSentCount = emailSubBatches.Rows.Count;

				PopulateSubBatchAndSend( emailSubBatches );
			}

			if ( emailSentCount < 50 )
			{
				testResult = UpdateBatchStatusesToSent( testResult );
			}

			testResult = ResetServiceRunningState( testResult );

			return testResult;
		}

        /// <summary>
        /// Creates a table of strings
        /// </summary>
        /// <returns>Table of strings</returns>
		private static DataTable GetParametersTable()
		{
			var dtParameters = new DataTable();

			#region Add Columns
			dtParameters.Columns.Add( "STRINGA", typeof( string ) );
			dtParameters.Columns.Add( "STRINGB", typeof( string ) );
			dtParameters.Columns.Add( "STRINGC", typeof( string ) );
			dtParameters.Columns.Add( "STRINGD", typeof( string ) );
			dtParameters.Columns.Add( "STRINGE", typeof( string ) );
			dtParameters.Columns.Add( "STRINGF", typeof( string ) );
			dtParameters.Columns.Add( "STRINGG", typeof( string ) );
			dtParameters.Columns.Add( "STRINGH", typeof( string ) );
			dtParameters.Columns.Add( "STRINGI", typeof( string ) );
			dtParameters.Columns.Add( "STRINGJ", typeof( string ) );
			dtParameters.Columns.Add( "STRINGK", typeof( string ) );
			dtParameters.Columns.Add( "STRINGL", typeof( string ) );
			dtParameters.Columns.Add( "STRINGM", typeof( string ) );
			dtParameters.Columns.Add( "STRINGN", typeof( string ) );
			dtParameters.Columns.Add( "STRINGO", typeof( string ) );
			dtParameters.Columns.Add( "STRINGP", typeof( string ) );
			dtParameters.Columns.Add( "STRINGQ", typeof( string ) );
			dtParameters.Columns.Add( "STRINGR", typeof( string ) );
			dtParameters.Columns.Add( "STRINGS", typeof( string ) );
			dtParameters.Columns.Add( "STRINGT", typeof( string ) );
			dtParameters.Columns.Add( "STRINGU", typeof( string ) );
			dtParameters.Columns.Add( "STRINGV", typeof( string ) );
			dtParameters.Columns.Add( "STRINGW", typeof( string ) );
			dtParameters.Columns.Add( "STRINGX", typeof( string ) );
			dtParameters.Columns.Add( "STRINGY", typeof( string ) );
			dtParameters.Columns.Add( "STRINGZ", typeof( string ) );
			#endregion

			return dtParameters;
		}

        /// <summary>
        /// Update the batches status to sent
        /// </summary>
        /// <param name="testResult">ACT.Core.Interfaces.Common.I_TestResult</param>
        /// <returns>ACT.Core.Interfaces.Common.I_TestResult</returns>
		private static I_TestResult UpdateBatchStatusesToSent( I_TestResult testResult )
		{
			try
			{
				WriteToEventLog( "Updating Batch Sent" );

				var result = GetConfigurationSettings( "UpdateBatchesSent", null ).FirstDataTable_WithRows();
				return testResult;
			}
			catch ( Exception ex )
			{
				string message = "ParseEmail.UpdateBatchesSent: " + ex.Message;
				WriteToEventLog( message );
				SendAdminEmail( message, null );
				testResult.Success = false;
				return testResult;
			}
		}

        /// <summary>
        /// Populate the sub batch and send
        /// </summary>
        /// <param name="subBatch">A table with all of the sub-batch records</param>
        /// <returns>Whether the records were sent</returns>
		private static bool PopulateSubBatchAndSend( DataTable subBatch )
		{
			if ( subBatch.Rows[0].RowState == DataRowState.Deleted )
			{
				return true;
			}

			// THIS IS FOR THE BATCH NOT THE RECORD, BUT ONLY NON-SENT ARE INCLUDED FOR THE BATCH
			// SENT SUCCESS ARE NOT IN THE BATCH
			bool prevSendFailed = subBatch.Rows[0]["FailedToSend"].ToString().ToBool();

			var recsToUpdate = GetParametersTable(); // CreateBatchRecordsTable();
			var updateBodies = GetParametersTable(); // CreateBatchRecordBodiesTable();

			foreach ( DataRow r in subBatch.Rows )
			{
				if ( r.RowState != DataRowState.Deleted )
				{
					updateBodies = CreateSmtpMessageAndSendSmtp( recsToUpdate, updateBodies, r );
				}
			}

			// THE UPDATE TO THE BODIES IS SEPARATE FROM THE INDIVIDUAL COL UPDATE BECAUSE
			// WE ARE SENDING A TABLE FULL OF MESSAGE BODIES THAT COULD BE QUITE LARGE.
			if ( updateBodies.Rows.Count > 0 )
			{
				if ( !UpdateEmailMessageBodies( updateBodies ) )
				{
					return false;
				}
			}

			if ( recsToUpdate.Rows.Count > 0 )
			{
				if ( !UpdateRecords( recsToUpdate ) )
				{
					return false;
				}
			}

			return true;
		}

        /// <summary>
        /// Get the dynamic sql and execute to update batch records
        /// </summary>
        /// <param name="recsToUpdate">A table with all of the records to update</param>
        /// <returns>bool whether the execution was successful</returns>
		private static bool UpdateRecords( DataTable recsToUpdate )
		{
			try
			{
				WriteToEventLog( "Preparing Batch: PROC_EMAIL_UPDATE_BATCH_RECORDS" );

				var result = GetConfigurationSettings( "UpdateBatchesSent", recsToUpdate ).FirstDataTable_WithRows();
				return true;
			}
			catch ( Exception ex )
			{
				string message = "PROC_EMAIL_UPDATE_BATCH_RECORDS: " + ex.Message;
				WriteToEventLog( message );
				SendAdminEmail( message, null );
				return false;
			}
		}

        /// <summary>
        /// Get the dynamic sql and execute to update batch record message
        /// bodies with the tracking image
        /// </summary>
        /// <param name="updateBodies">A table with all of the updated bodies</param>
        /// <returns>bool whether the update was successful</returns>
		private static bool UpdateEmailMessageBodies( DataTable updateBodies )
		{
			try
			{
				WriteToEventLog( "Preparing Batch: UpdateBatchRecordEmailBodies" );

				var result = GetConfigurationSettings( "UpdateBatchRecordEmailBodies", updateBodies ).FirstDataTable_WithRows();

				return true;
			}
			catch ( Exception ex )
			{
				string message = "PROC_EMAIL_UPDATE_BATCH_RECORD_EMAIL_BODIES: " + ex.Message;
				WriteToEventLog( message );
				SendAdminEmail( message, null );
				return false;
			}
		}

        /// <summary>
        /// Get smtp settings, prepare message and send
        /// </summary>
        /// <param name="recsToUpdate">A table with all of the email records</param>
        /// <param name="updateBodies">A table with all of the updated bodies</param>
        /// <param name="emailRec">A row with the email record</param>
        /// <returns></returns>
		private static DataTable CreateSmtpMessageAndSendSmtp( DataTable recsToUpdate, DataTable updateBodies, DataRow emailRec )
		{
			#region Set SMTP vars
			string smtp_userName = "", smtp_password = "", smtp_host = "", from = "";
			int smtp_port = 0;

			// Note that the "from" var will be reassigned below to the value from the db
			// we are just passing it in here because the same function is used in other places
			// where isn't not reassigned
			GetSmtpSettings( out smtp_userName, out smtp_password, out smtp_host, out smtp_port, out from );

			string batchId = "", subBatchNo = "", email = "", accountManager = "",
				replyTo = "", bccConnector = "", subject = "", body = "", urlShortCode = "";
			var emailsAndMessageIds = new Dictionary<string, Guid>();
			int totalCountForSubBatch = 0;
			Guid messageId;
			#endregion

			// Try to send SMTP
			// If some succeed then PartialSuccess = TRUE
			// If all succeed then insert/update the ACT_Email_Batch_SubBatch_History table 
			// with FALSE for PartialSuccess, AttemptedToSend, and FailedToSend
			// If none succeed then FailedToSend = TRUE
			#region Prepare and Send
			WriteToEventLog( "Preparing SMTP: PopulateMessageAttributes" );

			try
			{
				PopulateMessageAttributes( emailRec, out urlShortCode, out batchId, out subBatchNo, out totalCountForSubBatch, out email, out from, out accountManager, out replyTo, out bccConnector, out subject, out body, out messageId );
			}
			catch ( Exception ex )
			{
				string message = "CreateSmtpMessageAndSendSmtp.PopulateMessageAttributes: " + ex.Message;
				WriteToEventLog( message );
				SendAdminEmail( message, null );
				return null;
			}

			SendUsingSmtp( smtp_host, smtp_port, smtp_userName, smtp_password, from, accountManager, email, replyTo, bccConnector, subject, body, batchId, subBatchNo );

			WriteToEventLog( "Preparing SMTP: AddRowToTableBatchRecords" );

			recsToUpdate = AddRowToTableBatchRecords( recsToUpdate, messageId, email, urlShortCode );

			WriteToEventLog( "Preparing SMTP: AddRowToTableUpdateBodies" );

			updateBodies = AddRowToTableUpdateBodies( updateBodies, emailRec["Batch_Record_ID"].ToString().ToInt(), body );

			#endregion

			return updateBodies;
		}

        /// <summary>
        /// Insert a row into the table with updated bodies
        /// </summary>
        /// <param name="updateBodies">A table with all of the email records</param>
        /// <param name="batchRecordId">The batch record Id</param>
        /// <param name="body">The updated body of the email record</param>
        /// <returns>A table with the email records and the newly updated body</returns>
		private static DataTable AddRowToTableUpdateBodies( DataTable updateBodies, int batchRecordId, string body )
		{
			var drUpdateBody = updateBodies.NewRow();
			drUpdateBody["STRINGA"] = batchRecordId;
			drUpdateBody["STRINGB"] = body;
			updateBodies.Rows.Add( drUpdateBody );
			return updateBodies;
		}

        /// <summary>
        /// Inserts a batch email row into a table of all emails
        /// </summary>
        /// <param name="dtBatchRecords">A table with all of the emails</param>
        /// <param name="messageId">The message Id of the email</param>
        /// <param name="email">The email body</param>
        /// <param name="urlShortCode">The short url that goes in the email body</param>
        /// <returns></returns>
		private static DataTable AddRowToTableBatchRecords( DataTable dtBatchRecords, Guid messageId, string email, string urlShortCode )
		{
			var drBatchRecord = dtBatchRecords.NewRow();
			drBatchRecord["STRINGA"] = messageId;
			drBatchRecord["STRINGB"] = email;
			drBatchRecord["STRINGC"] = urlShortCode;
			drBatchRecord["STRINGD"] = "&MID=" + messageId.ToString();
			dtBatchRecords.Rows.Add( drBatchRecord );
			return dtBatchRecords;
		}

        /// <summary>
        /// Send using smtp
        /// </summary>
        /// <param name="smtp_host">The smtp host url</param>
        /// <param name="smtp_port">The smtp port</param>
        /// <param name="smtp_userName">The smtp username</param>
        /// <param name="smtp_password">The smtp password</param>
        /// <param name="from">The smtp send from address</param>
        /// <param name="accountManager">The account manager name that will show as the sender</param>
        /// <param name="email">The email address to send to</param>
        /// <param name="replyTo">The email address to reply to</param>
        /// <param name="bccConnector">The email address to send as bcc</param>
        /// <param name="subject">The email subject</param>
        /// <param name="body">The email body</param>
        /// <param name="batchId">The batch Id of the batch</param>
        /// <param name="subBatchNo">The sub-batch number</param>
		private static void SendUsingSmtp( string smtp_host, int smtp_port, string smtp_userName, string smtp_password, string from, string accountManager,
			string email, string replyTo, string bccConnector, string subject, string body, string batchId, string subBatchNo )
		{
			try
			{
				WriteToEventLog( "Preparing SMTP: client.Send" );

				SendSmtp( smtp_host, smtp_port, smtp_userName, smtp_password, from, null, email,
				subject, body, bccConnector, replyTo, accountManager );
			}
			catch ( Exception ex )
			{
				string message = "SendUsingSmtp: " + ex.Message;
				WriteToEventLog( message );

				var dtBatchRecords = GetParametersTable();
				var drBatchRecord = dtBatchRecords.NewRow();
				drBatchRecord["STRINGA"] = batchId;
				drBatchRecord["STRINGB"] = subBatchNo;
				drBatchRecord["STRINGC"] = false; // PartialSuccess
				drBatchRecord["STRINGD"] = true; // AttemptedToSend
				drBatchRecord["STRINGE"] = true; // FailedToSend
				dtBatchRecords.Rows.Add( drBatchRecord );

				var result = GetConfigurationSettings( "UpdateBatchHistory", dtBatchRecords ).FirstDataTable_WithRows();
				SendAdminEmail( message, batchId, subBatchNo, body, from, email, replyTo, subject, smtp_userName, smtp_password, smtp_host, smtp_port );
				return;
			}
		}

        /// <summary>
        /// Send email to admin using smtp
        /// </summary>
        /// <param name="messageBody">Email message body</param>
        /// <param name="batchId">The batch Id of the batch</param>
        /// <param name="subBatchNo">The sub-batch number</param>
        /// <param name="body">The email body</param>
        /// <param name="from">The smtp send from address</param>
        /// <param name="email">The email address to send to</param>
        /// <param name="replyTo">The email address to reply to</param>
        /// <param name="subject">The email subject</param>
        /// <param name="smtp_userName">The smtp username</param>
        /// <param name="smtp_password">The smtp password</param>
        /// <param name="smtp_host">The smtp host url</param>
        /// <param name="smtp_port">The smtp port</param>
		private static void SendAdminEmail( string messageBody, string batchId, string subBatchNo, string body, string from, string email,
			string replyTo, string subject, string smtp_userName, string smtp_password, string smtp_host, int smtp_port )
		{
			// We don't want to try to send SMTP on every timer tick because it sends the admin an email if SMTP fails
			if ( _ticksSinceLastEmail != _ticksBeforeEmail )
			{
				return;
			}

			string adminEmailBody = "";
			adminEmailBody += "Batch ID: " + batchId + "<br>";
			adminEmailBody += "Sub Batch ID: " + subBatchNo + "<br>";

			adminEmailBody += "The batch failed to send.<br>";

			if ( messageBody != null )
			{
				adminEmailBody += "Exception: " + messageBody + "<br><br>";
			}

			adminEmailBody += "Environment: " + _environment;

			string errorAddresses = ACT.Core.SystemSettings.GetSettingByName( "ERROR_NOTIFICATION_ADDRESSES" ).Value;
			var errorAddressList = errorAddresses.SplitString( ",", StringSplitOptions.RemoveEmptyEntries );

			SendSmtp( smtp_host, smtp_port, smtp_userName, smtp_password, from, errorAddressList, null,
				"EMAIL BATCH ERROR IN " + _environment, adminEmailBody, null, null, null );
		}

        /// <summary>
        /// The final send smtp method that actually sends out the emails
        /// </summary>
        /// <param name="smtp_host">The smtp host url</param>
        /// <param name="smtp_port">The smtp port</param>
        /// <param name="smtp_userName">The smtp username</param>
        /// <param name="smtp_password">The smtp password</param>
        /// <param name="from">The smtp send from address</param>
        /// <param name="toList">List of email addresses to send to</param>
        /// <param name="to">The email address to send to</param>
        /// <param name="subject">The email subject</param>
        /// <param name="emailBody">The email body</param>
        /// <param name="bccConnector">The email address to send as bcc</param>
        /// <param name="replyTo">The email address to reply to</param>
        /// <param name="accountManager">The account manager name that will show as the sender</param>
		private static void SendSmtp( string smtp_host, int smtp_port, string smtp_userName, string smtp_password,
			string from, string[] toList, string to, string subject, string emailBody, string bccConnector, string replyTo,
			string accountManager )
		{
			using ( SmtpClient client = new SmtpClient( smtp_host, smtp_port ) )
			{
				client.Credentials = new NetworkCredential( smtp_userName, smtp_password );
				client.EnableSsl = true;
				using ( MailMessage message = new MailMessage() )
				{
					message.From = new MailAddress( from );

					if ( toList != null )
					{
						foreach ( var s in toList.ToList() )
						{
							message.To.Add( s );
						}
					}

					if ( !bccConnector.NullOrEmpty() )
					{
						message.Bcc.Add( bccConnector );
					}

					if ( !replyTo.NullOrEmpty() )
					{
						message.ReplyToList.Add( new MailAddress( replyTo ) );
					}

					if ( !accountManager.NullOrEmpty() )
					{
						message.From = new MailAddress( from, accountManager );
					}
					else
					{
						message.From = new MailAddress( from );
					}

					message.Subject = subject;
					message.Body = emailBody;
					message.IsBodyHtml = true;
					client.Send( message );
				}
			}
		}

        /// <summary>
        /// Populates all of the message attributes
        /// </summary>
        /// <param name="dr">The record to update</param>
        /// <param name="urlShortCode">The short url for the email body</param>
        /// <param name="batchId">The batch Id</param>
        /// <param name="subBatchNo">The sub batch number</param>
        /// <param name="totalCountForSubBatch">The total number of records in the sub batch</param>
        /// <param name="email">The email address to send to</param>
        /// <param name="from">The smtp send from address</param>
        /// <param name="accountManager">The account manager name that will show as the sender</param>
        /// <param name="replyTo">The email address to reply to</param>
        /// <param name="bccConnector">The email address to send as bcc</param>
        /// <param name="subject">The email subject</param>
        /// <param name="body">The email body</param>
        /// <param name="messageId">The message Id of the record</param>
		private static void PopulateMessageAttributes( DataRow dr, out string urlShortCode, out string batchId, out string subBatchNo, out int totalCountForSubBatch, out string email, out string from, out string accountManager, out string replyTo, out string bccConnector, out string subject, out string body, out Guid messageId )
		{
			batchId = dr["Batch_ID"].ToString();
			subBatchNo = dr["SubBatchNumber"].ToString();
			totalCountForSubBatch = dr["CNT"].ToString().ToInt();
			from = dr["FromEmail"].ToString();
			accountManager = dr["AccountManager"].ToString();
			replyTo = dr["ReplyToEmail"].ToString();
			bccConnector = dr["BccConnector"].ToString();
			subject = dr["Subject"].ToString();
			body = dr["Body"].ToString();
			urlShortCode = dr["UrlShortCode"].ToString();
			messageId = Guid.NewGuid();
			body = AddTrackingImage( body, messageId );

			if ( body.Contains( "StartSurvey" ) ) // long link
			{
				body = body.Replace( "&MID=", "&MID=" + messageId );
			}
			else
			{
				body = body.Replace( "&MID=", "" );
			}

			email = dr["ToEmail"].ToString();
		}

        /// <summary>
        /// Add a tracking image to the email body
        /// </summary>
        /// <param name="body">The email body</param>
        /// <param name="messageId">The message Id of the email</param>
        /// <returns>string with the updated body containing the tracking image</returns>
		private static string AddTrackingImage( string body, Guid messageId )
		{
			body = body.Replace( "###TRACKINGID###", messageId.ToString() );
			
			return body;
		}

        /// <summary>
        /// Gets all of the batch records that are ready to send out
        /// </summary>
        /// <returns>all of the emails that are ready to send out</returns>
		private static I_QueryResult GetAllBatchesReadyToSend()
		{
			I_QueryResult results = null;
			I_QueryResult canSendResults = null;

			try
			{
				results = GetConfigurationSettings( "GetBatchRecordsForSend", null );

				if ( results == null )
				{
					return null;
				}
			}
			catch ( Exception ex )
			{
				string message = "GetAllBatchesReadyToSend.GetBatchRecordsForSend: " + ex.Message;
				WriteToEventLog( message );
				SendAdminEmail( message, null );
				return results;
			}

			var canSendDt = GetParametersTable();
			foreach ( DataTable d in results.Tables )
			{
				foreach ( DataRow r in d.Rows )
				{
					var drBatchRecord = canSendDt.NewRow();
					drBatchRecord["STRINGA"] = _applicationId.ToString();
					drBatchRecord["STRINGB"] = r["ToEmail"].ToString();
					canSendDt.Rows.Add( drBatchRecord );
				}
			}

			try
			{
				// Just in case.  The type passed to the stored proc can't have dups
				var distinctCanSendDt = canSendDt.DefaultView.ToTable( true, "STRINGB" );
				canSendResults = GetConfigurationSettings( "ValidateCanSendToEmail", distinctCanSendDt );
			}
			catch ( Exception ex )
			{
				string message = "GetAllBatchesReadyToSend.ValidateCanSendToEmail: " + ex.Message;
				WriteToEventLog( message );
				SendAdminEmail( message, null );
				return results;
			}

			results = RemoveRecsCantSend( results, canSendResults );

			return results;
		}

        /// <summary>
        /// Remove the records in the sub batch that failed to send previously
        /// </summary>
        /// <param name="results">All of the records</param>
        /// <param name="canSendResults">Records that can be sent</param>
        /// <returns>The records that can be sent out</returns>
		private static I_QueryResult RemoveRecsCantSend( I_QueryResult results, I_QueryResult canSendResults )
		{
			foreach ( DataTable ct in canSendResults.Tables )
			{
				foreach ( DataRow cr in ct.Rows )
				{
					if ( cr["CanSend"].ToString().ToBool() == false )
					{
						for ( int i = 0 ; i <= results.Tables.Count ; i++ ) // using indexes so I can directly update the source
						{
							if ( results.Tables.Count > i )
							{
								var t = results.Tables[i];
								for ( int j = 0 ; j <= t.Rows.Count ; j++ )
								{
									if ( t.Rows.Count > j )
									{
										var r = t.Rows[j];
										if ( t.Rows[j].RowState != DataRowState.Deleted )
										{
											if ( r["ToEmail"].ToString() == cr["Email"].ToString() )
											{
												results.Tables[i].Rows[j].Delete();
											}
										}
									}
								}
							}
						}
					}
				}
			}

			return results;
		}

        /// <summary>
        /// Add a row to the table of emails to check can send to
        /// </summary>
        /// <param name="checkCanSendTable">A table with the new row added</param>
        /// <param name="email">The email body that will be used in the new record</param>
        /// <returns>The table of email records</returns>
		private static DataTable AddRowToTableCreateCheckCanSendTable( DataTable checkCanSendTable, string email )
		{
			var dr = checkCanSendTable.NewRow();
			dr["STRINGA"] = email;
			checkCanSendTable.Rows.Add( dr );
			return checkCanSendTable;
		}

        /// <summary>
        /// Update the tick count in the database.
        /// After so many ticks it sends out any failed emails to the admin.
        /// This is so the admin doesn't get an email of failed emails sends every tick.
        /// </summary>
		private static void UpdateTickCount()
		{
			var ticksDt = GetSetting( "TickCountBeforeResendErrorEmailToAdmin" );
			_ticksBeforeEmail = ticksDt.Rows[0]["IDValue"].ToString().ToInt();

			ticksDt = GetSetting( "TickCountSinceLastEmailToAdmin" );
			_ticksSinceLastEmail = ticksDt.Rows[0]["IDValue"].ToString().ToInt();

			// Reset the tick count if at max
			if ( _ticksSinceLastEmail >= _ticksBeforeEmail )
			{
				_ticksSinceLastEmail = 1;
			}
			else
			{
				_ticksSinceLastEmail++;
			}

			SetSetting( "TickCountSinceLastEmailToAdmin", _ticksSinceLastEmail );
		}

        /// <summary>
        /// Sends a simple email to the admin
        /// </summary>
        /// <param name="body">The body of the email</param>
        /// <param name="settings">A list of dynamic settings to add to the email body</param>
		private static void SendAdminEmail( string body, DataRow settings )
		{
			string smtp_userName = "", smtp_password = "", smtp_host = "", from = "";
			int smtp_port = 0;

			GetSmtpSettings( out smtp_userName, out smtp_password, out smtp_host, out smtp_port, out from );

			string adminEmailBody = "";
			adminEmailBody += "Error in Batch Email Parser in " + _environment + ": " + body + "<br>";

			if ( settings != null )
			{
				foreach ( DataColumn dc in settings.Table.Columns )
				{
					adminEmailBody += dc.ColumnName + ": " + settings[dc.ColumnName].ToString() + "<br>";
				}
			}

			string errorAddresses = ACT.Core.SystemSettings.GetSettingByName( "ERROR_NOTIFICATION_ADDRESSES" ).Value;
			var errorAddressList = errorAddresses.SplitString( ",", StringSplitOptions.RemoveEmptyEntries );

			using ( SmtpClient client = new SmtpClient( smtp_host, smtp_port ) )
			{
				client.Credentials = new NetworkCredential( smtp_userName, smtp_password );
				client.EnableSsl = true;
				using ( MailMessage message = new MailMessage() )
				{
					message.From = new MailAddress( from );

					foreach ( var s in errorAddressList.ToList() )
					{
						message.To.Add( s );
					}

					message.Subject = "Error in Batch Email Parser";
					message.Body = adminEmailBody;
					message.IsBodyHtml = true;
					client.Send( message );
				}
			}
		}

        /// <summary>
        /// Get the smtp settings from the SystemConfiguration.xml file
        /// </summary>
        /// <param name="smtp_userName">The smtp username</param>
        /// <param name="smtp_password">The smtp password</param>
        /// <param name="smtp_host">The smtp host</param>
        /// <param name="smtp_port">The smtp port</param>
        /// <param name="from">The from address to send the email from</param>
		private static void GetSmtpSettings( out string smtp_userName, out string smtp_password, out string smtp_host, out int smtp_port, out string from )
		{
			smtp_userName = ACT.Core.SystemSettings.GetSettingByName( "SMTP_USERNAME" ).Value;
			smtp_password = ACT.Core.SystemSettings.GetSettingByName( "SMTP_PASSWORD" ).Value;
			smtp_host = ACT.Core.SystemSettings.GetSettingByName( "SMTP_HOST" ).Value;
			smtp_port = ACT.Core.SystemSettings.GetSettingByName( "SMTP_PORT" ).Value.ToInt();
			from = ACT.Core.SystemSettings.GetSettingByName( "SMTP_SENDER" ).Value;
		}

        /// <summary>
        /// Set the service running state
        /// </summary>
        /// <param name="testResult">ACT.Core.Interfaces.Common.I_TestResult</param>
        /// <returns>ACT.Core.Interfaces.Common.I_TestResult</returns>
		private static I_TestResult SetServiceRunningState( I_TestResult testResult )
		{
			try
			{
				WriteToEventLog( "Updating IsEmailServiceExec" );

				// Don't let scheduler execute this again until it's complete
				SetSetting( "IsEmailServiceExec", 1 );
				SetSetting( "EmailServiceDateStarted", DateTime.Now.ToString() );
				return testResult;
			}
			catch ( Exception ex )
			{
				string message = "ParseEmail.SetSetting: " + ex.Message;
				WriteToEventLog( message );
				SendAdminEmail( message, null );
				testResult.Success = false;
				return testResult;
			}
		}

        /// <summary>
        /// Reset the service running state
        /// </summary>
        /// <param name="testResult">ACT.Core.Interfaces.Common.I_TestResult</param>
        /// <returns>ACT.Core.Interfaces.Common.I_TestResult</returns>
		private static I_TestResult ResetServiceRunningState( I_TestResult testResult )
		{
			try
			{
				WriteToEventLog( "Updating IsEmailServiceExec" );

				// Reset and exit
				SetSetting( "IsEmailServiceExec", 0 );
				SetSetting( "EmailServiceDateEnded", DateTime.Now.ToString() );
				return testResult;
			}
			catch ( Exception ex )
			{
				string message = "ParseEmail.SetSetting: " + ex.Message;
				WriteToEventLog( message );
				SendAdminEmail( message, null );
				testResult.Success = false;
				return testResult;
			}
		}

        /// <summary>
        /// Get the setting value from the dynamic sql
        /// </summary>
        /// <param name="setting">The setting to get from the database</param>
        /// <returns>A table with the value(s) of the setting</returns>
		private static DataTable GetSetting( string setting )
		{
			var dtBatchRecords = GetParametersTable();
			var drBatchRecord = dtBatchRecords.NewRow();
			drBatchRecord["STRINGA"] = setting;
			dtBatchRecords.Rows.Add( drBatchRecord );

			var result = GetConfigurationSettings( "GetLookup", dtBatchRecords ).FirstDataTable_WithRows();
			return result;
		}

        /// <summary>
        /// Set the setting value from the dynamic sql
        /// </summary>
        /// <param name="setting">The setting to get from the database</param>
        /// <param name="setToValue">The value to set the setting to</param>
		private static void SetSetting( string setting, int setToValue )
        {
            var dtBatchRecords = GetParametersTable();
            var drBatchRecord = dtBatchRecords.NewRow();
            drBatchRecord["STRINGA"] = setting;
            drBatchRecord["STRINGB"] = setToValue;
            dtBatchRecords.Rows.Add( drBatchRecord );

            var result = GetConfigurationSettings( "UpdateLookup", dtBatchRecords ).FirstDataTable_WithRows();
        }

        private static void SetSetting( string setting, string setToValue )
        {
            var dtBatchRecords = GetParametersTable();
            var drBatchRecord = dtBatchRecords.NewRow();
            drBatchRecord["STRINGA"] = setting;
            drBatchRecord["STRINGB"] = setToValue;
            dtBatchRecords.Rows.Add( drBatchRecord );

            var result = GetConfigurationSettings( "UpdateLookup", dtBatchRecords ).FirstDataTable_WithRows();
        }

        /// <summary>
        /// Write a message to the Event Log
        /// </summary>
        /// <param name="message">The message to write to the Event Log</param>
		private static void WriteToEventLog( string message )
		{
			if ( _verboseDebugging )
			{
				ACT.Core.Windows.Events.WriteToWindowsEventLog( "SEND_BATCH_EMAIL", ACT.Core.Windows.Events.EventLogLocation.Application, message, 120 );
			}
		}
	}
}
