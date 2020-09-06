// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_Constants.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ACT.Core.Extensions;

namespace ACT.Core
{
    /// <summary>
    /// Class Constants.
    /// </summary>
    public static partial class ACTConstants
    {
        /// <summary>
        /// The verbose debugging
        /// </summary>
        private static bool? _VerboseDebugging = null;

        /// <summary>
        /// Holds The Base Directorys Found By The Passed Directory Info
        /// </summary>
        private static Dictionary<string, List<string>> _ResourcesDirectories = new Dictionary<string, List<string>>();

        /// <summary>
        /// Holds the Found Resources Directory
        /// </summary>
        private static Dictionary<string, string> _FoundResourceDirectories = new Dictionary<string, string>();


        /// <summary>
        /// Gets the Verbose Debugging Constant
        /// </summary>
        /// <value><c>true</c> if [verbose debugging]; otherwise, <c>false</c>.</value>
        public static bool VerboseDebugging
        {
            get
            {
                try
                {

#if VERBOSE
                    _VerboseDebugging = true;
                    ACT.Core.Helper.ErrorLogger.LogError("Constants - VerboseDebugging - BUILD WITH VERBOSE", "Setting Value Based On BUILD SYMBOL", null, Enums.ErrorLevel.Informational);
#else
                    if (_VerboseDebugging == null)
                    {
                        _VerboseDebugging = ACT.Core.SystemSettings.GetSettingByName("VerboseDebugging").Value.ToBool(false);
                    }
#endif
                    return _VerboseDebugging.Value;
                }
                catch
                {
                    return _VerboseDebugging.Value;
                }
            }
        }



        public static class SystemConfiguration
        {
            /// <summary>
            /// Gets the SystemSettingsRegistryKey
            /// </summary>
            /// <value>The system settings registry key.</value>
            public static Microsoft.Win32.RegistryKey SystemSettingsRegistryKey
            {
                get
                {
                    var _RegKey = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry64);

                    var _TmpReturn = _RegKey.OpenSubKey("SOFTWARE").OpenSubKey("NebulaGamesLLC", true);

                    if (_TmpReturn == null)
                    {
                        _TmpReturn = _RegKey.OpenSubKey("SOFTWARE", true).CreateSubKey("NebulaGamesLLC");
                    }

                    _RegKey.Dispose();
                    return _TmpReturn;
                }
            }

            /// <summary>
            /// Name if the System Settings Location
            /// </summary>
            /// <value>The name of the system settings registry value.</value>
            public static string SystemSettingsRegistryValueName { get { return "ACTConfigFileLocation"; } }

            /// <summary>
            /// ACT Plugin Directory Locations
            /// </summary>
            /// <value>The name of the system plugins registry value.</value>
            public static string SystemPluginsRegistryValueName { get { return "ACTPluginLocation"; } }

            /// <summary>
            /// Locate the Installation Directory
            /// </summary>
            /// <param name="BaseDirectory"></param>
            /// <returns></returns>
            public static string InstallationDirectory(string BaseDirectory = "") {


                if (_FoundResourceDirectories.ContainsKey(BaseDirectory)) { return _FoundResourceDirectories[BaseDirectory].EnsureDirectoryFormat() + "Installation\\"; }
                if (_FoundResourceDirectories.ContainsKey(BaseDirectory.EnsureDirectoryFormat())) { return _FoundResourceDirectories[BaseDirectory.EnsureDirectoryFormat()].EnsureDirectoryFormat() + "Installation\\"; }

                foreach (string PathItem in ResourcesDirectory(BaseDirectory))
                {
                    if (PathItem.DirectoryExists())
                    {
                        if ((PathItem.EnsureDirectoryFormat() + "SystemConfiguration.json").FileExists()) { _FoundResourceDirectories[BaseDirectory] = (PathItem.EnsureDirectoryFormat() + "SystemConfiguration.json"); }
                        if ((PathItem.EnsureDirectoryFormat() + "systemconfiguration.json").FileExists()) { _FoundResourceDirectories[BaseDirectory] = (PathItem.EnsureDirectoryFormat() + "systemconfiguration.json"); }
                    }
                }

                if (_FoundResourceDirectories.ContainsKey(BaseDirectory)) { return _FoundResourceDirectories[BaseDirectory].EnsureDirectoryFormat() + "Installation\\"; }
                if (_FoundResourceDirectories.ContainsKey(BaseDirectory.EnsureDirectoryFormat())) { return _FoundResourceDirectories[BaseDirectory.EnsureDirectoryFormat()].EnsureDirectoryFormat() + "Installation\\"; }

                // TODO Throw Error
                return null;
            }

