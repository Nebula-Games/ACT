// ***************************************************************
//  (c) 2019 - Mark Alicz.
//
// Author.........Mark Alicz
// Solution......: ACTCORE
// Project.......: ACTCore.DLL
// Filename......: SystemStatus.cs
//
// HISTORY 
// **********************************************************************
using ACT.Core.Types.SystemConfiguration;
using ACT.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ACT.Core.SystemConfiguration.Installation;


namespace ACT.Core
{
    /// <summary>
    /// System Status Represents ACT Status - This Provides Basic Information Including Default Configuration
    /// </summary>
    public static class SystemStatus
    {
        #region Private Properties

        /// <summary>
        /// Indicates if the System Status Data Has Been Loaded
        /// </summary>
        private static bool _HasLoaded = false;

        /// <summary>
        /// Indicates if the System Status Indicates the Verbosity Level
        /// </summary>
        private static bool? _Verbose = null;

        /// <summary>
        /// Installation Found True / False
        /// </summary>
        private static bool _InstallationFound = false;

        /// <summary>
        /// Loaded ACT Installation File
        /// </summary>
        private static InstallationFile _InstallationFile = null;

        /// <summary>
        /// Loaded Configuration File
        /// </summary>
        private static Types.SystemConfiguration.SystemConfiguration _BaseConfigurationFile = null;


        /// <summary>
        /// List of all the Calling Assembly Paths
        /// </summary>
        private static List<string> _CallingAssemblyPaths = new List<string>();

        /// <summary>
        /// Loaded Directory Library Used For Managing
        /// </summary>
        private static Dictionary<string, string> _DirectoryLibrary = new Dictionary<string, string>();

        //private static string InstallationDirectory

        #endregion

        #region Public Constructors

        /// <summary>
        /// Static Constructor Testing For Installation
        /// </summary>
        static SystemStatus()
        {
            
            _DirectoryLibrary.Add("Resources", "\\Resources\\");
            _DirectoryLibrary.Add("Assets", "\\Resources\\Assets\\");
            _DirectoryLibrary.Add("Localization", "\\Resources\\Localization\\");
            _DirectoryLibrary.Add("InstallationFiles", "\\Resources\\Installation\\");
            _DirectoryLibrary.Add("Features", "\\Resources\\Features\\");
            _DirectoryLibrary.Add("Javascript", "\\Resources\\Javascript\\");
            _DirectoryLibrary.Add("ACTConsole", "\\Resources\\ACTConsole\\");
            _DirectoryLibrary.Add("Templates", "\\Resources\\Templates\\");

        }

        public static void InitializeFromCloudSource(System.Net.EndPoint CloudBasedEndpoint, ACT.Core.Authentication.Member.ACT_BasicMemberInfo MemberAuthentication)
        {

        }

        #endregion Public Constructors





        #region Basic Settings / Status

        /// <summary>
        /// Directory Paths Public Property
        /// </summary>
        public static Dictionary<string, string> DirectoryPaths
        {
            get { if (_DirectoryLibrary == null) { _DirectoryLibrary = new Dictionary<string, string>(); } return _DirectoryLibrary;}
        }

        /// <summary>
        /// Get Directory Path
        /// </summary>
        /// <param name="FromInstallationLocation"></param>
        /// <param name="source"></param>
        /// <param name="otherSource"></param>
        /// <returns></returns>
        public static string GetDirectoryPath(bool FromInstallationLocation, ACT.Core.Enums.Installation.CoreDirectories source, string otherSource = "")
        {
            string _tmpReturn = InstallPath.EnsureDirectoryFormat();

            // TODO Modernize This To App Centric Location Based Using Status or 
            if (!FromInstallationLocation) { AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat();  }

            if (source == Enums.Installation.CoreDirectories.Other)
            {
                if (DirectoryPaths.ContainsKey(otherSource)) { return _tmpReturn + DirectoryPaths[otherSource].EnsureDirectoryFormat(); ; }
                else { throw new Exception("Unable To Locate OtherSource"); } //TODO ADD LOGGING
            }
            else{
                string _Base = Enum.GetName(typeof(ACT.Core.Enums.Installation.CoreDirectories), source);
                if (DirectoryPaths.ContainsKey(_Base))
                {
                    return _tmpReturn + DirectoryPaths[_Base].EnsureDirectoryFormat();
                }
                else { throw new Exception("Unable To Locate BaseDirectory [" + _Base + "]"); }  // TODO LOG ERROR
            }            
        }

