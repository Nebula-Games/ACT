// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="Random_Helper.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ACT.Core.Helper.RandomHelper
{

    public static class RandomNumberCrypto
    {
        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

        public static int GenerateNumberBetween(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];

            _generator.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            // We are using Math.Max, and substracting 0.00000000001, 
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // We need to add one to the range, to allow for the rounding done with Math.Floor
            int range = maximumValue - minimumValue + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minimumValue + randomValueInRange);
        }
    }


    /// <summary>
    /// Random Operations Methods
    /// </summary>
    public static class Random_Helper
    {
        #region Private Variables
        /// <summary>
        /// The random object
        /// </summary>
        private static Random randomObject = new Random();
        #endregion

        #region Methods

        /// <summary>
        /// Generate Random String with All Alpha and Special Characters
        /// </summary>
        /// <param name="Length">The length.</param>
        /// <returns>System.String.</returns>
        public static string GetRandomString(int Length)
        {
            return GenerateRandomString(Length, ACTConstants.AllAlphaNumericWithSpecial);
        }

        /// <summary>
        /// Generate Random String with All Alpha and Special Characters
        /// </summary>
        /// <param name="Length">The length.</param>
        /// <param name="Ext">Extension</param>
        /// <returns>System.String.</returns>
        public static string GetRandomFileNameString(int Length, string Ext)
        {
            return GenerateRandomString(Length, ACTConstants.AllAlphaNumeric) + "." + Ext;
        }

        /// <summary>
        /// Generate Random String With FileSafe Parameters
        /// </summary>
        /// <param name="Length">The length.</param>
        /// <returns>System.String.</returns>
        public static string GetRandomStringFileSafe(int Length)
        {
            return GenerateRandomString(Length, ACTConstants.AllAlpha);
        }

        /// <summary>
        /// Returns a "random" alpha-numeric string of specified length and characters.
        /// </summary>
        /// <param name="length">the length of the random string</param>
        /// <param name="pickfrom">the string of characters to pick randomly from</param>
        /// <returns>The generate random string.</returns>
        public static string GenerateRandomString(int length, string pickfrom)
        {
            if (pickfrom == null) { return ""; }

            var result = string.Empty;
            var picklen = pickfrom.Length - 1;

            for (var i = 0; i < length; i++)
            {
                int _skipCount = randomObject.Next(10); for (int skp = 0; skp < _skipCount; skp++) { }

                var index = randomObject.Next(picklen);
                result = result + pickfrom.Substring(index, 1);
            }

            return result;
        }

        /// <summary>
        /// Generates the random number.
        /// </summary>
        /// <param name="Min">Determines the minimum of the parameters.</param>
        /// <param name="Max">Determines the maximum of the parameters.</param>
        /// <returns>System.Int32.</returns>
        public static int GenerateRandomNumber(int Min, int Max)
        {
            return randomObject.Next(Min, Max);
        }
        #endregion
    }
}
