using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Plugins.CodeGeneration
{

    /// <summary>
    /// ACT Default Class Implementation
    /// </summary>
    public class ACT_MSSQL_CodeGenerationSettings : ACT.Core.Interfaces.CodeGeneration.I_MSSQL_CodeGenerationSettings
    {
        /// <summary>
        /// Database Connection String
        /// </summary>
        public string DatabaseConnectionString { get; set; }

        /// <summary>
        /// Database Connection Name
        /// </summary>
        public string DatabaseConnectionName { get; set; }

        /// <summary>
        /// Settings File Location
        /// </summary>
        public string SettingsFileLocation { get; set; }

        /// <summary>
        /// Generate Basic ACTPROCS
        /// </summary>
        public bool Generate_Basic_ACTPROCS { get; set; }

        /// <summary>
        /// Execute After Generation
        /// </summary>
        public bool Execute_After_Generation { get; set; }
    }
}