        /// <summary>
        /// The Currently Loaded Configuration File
        /// </summary>
        public static Types.SystemConfiguration.SystemConfiguration LoadedConfigurationFile
        {
            get
            {


                if (_BaseConfigurationFile != null) { return _BaseConfigurationFile; }

                try
                {
                    _BaseConfigurationFile = Types.SystemConfiguration.SystemConfiguration.LoadDefaultConfigurationFile(InstallationFile.Installation.BaseSystemDirectory);
                    DefaultConfigurationFileLoaded = false;
                }
                catch (Exception ex)
                {
                    // TODO LOG ERROR PROPERLY
                    ACT.Core.ACT_Class_Core.ConditionalLogError("SystemStatus", ex, Enums.ErrorLevel.Critical, InstallPath);
                }
                return _BaseConfigurationFile;
            }
            set { }
        }

        /// <summary>
        /// Indicates if the Default ACT Configuration File Was Loaded
        /// </summary>
        public static bool DefaultConfigurationFileLoaded = false;

        /// <summary>
        /// Install Path
        /// </summary>
        public static string InstallPath = "";

        /// <summary>
        /// Installed LicenseKey
        /// </summary>
        public static string LicenseKey
        {
            get
            {
                if (InstallationFound == false) { return ""; }
                else
                {
                    if (_InstallationFile == null)
                    {
                        ACT.Core.ACT_Class_Core.ConditionalLogError("ACT.Core.SystemStatus.LicenseKey", null, Enums.ErrorLevel.Severe, "Missing Installation File Object When Installatioon Found = true");
                        return "";
                    }
                    else
                    {
                        return _InstallationFile.Installation.LicenseKey;
                    }
                }
            }
        }

        /// <summary>
        /// Installed Version
        /// </summary>
        public static string Version
        {
            get
            {
                if (InstallationFound == false) { return ""; }
                else
                {
                    if (_InstallationFile == null)
                    {
                        ACT.Core.ACT_Class_Core.ConditionalLogError("ACT.Core.SystemStatus.Version", null, Enums.ErrorLevel.Severe, "Missing Installation File Object When Installatioon Found = true");
                        return "";
                    }
                    else
                    {
                        return _InstallationFile.Installation.CoreVersion;
                    }
                }
            }
        }

        public static InstallationFile InstallationFile { get { return _InstallationFile; } }

        #endregion Basic Settings / Status

        #region private methods

