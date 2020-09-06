using ACT.Core.Enums;
using ACT.Core.Extensions;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Plugins.DataAccess
{
    /// <summary>
    /// ACT Query Result - Represents the I_QueryResult Execution Results
    /// </summary>
    [ACT.Core.CustomAttributes.ClassID("ACT.Plugins.ACTQueryResult")]
    public class ACT_QueryResult : ACT.Plugins.ACT_Core, I_QueryResult
    {

        #region Private Fields

        private bool _Commited = false;
        private List<bool> _Errors = new List<bool>();
        private List<Exception> _Exceptions = new List<Exception>();
        private List<int> _RecordsEffected = new List<int>();
        private List<string> _IdentitesCaptured = new List<string>();
        private List<List<System.Data.IDataParameter>> _Params = new List<List<System.Data.IDataParameter>>();
        private bool _RolledBack = false;
        private List<System.Data.DataTable> _Tables = new List<System.Data.DataTable>();

        #endregion Private Fields

        #region I_QueryResult Public Properties

        /// <summary>
        /// First Table in the results
        /// </summary>
        public System.Data.DataTable FirstTable { get { if (_Tables.Count > 0) { return _Tables[0]; } else { return null; } } }

        /// <summary>
        /// Parameters for the query
        /// </summary>
        public List<List<System.Data.IDataParameter>> Params
        {
            get { return _Params; }
            set { _Params = value; }
        }

        /// <summary>
        /// Identities Captured
        /// </summary>
        public List<string> IdentitiesCaptured
        {
            get { return _IdentitesCaptured; }
            set { _IdentitesCaptured = value; }
        }

        /// <summary>
        /// Tables 
        /// </summary>
        public List<System.Data.DataTable> Tables
        {
            get { return _Tables; }
        }

        /// <summary>
        /// Records Effected
        /// </summary>
        public List<int> RecordsEffected
        {
            get { return _RecordsEffected; }
        }

        /// <summary>
        /// Errors in the last query execution
        /// </summary>
        public List<bool> Errors
        {
            get { return _Errors; }
        }

        /// <summary>
        /// All of the Exceptions from the Queries
        /// </summary>
        public List<Exception> Exceptions
        {
            get { return _Exceptions; }
        }

        /// <summary>
        /// Defines if the records are Commited
        /// </summary>
        public bool Commited
        {
            get { return _Commited; }
            set { _Commited = value; }
        }

        /// <summary>
        /// Determines if the records were rolled back
        /// </summary>
        public bool RolledBack
        {
            get { return _RolledBack; }
            set { _RolledBack = value; }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose the object
        /// </summary>
        public override void Dispose()
        {
            _Errors = null;
            _Exceptions = null;

            foreach (System.Data.DataTable _IDR in _Tables)
            {
                _IDR.Dispose();
            }

            _Tables = null;
            _RecordsEffected = null;

        }

        #endregion

        /// <summary>
        /// This class is always healthy as there are no dependancies required.
        /// </summary>
        /// <returns>Healthy Test Result</returns>
        public override I_TestResult HealthCheck()
        {
            return ACT.Core.CurrentCore<I_TestResult>.GetCurrent();
        }

        /// <summary>
        /// Returns the First Data Table With Rows. 
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable FirstDataTable_WithRows()
        {
            foreach (var T in Tables)
            {
                if (T != null)
                {
                    if (T.Rows.Count > 0)
                    {
                        return T;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// First Query Has Exceptions
        /// </summary>
        public bool FirstQueryHasExceptions
        {
            get
            {
                // IF Exceptions Are Found
                return Exceptions[0] == null ? false : true;
            }
        }

        /// <summary>
        /// Deterines if the query has rows and no errors
        /// </summary>
        public bool HasValidData
        {
            get
            {
                if (Exceptions[0] == null)
                {
                    if (_Tables[0].Rows.Count > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Returns the Messages
        /// </summary>
        public List<string> Messages { get; set; }



        /// <summary>
        /// Defines if the Data Set Has Rows.
        /// </summary>
        /// <returns></returns>
        public bool HasRows()
        {
            bool _HasExceptions = false;
            try
            {
                if (Exceptions[0] != null) { _HasExceptions = true; }
            }
            catch { }

            if (_HasExceptions == false)
            {
                if (_Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get a value from the results
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <param name="Row"></param>
        /// <returns></returns>
        public object GetValue(string ColumnName, int Row = 0)
        {
            if (HasRows() == false) { return null; }
            if (Tables[0].Rows.Count <= Row) { return null; }

            return Tables[0].Rows[Row][ColumnName];
        }

        /// <summary>
        /// Helper Class To Test the Query Results For Various measures
        /// </summary>
        /// <param name="ErrorMessageForClient"></param>
        /// <param name="TestForNoRecords"></param>
        /// <param name="NoRecordsIsClientIssue"></param>
        /// <param name="TestForNULLValue"></param>
        /// <param name="NULLValueIsClientIssue"></param>
        /// <param name="TestForBlankValue"></param>
        /// <param name="BlankValueIsClientIssue"></param>
        /// <param name="TestForMoreThanOneRecord"></param>
        /// <param name="MoreThanOneRecorIsClientIssue"></param>
        /// <returns></returns>
        public bool Test_Return_Data(string ErrorMessageForClient, bool TestForNoRecords, bool NoRecordsIsClientIssue, bool TestForNULLValue, bool NULLValueIsClientIssue, bool TestForBlankValue, bool BlankValueIsClientIssue, bool TestForMoreThanOneRecord, bool MoreThanOneRecorIsClientIssue)
        {
            //  var _ErrorLogger = ACT.Core.CurrentCore<ACT.Core.Interfaces.Common.I_ErrorLoggable>.GetCurrent();

            if (this.Exceptions[0] != null)
            {
                //  _ErrorLogger.LogError(this.GetType().FullName, "Query Contains Exceptions", this.Exceptions[0], "", ErrorLevel.Warning);
                return false;
            }

            if ((TestForNoRecords) && (this.Tables[0].Rows.Count == 0))
            {
                if (NoRecordsIsClientIssue)
                {
                    this.Messages.Add(ErrorMessageForClient);
                    return true;
                }
                else
                {
                    //    _ErrorLogger.LogError(this.GetType().FullName, "Query Contains No Data", null, "", ErrorLevel.Warning);
                    return false;
                }
            }

            if ((TestForNULLValue) && (this.Tables[0].Rows[0][0].ToString() == ""))
            {
                if (NULLValueIsClientIssue)
                {
                    this.Messages.Add(ErrorMessageForClient);
                    return true;
                }
                else
                {
                    //  _ErrorLogger.LogError(this.GetType().FullName, "The query returned an unexpected null value.", null, "", ErrorLevel.Warning);
                    return false;
                }
            }

            if ((TestForBlankValue) && (this.Tables[0].Rows[0][0].TryToString("") == ""))
            {
                if (BlankValueIsClientIssue)
                {
                    this.Messages.Add(ErrorMessageForClient);
                    return true;
                }
                else
                {
                    //   _ErrorLogger.LogError(this.GetType().FullName, "The query returned an unexpected blank value.", null, "", ErrorLevel.Warning);
                    return false;
                }
            }

            if ((TestForMoreThanOneRecord) && (this.Tables[0].Rows.Count != 1))
            {
                if (MoreThanOneRecorIsClientIssue)
                {

                    this.Messages.Add(ErrorMessageForClient);
                    return true;
                }
                else
                {
                    //    _ErrorLogger.LogError(this.GetType().FullName, "The query returned more than 1 record.", null, "", ErrorLevel.Warning);
                    return false;
                }
            }
            return true;
        }


        #region I_CacheAble Members

        /// <summary>
        /// Represents the objects unique HASH
        /// </summary>
        public string HashID { get { return ""; } }

        /// <summary>
        /// Represents if the object is stored in memory cache
        /// </summary>
        public bool MemoryCached { get; set; }

        /// <summary>
        /// Represents if the object is database cached.
        /// </summary>
        public bool DatabaseCached { get; set; }

        /// <summary>
        /// Represents the configuration data JSON
        /// </summary>
        public string ConfigurationData { get; set; }

        /// <summary>
        /// Save Update the Cache based on the HashID and the Settings
        /// </summary>
        /// <seealso cref="Common.I_TestResult"/>
        /// <returns>I_TestResult</returns>
        public ACT.Core.Interfaces.Common.I_TestResult SaveUpdate() { return null; }

        /// <summary>
        /// Retrieve the object from cache if it exists
        /// </summary>
        /// <returns>object in cache</returns>
        public object Retrieve() { return null; }
        #endregion
    }
}
