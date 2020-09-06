// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_Template_DB_Schema.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Net;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ACT.Core.Types.ACTConfig
{
    /// <summary>
    /// Class ACT_Template_DB_Schema.
    /// </summary>
    public class ACT_Template_DB_Schema
    {
        /// <summary>
        /// Gets or sets the tables.
        /// </summary>
        /// <value>The tables.</value>
        [JsonProperty("tables")]
        public List<string> Tables { get; set; }

        /// <summary>
        /// Gets or sets the procedures.
        /// </summary>
        /// <value>The procedures.</value>
        [JsonProperty("procedures")]
        public List<string> Procedures { get; set; }

        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>ACT_Template_DB_Schema.</returns>
        public static ACT_Template_DB_Schema FromJson(string json) => JsonConvert.DeserializeObject<ACT_Template_DB_Schema>(json, Settings);
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJson() => JsonConvert.SerializeObject(this, Settings);

        /// <summary>
        /// The settings
        /// </summary>
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

   
}

