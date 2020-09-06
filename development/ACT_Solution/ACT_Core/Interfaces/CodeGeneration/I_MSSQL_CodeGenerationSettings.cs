// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_MSSQL_CodeGenerationSettings.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.CustomAttributes;

namespace ACT.Core.Interfaces.CodeGeneration
{
    /// <summary>
    /// Defines the Code Generation Settings
    /// </summary>
    public interface I_MSSQL_CodeGenerationSettings
    {
        /// <summary>
        /// Gets or sets the database connection string.
        /// </summary>
        /// <value>The database connection string.</value>
        [Encrypted(true)]
        string DatabaseConnectionString { get; set; }
        /// <summary>
        /// Gets or sets the name of the database connection.
        /// </summary>
        /// <value>The name of the database connection.</value>
        string DatabaseConnectionName { get; set; }
        /// <summary>
        /// Gets or sets the settings file location.
        /// </summary>
        /// <value>The settings file location.</value>
        string SettingsFileLocation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [generate basic actprocs].
        /// </summary>
        /// <value><c>true</c> if [generate basic actprocs]; otherwise, <c>false</c>.</value>
        bool Generate_Basic_ACTPROCS { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [execute after generation].
        /// </summary>
        /// <value><c>true</c> if [execute after generation]; otherwise, <c>false</c>.</value>
        bool Execute_After_Generation { get; set; }

    }
}
