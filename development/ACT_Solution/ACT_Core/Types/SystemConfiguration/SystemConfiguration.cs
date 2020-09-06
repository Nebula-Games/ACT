using System;
using System.Linq;
using System.Collections.Generic;
using ACT.Core.Extensions;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.Interfaces.Security.Authentication;
using Newtonsoft.Json;
using ACT.Core.Enums.SystemConfiguration;
/*
 Classes To HOLD The Data Loaded From The System Configuration File
*/

namespace ACT.Core.Types.SystemConfiguration
{

    /// <summary>
    /// System Configuration Class
    /// </summary>
    public class SystemConfiguration : Interfaces.Data.I_DB_Store, Interfaces.Plugins.I_Simple_Plugin, ICloneable, Interfaces.IO.I_Saveable, IComparable, Interfaces.Security.Hashing.I_ACT_SecureHash
    {

        #region Hashing Methods

        /// <summary>
        /// Generates a Byte[] Hash
        /// </summary>
        /// <returns>Byte[]</returns>
        public byte[] GenerateHash(object o = null)
        {
            var _BIHash = ACT.Core.CurrentCore<Interfaces.Security.Hashing.I_ACT_SecureHash>.GetCurrent();
            return _BIHash.GenerateHash(this);
        }

        /// <summary>
        /// Generates a Base64 String of the class
        /// </summary>
        /// <returns>Base64 String</returns>
        public string GenerateHashString(object o = null)
        {
            var _BIHash = ACT.Core.CurrentCore<Interfaces.Security.Hashing.I_ACT_SecureHash>.GetCurrent();
            return _BIHash.GenerateHashString(this);
        }

        #endregion

        #region IClonable Implementation

        /// <summary>
        /// Clone System Configuration
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            SystemConfiguration _tmp = SystemConfiguration.FromJson(this.ToJson());
            return _tmp;
        }

        #endregion

        #region I_Saveable Implementation

        /// <summary>
        /// Save the Configuration File No Muss No Fuss
        /// </summary>
        /// <returns>Common.I_TestResult.</returns>
        public I_TestResult Save()
        {
            string _JSON = this.ToJson();
            _JSON = Helper.LocalComputerSpecific_HelperMethods.EncryptString_ComputerSpecific(_JSON, "ACT");
            _JSON.SaveAllText(SystemConfiguration._SystemConfigurationPath);

            try
            {
                var _tmpReturn = ACT.Core.CurrentCore<ACT.Core.Interfaces.Common.I_TestResult>.GetCurrent();
                _tmpReturn.Success = true;
                return _tmpReturn;
            }
            catch (Exception ex)
            {
                // TODO Log Error
                throw new TypeLoadException("Unable To Locate I_TestResult Implementation");
            }
        }

        /// <summary>
        /// Save the Configuration File Using Advanced Settings
        /// </summary>
        /// <param name="Location">The location.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="OverWrite">if set to <c>true</c> [over write].</param>
        /// <param name="CreateBackup">if set to <c>true</c> [create backup].</param>
        /// <returns>Common.I_TestResult.</returns>
        public I_TestResult Save(string Location, string FileName = "", bool OverWrite = true, bool CreateBackup = false)
        {
            // Simple Save If They are using the Current Loaded Directory
            // TODO Add Error Logging / Checking

            if (Location.ToLower().EnsureDirectoryFormat() == SystemConfiguration._SystemConfigurationPath.ToLower().EnsureDirectoryFormat()) { return Save(); }

            var _tmpReturn = ACT.Core.CurrentCore<ACT.Core.Interfaces.Common.I_TestResult>.GetCurrent();

            if (Location.DirectoryExists() == false)
            {
                _tmpReturn.Messages.Add("Error Locating Location: " + Location);
                _tmpReturn.Success = false;
                _tmpReturn.Exceptions.Add(new Exception("Error Location Does Not Exist: " + Location));
                return _tmpReturn;
            }

            if (FileName.NullOrEmpty()) { FileName = "SystemConfiguration.json"; }
            else { if (FileName.ToLower().EndsWith(".json") == false) { FileName += ".json"; } }

            string _locationPATH = Location.EnsureDirectoryFormat() + FileName;

            if (OverWrite == false)
            {
                if (System.IO.File.Exists(_locationPATH))
                {
                    _tmpReturn.Success = false;
                    _tmpReturn.Messages.Add("OverWrite Set To False BUT File Found");
                    return _tmpReturn;
                }
            }

            if (CreateBackup)
            {
                if (_locationPATH.FileExists())
                {
                    string _newBackupFile = _locationPATH.Replace(".json", DateTime.Now.ToUnixTime().ToString() + ".json");

                    try { _locationPATH.CopyFileTo(_newBackupFile); _tmpReturn.Messages.Add("File Backed Up To: " + _newBackupFile); }
                    catch (Exception) { _tmpReturn.Success = false; _tmpReturn.Messages.Add("Unable To Create Backup-Aborting"); return _tmpReturn; }
                }
            }

            string _JSON = this.ToJson();
            _JSON.SaveAllText(_locationPATH);
            _tmpReturn.Messages.Add("Configuration Saved To " + _locationPATH);
            _tmpReturn.Success = true;
            return _tmpReturn;
        }

