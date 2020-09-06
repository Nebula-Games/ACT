using ACT.Core.Extensions;
using System;
using System.Collections.Generic;

namespace ACT.Plugins.Development
{
    /// <summary>
    /// ACT Code Logger
    /// </summary>
    public class ACT_Code_Logger : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.Development.I_Code_Logging
    {
        private string mode = "";
        private string filepath = "";
        private string filenamemode = "";
        private string filenameformat = "";
        private string dbconnectionname = "";
        private string tablename = "";
        private bool createtable = false;
        private bool encrypt_data = false;
        private string injection = "";
        private string injectiondll = "";
        private string injectionfullclassname = "";
        /// <summary>
        /// Holds the Flag Indicating Initialization
        /// </summary>
        internal static bool Is_Initialized { get; private set; }

        /// <summary>
        /// Lists the accepted configurationsettings
        /// </summary>
        public List<(string, string)> AcceptedConfigurationSettings
        {
            get
            {
                List<(string, string)> _tmpReturn = new List<(string, string)>();
                _tmpReturn.Add(("ACT_Code_Logger_mode", "either database,filesystem"));
                _tmpReturn.Add(("ACT_Code_Logger_filepath", "filepath"));
                _tmpReturn.Add(("ACT_Code_Logger_filenamemode", "daily,weekly,monthly"));
                _tmpReturn.Add(("ACT_Code_Logger_filenameformat", "MM, DD, YYYY, YY"));
                _tmpReturn.Add(("ACT_Code_Logger_dbconnectionname", "databaseconnection name in ACTSettings"));
                _tmpReturn.Add(("ACT_Code_Logger_tablename", "Database Table Name"));
                _tmpReturn.Add(("ACT_Code_Logger_createtable", "Create Table? (true,false)"));
                _tmpReturn.Add(("ACT_Code_Logger_encrypt_data", "true,false"));
                _tmpReturn.Add(("ACT_Code_Logger_injectioncode", "raw code to inject in DEBUG"));
                _tmpReturn.Add(("ACT_Code_Logger_injectiondll", "DLL Path for PLUGIN TO RUN"));
                _tmpReturn.Add(("ACT_Code_Logger_injectionfullclassname", "Class That Implements ACT.Core.Interfaces.I_ExecuteWithParameters"));

                return _tmpReturn;
            }
        }

        /// <summary>
        /// Initialize with a JSON Object
        /// </summary>
        /// <param name="ConfigurationSettings"></param>
        public void Initialize(string ConfigurationSettings)
        {
            try
            {
                dynamic _loggerSettings = Newtonsoft.Json.Linq.JObject.Parse(ConfigurationSettings);

                try
                {
                    if (_loggerSettings.ACT_Code_Logger_mode == "database") { mode = "database"; }
                    else if (_loggerSettings.ACT_Code_Logger_mode == "inject") { mode = "inject"; }
                    else { mode = "filesystem"; }
                }
                catch { throw new Exception("ACT_Code_Logger_mode is missing"); }

                if (mode == "filesystem")
                {
                    try
                    {
                        filepath = _loggerSettings.ACT_Code_Logger_filepath;
                        if (filepath.GetDirectoryFromFileLocation().DirectoryExists(true) == false) { throw new Exception("Path Not Found"); }
                    }
                    catch (Exception e) { throw new Exception("ACT_Code_Logger_filepath is invalid or missing", e); }

                    try
                    {
                        filenamemode = _loggerSettings.ACT_Code_Logger_filenamemode;
                        if (filenamemode != "daily" || filenamemode != "weekly" || filenamemode != "monthly") { filenamemode = "weekly"; }
                    }
                    catch { throw new Exception("ACT_Code_Logger_filenamemode is missing"); }

                    try
                    {
                        filenameformat = _loggerSettings.ACT_Code_Logger_filenameformat;
                    }
                    catch { throw new Exception("ACT_Code_Logger_filenameformat is missing"); }
                }

                if (mode == "database")
                {
                    try
                    {
                        dbconnectionname = _loggerSettings.ACT_Code_Logger_dbconnectionname;
                        if (ACT.Core.SystemSettings.GetSettingByName(dbconnectionname).Value.NullOrEmpty())
                        {
                            throw new Exception("Invalid ConnectionStringName");
                        }

                    }
                    catch { throw new Exception("ACT_Code_Logger_dbconnectionname is missing or invalid"); }

                    try
                    {
                        tablename = _loggerSettings.ACT_Code_Logger_tablename;
                    }
                    catch { throw new Exception("ACT_Code_Logger_tablename is missing or invalid"); }

                    try
                    {
                        createtable = _loggerSettings.ACT_Code_Logger_createtable.ToBool();
                    }
                    catch { createtable = false; }

                    try
                    {
                        encrypt_data = _loggerSettings.ACT_Code_Logger_encrypt_data.ToBool();
                    }
                    catch { encrypt_data = false; }
                }

                if (mode == "inject")
                {
                    try
                    {
                        injection = _loggerSettings.ACT_Code_Logger_injectioncode;
                    }
                    catch { throw new Exception("ACT_Code_Logger_injectioncode is missing"); }

                    try
                    {
                        injectiondll = _loggerSettings.ACT_Code_Logger_injectiondll;
                        injectionfullclassname = _loggerSettings.ACT_Code_Logger_injectionfullclassname;
                    }

                    catch (Exception ex) { if (injection.NullOrEmpty()) { throw new Exception("ACT_Code_Logger_injectiondll or ACT_Code_Logger_injectionfullclassname is missing since injectioncode is blank", ex); } }
                }

                Is_Initialized = true;
            }
            catch
            {
                Is_Initialized = false;
            }

        }

        /// <summary>
        /// Initialize With ACT System Setting ACT_Code_Logger_Configuration
        /// </summary>
        public void Initialize()
        {
            string _tmp = ACT.Core.SystemSettings.GetSettingByName("ACT_Code_Logger_Configuration").Value;
            if (_tmp.NullOrEmpty()) { throw new Exception("Missing Configuration Setting: ACT_Code_Logger_Configuration - " + ACT.Core.SystemSettings.LoadedSettingsDirectory); }

            Initialize(_tmp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public void Log(params object[] args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Info"></param>
        public void Log(string Message, string Info)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="args"></param>
        public void Log(string Message, params object[] args)
        {
            throw new NotImplementedException();
        }


    }
}