            private static List<string> ResourcesDirectory(string BaseDirectory = "")
            {
                if (BaseDirectory == "") { BaseDirectory = AppDomain.CurrentDomain.BaseDirectory; }

                if (_ResourcesDirectories.ContainsKey(BaseDirectory) == false) { _ResourcesDirectories.Add(BaseDirectory, new List<string>()); }

                _ResourcesDirectories[BaseDirectory].Clear();
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "Resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "resources\\");

                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "bin\\Resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "bin\\resources\\");

                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "Bin\\Resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "Bin\\resources\\");

                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "bin\\ACT\\Resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "bin\\ACT\\resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "bin\\act\\Resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "bin\\act\\resources\\");

                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "Bin\\ACT\\Resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "Bin\\ACT\\resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "Bin\\act\\Resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "Bin\\act\\resources\\");

                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "act\\Resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "Act\\Resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "act\\resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "Act\\resources\\");

                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "app_data\\Resources\\");
                _ResourcesDirectories[BaseDirectory].Add(BaseDirectory.EnsureDirectoryFormat() + "app_data\\resources\\");

                return _ResourcesDirectories[BaseDirectory];
            }


            public static string GetSystemConfigurationFileFullPath(string BaseDirectory = "")
            {
                if (_FoundResourceDirectories.ContainsKey(BaseDirectory)) { return _FoundResourceDirectories[BaseDirectory]; }
                if (_FoundResourceDirectories.ContainsKey(BaseDirectory.EnsureDirectoryFormat())) { return _FoundResourceDirectories[BaseDirectory.EnsureDirectoryFormat()]; }

                foreach (string PathItem in ResourcesDirectory(BaseDirectory))
                {
                    if (PathItem.DirectoryExists())
                    {
                        if ((PathItem.EnsureDirectoryFormat() + "SystemConfiguration.json").FileExists()) { _FoundResourceDirectories[BaseDirectory] = (PathItem.EnsureDirectoryFormat() + "SystemConfiguration.json"); }
                        if ((PathItem.EnsureDirectoryFormat() + "systemconfiguration.json").FileExists()) { _FoundResourceDirectories[BaseDirectory] = (PathItem.EnsureDirectoryFormat() + "systemconfiguration.json"); }
                    }
                }

                if (_FoundResourceDirectories.ContainsKey(BaseDirectory)) { return _FoundResourceDirectories[BaseDirectory]; }
                if (_FoundResourceDirectories.ContainsKey(BaseDirectory.EnsureDirectoryFormat())) { return _FoundResourceDirectories[BaseDirectory.EnsureDirectoryFormat()]; }

                // TODO Throw Error
                return null;
            }

        }

        #region TEXT Constants

        /// <summary>
        /// a-zA-Z0-9 {Special}
        /// </summary>
        public const string AllAlphaNumericWithSpecial = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_[]{};':<>,./?\\|";

        /// <summary>
        /// a-z
        /// </summary>
        public const string LowerAlpha = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// A-Z
        /// </summary>
        public const string UpperAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// a-zA-Z
        /// </summary>
        public const string AllAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// a-zA-Z0-9
        /// </summary>
        public const string AllAlphaNumeric = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        /// <summary>
        /// a-zA-Z0-9
        /// </summary>
        public const string AllNumeric = "0123456789";

        /// <summary>
        /// All Numeric Array
        /// </summary>
        public static readonly char[] AllNumericArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /// <summary>
        /// Represents the Base64 String
        /// </summary>
        public const string Base64StringCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        #endregion
    }
}