        /// <summary>
        /// Save the configuration File using Advanced Settings
        /// </summary>
        /// <param name="Locations">The locations.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="OverWrite">if set to <c>true</c> [over write].</param>
        /// <param name="CreateBackup">if set to <c>true</c> [create backup].</param>
        /// <returns>Common.I_TestResult.</returns>
        public I_TestResult Save(List<string> Locations, string FileName = "", bool OverWrite = false, bool CreateBackup = false)
        {
            var _tmpReturn = ACT.Core.CurrentCore<ACT.Core.Interfaces.Common.I_TestResult>.GetCurrent();
            foreach (string l in Locations)
            {
                var _tmpR = Save(l, FileName, OverWrite, CreateBackup);
                if (_tmpR.Success == false) { return _tmpR; }
                _tmpReturn.Messages.AddRange(_tmpR.Messages);
                _tmpReturn.Messages.Add("Saved To Location: " + l);
            }
            _tmpReturn.Success = true;
            return _tmpReturn;
        }

        #endregion

        #region IComparable Implementation
        public int CompareTo(object obj)
        {
            if (!(obj is SystemConfiguration)) { return -1; }

            var _sourceobj = (SystemConfiguration)obj;

            if (this.GenerateHashString() == _sourceobj.GenerateHashString()) { return 0; }
            else { return -1; }

        }

        #endregion

        #region I_DB_STORE Implementation

        public I_TestResult LoadFromDB(I_DbWhereStatement whereStatement)
        {
            throw new NotImplementedException();
        }

        public I_TestResult LoadFromDB()
        {
            throw new NotImplementedException();
        }

        public I_TestResult SaveToDB()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region I_Simple_Plugin

        public void SetImpersonate(I_UserInfo UserInfo)
        {
            throw new NotImplementedException();
        }

        public List<string> ReturnSystemSettingRequirements()
        {

            return new List<string> { "ACT.Core.Interfaces.DataAccess.I_DataAccess", "Interfaces.Security.Hashing.I_ACT_SecureHash", "ACT.Core.Interfaces.Common.I_TestResult" };
        }

        public I_TestResult ValidatePluginRequirements()
        {
            // TODO Execute Validate Plugin Code
            throw new NotImplementedException();
        }

        #endregion

        #region Private Static Variables
        private static string _SystemConfigurationPath = "";
        #endregion

        #region Static Methods

        public static SystemConfiguration FromJson(string json) => JsonConvert.DeserializeObject<SystemConfiguration>(json, ACT.Core.Serialization.JsonSerializationSettings.Settings);

        /// <summary>
        /// Calls ACT.Core.ACTConstants.SystemConfiguration.GetSystemConfigurationFileFullPath
        /// </summary>
        /// <param name="BaseDirectory"></param>
        /// <returns></returns>
        public static SystemConfiguration LoadDefaultConfigurationFile(string BaseDirectory)
        {
            if (BaseDirectory.DirectoryExists() == false)
            {
                Helper.ErrorLogger.VLogError("Types.SystemConfiguration.SystemConfiguration", "Base Directory Not Found: " + BaseDirectory.ToString(), null, Enums.ErrorLevel.Critical);
                return null;
            }

            _SystemConfigurationPath = ACT.Core.ACTConstants.SystemConfiguration.GetSystemConfigurationFileFullPath(AppDomain.CurrentDomain.BaseDirectory);

            return SystemConfiguration.FromJson(_SystemConfigurationPath.ReadAllText());
        }

