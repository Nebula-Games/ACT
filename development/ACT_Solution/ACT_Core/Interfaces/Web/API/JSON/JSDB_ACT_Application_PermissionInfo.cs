// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="JSDB_ACT_Application_PermissionInfo.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Globalization;

namespace ACT.Core.Web.API.JSON
{
    /// <summary>
    /// Application JSON Representation
    /// </summary>
    public class JSDB_ACT_Application_PermissionInfo
    {
        /// <summary>
        /// Converts string JSON to Object
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>JSONReturn.</returns>
        public static JSONReturn FromJson(string json)
        {
            return JsonConvert.DeserializeObject<JSONReturn>(json, JSONSettings);
        }

        /// <summary>
        /// Return a JSON Object
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, JSONSettings);
        }

        /// <summary>
        /// JSON Settings
        /// </summary>
        public static readonly JsonSerializerSettings JSONSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = { new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal } },
        };

        /// <summary>
        /// ID
        /// </summary>
        /// <value>The display name.</value>
        [JsonProperty("displayname")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Application Name
        /// </summary>
        /// <value>The name of the group.</value>
        [JsonProperty("groupname")]
        public string GroupName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        /// <value>The name of the permission.</value>
        [JsonProperty("permissionname")]
        public string PermissionName { get; set; }              
    }
}
