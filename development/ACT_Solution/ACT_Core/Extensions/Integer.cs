// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Integer.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class Integer.
    /// </summary>
    public static class Integer
    {
        /// <summary>
        /// Bit Positions (Factors of 2)
        /// </summary>
        public static int[] BitPositions = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024,
                                            2048, 4096, 8192, 16384, 32768, 65536, 131072,
		                                    262144, 524288, 1048576, 2097152, 4194304, 8388608,
                                           16777216, 33554432,67108864,134217728,268435456,
                                           536870912, 1073741824};

        /// <summary>
        /// An int extension method that query if 'x' is single bit.
        /// </summary>
        /// <param name="x">The x to act on.</param>
        /// <returns>true if single bit, false if not.</returns>
        /// <remarks>Mark Alicz, 7/10/2016.</remarks>

        public static bool IsSingleBit(this int x)
        {
            if (BitPositions.Contains(x)) { return true; }
            else { return false; }
        }

        /// <summary>
        /// An int extension method that gets the previous power of two.
        /// </summary>
        /// <param name="x">The x to act on.</param>
        /// <returns>The previous power of two.</returns>
        /// <remarks>Mark Alicz, 7/10/2016.</remarks>

        public static int GetPreviousPowerOfTwo(this int x)
        {
            var _Possible = BitPositions.Where(xx => xx < x);
            return _Possible.Max();
        }

        /// <summary>
        /// An int extension method that gets the next power of two.
        /// </summary>
        /// <param name="x">The x to act on.</param>
        /// <returns>The next power of two.</returns>
        /// <remarks>Mark Alicz, 7/10/2016.</remarks>

        public static int GetNextPowerOfTwo(this int x)
        {
            var _Possible = BitPositions.Where(xx => xx > x);
            return _Possible.Min();
        }

        /// <summary>
        /// An int extension method that query if 'x' is power of two.
        /// </summary>
        /// <param name="x">The x to act on.</param>
        /// <returns>true if power of two, false if not.</returns>
        /// <remarks>Mark Alicz, 7/10/2016.</remarks>

        public static bool IsPowerOfTwo(this int x)
        {
            return BitPositions.Contains(x);
        }

        /// <summary>
        /// An int extension method that gets excel column name. A, B, AA, ZZ Etc.
        /// </summary>
        /// <param name="columnNumber">The columnNumber to act on.</param>
        /// <returns>The excel column name.</returns>
        /// <remarks>Mark Alicz, 7/10/2016.</remarks>

        public static string GetExcelColumnName(this int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        /// <summary>
        /// To File Size String
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>System.String.</returns>
        public static string ToFileSize(this int source)
        {
            return ToFileSize(Convert.ToInt64(source));
        }

        /// <summary>
        /// To File Size String
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>System.String.</returns>
        public static string ToFileSize(this long source)
        {
            const int byteConversion = 1024;
            double bytes = Convert.ToDouble(source);

            if (bytes >= Math.Pow(byteConversion, 3)) //GB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 3), 2), " GB");
            }
            else if (bytes >= Math.Pow(byteConversion, 2)) //MB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 2), 2), " MB");
            }
            else if (bytes >= byteConversion) //KB Range
            {
                return string.Concat(Math.Round(bytes / byteConversion, 2), " KB");
            }
            else //Bytes
            {
                return string.Concat(bytes, " Bytes");
            }
        }
    }
}
