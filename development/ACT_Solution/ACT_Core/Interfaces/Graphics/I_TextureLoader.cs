// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_TextureLoader.cs" company="Stonegate Intel LLC">
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
    /// Interface I_TextureManager
    /// </summary>
    public interface I_TextureManager
    {
        /// <summary>
        /// Loads the image.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="imageType">Type of the image.</param>
        /// <param name="Save">if set to <c>true</c> [save].</param>
        /// <param name="RefName">Name of the reference.</param>
        /// <returns>System.Object.</returns>
        object LoadImage(string filename, ImageType imageType, bool Save = false, string RefName = "");
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.Object.</returns>
        object GetImage(string name);
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rect">The rect.</param>
        /// <returns>System.Object.</returns>
        object GetImage(string name, Rectangle rect);
        /// <summary>
        /// Sets the device.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="MainDevice">The main device.</param>
        void SetDevice<T>(T MainDevice);
    }
}
