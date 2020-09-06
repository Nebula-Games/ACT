// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ImagingHelper.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;

namespace ACT.Core.Interfaces.Imaging
{
    /// <summary>
    /// Interface IImagingHelper
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    public interface IImagingHelper : ACT.Core.Interfaces.Common.I_Plugin
    {
        /// <summary>
        /// Gets the encoders.
        /// </summary>
        /// <value>The encoders.</value>
        Dictionary<string, ImageCodecInfo> Encoders { get;  }
        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="Img">The img.</param>
        /// <param name="Width">The width.</param>
        /// <param name="Height">The height.</param>
        /// <returns>Bitmap.</returns>
        Bitmap ResizeImage(Image Img, int Width, int Height);
        /// <summary>
        /// Saves the JPG.
        /// </summary>
        /// <param name="Path">The path.</param>
        /// <param name="Image">The image.</param>
        /// <param name="Quality">The quality.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool SaveJPG(string Path, Image Image, int Quality);
        /// <summary>
        /// Gets the encoder information.
        /// </summary>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <returns>ImageCodecInfo.</returns>
        ImageCodecInfo GetEncoderInfo(string mimeType);
    }

 
}
