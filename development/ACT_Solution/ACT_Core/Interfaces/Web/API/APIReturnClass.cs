// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="APIReturnClass.cs" company="Nebula Entertainment LLC">
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

namespace ACT.Core.Web.API
{
    /// <summary>
    /// Represents the return value of all WEB API Methods
    /// </summary>
    public class JSONReturn
    {
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public JSONReturn()
        {
            Apiversion = "1.09";
            Checksum = "";
            Data = new List<ReturnData>();
        }

        /// <summary>
        /// Gets or sets the security token.
        /// </summary>
        /// <value>The security token.</value>
        [JsonProperty("securitytoken")]
        public string SecurityToken { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="JSONReturn"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the primarykey.
        /// </summary>
        /// <value>The primarykey.</value>
        [JsonProperty("primarykey")]
        public string Primarykey { get; set; }

        /// <summary>
        /// Gets or sets the errormessage.
        /// </summary>
        /// <value>The errormessage.</value>
        [JsonProperty("errormessage")]
        public string Errormessage { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        [JsonProperty("data")]
        public List<ReturnData> Data { get; set; }

        /// <summary>
        /// Gets or sets the apiversion.
        /// </summary>
        /// <value>The apiversion.</value>
        [JsonProperty("apiversion")]
        public string Apiversion { get; set; }

        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        /// <value>The checksum.</value>
        [JsonProperty("checksum")]
        public string Checksum { get; set; }

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
        /// Class ReturnData.
        /// </summary>
        public class ReturnData
        {
            /// <summary>
            /// Gets or sets the key.
            /// </summary>
            /// <value>The key.</value>
            [JsonProperty("key")]
            public string Key { get; set; }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>The value.</value>
            [JsonProperty("value")]
            public string Value { get; set; }
        }
    }
}
