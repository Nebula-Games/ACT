// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-09-2019
// ***********************************************************************
// <copyright file="ULong.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// ULong Extension Methods
    /// </summary>
    public static class ULongExtensions
    {
        /// <summary>
        /// Convert to Binary String
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.String.</returns>
        public static string ToBinary(this uint x)
        {
            var _Bytes = BitConverter.GetBytes(x);
            string _Tmp = "";

            foreach (byte b in _Bytes.Reverse())
            {
                _Tmp += b.ToBinaryString();
            }

            return _Tmp;
        }

        /// <summary>
        /// Converts a <code>uint</code> value to a <code>byte[]</code>.
        /// </summary>
        /// <param name="value">The <code>uint</code> value to convert.</param>
        /// <param name="BigEndian">Logic</param>
        /// <returns>A <code>byte[]</code> representing the <code>uint</code> value.</returns>
        public static byte[] ToByteArray(this ulong value, bool BigEndian = false)
        {
            var size = 8;

            var result = new byte[size];

            if (BigEndian)
            {
                for (var i = 0; i < size; i++)
                {
                    var bitOffset = (size - (i + 1)) * 8;
                    result[i] = (byte)((value & ((ulong)0xFF << bitOffset)) >> bitOffset);
                }
            }
            else
            {
                for (var i = 0; i < size; i++)
                {
                    var bitOffset = (size - (i + 1)) * 8;
                    result[size - 1 - i] = (byte)((value & ((ulong)0xFF << bitOffset)) >> bitOffset);
                }
            }

            return result;
        }
    }
}
