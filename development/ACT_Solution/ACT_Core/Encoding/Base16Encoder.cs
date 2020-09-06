// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Base16Encoder.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACT.Core.Encoding
{
    /// <summary>
    /// Base16 Encoding Class
    /// </summary>
    public static class Base16Encoder
    {

        /// <summary>
        /// The hexadecimal
        /// </summary>
        public static char[] HEX = new char[]{
        '0', '1', '2', '3', '4', '5', '6', '7',
        '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        /**
         * Convert bytes to a base16 string.
         */
        /// <summary>
        /// Encodes the specified byte array.
        /// </summary>
        /// <param name="byteArray">The byte array.</param>
        /// <returns>String.</returns>
        public static String encode(byte[] byteArray)
        {
            StringBuilder hexBuffer = new StringBuilder(byteArray.Length * 2);
            for (int i = 0; i < byteArray.Length; i++)
                for (int j = 1; j >= 0; j--)
                    hexBuffer.Append(HEX[(byteArray[i] >> (j * 4)) & 0xF]);
            return hexBuffer.ToString();
        }

        /**
         * Convert a base16 string into a byte array.
         */
        /// <summary>
        /// Decodes the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] decode(String s)
        {
            int len = s.Length;
            byte[] r = new byte[len / 2];
            for (int i = 0; i < r.Length; i++)
            {
                int digit1 = s[i * 2], digit2 = s[i * 2 + 1];
                if (digit1 >= '0' && digit1 <= '9')
                    digit1 -= '0';
                else if (digit1 >= 'A' && digit1 <= 'F')
                    digit1 -= 'A' - 10;
                if (digit2 >= '0' && digit2 <= '9')
                    digit2 -= '0';
                else if (digit2 >= 'A' && digit2 <= 'F')
                    digit2 -= 'A' - 10;

                r[i] = (byte)((digit1 << 4) + digit2);
            }
            return r;
        }
    }
}
