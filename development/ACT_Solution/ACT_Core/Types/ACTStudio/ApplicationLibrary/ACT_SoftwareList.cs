// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_SoftwareList.cs" company="Stonegate Intel LLC">
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
    /// Software List
    /// </summary>
    public class SoftwareList
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        [JsonProperty("applicationname", NullValueHandling = NullValueHandling.Ignore)]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        [JsonProperty("index", NullValueHandling = NullValueHandling.Ignore)]
        public long? Index { get; set; }

        /// <summary>
        /// Gets or sets the install script.
        /// </summary>
        /// <value>The install script.</value>
        [JsonProperty("installscript", NullValueHandling = NullValueHandling.Ignore)]
        public string InstallScript { get; set; }

        /// <summary>
        /// Gets or sets the application files.
        /// </summary>
        /// <value>The application files.</value>
        [JsonProperty("applicationfiles", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApplicationLibrary_File> ApplicationFiles { get; set; }
    }
}
