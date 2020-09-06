// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="TemplatePackage.cs" company="Nebula Entertainment LLC">
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
using System.Globalization;

namespace ACT.Core.TemplateEngine
{
    /// <summary>
    /// Class TemplatePackage.
    /// </summary>
    public class TemplatePackage
    {
        /// <summary> The base directory </summary>
        [JsonProperty("basedirectory", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseDirectory;

        /// <summary>   Name of the package. </summary>
        [JsonProperty("packagename", NullValueHandling = NullValueHandling.Ignore)]
        public string PackageName;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name of the actdb connection. </summary>
        ///
        /// <value> The name of the actdb connection. </value>
        ///-------------------------------------------------------------------------------------------------
        [JsonProperty("actdb_connectionname", NullValueHandling = NullValueHandling.Ignore)]
        public string ACTDB_ConnectionName { get; set; }

        /// <summary>
        /// The package type
        /// </summary>
        [JsonProperty("basedirectory", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Enums.TemplateEngine.PackageType))]
        public Enums.TemplateEngine.PackageType Package_Type;

        /// <summary>
        /// The templates
        /// </summary>
        [JsonProperty("templates", NullValueHandling = NullValueHandling.Ignore)]
        public List<Template> Templates = new List<Template>();

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets information describing the connection. </summary>
        ///
        /// <value> Information describing the connection. </value>
        ///-------------------------------------------------------------------------------------------------
        [JsonProperty("connectioninfo", NullValueHandling = NullValueHandling.Ignore)]
        ACT.Core.Types.Database.GenericConnectionInformation ConnectionInformation { get; set; }
              
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the user indentity. </summary>
        ///
        /// <value> The user indentity. </value>
        ///-------------------------------------------------------------------------------------------------
        [JsonProperty("useridentity", NullValueHandling = NullValueHandling.Ignore)]
        public ACT.Core.Types.Security.ACT_UserIdentify UserIndentity { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes this object from the given JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/31/2019. </remarks>
        ///
        /// <param name="json"> The JSON. </param>
        ///
        /// <returns>   A GenericConnectionInformation. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static TemplatePackage FromJson(string json) => JsonConvert.DeserializeObject<TemplatePackage>(json, ACT.Core.Serialization.JsonSerializationSettings.Settings);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts this object to a JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/31/2019. </remarks>
        ///
        /// <returns>   This object as a string. </returns>
        ///-------------------------------------------------------------------------------------------------
        public string ToJson() => JsonConvert.SerializeObject(this, ACT.Core.Serialization.JsonSerializationSettings.Settings);


        /// <summary>
        /// Loads the package.
        /// </summary>
        /// <param name="ConnectionName">Name of the connection.</param>
        /// <returns>TemplatePackage.</returns>
        public TemplatePackage LoadPackage(string ConnectionName)
        {
            return null;
            if (Package_Type == Enums.TemplateEngine.PackageType.DBNGT)
            {

            }
        }
    }
}