        #endregion

        #region Public Properties

        [JsonProperty("file_version", NullValueHandling = NullValueHandling.Ignore)]
        public long? FileVersion { get; set; }

        [JsonProperty("dependancies", NullValueHandling = NullValueHandling.Ignore)]
        public List<Dependancy> Dependancies { get; set; }

        [JsonProperty("interfaces", NullValueHandling = NullValueHandling.Ignore)]
        public List<Interface> Interfaces { get; set; }

        [JsonProperty("plugins", NullValueHandling = NullValueHandling.Ignore)]
        public List<Plugin> Plugins { get; set; }

        [JsonProperty("encryptionkeys", NullValueHandling = NullValueHandling.Ignore)]
        public List<Encryptionkey> Encryptionkeys { get; set; }

        [JsonProperty("basicsettings", NullValueHandling = NullValueHandling.Ignore)]
        public List<BasicSetting> BasicSettings { get; set; }

        [JsonProperty("complexsettings", NullValueHandling = NullValueHandling.Ignore)]
        public List<ComplexSetting> ComplexSettings { get; set; }

        [JsonProperty("applications", NullValueHandling = NullValueHandling.Ignore)]
        public List<Application> Applications { get; set; }

        #endregion

        #region Public Methods

        public string ToJson() => JsonConvert.SerializeObject(this, ACT.Core.Serialization.JsonSerializationSettings.Settings);

        /// <summary>
        /// Has Key Somewhere in the System
        /// </summary>
        /// <param name="KeyName"></param>
        /// <returns></returns>
        public bool HasKey(string KeyName)
        {
            if (_AllSettings.Count == 0) { TryLoadAllSettingKeys(); }
            return _AllSettings.Exists(x => x.Key == KeyName);
        }

        /// <summary>
        /// Returns a List of Distinct Keys Duplicates are NOT Handled Properly.
        /// </summary>
        public List<string> SettingKeys
        {
            get
            {
                if (_AllSettings.Count == 0)
                {
                    if (TryLoadAllSettingKeys() == false) { return new List<string>(); }
                }
                return _AllSettings.Select(x => x.Key).Distinct().ToList();
            }
        }

        #endregion

        #region Private Fields

        private List<(SettingType settingType, string Key)> _AllSettings = new List<(SettingType settingType, string Key)>();
        private bool _Loaded = false;

        #endregion

        #region Private Methods

        /// <summary>
        /// Load the Settings Into The Cache
        /// </summary>
        /// <returns></returns>
        private bool TryLoadAllSettingKeys()
        {
            _AllSettings.Clear();

            int _Counter = 0;
            foreach (string name in BasicSettings.Select(x => x.Name)) { _AllSettings.Add((SettingType.BasicSetting, name)); _Counter++; }
            foreach (string name in ComplexSettings.Select(x => x.Name)) { _AllSettings.Add((SettingType.ComplexSetting, name)); _Counter++; }
            foreach (string name in Plugins.Select(x => x.FullClassName)) { _AllSettings.Add((SettingType.PluginSetting, name)); _Counter++; }
            foreach (string name in Interfaces.Select(x => x.Name)) { _AllSettings.Add((SettingType.InterfaceSetting, name)); _Counter++; }
            foreach (string name in Encryptionkeys.Select(x => x.Identifier)) { _AllSettings.Add((SettingType.EncryptionKeySetting, name)); _Counter++; }
            foreach (string name in Applications.Select(x => x.Name)) { _AllSettings.Add((SettingType.ApplicationSetting, name)); _Counter++; }
            foreach (Environment name in Applications.SelectMany(x => x.Environments)) { _AllSettings.Add((SettingType.ApplicationEnvironmentSetting, name.Name)); _Counter++; }
            foreach (string name in Dependancies.Select(x => x.Name)) { _AllSettings.Add((SettingType.DependancySetting, name)); _Counter++; }

            if (_Counter == 0) { return false; }
            return true;
        }

        #endregion

    }
}