        /// <summary>
        /// Locate Installation Directory
        /// </summary>
        /// <returns></returns>
        public static string LocateInstallation(bool ForceReload = false)
        {

            if (_InstallationFound == true && ForceReload == false) { return InstallPath; }

            if (ACTConstants.SystemConfiguration.InstallationDirectory(AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat()) == null) { return ""; }


            var Hints = new String[] { AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat(),
            AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + "Resources\\", "c:\\ACT\\", "c:\\program files\\act\\", "c:\\program files(x86)\\act\\", "d:\\act\\","d:\\program files\\act\\", "d:\\program files(x86)\\act\\" };

            foreach (string hint in Hints)
            {
                if (hint.DirectoryExists())
                {
                    string _path = hint.FastAppend("resources\\act.enc");
                    if (_path.FileExists()) { InstallPath = _path; _InstallationFound = true; }
                    else
                    {
                        _path = hint.FastAppend("act.enc");
                        if (_path.FileExists()) { InstallPath = _path; _InstallationFound = true; }
                        else
                        {
                            _path = hint.FastAppend("bin\\act.enc");
                            if (_path.FileExists())
                            {
                                InstallPath = _path; _InstallationFound = true;
                            }
                            else
                            {
                                _path = hint.FastAppend("bin\\resources\\act.enc");
                                if (_path.FileExists()) { InstallPath = _path; _InstallationFound = true; }
                            }
                        }
                    }
                }
            }

            if (_InstallationFound == false)
            {
                foreach (string hint in Hints)
                {
                    if (hint.DirectoryExists())
                    {
                        string _path = hint.FastAppend("resources\\act.json");
                        if (_path.FileExists()) { InstallPath = _path; _InstallationFound = true; }
                        else
                        {
                            _path = hint.FastAppend("act.json");
                            if (_path.FileExists()) { InstallPath = _path; _InstallationFound = true; }
                            else
                            {
                                _path = hint.FastAppend("bin\\act.json");
                                if (_path.FileExists())
                                {
                                    InstallPath = _path; _InstallationFound = true;
                                }
                                else
                                {
                                    _path = hint.FastAppend("bin\\resources\\act.json");
                                    if (_path.FileExists()) { InstallPath = _path; _InstallationFound = true; }
                                }
                            }
                        }
                    }
                }
            }

            if (_InstallationFound)
            {
                if (InstallPath.ToLower().EndsWith("enc"))
                {
                    var _UnencryptedData = InstallPath.ReadAllText().ScopeSpecific_Decrypt("ACT.Core.SystemConfiguration.ActInstallationFile", System.Security.Cryptography.DataProtectionScope.LocalMachine);
                    _InstallationFile = SystemConfiguration.Installation.InstallationFile.FromJson(_UnencryptedData);
                }
                else
                {
                    _InstallationFile = SystemConfiguration.Installation.InstallationFile.FromJson(InstallPath.ReadAllText());
                    _InstallationFile.SaveInstallationFile(InstallPath);
                    try
                    {
                        InstallPath.DeleteFile(50, true);
                    }
                    catch (Exception ex)
                    {
                        ACT.Core.ACT_Class_Core.ConditionalLogError("ACT.Core.SystemStatus", ex, Enums.ErrorLevel.Severe, InstallPath, "Error Securing Installation File");
                    }
                }
            }

            if (_InstallationFound) { return InstallPath; }
            else { return ""; }
        }

        /// <summary>
        /// Load The Installation Data and Set the Status Parameters
        /// </summary>
        private static void Load()
        {
            LocateInstallation();
            if (InstallationFound == false) { return; }
        }

        #endregion

        #region Public Properties

        public static Types.SystemConfiguration.SystemConfiguration GenerateStandardConfigurationFile()
        {
            string _CurrentVersion = AssemblyName.GetAssemblyName("ACTCore.dll").Version.ToString().Replace(".", "-");

            string _VersionResult = ACT.Core.Communications.Http.CallGenericHandler_StringReturn("http://services.act-net.us/sysconfig/" + _CurrentVersion + ".enc", null);

            Types.SystemConfiguration.SystemConfiguration _StandardConfig = Types.SystemConfiguration.SystemConfiguration.FromJson(_VersionResult.DecryptString());

            return _StandardConfig;
        }


        /// <summary>
        /// Indicates if the ACT Installation was Found
        /// </summary>
        public static bool InstallationFound
        {
            get
            {
                if (_HasLoaded == false) { Load(); }
                return _InstallationFound;
            }
            set { _InstallationFound = value; }

        }

        /// <summary>
        /// ACT Company Name
        /// </summary>
        public static string ACT_CompanyName
        {
            get
            {
                try
                {
                    var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
                    return versionInfo.CompanyName;
                }
                catch
                {
                    return "SGI";
                }
            }
        }
        /// <summary>
        /// Defines the level of debugging specified by the System Configuration Setting (Verbose)
        /// </summary>
        public static bool VerboseDebugging
        {
            get
            {
                if (_Verbose != null) { return _Verbose.Value; }

                if (ACT.Core.SystemSettings.HasSettingWithValue("Verbose"))
                {
                    _Verbose = ACT.Core.SystemSettings.GetSettingByName("Verbose").Value.ToBool(false);
                }
                else
                {
                    _Verbose = false;
                }

                return _Verbose.Value;
            }
        }
        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Force Reload
        /// </summary>
        public static void ForceReload()
        {
            InstallationFound = true;
            DefaultConfigurationFileLoaded = false;

            LoadedConfigurationFile = GenerateStandardConfigurationFile();
            SystemSettings.Reloaded();
        }

