using System;
using System.Collections.Generic;
using System.Globalization;
using ACT.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ACT.Core.SystemConfiguration.Installation
{
    public partial class InstallationFile : Interfaces.Security.Hashing.I_ACT_SecureHash
    {

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

        [JsonProperty("Installation", NullValueHandling = NullValueHandling.Ignore)]
        public Installation Installation { get; set; }

        public static InstallationFile FromJson(string json) => JsonConvert.DeserializeObject<InstallationFile>(json, ACT.Core.Serialization.JsonSerializationSettings.Settings);
        public string ToJson() => JsonConvert.SerializeObject(this, ACT.Core.Serialization.JsonSerializationSettings.Settings);

        public void SaveInstallationFile(string Location)
        {
            string _Data = this.ToJson();
            _Data.ScopeSpecific_Encrypt("ACT.Core.SystemConfiguration.ActInstallationFile", System.Security.Cryptography.DataProtectionScope.LocalMachine);
            _Data.SaveAllText(Location.EnsureDirectoryFormat() + "Resources\\act.enc");
        }

    }

    public partial class Installation : Interfaces.Security.Hashing.I_ACT_SecureHash
    {

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

        [JsonProperty("LicenseKey", NullValueHandling = NullValueHandling.Ignore)]
        public string LicenseKey { get; set; }

        [JsonProperty("BaseSystemDirectory", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseSystemDirectory { get; set; }

        [JsonProperty("InstallDate", NullValueHandling = NullValueHandling.Ignore)]
        public string InstallDate { get; set; }

        [JsonProperty("CoreVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string CoreVersion { get; set; }

        [JsonProperty("InstalledApplications", NullValueHandling = NullValueHandling.Ignore)]
        public List<InstalledApplication> InstalledApplications { get; set; }

        [JsonProperty("APIConfiguration", NullValueHandling = NullValueHandling.Ignore)]
        public ApiConfiguration ApiConfiguration { get; set; }

        [JsonProperty("CallingApplicationLog", NullValueHandling = NullValueHandling.Ignore)]
        public List<CallingApplicationLog> CallingApplicationLog { get; set; }

        [JsonProperty("LogToDisk", NullValueHandling = NullValueHandling.Ignore)]
        public string LogToDisk { get; set; }
    }

    public partial class ApiConfiguration : Interfaces.Security.Hashing.I_ACT_SecureHash
    {

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

        [JsonProperty("GlobalConfigURL", NullValueHandling = NullValueHandling.Ignore)]
        public Uri GlobalConfigUrl { get; set; }
    }

    public partial class CallingApplicationLog : Interfaces.Security.Hashing.I_ACT_SecureHash
    {

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

        [JsonProperty("BasePath", NullValueHandling = NullValueHandling.Ignore)]
        public string BasePath { get; set; }

        [JsonProperty("DateLastAccessed", NullValueHandling = NullValueHandling.Ignore)]
        public string DateLastAccessed { get; set; }

        [JsonProperty("UsageCount", NullValueHandling = NullValueHandling.Ignore)]
        public long? UsageCount { get; set; }
    }

    public partial class InstalledApplication : Interfaces.Security.Hashing.I_ACT_SecureHash
    {

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

        [JsonProperty("ID", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Id { get; set; }

        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("Version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        [JsonProperty("InstallPath", NullValueHandling = NullValueHandling.Ignore)]
        public string InstallPath { get; set; }
    }
}
