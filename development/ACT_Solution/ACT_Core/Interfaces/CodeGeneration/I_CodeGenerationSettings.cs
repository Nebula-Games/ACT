// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_CodeGenerationSettings.cs" company="Stonegate Intel LLC">
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
    public interface I_CodeGenerationSettings
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
        /// Gets or sets the output language.
        /// </summary>
        /// <value>The output language.</value>
        ACT.Core.Enums.Code.Language OutputLanguage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [compile code].
        /// </summary>
        /// <value><c>true</c> if [compile code]; otherwise, <c>false</c>.</value>
        bool CompileCode { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [output solution with project].
        /// </summary>
        /// <value><c>true</c> if [output solution with project]; otherwise, <c>false</c>.</value>
        bool OutputSolutionWithProject { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [generate base layer].
        /// </summary>
        /// <value><c>true</c> if [generate base layer]; otherwise, <c>false</c>.</value>
        bool GenerateBaseLayer { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [generate user layer].
        /// </summary>
        /// <value><c>true</c> if [generate user layer]; otherwise, <c>false</c>.</value>
        bool GenerateUserLayer { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [generate stored procedures].
        /// </summary>
        /// <value><c>true</c> if [generate stored procedures]; otherwise, <c>false</c>.</value>
        bool GenerateStoredProcedures { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [generate web services].
        /// </summary>
        /// <value><c>true</c> if [generate web services]; otherwise, <c>false</c>.</value>
        bool GenerateWebServices { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [generate view access].
        /// </summary>
        /// <value><c>true</c> if [generate view access]; otherwise, <c>false</c>.</value>
        bool GenerateViewAccess { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [notify when code is generated].
        /// </summary>
        /// <value><c>true</c> if [notify when code is generated]; otherwise, <c>false</c>.</value>
        bool NotifyWhenCodeIsGenerated { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [notify when project code is updated].
        /// </summary>
        /// <value><c>true</c> if [notify when project code is updated]; otherwise, <c>false</c>.</value>
        bool NotifyWhenProjectCodeIsUpdated { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [backup project code before deployment].
        /// </summary>
        /// <value><c>true</c> if [backup project code before deployment]; otherwise, <c>false</c>.</value>
        bool BackupProjectCodeBeforeDeployment { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [namespace driven procedures].
        /// </summary>
        /// <value><c>true</c> if [namespace driven procedures]; otherwise, <c>false</c>.</value>
        bool NamespaceDrivenProcedures { get; set; }

        /// <summary>
        /// Gets or sets the namespace driven procedures delimiter.
        /// </summary>
        /// <value>The namespace driven procedures delimiter.</value>
        string NamespaceDrivenProceduresDelimiter { get; set; }

        /// <summary>
        /// Gets or sets the name of the DLL.
        /// </summary>
        /// <value>The name of the DLL.</value>
        string DLLName { get; set; }

        /// <summary>
        /// Gets or sets the name space.
        /// </summary>
        /// <value>The name space.</value>
        string NameSpace { get; set; }
        /// <summary>
        /// Gets or sets the base code name space.
        /// </summary>
        /// <value>The base code name space.</value>
        string BaseCodeNameSpace { get; set; }
        /// <summary>
        /// Gets or sets the user code name space.
        /// </summary>
        /// <value>The user code name space.</value>
        string UserCodeNameSpace { get; set; }

        /// <summary>
        /// Gets or sets the root output directory.
        /// </summary>
        /// <value>The root output directory.</value>
        string RootOutputDirectory { get; set; }
        /// <summary>
        /// Gets or sets the project update directory.
        /// </summary>
        /// <value>The project update directory.</value>
        string ProjectUpdateDirectory { get; set; }
        /// <summary>
        /// Gets or sets the base folder path.
        /// </summary>
        /// <value>The base folder path.</value>
        string BaseFolderPath { get; set; }
        /// <summary>
        /// Gets or sets the user folder path.
        /// </summary>
        /// <value>The user folder path.</value>
        string UserFolderPath { get; set; }
        /// <summary>
        /// Gets or sets the stored procedure namespace method path.
        /// </summary>
        /// <value>The stored procedure namespace method path.</value>
        string StoredProcedureNamespaceMethodPath { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        string ProjectName { get; set; }
        /// <summary>
        /// Gets or sets the name of the solution.
        /// </summary>
        /// <value>The name of the solution.</value>
        string SolutionName { get; set; }

        /// <summary>
        /// Gets or sets the object age maximum.
        /// </summary>
        /// <value>The object age maximum.</value>
        int ObjectAgeMax { get; set; }

    }
}
