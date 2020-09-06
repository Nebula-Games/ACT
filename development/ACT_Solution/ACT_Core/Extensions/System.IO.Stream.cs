// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-14-2019
// ***********************************************************************
// <copyright file="System.IO.Stream.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// SystemIO Stream Extensions
    /// </summary>
    public static class SystemIOStreamExtensions
    {
        /// <summary>
        /// Read the File and Return the Data
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>byte[] Containing the Stream Data</returns>
        public static byte[] ReadFully(this Stream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }

        /// <summary>
        /// Converts A Memory Stream To String
        /// </summary>
        /// <param name="ms">The ms.</param>
        /// <returns>System.String.</returns>
        public static string ConvertToString(this System.IO.MemoryStream ms)
        {
            string _TmpReturn = "";
            using (var sr = new System.IO.StreamReader(ms))
            {
                _TmpReturn = sr.ReadToEnd();
            }
            return _TmpReturn;
        }

        /// <summary>
        /// Converts A Normal Stream to a MemoryStream
        /// </summary>
        /// <param name="MainStream">The main stream.</param>
        /// <returns>System.IO.MemoryStream.</returns>
        public static System.IO.MemoryStream ToMemoryStream(this System.IO.Stream MainStream)
        {

            byte[] buffer = new byte[16 * 1024];
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            int read;
            while ((read = MainStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms;
        }

        /// <summary>
        /// Calculated the SHA512 Hash
        /// </summary>
        /// <param name="s">Stream to Get the HASH For</param>
        /// <returns>System.String.</returns>
        public static string GetSHA512Hash(this Stream s)
        {
            using (var sha512 = SHA512CryptoServiceProvider.Create())
            {
                s.Position = 0;
                return ACT.Core.Helper.Security.Security_Helper.GetHash(s, sha512);
            }
        }

        
    }
}
