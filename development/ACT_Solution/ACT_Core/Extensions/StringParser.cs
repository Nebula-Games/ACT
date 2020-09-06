using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class StringParserExtensions.
    /// </summary>
    public static class StringParserExtensions
    {

        /// <summary>
        /// Parse Out the String
        /// </summary>
        /// <param name="DelimitedString">The delimited string.</param>
        /// <param name="ElementPosition">The element position.</param>
        /// <param name="Delimiter">The delimiter.</param>
        /// <param name="Trim">if set to <c>true</c> [trim].</param>
        /// <param name="ErrorString">The error string.</param>
        /// <returns>System.String.</returns>
        public static string ParseOutString(this string DelimitedString, int ElementPosition, string Delimiter = ",", bool Trim = true, string ErrorString = "")
        {           

            if (Delimiter.NullOrEmpty())
            {
                // TODO LOG ERROR IF VERBOSE
                return ErrorString;
            }

            var _ParsedString = DelimitedString.SplitString(Delimiter, StringSplitOptions.RemoveEmptyEntries);

            if (_ParsedString.Length > ElementPosition)
            {
                if (Trim)
                {
                    return _ParsedString[ElementPosition].Trim();
                }
                else
                {
                    return _ParsedString[ElementPosition].Trim();
                }
            }
            else
            {
                // TODO LOG ERROR IF VERBOSE
                return ErrorString;
            }
        }
    }
}
