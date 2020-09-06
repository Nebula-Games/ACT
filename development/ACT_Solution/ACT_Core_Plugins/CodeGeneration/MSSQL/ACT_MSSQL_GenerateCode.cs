using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACT.Core.Extensions;
using ACT.Core.Interfaces.CodeGeneration;
using ACT.Core.Interfaces.DataAccess;

namespace ACT.Plugins.CodeGeneration.MSSQL
{
    /// <summary>
    /// Generates MSSQL Code including CRUD Operatons, Relational Lookups, and Anything Else Defined
    /// </summary>
    public partial class ACT_MSSQL_GenerateCode : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.CodeGeneration.I_CodeGenerator         
    {
        /// <summary>
        /// Constructor for ACT_MSSQL_GenerateCode
        /// </summary>
        public ACT_MSSQL_GenerateCode()
        {
        }

        public List<I_GeneratedCode> GenerateCode(I_CodeGenerationSettings CodeSettings)
        {
            throw new NotImplementedException();
        }

        public List<I_GeneratedCode> GenerateCode(I_Db Database, I_CodeGenerationSettings CodeSettings)
        {
            throw new NotImplementedException();
        }

        public List<I_GeneratedCode> GenerateWebFormCode(I_CodeGenerationSettings CodeSettings)
        {
            throw new NotImplementedException();
        }
    }
}