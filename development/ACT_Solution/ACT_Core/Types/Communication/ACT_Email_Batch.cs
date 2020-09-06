///-------------------------------------------------------------------------------------------------
// file:	Types\Communication\ACT_Email_Batch.cs
//
// summary:	Implements the act email batch class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;



namespace ACT.Core.Types.Communication
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An act email batch. </summary>
    ///
    /// <remarks>   Mark Alicz, 7/30/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------
    public class ACT_Email_Batch
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets information describing the SMTP. </summary>
        ///
        /// <value> Information describing the SMTP. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("smtpinfo")]
        public ACT_SMTP_Info SMTPInfo { get; set; }

        [JsonProperty("databaseconnectionname")]
        public string DatabaseConnectionName { get; set; }
        
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("htmlbody")]
        public string HTMLBody { get; set; }

        [JsonProperty("sqlgetallrecipients")]
        public string SQLGetAllRecipients { get; set; }

        [JsonProperty("sqlupdatesinglerecipient")]
        public string SQLUpdateSingleRecipient { get; set; }

        [JsonProperty("fromname")]
        public string FromName { get; set; }

        [JsonProperty("fromemail")]
        public string FromEmail { get; set; }

        [JsonProperty("batchname")]
        public string BatchName { get; set; }


    }
}
