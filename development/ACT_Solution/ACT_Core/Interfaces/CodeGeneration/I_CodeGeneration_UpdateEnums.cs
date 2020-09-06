// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_CodeGeneration_UpdateEnums.cs" company="Nebula Entertainment LLC">
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
    /// Interface I_CodeGeneration_UpdateEnums
    /// </summary>
    public interface I_CodeGeneration_UpdateEnums
    {
        /// <summary>
        /// Locates the enums.
        /// </summary>
        /// <param name="asm">The asm.</param>
        /// <returns>System.String[].</returns>
        string[] LocateEnums(System.Reflection.Assembly asm);
    }
}
