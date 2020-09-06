// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="RegularExpressions.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACT.Core.Extensions;

namespace ACT.Core.Helper
{
    /// <summary>
    /// Class RegularExpressions.
    /// </summary>
    public static class RegularExpressions
    {
        /// <summary>
        /// Hex Color String
        /// </summary>
        public static string ColorHex = @"^#(?:[0-9a-fA-F]{3}){1,2}$";
        /// <summary>
        /// Standard Email Address
        /// </summary>
        public static string StandardEmail = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        /// <summary>
        /// with or without http
        /// </summary>
        public static string ValidURL = @"(http(s)?://)?([\w-]+\.)+[\w-]+[.com]+(/[/?%&=]*)?";
        /// <summary>
        /// The password strength a
        /// </summary>
        public static string PasswordStrengthA = @"^[a-z0-9\.@#\$%&]+$";
        /// <summary>
        /// Minimum 8 characters at least 1 Alphabet and 1 Number
        /// </summary>
        public static string PasswordStrengthB = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
        /// <summary>
        /// Minimum 8 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character
        /// </summary>
        public static string PasswordStrengthC = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}";
        /// <summary>
        /// Minimum 8 and Maximum 10 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character
        /// </summary>
        public static string PasswordStrengthD = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,10}";

        /// <summary>
        /// without +91 or 0
        /// </summary>
        public static string MobilePhoneNumber = @"^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}9[0-9](\s){0,1}(\-){0,1}(\s){0,1}[1-9]{1}[0-9]{7}$";


    }
}
