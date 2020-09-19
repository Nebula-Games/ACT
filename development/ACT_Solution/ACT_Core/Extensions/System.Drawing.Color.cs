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
using System;


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

            try { _tmpReturn = Color.FromName(x); }
            catch { _tmpReturn = Color.Empty; }

            if (_tmpReturn != Color.Empty) { return _tmpReturn; }

            try { _tmpReturn = Color.FromArgb(x.ToInt(0)); }
            catch { _tmpReturn = Color.Empty; }

            if (_tmpReturn != Color.Empty) { return _tmpReturn; }

            return _tmpReturn;
        }

        public static ConsoleColor ToConsoleColor(this Color cl)
        {
            byte r;
            byte g;
            byte b;
            r = cl.R; g = cl.G; b = cl.B;

            ConsoleColor ret = 0;
            double rr = r, gg = g, bb = b, delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var n = Enum.GetName(typeof(ConsoleColor), cc);
                var c = System.Drawing.Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
                var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
                if (t == 0.0)
                    return cc;
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }
    }

    public static class AdditionalColorExtensions
    {
        static Random randomizer = new Random();
        public static Color GetContrast(this Color source, bool preserveOpacity)
        {
            Color inputColor = source;
            //if RGB values are close to each other by a diff less than 10%, then if RGB values are lighter side, decrease the blue by 50% (eventually it will increase in conversion below), if RBB values are on darker side, decrease yellow by about 50% (it will increase in conversion)
            byte avgColorValue = (byte)((source.R + source.G + source.B) / 3);
            int diff_r = Math.Abs(source.R - avgColorValue);
            int diff_g = Math.Abs(source.G - avgColorValue);
            int diff_b = Math.Abs(source.B - avgColorValue);
            if (diff_r < 20 && diff_g < 20 && diff_b < 20) //The color is a shade of gray
            {
                if (avgColorValue < 123) //color is dark
                {
                    inputColor = Color.FromArgb(50, 230, 220);
                }
                else
                {
                    inputColor = Color.FromArgb(255, 255, 50);
                }
            }
            byte sourceAlphaValue = source.A;
            if (!preserveOpacity)
            {
                sourceAlphaValue = Math.Max(source.A, (byte)127); //We don't want contrast color to be more than 50% transparent ever.
            }
            RGB rgb = new RGB { R = inputColor.R, G = inputColor.G, B = inputColor.B };
            HSB hsb = ConvertToHSB(rgb);
            hsb.H = hsb.H < 180 ? hsb.H + 180 : hsb.H - 180;
            //hsb.B = isColorDark ? 240 : 50; //Added to create dark on light, and light on dark
            rgb = ConvertToRGB(hsb);
            return Color.FromArgb(sourceAlphaValue, (byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
        }
        internal static RGB ConvertToRGB(HSB hsb)
        {
            // By: <a href="http://blogs.msdn.com/b/codefx/archive/2012/02/09/create-a-color-picker-for-windows-phone.aspx" title="MSDN" target="_blank">Yi-Lun Luo</a>
            double chroma = hsb.S * hsb.B;
            double hue2 = hsb.H / 60;
            double x = chroma * (1 - Math.Abs(hue2 % 2 - 1));
            double r1 = 0d;
            double g1 = 0d;
            double b1 = 0d;
            if (hue2 >= 0 && hue2 < 1)
            {
                r1 = chroma;
                g1 = x;
            }
            else if (hue2 >= 1 && hue2 < 2)
            {
                r1 = x;
                g1 = chroma;
            }
            else if (hue2 >= 2 && hue2 < 3)
            {
                g1 = chroma;
                b1 = x;
            }
            else if (hue2 >= 3 && hue2 < 4)
            {
                g1 = x;
                b1 = chroma;
            }
            else if (hue2 >= 4 && hue2 < 5)
            {
                r1 = x;
                b1 = chroma;
            }
            else if (hue2 >= 5 && hue2 <= 6)
            {
                r1 = chroma;
                b1 = x;
            }
            double m = hsb.B - chroma;
            return new RGB()
            {
                R = r1 + m,
                G = g1 + m,
                B = b1 + m
            };
        }
        internal static HSB ConvertToHSB(RGB rgb)
        {
            // By: <a href="http://blogs.msdn.com/b/codefx/archive/2012/02/09/create-a-color-picker-for-windows-phone.aspx" title="MSDN" target="_blank">Yi-Lun Luo</a>
            double r = rgb.R;
            double g = rgb.G;
            double b = rgb.B;

            double max = Max(r, g, b);
            double min = Min(r, g, b);
            double chroma = max - min;
            double hue2 = 0d;
            if (chroma != 0)
            {
                if (max == r)
                {
                    hue2 = (g - b) / chroma;
                }
                else if (max == g)
                {
                    hue2 = (b - r) / chroma + 2;
                }
                else
                {
                    hue2 = (r - g) / chroma + 4;
                }
            }
            double hue = hue2 * 60;
            if (hue < 0)
            {
                hue += 360;
            }
            double brightness = max;
            double saturation = 0;
            if (chroma != 0)
            {
                saturation = chroma / brightness;
            }
            return new HSB()
            {
                H = hue,
                S = saturation,
                B = brightness
            };
        }
        private static double Max(double d1, double d2, double d3)
        {
            if (d1 > d2)
            {
                return Math.Max(d1, d3);
            }
            return Math.Max(d2, d3);
        }
        private static double Min(double d1, double d2, double d3)
        {
            if (d1 < d2)
            {
                return Math.Min(d1, d3);
            }
            return Math.Min(d2, d3);
        }
        internal struct RGB
        {
            internal double R;
            internal double G;
            internal double B;
        }
        internal struct HSB
        {
            internal double H;
            internal double S;
            internal double B;
        }
    }
}
