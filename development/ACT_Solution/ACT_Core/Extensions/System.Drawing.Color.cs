// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.Drawing.Color.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Text.RegularExpressions;
using System.Drawing;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Holds Extension Methods For System.Drawing.Color objects.
    /// </summary>
    /// <remarks>Mark Alicz, 11/22/2016.</remarks>
    public static class SystemDrawingColorExtensions
    {
        /// <summary>
        /// A System.Drawing.Color extension method that converts a c to a hexadecimal string.
        /// </summary>
        /// <param name="c">The Color to process.</param>
        /// <returns>Hex Value Of the Color.</returns>
        /// <remarks>Mark Alicz, 11/22/2016.</remarks>
        public static string ToHexString(this System.Drawing.Color c)
        {
            return c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        /// <summary>
        /// Checks the color of the valid format HTML.
        /// </summary>
        /// <param name="inputColor">Color of the input.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CheckValidFormatHtmlColor(this string inputColor)
        {
            if (Regex.Match(inputColor, ACT.Core.Helper.RegularExpressions.ColorHex).Success) { return true; }
            return false;
        }

        /// <summary>
        /// Gets the image from hexadecimal string.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.Nullable&lt;System.Drawing.Color&gt;.</returns>
        public static Color? GetImageFromHexString(this string x)
        {
            if (x.CheckValidFormatHtmlColor() == false)
            {
                if (x.StartsWith("#") == false) { x = "#" + x; }
                if (x.CheckValidFormatHtmlColor() == false) { return null; }
            }

            return System.Drawing.ColorTranslator.FromHtml(x);
        }

        /// <summary>
        /// Ideal Text Color For a Background Color
        /// </summary>
        /// <param name="bg">Background Color To Test</param>
        /// <returns>Nice Contrasting Color</returns>
        public static Color IdealTextColor(this Color bg)
        {
            int nThreshold = 105;
            int bgDelta = System.Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) + (bg.B * 0.114));

            Color foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
            return foreColor;
        }

        /// <summary>
        /// To System Drawing Color
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Color? ToSystemDrawingColor(this string x)
        {
            Color _tmpReturn = Color.White;

            if (x.StartsWith("#"))
            {
                try { return System.Drawing.ColorTranslator.FromHtml(x); } catch { }
            }

            try { _tmpReturn = Color.FromName(x);  }
            catch { _tmpReturn = Color.Empty; }

            if (_tmpReturn != Color.Empty) { return _tmpReturn; }

            try { _tmpReturn = Color.FromArgb(x.ToInt(0)); }
            catch { _tmpReturn = Color.Empty; }

            if (_tmpReturn != Color.Empty) { return _tmpReturn; }

            return _tmpReturn;
        }
    }
}
