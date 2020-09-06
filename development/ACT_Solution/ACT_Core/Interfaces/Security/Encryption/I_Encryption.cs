// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Encryption.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security;

namespace ACT.Core.Interfaces.Security.Encryption
{

    /// <summary>
    /// Interface I_Encryption
    /// </summary>
    public interface I_Encryption
    {
        /// <summary>
        /// Encrypts the specified clear text.
        /// </summary>
        /// <param name="ClearText">The clear text.</param>
        /// <returns>System.String.</returns>
        string Encrypt(string ClearText);
        /// <summary>
        /// Encrypts the specified clear text.
        /// </summary>
        /// <param name="clearText">The clear text.</param>
        /// <param name="Password">The password.</param>
        /// <returns>System.String.</returns>
        string Encrypt(string clearText, string Password);
        /// <summary>
        /// Encrypts the specified clear data.
        /// </summary>
        /// <param name="clearData">The clear data.</param>
        /// <param name="Password">The password.</param>
        /// <returns>System.Byte[].</returns>
        byte[] Encrypt(byte[] clearData, string Password);
        /// <summary>
        /// Encrypts the specified file in.
        /// </summary>
        /// <param name="fileIn">The file in.</param>
        /// <param name="fileOut">The file out.</param>
        /// <param name="Password">The password.</param>
        void Encrypt(string fileIn, string fileOut, string Password);
        /// <summary>
        /// Encrypts the specified clear data.
        /// </summary>
        /// <param name="clearData">The clear data.</param>
        /// <param name="Salt">The salt.</param>
        /// <param name="IV">The iv.</param>
        /// <param name="Password">The password.</param>
        /// <returns>System.Byte[].</returns>
        byte[] Encrypt(byte[] clearData, string Salt, byte[] IV, string Password);
        /// <summary>
        /// Decrypts the specified cipher data.
        /// </summary>
        /// <param name="cipherData">The cipher data.</param>
        /// <param name="Salt">The salt.</param>
        /// <param name="IV">The iv.</param>
        /// <param name="Password">The password.</param>
        /// <returns>System.Byte[].</returns>
        byte[] Decrypt(byte[] cipherData, string Salt, byte[] IV, string Password);
        /// <summary>
        /// Decrypts the specified clear text.
        /// </summary>
        /// <param name="ClearText">The clear text.</param>
        /// <returns>System.String.</returns>
        string Decrypt(string ClearText);
        /// <summary>
        /// Decrypts the specified cipher text.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="Password">The password.</param>
        /// <returns>System.String.</returns>
        string Decrypt(string cipherText, string Password);
        /// <summary>
        /// Decrypts the specified cipher data.
        /// </summary>
        /// <param name="cipherData">The cipher data.</param>
        /// <param name="Password">The password.</param>
        /// <returns>System.Byte[].</returns>
        byte[] Decrypt(byte[] cipherData, string Password);
        /// <summary>
        /// Decrypts the specified file in.
        /// </summary>
        /// <param name="fileIn">The file in.</param>
        /// <param name="fileOut">The file out.</param>
        /// <param name="Password">The password.</param>
        void Decrypt(string fileIn, string fileOut, string Password);
        /// <summary>
        /// ms the d5.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        string MD5(string value);
        /// <summary>
        /// ms the d5 alt.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        string MD5ALT(string value);
        /// <summary>
        /// Shes the a256.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        string SHA256(string value);
        /// <summary>
        /// Shes the a512.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        string SHA512(string value);
        /// <summary>
        /// Healthes the check.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool HealthCheck();
    }
}
