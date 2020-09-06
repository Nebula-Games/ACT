// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_EncryptedFile.cs" company="Stonegate Intel LLC">
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
using System.Security.Cryptography;

namespace ACT.Core.Types.IO
{
    /// <summary>
    /// Represents any ACT Encrypted File
    /// </summary>
    [Serializable()]
    public class ACT_EncryptedFile
    {
        /// <summary>
        /// Holds The File Data
        /// </summary>
        /// <value>The file data.</value>
        private byte[] FileData { get; set; }

        /// <summary>
        /// Encrypted File Data
        /// </summary>
        /// <value>The encrypted file data.</value>
        private byte[] EncryptedFileData { get; set; }

        /// <summary>
        /// Raw File Data
        /// </summary>
        /// <param name="RawData">The raw data.</param>
        public ACT_EncryptedFile(byte[] RawData)
        {
            string _FileData = RawData.ToBase64String();
            byte[] _Hash;
            using (SHA512CryptoServiceProvider sha1 = new SHA512CryptoServiceProvider())
            {
                _Hash = sha1.ComputeHash(RawData);
            }

            var _PWH = _Hash.ToBase64String().ToBaseACTString();
            var _BB = _PWH.FromACTString();

          //  _FileData.EncryptAES(_Hash.ToBase16());
        }
    }
}
