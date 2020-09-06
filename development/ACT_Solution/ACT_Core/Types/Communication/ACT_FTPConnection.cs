// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 03-05-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-24-2019
// ***********************************************************************
// <copyright file="ACT_FTPConnection.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Globalization;
using ACT.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace ACT.Core.Types.Communication
{
    /// <summary>
    /// ACT FTP Connection Class
    /// </summary>
    public class ACT_FTPConnection
    {
        /// <summary>
        /// Gets or sets the encryption.
        /// </summary>
        /// <value>The encryption.</value>
        [JsonProperty("encryption")]
        public string Encryption { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        [JsonProperty("ipaddress")]
        public string IPAddress { get; set; }
        /// <summary>
        /// Gets or sets the name of the host.
        /// </summary>
        /// <value>The name of the host.</value>
        [JsonProperty("hostname")]
        public string HostName { get; set; }
        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        /// <value>The port number.</value>
        [JsonProperty("portnumber", NullValueHandling = NullValueHandling.Ignore)]
        public int PortNumber { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [JsonProperty("username")]
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the pass word.
        /// </summary>
        /// <value>The pass word.</value>
        [JsonProperty("password")]
        public string PassWord { get; set; }
        /// <summary>
        /// Gets or sets the download path.
        /// </summary>
        /// <value>The download path.</value>
        [JsonProperty("downloadpath")]
        public string DownloadPath { get; set; }
        /// <summary>
        /// Gets or sets the upload path.
        /// </summary>
        /// <value>The upload path.</value>
        [JsonProperty("uploadpath")]
        public string UploadPath { get; set; }

        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>ACT_FTPConnection.</returns>
        internal static ACT_FTPConnection FromJson(string json) => JsonConvert.DeserializeObject<ACT_FTPConnection>(json, Converter.Settings);

        /// <summary>
        /// Froms the json string. Either All Properties (Except Port) Are Encrypted Or No Properties Are See Below For Instructions
        /// Internal, CustomKey, ""
        /// Encryption Key = Internal, CustomKey (ACTSetting), "" for Not Encrypted
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="Key">The key.</param>
        /// <returns>ACT_FTPConnection.</returns>
        /// <exception cref="Exception">
        /// Error Locating Encryption Key
        /// or
        /// Invalid JSON
        /// </exception>
        public static ACT_FTPConnection FromJsonString(string json, string Key)
        {
            var _tmpReturn = JsonConvert.DeserializeObject<ACT_FTPConnection>(json, Converter.Settings);
            if (_tmpReturn != null)
            {
                if (_tmpReturn.Encryption == "internal")
                {
                    _tmpReturn.DownloadPath = _tmpReturn.DownloadPath.DecryptString();
                    _tmpReturn.HostName = _tmpReturn.HostName.DecryptString();
                    _tmpReturn.IPAddress = _tmpReturn.IPAddress.DecryptString();
                    _tmpReturn.PassWord = _tmpReturn.PassWord.DecryptString();
                    _tmpReturn.UploadPath = _tmpReturn.UploadPath.DecryptString();
                    _tmpReturn.UserName = _tmpReturn.UserName.DecryptString();
                }
                else if (_tmpReturn.Encryption.NullOrEmpty() == false)
                {
                    if (Key == "")
                    {
                        Key = ACT.Core.SystemSettings.GetSettingByName(_tmpReturn.Encryption).Value;
                    }
                    if (Key == "") { throw new Exception("Error Locating Encryption Key"); }

                    if (_tmpReturn.DownloadPath.NullOrEmpty() == false) { _tmpReturn.DownloadPath = _tmpReturn.DownloadPath.DecryptString(Key); }
                    if (_tmpReturn.HostName.NullOrEmpty() == false) { _tmpReturn.HostName = _tmpReturn.HostName.DecryptString(Key); }
                    if (_tmpReturn.IPAddress.NullOrEmpty() == false) { _tmpReturn.IPAddress = _tmpReturn.IPAddress.DecryptString(Key); }
                    if (_tmpReturn.PassWord.NullOrEmpty() == false) { _tmpReturn.PassWord = _tmpReturn.PassWord.DecryptString(Key); }
                    if (_tmpReturn.UploadPath.NullOrEmpty() == false) { _tmpReturn.UploadPath = _tmpReturn.UploadPath.DecryptString(Key); }
                    if (_tmpReturn.UserName.NullOrEmpty() == false) { _tmpReturn.UserName = _tmpReturn.UserName.DecryptString(Key); }
                }
            }
            else
            {
                throw new Exception("Invalid JSON");
            }

            return _tmpReturn;
        }

        /// <summary>
        /// Encrypts the specified key.
        /// </summary>
        /// <param name="Key">The key.</param>
        public void Encrypt(string Key)
        {
            if (Key.NullOrEmpty())
            {
                if (DownloadPath.NullOrEmpty() == false) { DownloadPath = DownloadPath.EncryptString(); }
                if (HostName.NullOrEmpty() == false) { HostName = HostName.EncryptString(); }
                if (IPAddress.NullOrEmpty() == false) { IPAddress = IPAddress.EncryptString(); }
                if (PassWord.NullOrEmpty() == false) { PassWord = PassWord.EncryptString(); }
                if (UploadPath.NullOrEmpty() == false) { UploadPath = UploadPath.EncryptString(); }
                if (UserName.NullOrEmpty() == false) { UserName = UserName.EncryptString(); }
            }
            else
            {
                if (DownloadPath.NullOrEmpty() == false) { DownloadPath = DownloadPath.EncryptString(Key); }
                if (HostName.NullOrEmpty() == false) { HostName = HostName.EncryptString(Key); }
                if (IPAddress.NullOrEmpty() == false) { IPAddress = IPAddress.EncryptString(Key); }
                if (PassWord.NullOrEmpty() == false) { PassWord = PassWord.EncryptString(Key); }
                if (UploadPath.NullOrEmpty() == false) { UploadPath = UploadPath.EncryptString(Key); }
                if (UserName.NullOrEmpty() == false) { UserName = UserName.EncryptString(Key); }
            }
        }

        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);

        /// <summary>
        /// Class Converter.
        /// </summary>
        internal static class Converter
        {
            /// <summary>
            /// The settings
            /// </summary>
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
    }
}
