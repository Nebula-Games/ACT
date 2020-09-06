///-------------------------------------------------------------------------------------------------
// file:	Types\Database\GenericConnectionInformation.cs
//
// summary:	Implements the generic connection information class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ACT.Core.Extensions;
using System.Globalization;


namespace ACT.Core.Types.Database
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Information about the generic connection. </summary>
    ///
    /// <remarks>   Mark Alicz, 8/31/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------
    public class GenericConnectionInformation
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the server. </summary>
        ///
        /// <value> The server. </value>
        ///-------------------------------------------------------------------------------------------------
        [JsonProperty("server")]
        public string Server { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the server port. </summary>
        ///
        /// <value> The server port. </value>
        ///-------------------------------------------------------------------------------------------------
        [JsonProperty("port")]
        public int Port { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the integrated security. </summary>
        ///
        /// <value> The integrated security. </value>
        ///-------------------------------------------------------------------------------------------------
        [JsonProperty("integrated_security")]
        public string IntegratedSecurity { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name of the user. </summary>
        ///
        /// <value> The name of the user. </value>
        ///-------------------------------------------------------------------------------------------------
        [JsonProperty("username")]
        public string UserName { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the encrypted password. </summary>
        ///
        /// <value> The encrypted password. </value>
        ///-------------------------------------------------------------------------------------------------
        [JsonProperty("encrypted_password")]
        public string EncryptedPassword { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the act settings encryption key. </summary>
        ///
        /// <value> The act settings encryption key. </value>
        ///-------------------------------------------------------------------------------------------------
        [JsonProperty("actsettings_encryptionkey")]
        public string ActSettings_EncryptionKey { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name of the database. </summary>
        ///
        /// <value> The name of the database. </value>
        ///-------------------------------------------------------------------------------------------------
        [JsonProperty("database")]
        public string DatabaseName { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets information describing the encrypted. </summary>
        ///
        /// <value> Information describing the encrypted. </value>
        ///-------------------------------------------------------------------------------------------------
        private string _EncryptedData { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets information describing the fully encrypted. </summary>
        ///
        /// <value> Information describing the fully encrypted. </value>
        ///-------------------------------------------------------------------------------------------------
        [JsonProperty("fullyencrypted_data")]
        public string FullyEncryptedData { get { return _EncryptedData; } set { _EncryptedData = value; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process the encrypted data string described by EncryptedData. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/31/2019. </remarks>
        ///
        /// <param name="EncryptedData">    (Optional) Information describing the encrypted. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------
        public bool ProcessEncryptedDataString(string EncryptedData = "")
        {
            try
            {
                string _JSONData = EncryptedData.DecryptString(ActSettings_EncryptionKey);
                var _tmpObj = GenericConnectionInformation.FromJson(_JSONData);
                this.DatabaseName = _tmpObj.DatabaseName;
                this.EncryptedPassword = _tmpObj.EncryptedPassword;
                this.FullyEncryptedData = EncryptedData;
                this.IntegratedSecurity = _tmpObj.IntegratedSecurity;
                this.Server = _tmpObj.Server;
                this.UserName = _tmpObj.UserName;
            }
            catch (Exception ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError(this, "Error Processing Encrypted Data", ex, Enums.ErrorLevel.Severe);
                return false;
            }

            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Generates an encrypted data string. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/31/2019. </remarks>
        ///
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        ///
        /// <param name="EncryptionKey">    The encryption key. </param>
        ///
        /// <returns>   The encrypted data string. </returns>
        ///-------------------------------------------------------------------------------------------------
        public string GenerateEncryptedDataString(string EncryptionKey)
        {
            if (EncryptionKey.NullOrEmpty()) { throw new Exception("Error Encryption Key is Invalid!"); }
            ActSettings_EncryptionKey = EncryptionKey;

            _EncryptedData = this.ToJson();
            _EncryptedData = _EncryptedData.EncryptString(ActSettings_EncryptionKey);

            return _EncryptedData;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes this object from the given JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/31/2019. </remarks>
        ///
        /// <param name="json"> The JSON. </param>
        ///
        /// <returns>   A GenericConnectionInformation. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static GenericConnectionInformation FromJson(string json) => JsonConvert.DeserializeObject<GenericConnectionInformation>(json, Converter.Settings);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts this object to a JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/31/2019. </remarks>
        ///
        /// <returns>   This object as a string. </returns>
        ///-------------------------------------------------------------------------------------------------
        public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A converter. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/31/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------
        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A parse string converter. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/31/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------
        internal class ParseStringConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                long l;
                if (Int64.TryParse(value, out l))
                {
                    return l;
                }
                throw new Exception("Cannot unmarshal type long");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (long)untypedValue;
                serializer.Serialize(writer, value.ToString());
                return;
            }

            public static readonly ParseStringConverter Singleton = new ParseStringConverter();
        }

        /// <summary>
        /// To Connection String
        /// </summary>
        /// <param name="DBType"></param>
        /// <returns></returns>
        public string ToConnectionString(Enums.Database.DatabaseTypes DBType = Enums.Database.DatabaseTypes.MSSQL)
        {
            if (DBType == Enums.Database.DatabaseTypes.MSSQL)
            {
                string DataSource = Server;
                if (Port > 0) { DataSource += ", " + Port.ToString(); }

                string _PW = ACT.Core.SystemSettings.GetSettingByName("ActSettings_EncryptionKey").Value;
                return "Data Source=" + DataSource + ";Initial Catalog=" + DatabaseName + ";User ID=" + UserName + ";Password=" + EncryptedPassword.DecryptString(_PW);
            }

            return "TODO Unsupported";
        }
        /// <summary>
        /// Test the Current connection Info
        /// </summary>
        /// <returns></returns>
        public bool TestConnection(Enums.Database.DatabaseTypes DBType = Enums.Database.DatabaseTypes.MSSQL)
        {
            var _DataAccess = ACT.Core.CurrentCore<Interfaces.DataAccess.I_DataAccess>.GetCurrent();
            _DataAccess.Open(ToConnectionString(DBType));
            if (_DataAccess.Connected) { return true; }
            else { return false; }
        }
    }
}
