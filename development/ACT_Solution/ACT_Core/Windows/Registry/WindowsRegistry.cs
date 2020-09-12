///-------------------------------------------------------------------------------------------------
// file:	Windows\Registry\WindowsRegistry.cs
//
// summary:	Implements the windows registry class
///-------------------------------------------------------------------------------------------------

using ACT.Core.Extensions;
using System;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Windows Registry Helper Methods
    /// </summary>
    public static class WindowsRegistryExtension
    {
        /// <summary>
        /// Create A SubKey
        /// </summary>
        /// <param name="Parent"></param>
        /// <param name="SubKeyName"></param>
        public static bool CreateChild(this Microsoft.Win32.RegistryKey Parent, string SubKeyName)
        {
            try { Parent.CreateSubKey(SubKeyName, true, Microsoft.Win32.RegistryOptions.None); }
            catch { return false; }

            return true;
        }
    }
}

namespace ACT.Core.Windows.Registry
{
    /// <summary>
    /// Windows Registry Helper Methods
    /// </summary>
    public static class WindowsRegistry
    {
        /// <summary>
        /// Create Registry Key With Value
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <param name="TypeOfKey"></param>
        public static bool CreateKey(Microsoft.Win32.RegistryKey Key, string Name, string Value, Microsoft.Win32.RegistryValueKind TypeOfKey = Microsoft.Win32.RegistryValueKind.String)
        {
            try
            {
                Key.SetValue(Name, Value, TypeOfKey);
                if (Key.GetValue(Name) == null) { return false; }
            }
            catch { return false; }

            return true;
        }

        /// <summary>
        /// Read Key
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Name"></param>
        /// <param name="Create"></param>
        /// <param name="Val"></param>
        /// <returns></returns>
        public static string ReadKey(Microsoft.Win32.RegistryKey Key, string Name, bool Create = false, string Val = "")
        {
            if (Key.GetValue(Name) == null) { if (Create) { CreateKey(Key, Name, Val); } }

            try
            {
                string _TmpReturn = Key.GetValue(Name).ToString();
                Key.Close();
                return _TmpReturn;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Update A Key
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        public static void UpdateKey(Microsoft.Win32.RegistryKey Key, string Name, object Value)
        {
            Key.SetValue(Name, Value);
        }

        /// <summary>
        /// Read ACT Specific Registry Key
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ApplicationName"></param>
        /// <returns></returns>
        public static string ReadACTKey(string Name, string ApplicationName = "")
        {
            string _tmpReturn = null;
            Microsoft.Win32.RegistryKey _BaseKey = null;
            Microsoft.Win32.RegistryKey _Software = null;
            Microsoft.Win32.RegistryKey _ACTKey = null;
            Microsoft.Win32.RegistryKey _ApplicationKey = null;

            try
            {
                //var _RegKey = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.);

                _BaseKey = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Default);
                 _Software = _BaseKey.OpenSubKey("SOFTWARE", true);
                 _ACTKey = _Software.OpenSubKey(ACT.Core.Consts.Windows.Registry.REGISTRY_CONSTANTS.ACTCORE_KEYNAME, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.ReadKey);

                if (ApplicationName.NullOrEmpty() == false)
                {
                     _ApplicationKey = _ACTKey.OpenSubKey(ApplicationName);
                    _tmpReturn = _ApplicationKey.GetValue(Name).TryToString("");
                }
                else
                {
                    _tmpReturn = _ACTKey.GetValue(Name).TryToString("");
                }               
            }
            catch (Exception ex)
            {
                if (_BaseKey != null) { _BaseKey.Dispose(); }
                if (_Software != null) { _Software.Dispose(); }
                if (_ACTKey != null) { _ACTKey.Dispose(); }
                if (_ApplicationKey != null) { _ApplicationKey.Dispose(); }

                ACT.Core.Helper.ErrorLogger.LogError("ACT.Core.Windows.Registry.WindowsRegistry.ReadACTKey()", "Error Reading Registry Key", ex, Enums.ErrorLevel.Informational);
                _tmpReturn = null;
            }

            if (_BaseKey != null) { _BaseKey.Dispose(); }
            if (_Software != null) { _Software.Dispose(); }
            if (_ACTKey != null) { _ACTKey.Dispose(); }
            if (_ApplicationKey != null) { _ApplicationKey.Dispose(); }

            return _tmpReturn ?? "";
        }

        /// <summary>
        /// Return the ACT Registry Key
        /// </summary>
        /// <returns></returns>
        public static Microsoft.Win32.RegistryKey GetACT_RegistryKey()
        {          
            Microsoft.Win32.RegistryKey _BaseKey = null;
            Microsoft.Win32.RegistryKey _Software = null;
            Microsoft.Win32.RegistryKey _ACTKey = null;
      
            try
            {
                _BaseKey = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Default);
                _Software = _BaseKey.OpenSubKey("SOFTWARE", true);
                _ACTKey = _Software.OpenSubKey(ACT.Core.Consts.Windows.Registry.REGISTRY_CONSTANTS.ACTCORE_KEYNAME, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);

                if (_ACTKey != null)
                {
                    _BaseKey.Dispose();
                    _Software.Dispose();
                    return _ACTKey;
                }
            }
            catch (Exception ex)
            {
                if (_BaseKey != null) { _BaseKey.Dispose(); }
                if (_Software != null) { _Software.Dispose(); }
                if (_ACTKey != null) { _ACTKey.Dispose(); }
           
                ACT.Core.Helper.ErrorLogger.LogError("ACT.Core.Windows.Registry.WindowsRegistry.ReadACTKey()", "Error Reading Registry Key", ex, Enums.ErrorLevel.Informational);
            }

            if (_BaseKey != null) { _BaseKey.Dispose(); }
            if (_Software != null) { _Software.Dispose(); }
            if (_ACTKey != null) { _ACTKey.Dispose(); }

            return null;
        }
    }
}
