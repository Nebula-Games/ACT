// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.Drawing.Size.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class SystemDrawingSizeExtensions.
    /// </summary>
    public static class SystemDrawingSizeExtensions
    {
        /// <summary>
        /// Returns a Size Object From A Formatted String Object
        /// String Format Is {Width=100,Height=100}
        /// </summary>
        /// <param name="x">SizeObject</param>
        /// <param name="Value">String</param>
        /// <returns>Size.</returns>
        public static Size FromString(this Size x, string Value)
        {

            Value = Value.Replace("{Width=", "").Replace("Height=", "").Replace("}", "");
            var _DLS = Value.Split(",".ToCharArray());
            return new System.Drawing.Size(Convert.ToInt32(_DLS[0]), Convert.ToInt32(_DLS[1]));
        }
    }
}
