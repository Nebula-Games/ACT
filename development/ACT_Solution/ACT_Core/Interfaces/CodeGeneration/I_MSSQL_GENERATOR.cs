// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_MSSQL_GENERATOR.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.CodeGeneration
{
    /// <summary>
    /// MSSQL Generator
    /// </summary>
    public interface I_MSSQL_Generator
    {
        /// <summary>
        /// Generate Code
        /// </summary>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>List&lt;I_GeneratedCode&gt;.</returns>
        List<I_GeneratedCode> GenerateCode(ACT.Core.Interfaces.CodeGeneration.I_MSSQL_CodeGenerationSettings CodeSettings);

        /// <summary>
        /// Generate Code
        /// </summary>
        /// <param name="ConnectionName">Name of the connection.</param>
        /// <returns>System.String.</returns>
        string GenerateBasicStoredProcCode(string ConnectionName);

        /// <summary>
        /// Generate Code
        /// </summary>
        /// <param name="ConnectionName">Name of the connection.</param>
        /// <returns>System.String.</returns>
        string GenerateBasicViewsCode(string ConnectionName);

    }
}
