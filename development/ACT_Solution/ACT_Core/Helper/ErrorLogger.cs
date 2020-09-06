// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-14-2019
// ***********************************************************************
// <copyright file="ErrorLogger.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using ACT.Core.CustomAttributes;
using ACT.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ACT.Core.Helper
{
    /// <summary>
    /// Internal Windows ErrorLogger
    /// </summary>
    public static class ErrorLogger
    {
        /// <summary>
        /// The error count
        /// </summary>
        public static int ErrorCount = 0;

        /// <summary>
        /// Create Error Data
        /// </summary>
        /// <param name="Class">Class Name</param>
        /// <param name="Message">Error Message</param>
        /// <param name="AdditionalInfo">Additional Information</param>
        /// <param name="ex">Actual Exception</param>
        /// <param name="errLevel">Error Level</param>
        /// <param name="_lineNumber">Line Number</param>
        /// <returns>JSON Object</returns>
        public static string CreateErrorData(string Class, string Message, string AdditionalInfo, Exception ex, Enums.ErrorLevel errLevel, [CallerLineNumber] int _lineNumber = 0)
        {
            string _tmpOut = "{" + Environment.NewLine;
            _tmpOut += "\t \"classname\":" + "\"" + Class + "\"," + Environment.NewLine;
            _tmpOut += "\t \"message\":" + "\"" + Message + "\"," + Environment.NewLine;
            _tmpOut += "\t \"additionalinfo\":" + "\"" + AdditionalInfo + "\"," + Environment.NewLine;
            if (ex != null)
            {
                _tmpOut += "\t \"exception\":" + "\"" + ex.Message + "\"," + Environment.NewLine;
                _tmpOut += "\t \"stacktrace\":" + "\"" + ex.StackTrace + "\"," + Environment.NewLine;
            }
            _tmpOut += "\t \"errlevel\":" + "\"" + Class + "\"," + Environment.NewLine;
            _tmpOut += "\t \"linenumber\":" + "\"" + Class + "\"" + Environment.NewLine;
            _tmpOut += "},";

            return _tmpOut;
        }

        /// <summary>
        /// Get Error File Name
        /// </summary>
        /// <value>The name of the get error file.</value>
        public static string GetErrorFileName
        {
            get
            {
                string _tmpReturn = DateTime.Now.Month.ToString() + "-";
                _tmpReturn += DateTime.Now.Day.ToString() + "-";
                _tmpReturn += DateTime.Now.Year.ToString();
                return _tmpReturn;
            }
        }

        /// <summary>
        /// Log Error - ONLY if VerboseDebugging Is Set to True
        /// </summary>
        /// <param name="classobj">The classobj.</param>
        /// <param name="Message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="ErrLevel">The error level.</param>
        /// <param name="_LineNumber">The line number.</param>
        public static void VLogError(object classobj, string Message, Exception ex, Enums.ErrorLevel ErrLevel, [CallerLineNumber] int _LineNumber = 0)
        {
            try
            {
                if (SystemStatus.VerboseDebugging)
                {
                    LogError(classobj, Message, ex, ErrLevel, _LineNumber);
                }
                else
                {
                    if (SystemSettings.SettingKeys.Contains("VerboseDebugging"))
                    {
                        try 
                        { 
                            if (ACT.Core.SystemSettings.GetSettingByName("VerboseDebugging").Value.ToLower() != "true") { return; } 
                        }
                        catch { }
                    }
                    else { return; }                    
                }
            }
            catch
            {
                return;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Logs Error if Verbose Param is Not Null and True OR VerboseParam is null and  System settings VerboseDebugging = true </summary>
        ///
        /// <remarks>   Mark Alicz, 8/29/2019. </remarks>
        ///
        /// <param name="VerboseParam"> The verbose parameter. </param>
        /// <param name="classobj">     The classobj. </param>
        /// <param name="Message">      The message. </param>
        /// <param name="ex">           The ex. </param>
        /// <param name="ErrLevel">     The error level. </param>
        /// <param name="_LineNumber">  (Optional) The line number. </param>
        ///-------------------------------------------------------------------------------------------------
        public static void VLogErrorParam(bool? VerboseParam, object classobj, string Message, Exception ex, Enums.ErrorLevel ErrLevel, [CallerLineNumber] int _LineNumber = 0)
        {
            try
            {
                if (VerboseParam == null)
                {
                    if (SystemStatus.VerboseDebugging == false) { return; }
                    if (SystemSettings.SettingKeys.Contains("VerboseDebugging"))
                    {
                        try { if (ACT.Core.SystemSettings.GetSettingByName("VerboseDebugging").Value.ToLower() != "true") { return; } }
                        catch { }
                    }
                    else { return; }

                    LogError(classobj, Message, ex, ErrLevel, _LineNumber);
                }
                else if (VerboseParam.Value == true)
                {
                    try { if (ACT.Core.SystemSettings.GetSettingByName("VerboseDebugging").Value.ToLower() != "true") { return; } }
                    catch { return; }
                }
                else { return; }

                LogError(classobj, Message, ex, ErrLevel, _LineNumber);
            }
            catch { return; }
        }

        /// <summary>
        /// Log Error
        /// </summary>
        /// <param name="classobj">The classobj.</param>
        /// <param name="Message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="ErrLevel">The error level.</param>
        /// <param name="_LineNumber">a</param>
        public static void LogError(object classobj, string Message, Exception ex, Enums.ErrorLevel ErrLevel, [CallerLineNumber] int _LineNumber = 0)
        {
            try
            {
                var _attrs = System.Attribute.GetCustomAttribute(classobj.GetType(), typeof(ACT.Core.CustomAttributes.ClassID));
                var _attr = (ACT.Core.CustomAttributes.ClassID)_attrs;

                if (_attr == null)
                {
                    LogError(classobj.ToString(), Message, "", ex, ErrLevel, _LineNumber);
                }
                else
                {
                    LogError(classobj.GetType().ToString() + ":" + _attr.Internal.ToString(), Message, "", ex, ErrLevel, _LineNumber);
                }
            }
            catch
            {
                try
                {
                    /*
                    Properties.ErrLog.Default.UnhandledErrorCount = ErrorCount;

                    if (Properties.ErrLog.Default.Last100Errors.Count > 99)
                    {
                        Properties.ErrLog.Default.Last100Errors.RemoveAt(0);
                    }

                    Properties.ErrLog.Default.Last100Errors.Add(CreateErrorData(classobj.GetType().ToString(), Message, "", ex, ErrLevel, _LineNumber));
                    */
                }
                catch { }
            }
        }

        /// <summary>
        /// Log Error To Disk
        /// </summary>
        /// <param name="ErrorContents">The error contents.</param>
        public static void LogErrorToDisk(string ErrorContents)
        {
            try
            {
                string _Dir = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat();
                string _FileName = DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt";
            
                if (SystemStatus.InstallationFound == true)
                {
                    string _LogToLocation = SystemStatus.InstallationFile.Installation.LogToDisk;

                    if (_LogToLocation.NullOrEmpty() == false && _LogToLocation.DirectoryExists(true))
                    {
                        _Dir = _LogToLocation.EnsureDirectoryFormat() + _FileName;
                    }
                    else { _Dir = _Dir + _FileName; }

                    System.IO.File.AppendAllText(_Dir, ErrorContents);
                }
                else
                {                       
                    _Dir = _Dir + _FileName;

                    if (SystemSettings.HasSettingWithValue("LogFileDirectory"))
                    {
                        string _tmpDir = SystemSettings.GetSettingByName("LogFileDirectory").Value.EnsureDirectoryFormat();
                        if (_tmpDir.DirectoryExists(true)) { _Dir = _tmpDir + _FileName; }
                    }

                    System.IO.File.AppendAllText(_Dir, ErrorContents);
                }

            }
            catch(Exception ex) { throw new Exception("Error Logging To Disk", ex); }
        }

        /// <summary>
        /// LOG The Error
        /// </summary>
        /// <param name="ClassName">Name of the class.</param>
        /// <param name="ErrorMessage">The error message.</param>
        /// <param name="AdditionalInfo">The additional information.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="WarningLevel">The warning level.</param>
        /// <param name="_LineNumber">The line number.</param>
        public static void LogError(string ClassName, string ErrorMessage, string AdditionalInfo, Exception ex, Enums.ErrorLevel WarningLevel, [CallerLineNumber] int _LineNumber = 0)
        {
            ErrorCount++;
            // Properties.ErrLog.Default.TotalErrorCount = ErrorCount;
            try
            {
                if (SystemStatus.InstallationFound == true && SystemStatus.InstallationFile.Installation.LogToDisk.NullOrEmpty() == false)
                {                    
                    LogErrorToDisk(ClassName + "|" + ErrorMessage + "|" + AdditionalInfo + "|" + ex.Message + "|" + WarningLevel.ToString());
                    return;                    
                }
            }
            catch { }

            try
            {
                ACT.Core.Interfaces.Common.I_ErrorLoggable _ErrorLoggable = ACT.Core.CurrentCore<ACT.Core.Interfaces.Common.I_ErrorLoggable>.GetCurrent();
                _ErrorLoggable.LogError(ClassName, ErrorMessage, ex, AdditionalInfo + " - Line Number: " + _LineNumber.ToString(), WarningLevel);
                _ErrorLoggable = null;
            }
            catch // (Exception exc) TODO
            {
                try
                {
                    string _tmpOut = CreateErrorData(ClassName, ErrorMessage, AdditionalInfo, ex, WarningLevel, _LineNumber);
                    System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + GetErrorFileName + ".json", _tmpOut);
                }
                catch
                {
                    try { Windows.Events.WriteToWindowsEventLog("ACT.Core", Windows.Events.EventLogLocation.Application, ClassName + ":" + ErrorMessage, (int)Enums.ErrorCodes.ErrorCodeEnums.ExceptionInErrorLogger); }
                    catch
                    {
                        LogErrorToDisk(ClassName + "|" + ErrorMessage + "|" + AdditionalInfo + "|" + ex.Message + "|" + WarningLevel.ToString());
                    }
                }
            }

        }
    }
}


