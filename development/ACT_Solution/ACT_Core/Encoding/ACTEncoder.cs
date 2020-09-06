// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACTEncoder.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Encoding
{
    /// <summary>
    /// Base16 Encoding Class
    /// </summary>
    public static class ACTEncoder
    {
        /// <summary>
        /// RData
        /// </summary>
        public static char[] RData = new char[] {
        '~', '%', '|', '#', '@', '&', '*', '+',
        '=', '-', '!', '^', '<', '>', '?', ',' };


        public static byte[] StringToByteArray(string Data)
        {
            byte[] tempByte = new byte[Data.Length * 2];
            unsafe
            {
                fixed (void* ptr = Data)
                {
                    tempByte = System.Text.Encoding.Unicode.GetBytes(Data);
                }
            }

            return tempByte;
        }

        /// <summary>
        /// Convert bytes to a ACT string.
        /// </summary>
        /// <param name="byteArray">byte Array To Encode</param>
        /// <returns>String.</returns>
        public static String encode(byte[] byteArray)
        {
            StringBuilder hexBuffer = new StringBuilder(byteArray.Length * 2);
            for (int i = 0; i < byteArray.Length; i++)
                for (int j = 1; j >= 0; j--)
                    hexBuffer.Append(RData[(byteArray[i] >> (j * 4)) & 0xF]);
            return hexBuffer.ToString();
        }

        /// <summary>
        /// Convert a ACTEncoder string into a byte array.
        /// </summary>
        /// <param name="s">String To Decode</param>
        /// <returns>System.Byte[].</returns>
        [ACT.Core.CustomAttributes.NeedsDebugging(Comments = "Doesnt Work At All")]
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
