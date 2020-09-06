// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="String_Methods_Helper.cs" company="Stonegate Intel LLC">
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
using System.Globalization;
using ACT.Core.Types;
using ACT.Core.Extensions;


namespace ACT.Core.Helper
{
    /// <summary>
    /// Class StringMethods.
    /// </summary>
    public static class StringMethods
    {


        #region Constants



        #endregion

        /// <summary>
        /// This method generates a string that can be used to represent a password on a web or windows form
        /// </summary>
        /// <param name="Length">Lenght of Password To Generate</param>
        /// <param name="VisChar">Visible character</param>
        /// <returns>System.String.</returns>
        public static string ToHiddenPasswordString(int Length, string VisChar = "*")
        {
            string _TmpREturn = "";
            for (int x = 0; x < Length; x++) { _TmpREturn += VisChar; }
            return _TmpREturn;
        }

        /// <summary>
        /// Domains the mapper.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns>System.String.</returns>
        public static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            var idn = new IdnMapping();

            var domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return match.Groups[1].Value + domainName;
        }




        /// <summary>
        /// Processes the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The process text.</returns>
        public static string ProcessText(string text)
        {
            return ProcessText(text, true);
        }

        /// <summary>
        /// Processes the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="nullify">The nullify.</param>
        /// <returns>The process text.</returns>
        public static string ProcessText(string text, bool nullify)
        {
            return ProcessText(text, nullify, true);
        }

        /// <summary>
        /// Processes the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="nullify">The nullify.</param>
        /// <param name="trim">The trim.</param>
        /// <returns>The process text.</returns>
        public static string ProcessText(string text, bool nullify, bool trim)
        {
            if (trim && text.IsSet())
                text = text.Trim();

            if (nullify && text.IsNotSet())
                text = null;

            return text;
        }

    }
}
