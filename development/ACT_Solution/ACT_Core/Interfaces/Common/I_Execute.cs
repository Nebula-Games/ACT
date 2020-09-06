// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Execute.cs" company="Stonegate Intel LLC">
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
    /// Interface I_SimpleExecute
    /// </summary>
    public interface I_SimpleExecute
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        string Execute();
    }

    /// <summary>
    /// Interface I_Execute
    /// </summary>
    public interface I_Execute
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns>I_TestResult.</returns>
        I_TestResult Execute();
        /// <summary>
        /// Gets or sets a value indicating whether [needs execute].
        /// </summary>
        /// <value><c>true</c> if [needs execute]; otherwise, <c>false</c>.</value>
        bool NeedsExecute { get; set; }
    }

    /// <summary>
    /// Interface I_ExecuteWithParameters
    /// </summary>
    public interface I_ExecuteWithParameters
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }
        /// <summary>
        /// Executes the specified parameters.
        /// </summary>
        /// <param name="Params">The parameters.</param>
        /// <returns>I_TestResult.</returns>
        I_TestResult Execute(object[] Params);
        /// <summary>
        /// Gets or sets a value indicating whether [needs execute].
        /// </summary>
        /// <value><c>true</c> if [needs execute]; otherwise, <c>false</c>.</value>
        bool NeedsExecute { get; set; }
    }
}
