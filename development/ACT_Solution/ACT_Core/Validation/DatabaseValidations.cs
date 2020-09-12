// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="DatabaseValidations.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ACT.Core.Validation
{
    /// <summary>
    /// Class DatabaseValidations.
    /// </summary>
    public static class DatabaseValidations
    {
        /// <summary>
        /// Validates the connection string.
        /// </summary>
        /// <param name="ConnectionString">The connection string.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ValidateConnectionString(string ConnectionString)
        {
            return Regex.IsMatch(ConnectionString, @"^([^=;]+=[^=;]*)(;[^=;]+=[^=;]*)*;?$");
        }

    }
}
