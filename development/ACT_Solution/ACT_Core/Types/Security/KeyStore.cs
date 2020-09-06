using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ACT.Core.Extensions;
using System.Runtime.CompilerServices;

namespace ACT.Core.Types.Security
{

    public partial class KeyStore
    {
        [JsonProperty("keystore", NullValueHandling = NullValueHandling.Ignore)]
        public Keystore Keystore { get; set; }

        public static KeyStore FromJson(string json) => JsonConvert.DeserializeObject<KeyStore>(json, ACT.Core.Serialization.JsonSerializationSettings.Settings);

        public string ToJson() => JsonConvert.SerializeObject(this, ACT.Core.Serialization.JsonSerializationSettings.Settings);

        public KeyStore(Enums.Security.KeyStoreSource source)
        {

        }

        public static KeyStore FromEncryptedFile(string Path)
        {
            string _DataString = Path.ReadAllText();
            byte[] _ProtectedData = _DataString.FromBase64String();
            byte[] _Data = System.Security.Cryptography.ProtectedData.Unprotect(_ProtectedData, ACT.Core.SystemSettings.GetSettingByName("BaseEncryptionKey"), System.Security.Cryptography.DataProtectionScope.LocalMachine);
            
            return FromJson(_Data.GetString());
        }

        public void Save(string Path)
        {
            string _JSON = this.ToJson();
            byte[] _ProtectedData = System.Security.Cryptography.ProtectedData.Protect(_JSON.ToBytes(), ACT.Core.SystemSettings.GetSettingByName(""), System.Security.Cryptography.DataProtectionScope.LocalMachine);
            string _ProtectedDataString = _ProtectedData.ToBase64String();

            _ProtectedDataString.SaveAllText(Path);
        }
    }

    public partial class Keystore
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("keys", NullValueHandling = NullValueHandling.Ignore)]
        public List<Key> Keys { get; set; }
    }

    public partial class Key
    {
        [JsonProperty("identifier", NullValueHandling = NullValueHandling.Ignore)]
        public string Identifier { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("applicationid", NullValueHandling = NullValueHandling.Ignore)]
        public string Applicationid { get; set; }
    }


}
