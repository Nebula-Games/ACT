// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="RomanNumeral.cs" company="Stonegate Intel LLC">
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

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class ACTRomanNumeralExtensions.
    /// </summary>
    public static class ACTRomanNumeralExtensions
    {
        /// <summary>
        /// The number of roman numeral maps
        /// </summary>
        public static int NumberOfRomanNumeralMaps = 13;

        /// <summary>
        /// The roman numerals
        /// </summary>
        public static readonly Dictionary<string, int> romanNumerals =
            new Dictionary<string, int>(NumberOfRomanNumeralMaps)
            {
                { "M", 1000 }, 
                { "CM", 900 }, 
                { "D", 500 }, 
                { "CD", 400 }, 
                { "C", 100 }, 
                { "XC", 90 }, 
                { "L", 50 }, 
                { "XL", 40 }, 
                { "X", 10 }, 
                { "IX", 9 }, 
                { "V", 5 }, 
                { "IV", 4 }, 
                { "I", 1 }
            };

        /// <summary>
        /// The valid roman numeral
        /// </summary>
        public static readonly Regex validRomanNumeral = new Regex(
            "^(?i:(?=[MDCLXVI])((M{0,3})((C[DM])|(D?C{0,3}))"
            + "?((X[LC])|(L?XX{0,2})|L)?((I[VX])|(V?(II{0,2}))|V)?))$",
            RegexOptions.Compiled);

        /// <summary>
        /// Determines whether [is valid roman numeral] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is valid roman numeral] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsValidRomanNumeral(this string value)
        {
            return validRomanNumeral.IsMatch(value);
        }

        /// <summary>
        /// Parses the roman numeral.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentNullException">value</exception>
        /// <exception cref="ArgumentException">Empty or invalid Roman numeral string. - value</exception>
        public static int ParseRomanNumeral(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            value = value.ToUpperInvariant().Trim();

            var length = value.Length;

            if ((length == 0) || !value.IsValidRomanNumeral())
            {
                throw new ArgumentException("Empty or invalid Roman numeral string.", "value");
            }

            var total = 0;
            var i = length;

            while (i > 0)
            {
                var digit = romanNumerals[value[--i].ToString()];

                if (i > 0)
                {
                    var previousDigit = romanNumerals[value[i - 1].ToString()];

                    if (previousDigit < digit)
                    {
                        digit -= previousDigit;
                        i--;
                    }
                }

                total += digit;
            }

            return total;
        }

        /// <summary>
        /// Converts to romannumeral.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentOutOfRangeException">value - Argument out of Roman numeral range.</exception>
        public static string ToRomanNumeral(this int x)
        {
            const int MinValue = 1;
            const int MaxValue = 3999;

            if ((x < MinValue) || (x > MaxValue))
            {
                throw new ArgumentOutOfRangeException("value", x, "Argument out of Roman numeral range.");
            }

            const int MaxRomanNumeralLength = 15;
            var sb = new StringBuilder(MaxRomanNumeralLength);

            foreach (var pair in romanNumerals)
            {
                while (x / pair.Value > 0)
                {
                    sb.Append(pair.Key);
                    x -= pair.Value;
                }
            }

            return sb.ToString();
        }
    }
}
