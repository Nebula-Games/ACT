// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="API_Definition.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ACT.Core.Web.API
{
    /// <summary>
    /// Represents the definition for a ACT API using Generic Handlers or ACT Handlers
    /// </summary>
    public partial class API_Definition
    {
        /// <summary>
        /// APIName
        /// </summary>
        /// <value>The apiname.</value>
        [JsonProperty("apiname")]
        public string Apiname { get; set; }

        /// <summary>
        /// Encryption Methods Used
        /// </summary>
        /// <value>The encryption.</value>
        [JsonProperty("encryption")]
        public List<string> Encryption { get; set; }

        /// <summary>
        /// List of available Actions
        /// </summary>
        /// <value>The actions.</value>
        [JsonProperty("actions")]
        public List<Action> Actions { get; set; }

        /// <summary>
        /// Read From JSON String To API_Definition
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>API_Definition</returns>
        public static API_Definition FromJson(string json) => JsonConvert.DeserializeObject<API_Definition>(json, APIDefinitionJSONSettings);

        /// <summary>
        /// Convers the object to JSON
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJson() => JsonConvert.SerializeObject(this, APIDefinitionJSONSettings);

        /// <summary>
        /// APIDefinition JSON Converter Settings
        /// </summary>
        public static readonly JsonSerializerSettings APIDefinitionJSONSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    /// <summary>
    /// Represents a specific action supported by the API
    /// </summary>
    public partial class Action
    {
        /// <summary>
        /// Name of the Action
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// List of Parameters
        /// </summary>
        /// <value>The parameters.</value>
        [JsonProperty("parameters")]
        public List<Parameter> Parameters { get; set; }

        /// <summary>
        /// Help Text
        /// </summary>
        /// <value>The help text.</value>
        [JsonProperty("helptext")]
        public string HelpText { get; set; }


    }

    /// <summary>
    /// Represents a Parameter in the action
    /// </summary>
    public partial class Parameter
    {
        /// <summary>
        /// Get Proper Value Type
        /// </summary>
        /// <value>The type of the get proper value.</value>
        public Type GetProperValueType
        {
            get
            {
                if (ValueType.ToLower() == "email") { return typeof(String); }
                if (ValueType.ToLower() == "int") { return typeof(Int32); }
                if (ValueType.ToLower() == "money") { return typeof(Decimal); }
                if (ValueType.ToLower() == "float") { return typeof(float); }
                if (ValueType.ToLower() == "datetime") { return typeof(DateTime); }

                if (ValueType.ToLower().StartsWith("enum"))
                {
                    var _ACTAssembly = System.Reflection.Assembly.Load("ACT_CORE_DLL.DLL");
                    foreach (var enumtype in _ACTAssembly.DefinedTypes.Where(t => t.IsEnum && t.IsPublic))
                    {
                        if (enumtype.Name.ToLower() == ValueType.ToLower().Replace("enum:", "")) { return enumtype.AsType(); }
                    }
                    _ACTAssembly = null;
                }

                return null;
            }
        }

        /// <summary>
        /// Name of the Parameter
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Type of the Parameter (See ACT API Types)
        /// </summary>
        /// <value>The type of the value.</value>
        [JsonProperty("type")]
        public string ValueType { get; set; }

        /// <summary>
        /// Required or Not Required
        /// </summary>
        /// <value><c>true</c> if [parameter required]; otherwise, <c>false</c>.</value>
        [JsonProperty("required")]
        public bool ParameterRequired { get; set; }

        /// <summary>
        /// Encryption Method
        /// </summary>
        /// <value>The encryption.</value>
        [JsonProperty("encryption", NullValueHandling = NullValueHandling.Ignore)]
        public string Encryption { get; set; }
    }




}
