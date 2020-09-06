// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 03-15-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-18-2019
// ***********************************************************************
// <copyright file="ACT_RSA_ENCRYPTION.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.Security.Cryptography;


namespace ACT.Core.BuiltInPlugins.Security
{

    /// <summary>
    /// ACT RSA Encryption
    /// </summary>
    public class ACT_RSA_ENCRYPTION
    {        

        /// <summary>
        /// The plaintext
        /// </summary>
        private byte[] plaintext;

        /// <summary>
        /// The encryptedtext
        /// </summary>
        private byte[] encryptedtext;

        /// <summary>
        /// The byte converter
        /// </summary>
        private UnicodeEncoding ByteConverter = new UnicodeEncoding();

        /// <summary>
        /// The RSA
        /// </summary>
        private RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

        #region-----Encryptionand Decryption Function-----

        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <param name="RSAKey">The RSA key.</param>
        /// <param name="DoOAEPPadding">if set to <c>true</c> [do oaep padding].</param>
        /// <returns>System.Byte[].</returns>
        static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        /// <summary>
        /// Decryption
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <param name="RSAKey">The RSA key.</param>
        /// <param name="DoOAEPPadding">if set to <c>true</c> [do oaep padding].</param>
        /// <returns>System.Byte[].</returns>
        static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }

        #endregion

        #region-- Function Implemantation

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
      //      plaintext = ByteConverter.GetBytes(txtplain.Text);
        //    encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);
        //    txtencrypt.Text = ByteConverter.GetString(encryptedtext);

        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
        //    byte[] decryptedtex = Decryption(encryptedtext, RSA.ExportParameters(true), false);
        //    txtdecrypt.Text = ByteConverter.GetString(decryptedtex);
        }
        
        #endregion
    }
}
