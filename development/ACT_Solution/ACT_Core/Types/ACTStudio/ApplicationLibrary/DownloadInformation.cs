// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="DownloadInformation.cs" company="Nebula Entertainment LLC">
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
    /// Represents the download information specific to this item.
    /// </summary>
    public partial class DownloadInformation
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the urls.
        /// </summary>
        /// <value>The urls.</value>
        [JsonProperty("urls", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> URLS { get; set; }

        /// <summary>
        /// Gets or sets the file name pattern.
        /// </summary>
        /// <value>The file name pattern.</value>
        [JsonProperty("filenamepattern", NullValueHandling = NullValueHandling.Ignore)]
        public string FileNamePattern { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of download.
        /// </summary>
        /// <value>The type of download.</value>
        [JsonProperty("typeofdownload", NullValueHandling = NullValueHandling.Ignore)]
        public string TypeOfDownload { get; set; }

        /// <summary>
        /// Gets the type of the download.
        /// </summary>
        /// <value>The type of the download.</value>
        [JsonIgnore()]
        public DownloadType Download_Type
        {
            get
            {
                try
                {
                    return (DownloadType)Enum.Parse(typeof(DownloadType), TypeOfDownload);
                }
                catch { TypeOfDownload = "OTHER"; return DownloadType.OTHER; }
            }
        }

        /// <summary>
        /// Enum DownloadType
        /// </summary>
        public enum DownloadType
        {
            /// <summary>
            /// The sourcecode
            /// </summary>
            SOURCECODE,
            /// <summary>
            /// The executable
            /// </summary>
            EXE,
            /// <summary>
            /// The vsix
            /// </summary>
            VSIX,
            /// <summary>
            /// The documentation
            /// </summary>
            DOCUMENTATION,
            /// <summary>
            /// The cli
            /// </summary>
            CLI,
            /// <summary>
            /// The other
            /// </summary>
            OTHER
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return FileNamePattern;
        }
    }
}
