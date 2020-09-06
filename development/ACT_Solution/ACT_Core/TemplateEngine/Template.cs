// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Template.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary>The template represents a block of information that needs to be processed.  A template can be partial or all containing.  </summary>
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
    /// Represents a Single Template
    /// </summary>
    public class Template
    {
        /// <summary>
        /// Base File System Directory NULL if Database Is Used
        /// </summary>
        /// <value>The base directory.</value>
        [JsonProperty("basedirectory")]
        public string BaseDirectory { get; set; }

        /// <summary>
        /// Group Name.  Group name is a sub classification
        /// </summary>
        /// <value>The name of the group.</value>
        [JsonProperty("groupname")]
        public string GroupName { get; set; }

        /// <summary>
        /// Item Name without Extension (used by database and filesystem)
        /// IF File System this is the same as filename without the extension
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Template Type.  Each Template Type has a custom defined plugin capability. (See Template Engine Documentation)
        /// </summary>
        /// <value>The type of the template.</value>
        [JsonProperty("template_type")]
        public ACT.Core.Enums.TemplateEngine.TemplateType Template_Type { get; set; }

        /// <summary>
        /// Type Name - Common Type Name Used By Various Systems
        /// </summary>
        [JsonProperty("typename")]
        public string TypeName;

        /// <summary>
        /// Filename Just the name with extension only used by the filesystem
        /// </summary>
        [JsonProperty("filename")]
        public string FileName;

        /// <summary>
        /// Data inside the template All the Template Data That will be PARSED
        /// </summary>
        [JsonProperty("data")]
        public string Data;

        /// <summary>
        /// Version of the Template
        /// </summary>
        [JsonProperty("version")]
        public int Version;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the replacement indicator.
        /// This allows the override of the default replacement indicator.  By Default ACT Uses "###"
        ///    i.e. ###{TOKEN}###
        /// You could specify for this template specifically use "$$$"
        /// You can also override the default behavor in the SystemConfiguration File (See ACT Configuration - Template Engine Settings)
        /// </summary>
        ///
        /// <value> The replacement indicator. </value>
        ///-------------------------------------------------------------------------------------------------
        [JsonProperty("replacementindicator")]
        public string ReplacementIndicator { get; set; }

        /// <summary>   The template parts. </summary>
        [JsonProperty("templateparts")]
        public Dictionary<string, string> TemplateParts = new Dictionary<string, string>();
    }
}
