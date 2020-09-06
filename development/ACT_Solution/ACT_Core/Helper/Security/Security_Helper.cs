// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="Security_Helper.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace ACT.Core.Helper.Security
{
    /// <summary>
    /// Security Helper
    /// </summary>
    public static class Security_Helper
    {
        /// <summary>
        /// TODO Write Code
        /// </summary>
        /// <param name="PasswordRequirements">The password requirements.</param>
        /// <returns>System.String.</returns>
        public static string GeneratePassword(Enums.Security.SecurityDifficulty PasswordRequirements)
        {
            return "NOT IMPLEMENTED";
        }

        /// <summary>
        /// Private Method
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="hasher">The hasher.</param>
        /// <returns>System.String.</returns>
        public static string GetHash(Stream s, HashAlgorithm hasher)
        {
            var hash = hasher.ComputeHash(s);
            var hashStr = Convert.ToBase64String(hash);
            return hashStr;
        }
    }
}
