// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="byte_array.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// Extension Method
/// </summary>
namespace ACT.Core.Extensions
{
    /// <summary>
    /// Byte Arary Extensions
    /// </summary>
    public static class Byte_Array_Extensions
    {
        /// <summary>
        /// Return a String From The Byte Array
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string ToString(this byte[] x)
        {
            return System.Text.Encoding.UTF8.GetString(x);
        }


        /// <summary>
        /// Converts the byte[] to a unicode char[]
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.Char[].</returns>
        public static char[] ToCharArray(this byte[] x)
        {
            return System.Text.Encoding.Unicode.GetString(x).ToCharArray();
        }

        public static string ToSHA256Hash(this byte[] x)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {                
                try
                {
                    System.IO.MemoryStream _MS = new MemoryStream(x);
                    byte[] hashValue = mySHA256.ComputeHash(_MS);
                    string _tmpReturn = hashValue.ToBase64String();
                    return _tmpReturn;
                }
                catch (IOException e) { throw e; }
                catch (UnauthorizedAccessException e) { throw e; }

            }
        }
    }
}
