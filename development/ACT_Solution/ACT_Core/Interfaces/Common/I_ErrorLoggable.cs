// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ErrorLoggable.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Enums;

namespace ACT.Core.Interfaces.Common
{

    /// <summary>
    /// Defines a method for logging errors
    /// </summary>
    public interface I_ErrorLoggable
    {
        /// <summary>
        /// Logs the Error
        /// </summary>
        /// <param name="className">Name of the class the error occured</param>
        /// <param name="summary">Summary of the Error</param>
        /// <param name="ex">Exception</param>
        /// <param name="additionInformation">Additional Information</param>
        /// <param name="errorType">Error Type</param>
        void LogError(string className, string summary, Exception ex, string additionInformation, ErrorLevel errorType);
    }

}
