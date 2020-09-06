// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_TestResult_Expanded.cs" company="Stonegate Intel LLC">
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
    /// Defines a Common Test Result
    /// </summary>
    public interface I_TestResultExpanded
    {
        /// <summary>
        /// Gets or sets the exceptions.
        /// </summary>
        /// <value>The exceptions.</value>
        List<Exception> Exceptions { get; set; }
        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>The messages.</value>
        List<string> Messages { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="I_TestResultExpanded"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        bool Success { get; set; }
        /// <summary>
        /// Gets or sets the warnings.
        /// </summary>
        /// <value>The warnings.</value>
        List<string> Warnings { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has warnings.
        /// </summary>
        /// <value><c>true</c> if this instance has warnings; otherwise, <c>false</c>.</value>
        bool HasWarnings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [exit process].
        /// </summary>
        /// <value><c>true</c> if [exit process]; otherwise, <c>false</c>.</value>
        bool ExitProcess { get; set; }

        /// <summary>
        /// Gets or sets the return data.
        /// </summary>
        /// <value>The return data.</value>
        object[] ReturnData { get; set; }
    }
}
