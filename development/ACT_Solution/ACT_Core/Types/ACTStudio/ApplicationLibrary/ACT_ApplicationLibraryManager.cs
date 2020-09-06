// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_ApplicationLibraryManager.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ACT.Core.Extensions;


namespace ACT.Core.Types.ACTStudio.ApplicationLibrary
{

    /// <summary>
    /// Application Library Class Holds all the items
    /// </summary>
    public class ApplicationLibrary
    {
        #region Static Methods

        /// <summary>
        /// Application JSON Settings
        /// </summary>
        public static readonly JsonSerializerSettings ApplicationLibraryManager_JSONSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        /// <summary>
        /// List of the Configuration Requirements
        /// </summary>
        public static List<string> ConfigurationRequirements = new List<string>() { "ApplicationLibraryRootDirectory" };

        /// <summary>
        /// Load the Library From a Directory
        /// </summary>
        /// <param name="DirectoryRoot">The directory root.</param>
        /// <returns>ApplicationLibrary.</returns>
        /// <exception cref="Exception">Missing Requirements</exception>
        public static ApplicationLibrary LoadLibrary(string DirectoryRoot = "")
        {
            if (DirectoryRoot == "")
            {
                if (ACT.Core.SystemSettings.GetSettingByName(ConfigurationRequirements[0]) == null)
                {
                    throw new Exception("Missing Requirements");
                }

                DirectoryRoot = ACT.Core.SystemSettings.GetSettingByName(ConfigurationRequirements[0]).Value;
            }

            return ApplicationLibrary.FromJson(System.IO.File.ReadAllText(DirectoryRoot.EnsureDirectoryFormat() + "index.json"));
        }

        /// <summary>
        /// From JSON Method
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>ApplicationLibrary.</returns>
        public static ApplicationLibrary FromJson(string json)
        {
            var _tmpReturn = JsonConvert.DeserializeObject<ApplicationLibrary>(json, ApplicationLibraryManager_JSONSettings);

            if (_tmpReturn.ID.NullOrEmpty()) { _tmpReturn.ID = Guid.NewGuid().ToString(); }

            foreach (var di in _tmpReturn.InstallationGroups)
            {
                if (di.ID.NullOrEmpty()) { di.ID = Guid.NewGuid().ToString(); }

                foreach (var ai in di.SoftwareLists) { if (ai.ID.NullOrEmpty()) { ai.ID = Guid.NewGuid().ToString(); } }
            }
            foreach (var di in _tmpReturn.AutoUpdateSettings) { if (di.ID.NullOrEmpty()) { di.ID = Guid.NewGuid().ToString(); } }
            

            return _tmpReturn;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the base directory.
        /// </summary>
        /// <value>The base directory.</value>
        [JsonProperty("basedirectory", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseDirectory { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ApplicationLibrary"/> is encrypted.
        /// </summary>
        /// <value><c>null</c> if [encrypted] contains no value, <c>true</c> if [encrypted]; otherwise, <c>false</c>.</value>
        [JsonProperty("encrypted", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Encrypted { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the automatic update settings.
        /// </summary>
        /// <value>The automatic update settings.</value>
        [JsonProperty("autoupdatesettings", NullValueHandling = NullValueHandling.Ignore)]
        public List<AutoUpdate> AutoUpdateSettings { get; set; }

        /// <summary>
        /// Gets or sets the installation groups.
        /// </summary>
        /// <value>The installation groups.</value>
        [JsonProperty("installationGroups", NullValueHandling = NullValueHandling.Ignore)]
        public List<InstallationGroup> InstallationGroups { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the last save date.
        /// </summary>
        /// <value>The last save date.</value>
        [JsonProperty("lastsavedate", NullValueHandling = NullValueHandling.Ignore)]
        public string LastSaveDate { get; set; }

        #endregion

        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJson() => JsonConvert.SerializeObject(this, ApplicationLibraryManager_JSONSettings);

    }

   



   
}
