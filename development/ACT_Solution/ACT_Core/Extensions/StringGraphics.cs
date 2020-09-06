// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="StringGraphics.cs" company="Nebula Entertainment LLC">
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
    /// String Graphic Functions
    /// </summary>
    public static class StringGraphics
    {
        /// <summary>
        /// CSV string to Rectangle (4 Parts) i.e. 4,4,4,4
        /// </summary>
        /// <param name="x">string</param>
        /// <returns>System.Drawing.Rectangle.</returns>
        public static System.Drawing.Rectangle ToRectangle(this string x)
        {
            string[] _Parts = x.SplitString(",", StringSplitOptions.RemoveEmptyEntries);

            if (_Parts.Length != 4)
            {
                return System.Drawing.Rectangle.Empty;
            }

            try
            {
                return new System.Drawing.Rectangle(_Parts[0].ToInt(), _Parts[1].ToInt(), _Parts[2].ToInt(), _Parts[3].ToInt());
            }
            catch
            {
                return System.Drawing.Rectangle.Empty;
            }
        }
    }
}
