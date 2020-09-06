// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_CodeGenerator.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.CustomAttributes;

namespace ACT.Core.Interfaces.CodeGeneration
{






    /// <summary>
    /// Defines the Actual Code Generation Class
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    public interface I_CodeGenerator : ACT.Core.Interfaces.Common.I_Plugin
    {
        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>List&lt;I_GeneratedCode&gt;.</returns>
        List<I_GeneratedCode> GenerateCode(ACT.Core.Interfaces.CodeGeneration.I_CodeGenerationSettings CodeSettings);
        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <param name="Database">The database.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>List&lt;I_GeneratedCode&gt;.</returns>
        List<I_GeneratedCode> GenerateCode(ACT.Core.Interfaces.DataAccess.I_Db Database, ACT.Core.Interfaces.CodeGeneration.I_CodeGenerationSettings CodeSettings);
        /// <summary>
        /// Generates the web form code.
        /// </summary>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>List&lt;I_GeneratedCode&gt;.</returns>
        List<I_GeneratedCode> GenerateWebFormCode(ACT.Core.Interfaces.CodeGeneration.I_CodeGenerationSettings CodeSettings);

    }
}
