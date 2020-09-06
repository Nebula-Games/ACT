// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_ApplicationLibrary_Item.cs" company="Nebula Entertainment LLC">
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
    /// Represents a single item in the application library
    /// </summary>
    public class ApplicationLibrary_File
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the directory.
        /// </summary>
        /// <value>The name of the directory.</value>
        [JsonProperty("directoryname", NullValueHandling = NullValueHandling.Ignore)]
        public string DirectoryName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the homepage.
        /// </summary>
        /// <value>The homepage.</value>
        [JsonProperty("homepage", NullValueHandling = NullValueHandling.Ignore)]
        public string Homepage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [open source].
        /// </summary>
        /// <value><c>null</c> if [open source] contains no value, <c>true</c> if [open source]; otherwise, <c>false</c>.</value>
        [JsonProperty("opensource", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OpenSource { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [save all versions].
        /// </summary>
        /// <value><c>null</c> if [save all versions] contains no value, <c>true</c> if [save all versions]; otherwise, <c>false</c>.</value>
        [JsonProperty("saveallversions", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SaveAllVersions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [download source code].
        /// </summary>
        /// <value><c>null</c> if [download source code] contains no value, <c>true</c> if [download source code]; otherwise, <c>false</c>.</value>
        [JsonProperty("downloadsourcecode", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DownloadSourceCode { get; set; }

        /// <summary>
        /// Gets or sets the download information.
        /// </summary>
        /// <value>The download information.</value>
        [JsonProperty("downloadinformation", NullValueHandling = NullValueHandling.Ignore)]
        public List<DownloadInformation> DownloadInformation { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the last download date.
        /// </summary>
        /// <value>The last download date.</value>
        [JsonProperty("lastdownloaddate", NullValueHandling = NullValueHandling.Ignore)]
        public string LastDownloadDate { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>The keywords.</value>
        [JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Keywords { get; set; }

        /// <summary>
        /// Only Used When Loading For convienance Accessing
        /// </summary>
        /// <value>The child application items.</value>
        [JsonIgnore()]
        public List<ApplicationLibrary_File> ChildApplicationItems { get; set; }

        /// <summary>
        /// Gets or sets the keywords as string.
        /// </summary>
        /// <value>The keywords as string.</value>
        [JsonIgnore()]
        public string KeywordsAsString
        {
            get { return Keywords.ToDelimitedString(","); }
            set
            {
                string[] _data = value.SplitString(",", StringSplitOptions.RemoveEmptyEntries);
                Keywords = new List<string>();
                Keywords.AddRange(_data);
            }
        }

        /// <summary>
        /// The application library item settings
        /// </summary>
        public static readonly JsonSerializerSettings AppLibraryItem_Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>ApplicationLibrary_File.</returns>
        public static ApplicationLibrary_File FromJson(string json)
        {
            var _tmpReturn = JsonConvert.DeserializeObject<ApplicationLibrary_File>(json, AppLibraryItem_Settings);

            if (_tmpReturn.ID.NullOrEmpty()) { _tmpReturn.ID = Guid.NewGuid().ToString(); }

            foreach(var di in _tmpReturn.DownloadInformation) { if (di.ID.NullOrEmpty()) { di.ID = Guid.NewGuid().ToString(); } }

            if (_tmpReturn.ChildApplicationItems == null)
            {
                _tmpReturn.ChildApplicationItems = new List<ApplicationLibrary_File>();
            }

            return _tmpReturn;
        }

        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJson() => JsonConvert.SerializeObject(this, AppLibraryItem_Settings);
    }

   
}


