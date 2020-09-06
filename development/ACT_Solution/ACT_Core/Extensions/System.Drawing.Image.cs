// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.Drawing.Image.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace ACT.Core.Extensions
{

    /*
     * Resizes an image
     **/
    /// <summary>
    /// Class ImageExtensions.
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Get the Base64 String That Represents an Image <see cref="System.Drawing.Image" />
        /// </summary>
        /// <param name="image"><see cref="System.Drawing.Image" /> To Convert</param>
        /// <param name="format"><see cref="System.Drawing.Imaging.ImageFormat" /> Image Format to Save the Base64 Image As</param>
        /// <returns>Base 64 String: <see cref="System.String" /></returns>
        public static string ToBase64(this Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /// <summary>
        /// Convert a Base64 Image String to a System.Drawing.Image
        /// </summary>
        /// <param name="ImgBase64">Base 64 Encoded Image</param>
        /// <returns>Image.</returns>
        public static Image ConvertBase64ToImage(this string ImgBase64)
        {

            //get a temp image from bytes, instead of loading from disk
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(ImgBase64);

            Image image;

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }

        /// <summary>
        /// Convert The Image To a New Format
        /// </summary>
        /// <param name="theImage">The image.</param>
        /// <param name="NewFormat">Creates new format.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] ConvertToFormat(this Image theImage, System.Drawing.Imaging.ImageFormat NewFormat)
        {
            byte[] result = null;
            using (MemoryStream stream = new MemoryStream())
            {
                theImage.Save(stream, NewFormat);
                result = stream.ToArray();
            }
            return result;
        }
        /// <summary>
        /// Images to byte array.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] imageToByteArray(this System.Drawing.Image image)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// Save The Image To Location
        /// </summary>
        /// <param name="theImage">The image.</param>
        /// <param name="saveLocation">The save location.</param>
        /// <param name="NewFormat">Creates new format.</param>
        public static void SaveImageToLocation(this Image theImage, string saveLocation, System.Drawing.Imaging.ImageFormat NewFormat)
        {
            string saveFolder = Path.GetDirectoryName(saveLocation);
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
            // Save to disk
            theImage.Save(saveLocation, NewFormat);
        }

        /// <summary>
        /// Saves the image to specific location, save location includes filename
        /// </summary>
        /// <param name="theImage">The image.</param>
        /// <param name="saveLocation">The save location.</param>
        public static void saveImageToLocation(this Image theImage, string saveLocation)
        {
            // Strip the file from the end of the dir
            string saveFolder = Path.GetDirectoryName(saveLocation);
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
            // Save to disk
            theImage.Save(saveLocation);
        }

        /// <summary>
        /// Resize The Image and Save IT
        /// </summary>
        /// <param name="ImageToResize">The image to resize.</param>
        /// <param name="MaxWidth">The maximum width.</param>
        /// <param name="UseHeightRatio">if set to <c>true</c> [use height ratio].</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="NewFormat">Creates new format.</param>
        public static void ResizeAndSave(this Image ImageToResize, int MaxWidth, bool UseHeightRatio, string FileName, System.Drawing.Imaging.ImageFormat NewFormat)
        {
            Image thumbnail = ImageToResize.resizeImage(MaxWidth, UseHeightRatio);

            thumbnail.SaveImageToLocation(FileName, NewFormat);
        }

        // Resizes the image and saves it to disk.  Save as property is full path including file extension
        /// <summary>
        /// Resizes the and save.
        /// </summary>
        /// <param name="ImageToResize">The image to resize.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="UseHeightRatio">if set to <c>true</c> [use height ratio].</param>
        /// <param name="FileName">Name of the file.</param>
        public static void resizeAndSave(this Image ImageToResize, int max, bool UseHeightRatio, string FileName)
        {
            Image thumbnail = ImageToResize.resizeImage(max, UseHeightRatio);

            thumbnail.saveImageToLocation(FileName);
        }

        // Overload if filepath is passed in
        /// <summary>
        /// Resizes the image and save.
        /// </summary>
        /// <param name="imageLocation">The image location.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="UseHeightRatio">if set to <c>true</c> [use height ratio].</param>
        /// <param name="FileName">Name of the file.</param>
        public static void resizeImageAndSave(string imageLocation, int max, bool UseHeightRatio, string FileName)
        {
            Image loadedImage = Image.FromFile(imageLocation);
            Image thumbnail = loadedImage.resizeImage(max, UseHeightRatio);

            thumbnail.saveImageToLocation(FileName);
        }

        /// <summary>
        /// Convert the Image To Black And White
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="Threshold">The threshold.</param>
        /// <returns>Bitmap.</returns>
        public static Bitmap convertToBlackAndWhite(this Bitmap image, float Threshold)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color c = image.GetPixel(i, j);
                    if (c.GetBrightness() > Threshold) //You can change the value
                    {
                        image.SetPixel(i, j, Color.White);
                    }
                    else
                    {
                        image.SetPixel(i, j, Color.Black);
                    }
                }
            }
            return image;
        }

        /// <summary>
        /// Invert An Images Colors
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns>Bitmap.</returns>
        public static Bitmap ApplyInvert(this Bitmap m)
        {
            var bitmapRead = m.LockBits(new Rectangle(0, 0, m.Width, m.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);
            var bitmapLength = bitmapRead.Stride * bitmapRead.Height;
            var bitmapBGRA = new byte[bitmapLength];
            Marshal.Copy(bitmapRead.Scan0, bitmapBGRA, 0, bitmapLength);
            m.UnlockBits(bitmapRead);

            for (int i = 0; i < bitmapLength; i += 4)
            {
                bitmapBGRA[i] = (byte)(255 - bitmapBGRA[i]);
                bitmapBGRA[i + 1] = (byte)(255 - bitmapBGRA[i + 1]);
                bitmapBGRA[i + 2] = (byte)(255 - bitmapBGRA[i + 2]);
                //        [i + 3] = ALPHA.
            }

            var bitmapWrite = m.LockBits(new Rectangle(0, 0, m.Width, m.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
            Marshal.Copy(bitmapBGRA, 0, bitmapWrite.Scan0, bitmapLength);
            m.UnlockBits(bitmapWrite);
            return m;
        }
        // Returns the thumbnail image when an image object is passed in
        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="ImageToResize">The image to resize.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="UseHeightRatio">if set to <c>true</c> [use height ratio].</param>
        /// <returns>Image.</returns>
        public static Image resizeImage(this Image ImageToResize, int max, bool UseHeightRatio)
        {
            if (UseHeightRatio)
            {
                var ratio = (double)max / ImageToResize.Height;

                var newWidth = (int)(ImageToResize.Width * ratio);
                var newHeight = (int)(ImageToResize.Height * ratio);

                var newImage = new Bitmap(newWidth, newHeight);
                using (var g = System.Drawing.Graphics.FromImage(newImage))
                {
                    g.DrawImage(ImageToResize, 0, 0, newWidth, newHeight);
                }

                return newImage;
            }
            else
            {
                var ratio = (double)max / ImageToResize.Width;

                var newWidth = (int)(ImageToResize.Width * ratio);
                var newHeight = (int)(ImageToResize.Height * ratio);

                var newImage = new Bitmap(newWidth, newHeight);
                using (var g = System.Drawing.Graphics.FromImage(newImage))
                {
                    g.DrawImage(ImageToResize, 0, 0, newWidth, newHeight);
                }

                return newImage;
            }
        }

        // Overload if file path is passed in instead
        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="imageLocation">The image location.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="UseHeightRatio">if set to <c>true</c> [use height ratio].</param>
        /// <returns>Image.</returns>
        public static Image resizeImage(string imageLocation, int max, bool UseHeightRatio)
        {
            Image loadedImage = Image.FromFile(imageLocation);
            return loadedImage.resizeImage(max, UseHeightRatio);
        }
    }


}
