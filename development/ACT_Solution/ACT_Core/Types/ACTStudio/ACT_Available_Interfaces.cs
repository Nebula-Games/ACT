// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_Available_Interfaces.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Net;
using System.Collections.Generic;
using ACT.Core.Extensions;
using Newtonsoft.Json;

namespace ACT.Core.Types.ACTStudio
{


    /// <summary>
    /// Class ACT_Available_Interfaces.
    /// </summary>
    public partial class ACT_Available_Interfaces
    {
        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        /// <value>The groups.</value>
        [JsonProperty("groups")]
        public List<ACT_Interface_Group> Groups { get; set; }

        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJson() => JsonConvert.SerializeObject(this, ACT_Available_Interfaces_Converter.Settings);
        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>ACT_Available_Interfaces.</returns>
        public static ACT_Available_Interfaces FromJson(string json) => JsonConvert.DeserializeObject<ACT_Available_Interfaces>(json, ACT_Available_Interfaces_Converter.Settings);
        /// <summary>
        /// Froms the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>ACT_Available_Interfaces.</returns>
        public static ACT_Available_Interfaces FromFile(string fileName) => FromJson(fileName.ReadAllText());
    }

    /// <summary>
    /// Class ACT_Interface_Group.
    /// </summary>
    public partial class ACT_Interface_Group
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the interfaces.
        /// </summary>
        /// <value>The interfaces.</value>
        [JsonProperty("interfaces")]
        public List<string> Interfaces { get; set; }
    }

    /// <summary>
    /// Class ACT_Available_Interfaces_Converter.
    /// </summary>
    public class ACT_Available_Interfaces_Converter
    {
        /// <summary>
        /// The settings
        /// </summary>
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
