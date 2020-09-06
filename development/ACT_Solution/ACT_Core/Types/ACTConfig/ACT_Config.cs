// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_Config.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Types.ACTConfig
{

    /// <summary>
    /// Class ACTConfiguration.
    /// </summary>
    public class ACTConfiguration
    {
        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        public string project_name { get; set; }
        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>The last updated.</value>
        public string last_updated { get; set; }
        /// <summary>
        /// Gets or sets the file version.
        /// </summary>
        /// <value>The file version.</value>
        public string file_version { get; set; }
        /// <summary>
        /// Gets or sets the installation directory.
        /// </summary>
        /// <value>The installation directory.</value>
        public string installation_directory { get; set; }
        /// <summary>
        /// Gets or sets the configuration settings.
        /// </summary>
        /// <value>The configuration settings.</value>
        public Configuration_Settings[] configuration_settings { get; set; }
        /// <summary>
        /// Gets or sets the plugin definitions.
        /// </summary>
        /// <value>The plugin definitions.</value>
        public Plugin_Definitions[] plugin_definitions { get; set; }
        /// <summary>
        /// Gets or sets the database statements.
        /// </summary>
        /// <value>The database statements.</value>
        public Database_Statements[] database_statements { get; set; }
        /// <summary>
        /// Gets or sets the codegeneration templates.
        /// </summary>
        /// <value>The codegeneration templates.</value>
        public Codegeneration_Templates[] codegeneration_templates { get; set; }
        /// <summary>
        /// Gets or sets the act tables.
        /// </summary>
        /// <value>The act tables.</value>
        public Act_Tables[] act_tables { get; set; }
    }

    /// <summary>
    /// Class Configuration_Settings.
    /// </summary>
    public class Configuration_Settings
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string value { get; set; }
        /// <summary>
        /// Gets or sets the encrypted.
        /// </summary>
        /// <value>The encrypted.</value>
        public string encrypted { get; set; }
    }

    /// <summary>
    /// Class Plugin_Definitions.
    /// </summary>
    public class Plugin_Definitions
    {
        /// <summary>
        /// Gets or sets the act interface.
        /// </summary>
        /// <value>The act interface.</value>
        public string act_interface { get; set; }
        /// <summary>
        /// Gets or sets the default plugin.
        /// </summary>
        /// <value>The default plugin.</value>
        public string default_plugin { get; set; }
        /// <summary>
        /// Gets or sets the plugins.
        /// </summary>
        /// <value>The plugins.</value>
        public Plugin[] plugins { get; set; }
    }

    /// <summary>
    /// Class Plugin.
    /// </summary>
    public class Plugin
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the DLL.
        /// </summary>
        /// <value>The DLL.</value>
        public string dll { get; set; }
        /// <summary>
        /// Gets or sets the full name of the class.
        /// </summary>
        /// <value>The full name of the class.</value>
        public string full_class_name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [store once].
        /// </summary>
        /// <value><c>true</c> if [store once]; otherwise, <c>false</c>.</value>
        public bool store_once { get; set; }
        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public string[] args { get; set; }
    }

    /// <summary>
    /// Class Database_Statements.
    /// </summary>
    public class Database_Statements
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        public string template { get; set; }
        /// <summary>
        /// Gets or sets the encrypted.
        /// </summary>
        /// <value>The encrypted.</value>
        public string encrypted { get; set; }
    }

    /// <summary>
    /// Class Codegeneration_Templates.
    /// </summary>
    public class Codegeneration_Templates
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        public string template { get; set; }
        /// <summary>
        /// Gets or sets the encrypted.
        /// </summary>
        /// <value>The encrypted.</value>
        public string encrypted { get; set; }
    }

    /// <summary>
    /// Class Act_Tables.
    /// </summary>
    public class Act_Tables
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        public string template { get; set; }
        /// <summary>
        /// Gets or sets the encrypted.
        /// </summary>
        /// <value>The encrypted.</value>
        public string encrypted { get; set; }
    }

}
