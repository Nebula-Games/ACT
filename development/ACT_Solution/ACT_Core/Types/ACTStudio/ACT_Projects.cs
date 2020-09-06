// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_Projects.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************;

namespace ACT.Core.Types.ACTStudio
{
    using System;
    using System.Net;
    using System.Collections.Generic;
    using ACT.Core.Extensions;

    using Newtonsoft.Json;

    /// <summary>
    /// Class ACT_Projects.
    /// </summary>
    public class ACT_Projects
    {
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        [JsonProperty("users")]
        public List<User> Users { get; set; }
        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>ACT_Projects.</returns>
        public static ACT_Projects FromJson(string json) => JsonConvert.DeserializeObject<ACT_Projects>(json, ACTProjectConverter.Settings);
        /// <summary>
        /// Froms the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>ACT_Projects.</returns>
        public static ACT_Projects FromFile(string fileName) => FromJson(fileName.ReadAllText());

        /// <summary>
        /// Saves the specified path.
        /// </summary>
        /// <param name="Path">The path.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Save(string Path)
        {
            return this.ToJson().SaveAllText(Path);
        }

        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJson() => JsonConvert.SerializeObject(this, ACTProjectConverter.Settings);
    }

    /// <summary>
    /// Class User.
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>The projects.</value>
        [JsonProperty("projects")]
        public List<Project> Projects { get; set; }
    }

    /// <summary>
    /// Class Project.
    /// </summary>
    public partial class Project
    {
        /// <summary>
        /// Gets or sets the uid.
        /// </summary>
        /// <value>The uid.</value>
        [JsonProperty("uid")]
        public string Uid { get; set; }

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the outputlocation.
        /// </summary>
        /// <value>The outputlocation.</value>
        [JsonProperty("outputlocation")]
        public string Outputlocation { get; set; }

        /// <summary>
        /// Gets or sets the basefoldername.
        /// </summary>
        /// <value>The basefoldername.</value>
        [JsonProperty("basefoldername")]
        public string Basefoldername { get; set; }

        /// <summary>
        /// Gets or sets the settingsfilelocation.
        /// </summary>
        /// <value>The settingsfilelocation.</value>
        [JsonProperty("settingsfilelocation")]
        public string Settingsfilelocation { get; set; }

        /// <summary>
        /// Gets or sets the userfoldername.
        /// </summary>
        /// <value>The userfoldername.</value>
        [JsonProperty("userfoldername")]
        public string Userfoldername { get; set; }

        /// <summary>
        /// Gets or sets the connectionname.
        /// </summary>
        /// <value>The connectionname.</value>
        [JsonProperty("connectionname")]
        public string Connectionname { get; set; }

        /// <summary>
        /// Gets or sets the history location.
        /// </summary>
        /// <value>The history location.</value>
        [JsonProperty("historylocation")]
        public string HistoryLocation { get; set; }

        /// <summary>
        /// Gets or sets the history.
        /// </summary>
        /// <value>The history.</value>
        [JsonProperty("history")]
        public List<History> History { get; set; }

        /// <summary>
        /// Gets or sets the Visual Studio Project location.
        /// </summary>
        /// <value>The Visual Studio Project location.</value>
        [JsonProperty("ProjectLocation")]
        public string ProjectLocation { get; set; }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        [JsonProperty("options")]
        public Options Options { get; set; }
    }

    /// <summary>
    /// Class History.
    /// </summary>
    public partial class History
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        [JsonProperty("date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the code filename.
        /// </summary>
        /// <value>The code filename.</value>
        [JsonProperty("code-filename")]
        public string CodeFilename { get; set; }

        /// <summary>
        /// Gets or sets the database snapshot.
        /// </summary>
        /// <value>The database snapshot.</value>
        [JsonProperty("database-snapshot")]
        public string DatabaseSnapshot { get; set; }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        [JsonProperty("options")]
        public Options Options { get; set; }
    }

    /// <summary>
    /// Class Options.
    /// </summary>
    public partial class Options
    {
        /// <summary>
        /// Gets or sets a value indicating whether [generate stored procs].
        /// </summary>
        /// <value><c>true</c> if [generate stored procs]; otherwise, <c>false</c>.</value>
        [JsonProperty("generate-stored-procs")]
        public bool GenerateStoredProcs { get; set; }
                
        /// <summary>
        /// Gets or sets a value indicating whether [generate user class].
        /// </summary>
        /// <value><c>true</c> if [generate user class]; otherwise, <c>false</c>.</value>
        [JsonProperty("generate-user-class")]
        public bool GenerateUserClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [generate base class].
        /// </summary>
        /// <value><c>true</c> if [generate base class]; otherwise, <c>false</c>.</value>
        [JsonProperty("generate-base-class")]
        public bool GenerateBaseClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [generate project file].
        /// </summary>
        /// <value><c>true</c> if [generate project file]; otherwise, <c>false</c>.</value>
        [JsonProperty("generate-project-file")]
        public bool GenerateProjectFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [generate view access].
        /// </summary>
        /// <value><c>true</c> if [generate view access]; otherwise, <c>false</c>.</value>
        [JsonProperty("generate-view-access")]
        public bool GenerateViewAccess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [generate web classes].
        /// </summary>
        /// <value><c>true</c> if [generate web classes]; otherwise, <c>false</c>.</value>
        [JsonProperty("generate-web-classes")]
        public bool GenerateWebClasses { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [generate update project].
        /// </summary>
        /// <value><c>true</c> if [generate update project]; otherwise, <c>false</c>.</value>
        [JsonProperty("generate-update-project")]
        public bool GenerateUpdateProject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [generate compile code].
        /// </summary>
        /// <value><c>true</c> if [generate compile code]; otherwise, <c>false</c>.</value>
        [JsonProperty("generate-compile-code")]
        public bool GenerateCompileCode { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [generate proc namespace].
        /// </summary>
        /// <value><c>true</c> if [generate proc namespace]; otherwise, <c>false</c>.</value>
        [JsonProperty("generate-proc-namespace")]
        public bool GenerateProcNamespace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [generate Namespace Delimiter].
        /// </summary>
        /// <value><c>true</c> if [generate Namespace Delimiter]; otherwise, <c>false</c>.</value>
        [JsonProperty("generate-proc-namespace-delimiter")]
        public string GenerateProcNamespaceDelimiter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [generate Namespace Delimiter].
        /// </summary>
        /// <value><c>true</c> if [generate Namespace Delimiter]; otherwise, <c>false</c>.</value>
        [JsonProperty("generate-edited-since")]
        public string GenerateEditedSince { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [generate Database Engine].
        /// </summary>
        /// <value><c>true</c> if [generate Namespace Delimiter]; otherwise, <c>false</c>.</value>
        [JsonProperty("generate-daatabase-engine")]
        public string GenerateDatabaseEngine { get; set; }
    }

    /// <summary>
    /// Generated Item Class
    /// </summary>
    public class GeneratedItem
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is table.
        /// </summary>
        /// <value><c>true</c> if this instance is table; otherwise, <c>false</c>.</value>
        public bool IsTable { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is proc.
        /// </summary>
        /// <value><c>true</c> if this instance is proc; otherwise, <c>false</c>.</value>
        public bool IsProc { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is scalar function.
        /// </summary>
        /// <value><c>true</c> if this instance is scalar function; otherwise, <c>false</c>.</value>
        public bool IsScalarFunction { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is table function.
        /// </summary>
        /// <value><c>true</c> if this instance is table function; otherwise, <c>false</c>.</value>
        public bool IsTableFunction { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
    }

    /// <summary>
    /// Class ACTProjectConverter.
    /// </summary>
    public class ACTProjectConverter
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
