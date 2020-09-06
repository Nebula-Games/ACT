// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Graphics.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Enums.Graphics;
using System.Drawing;

namespace ACT.Core.Interfaces.Graphics
{

    /// <summary>
    /// Interface I_Graphics
    /// </summary>
    public interface I_Graphics
    {
        /// <summary>
        /// Initializes the graphics.
        /// </summary>
        /// <param name="Width">The width.</param>
        /// <param name="Height">The height.</param>
        void InitGraphics(int Width, int Height);
        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="ImageData">The image data.</param>
        /// <param name="ImgType">Type of the img.</param>
        void AddImage(byte[] ImageData, ImageType ImgType);

        /// <summary>
        /// Draws the image.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        void DrawImage(int x, int y);
        /// <summary>
        /// Draws the image.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void DrawImage(int x, int y, int width, int height);
        /// <summary>
        /// Draws the image.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="Effects">The effects.</param>
        void DrawImage(int x, int y, IEffect[] Effects);

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="r">The r.</param>
        /// <param name="c">The c.</param>
        /// <param name="LineWidth">Width of the line.</param>
        /// <param name="InsideBoundry">if set to <c>true</c> [inside boundry].</param>
        void DrawRectangle(int x, int y, Rectangle r, System.Drawing.Color c, int LineWidth, bool InsideBoundry = true);
        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="c">The c.</param>
        /// <param name="LineWidth">Width of the line.</param>
        /// <param name="InsideBoundry">if set to <c>true</c> [inside boundry].</param>
        void DrawLine(int x, int y, int x2, int y2, System.Drawing.Color c, int LineWidth, bool InsideBoundry = true);
    }
    /// <summary>
    /// Interface IEffect
    /// </summary>
    public interface IEffect
    {

    }
}
