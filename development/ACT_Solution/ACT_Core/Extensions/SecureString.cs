// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="SecureString.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACT.Core.Extensions;
using System.Security;
using System.Runtime.InteropServices;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class SecureStringExtensions.
    /// </summary>
    public static class SecureStringExtensions
    {
        /// <summary>
        /// A SecureString extension method that converts this object to a byte array.
        /// </summary>
        /// <param name="secureString">The secureString to act on.</param>
        /// <param name="encoding">(Optional) the encoding.</param>
        /// <returns>The given data converted to a byte[].</returns>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are
        /// null.</exception>
        /// <remarks>Mark Alicz, 12/17/2016.</remarks>

        public static byte[] ToByteArray(this SecureString secureString, System.Text.Encoding encoding = null)
        {
            if (secureString == null) { throw new ArgumentNullException(nameof(secureString)); }

            encoding = encoding ?? System.Text.Encoding.UTF8;

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return encoding.GetBytes(Marshal.PtrToStringUni(unmanagedString));
            }
            finally
            {
                if (unmanagedString != IntPtr.Zero) { Marshal.ZeroFreeBSTR(unmanagedString); }
            }
        }

        /// <summary>
        /// A SecureString extension method that converts a securePassword to an unsecure string.
        /// </summary>
        /// <param name="securePassword">The securePassword to act on.</param>
        /// <returns>The given data converted to an unsecure string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are
        /// null.</exception>
        /// <remarks>Mark Alicz, 12/17/2016.</remarks>

        public static string ConvertToUnsecureString(this SecureString securePassword)
        {
            
            if (securePassword == null)
                throw new ArgumentNullException("securePassword");

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