        /// <summary>
        /// Check for a Valid License File
        /// </summary>
        /// <param name="ApplicationName"></param>
        /// <returns></returns>
        public static bool CheckLicense(string ApplicationName)
        {
            // TODO MARK
            return true;
        }

        /// <summary>
        /// Get Application By Name
        /// </summary>
        /// <param name="ApplicationName">Name of the application.</param>
        /// <returns>ACT.Core.SystemConfiguration.ACT_ApplicationSettings.</returns>
        public static Types.SystemConfiguration.Application GetApplicationByName(string ApplicationName)
        {
            if (Application_Installation_Settings.Applications.Exists(x => x.Name.ToLower() == ApplicationName.ToLower()))
            {
                return Application_Installation_Settings.Applications.First(x => x.Name.ToLower() == ApplicationName.ToLower());
            }
            else
            {
                return null;
            }
        }



        #endregion Public Methods

        #region Sub Class - Application Installation Settings

        /// <summary>
        /// Applications Installation and Settings Class
        /// </summary>
        public static class Application_Installation_Settings
        {
            /// <summary>
            /// Check if ACT Application was Installed.
            /// </summary>
            /// <param name="ApplicationName">Name of the application.</param>
            /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
            public static bool Application_Is_Installed(string ApplicationName)
            {
                if (ACTApplicationInstallationStatusData.ContainsKey(ApplicationName))
                {
                    return ACTApplicationInstallationStatusData[ApplicationName];
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// Check For Installed Applications
            /// </summary>
            public static void LoadAll_Applications()
            {
                // TODO
                //Applications.Clear();

                //try
                //{
                //    using (var _ACTKey = ACT.Core.Windows.Registry.WindowsRegistry.GetACT_RegistryKey())
                //    {
                //        string[] _SubKeys = _ACTKey.GetSubKeyNames();

                //        foreach (string appName in _SubKeys)
                //        {
                //            using (var _tmpApplicationKey = _ACTKey.OpenSubKey(appName))
                //            {
                //                Types.SystemConfiguration.AppliApplicationSettings _tmpSettings = new SystemConfiguration.ACT_ApplicationSettings();

                //                _tmpSettings.Name = appName;
                //                _tmpSettings.Version = _tmpApplicationKey.GetValue("Version").ToString();
                //                _tmpSettings.Location = _tmpApplicationKey.GetValue("Location").ToString();
                //                _tmpSettings.LicenseKey = _tmpApplicationKey.GetValue("LicenseKey").ToString();
                //                _tmpSettings.FullClassName = _tmpApplicationKey.GetValue("FullClassName").ToString();
                //                _tmpSettings.InstallerDLLName = _tmpApplicationKey.GetValue("DLLName").ToString();

                //                // if (_tmpSettings.)
                //            }
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    ACT.Core.Helper.ErrorLogger.LogError("ACT.Core.SystemStatus.CheckForInstalledApplications()", ex.Message, "Error Reading ACT KEY", ex, ACT.Core.Enums.ErrorLevel.Critical);
                //    return;
                //}
            }

            /// <summary>
            /// Raw Data as it pertains to what act applications are installed.
            /// </summary>
            public static Dictionary<string, bool> ACTApplicationInstallationStatusData = new Dictionary<string, bool>();

            /// <summary>
            /// Applications Loaded
            /// </summary>
            public static List<Types.SystemConfiguration.Application> Applications = new List<Application>();

            /// <summary>
            /// Installed LicenseKey
            /// </summary>
            public static string LicenseKey = "";
        }

        #endregion

        #region Sub Class - Email

        /// <summary>
        /// Email Engine Settings
        /// </summary>
        public static class EmailSettings
        {
            /// <summary>
            /// Enable Email Service Engine
            /// </summary>
            public static bool EnableEmailService = false;
            /// <summary>
            /// Validate Before Sending Emails
            /// </summary>
            public static bool ValidateBeforeSendingEmails = false;

            /// <summary>
            /// Valid Emails
            /// </summary>
            public static List<string> ValidEmails = null;
        }

        #endregion
    }
}