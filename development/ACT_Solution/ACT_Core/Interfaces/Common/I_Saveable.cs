// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Saveable.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Common
{
    /// <summary>
    /// Specifies that the class can be Saved and Deleted
    /// </summary>
    public interface I_Saveable
    {
        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>I_TestResult.</returns>
        I_TestResult Save();
        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <returns>I_TestResult.</returns>
        I_TestResult Delete();
    }

}
