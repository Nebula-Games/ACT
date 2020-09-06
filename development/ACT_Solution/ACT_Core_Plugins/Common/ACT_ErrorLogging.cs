using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Interfaces.Common;
using ACT.Core.Enums;
using ACT.Core;
using System.Diagnostics;
using ACT.Core.Extensions;


namespace ACT.Plugins.Common
{
    /// <summary>
    /// ACT Error Logging Class
    /// </summary>
    public class ACT_ErrorLogging : I_ErrorLoggable
    {
        #region IErrorLoggable Members

        /// <summary>
        /// Implement the LogError Method
        /// </summary>
        /// <param name="className"></param>
        /// <param name="summary"></param>
        /// <param name="ex"></param>
        /// <param name="additionInformation"></param>
        /// <param name="errorType"></param>
        public virtual void LogError(string className, string summary, Exception ex, string additionInformation, ErrorLevel errorType)
        {
            try
            {
                string _Location = SystemSettings.GetSettingByName("LogFileDirectory").Value.EnsureDirectoryFormat();

                if (System.IO.Directory.Exists(_Location) == false)
                {
                    _Location = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat();
                }

                if (System.IO.Directory.Exists(_Location) == false)
                {
                    return;
                    // DONT throw errors from this plugin
                    // throw new Exception("Error Locating Logging Directory");
                }

                DateTime _n = DateTime.Now;

                string _FileName = _n.Month + "-" + _n.Year + ".txt";

                string _Contents = className + " : " + summary + " : " + additionInformation;



                if (!(ex == null))
                {
                    _Contents += ex.Message;
                    try { _Contents = _Contents + ex.StackTrace; }
                    catch { }
                }

                _Contents = _Contents + Environment.NewLine;

                try
                {
                    System.IO.File.AppendAllText(_Location + _FileName, _Contents);
                }
                catch (Exception ie)
                {
                    throw new Exception(this.GetType().FullName + ": Error Creating Log File: " + _Location + _FileName, ie);
                }


                if (errorType == ErrorLevel.Critical)
                {
                    if (ex == null)
                    {
                        throw new Exception(className + " : " + summary + " : " + additionInformation);
                    }
                    else
                    {
                        throw new Exception(className + " : " + summary + " : " + additionInformation, ex);
                    }
                }


                /*
                   EventLog _EventLog = new System.Diagnostics.EventLog();

                    if (errorType == ErrorLevel.Warning)
                    {
                        _EventLog.Source = "DIP";
                        _EventLog.WriteEntry("DIP " + className + " " + ex.Message, EventLogEntryType.Warning);
                    }
                    else
                    {
                        _EventLog.Source = "DIP";
                        _EventLog.WriteEntry("DIP " + className + " " + ex.Message, EventLogEntryType.Error);
                    }
                 */
            }
            catch { }
        }

        #endregion
    }

}
