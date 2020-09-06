// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Short.cs" company="Stonegate Intel LLC">
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
    /// Class Short.
    /// </summary>
    public static class Short
    {
        /// <summary>
        /// Converts to bytearray.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] ToByteArray(this ushort x)
        {
            byte[] _TmpReturn = new byte[2];

            _TmpReturn[0] = (byte)(x >> 8);
            _TmpReturn[1] = (byte)(x & 255);

            return _TmpReturn;
        }
    }
}
