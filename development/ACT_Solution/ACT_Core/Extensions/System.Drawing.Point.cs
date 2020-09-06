// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.Drawing.Point.cs" company="Nebula Entertainment LLC">
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
    /// Class SystemDrawingPointExtensions.
    /// </summary>
    public static class SystemDrawingPointExtensions
    {
        /// <summary>
        /// Froms the string.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="Value">The value.</param>
        /// <returns>Point.</returns>
        public static Point FromString(this Point x, string Value)
        {
            Value = Value.Replace("{X=", "").Replace("Y=", "").Replace("}", "");
            var _DLS = Value.Split(",".ToCharArray());
            return new System.Drawing.Point(Convert.ToInt32(_DLS[0]), Convert.ToInt32(_DLS[1]));

        }
    }
}
