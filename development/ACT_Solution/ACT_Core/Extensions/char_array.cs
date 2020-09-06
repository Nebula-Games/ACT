// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-09-2019
// ***********************************************************************
// <copyright file="char_array.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Txt = System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Char Array Extension Methods
    /// </summary>
    public static class Char_Array_Extension_Methods
    {
        /// <summary>
        /// Get Bytes from the Char[]
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] GetBytes(this char[] x)
        {
            return x.Select(c => (byte)c).ToArray();
            //return Txt.Encoding.Unicode.GetBytes(x);
        }                
    }
}
