// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Enums.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Enums.Code
{
    /// <summary>
    /// Enum CodeSignature_ItemType
    /// </summary>
    public enum CodeSignature_ItemType
    {
        /// <summary>
        /// The solution
        /// </summary>
        Solution = 1,
        /// <summary>
        /// The project
        /// </summary>
        Project = 2,
        /// <summary>
        /// The file
        /// </summary>
        File = 3,
        /// <summary>
        /// The class
        /// </summary>
        Class = 4,
        /// <summary>
        /// The method
        /// </summary>
        Method = 5,
        /// <summary>
        /// The property
        /// </summary>
        Property = 6,
        /// <summary>
        /// The enum
        /// </summary>
        Enum = 7,
        /// <summary>
        /// The interface
        /// </summary>
        Interface = 8
    }

    /// <summary>
    /// Enum Language
    /// </summary>
    [Flags()]
    /// <summary>
    /// The c sharp
    /// </summary>
    public enum Language { CSharp, VisualBasic, CPlusPlus, C, Perl, Javascript, Ruby, Python, Custom }

}
