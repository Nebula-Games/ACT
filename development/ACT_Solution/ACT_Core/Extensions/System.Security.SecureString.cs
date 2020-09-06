// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.Security.SecureString.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class SystemSecuritySecureStringExtensions.
    /// </summary>
    public static class SystemSecuritySecureStringExtensions
    {
        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] GetBytes(this System.Security.SecureString x)
        {
            byte[] _TmpReturn = new byte[x.Length * 2];
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                int _Position = 0;
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(x);
                for (int i = 0; i < x.Length; i++)
                {
                    byte[] _TmpAry = new byte[] { Marshal.ReadByte(valuePtr, _Position), Marshal.ReadByte(valuePtr, _Position+1) };
                    _TmpReturn[_Position] = _TmpAry[0];
                    _Position++;
                    _TmpReturn[_Position] = _TmpAry[1];
                    _Position++;
                }
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }

            return _TmpReturn;
        }
    }
}
