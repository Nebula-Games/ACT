// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-11-2019
// ***********************************************************************
// <copyright file="ACT_Package_Header.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using ACT.Core.Extensions;


namespace ACT.Core.Encoding.ACTPackaging
{
    /// <summary>
    /// Packed Version Header Structure
    /// </summary>
    /// <summary>
    /// Class ACT_Package_Header.
    /// </summary>
    public class ACT_Package_Header
    {

        #region Constructors

        /// <summary>
        /// ACT_Package_Header
        /// </summary>
        /// <param name="textEncoding">Text Encoding To Use</param>
        /// <param name="password"></param>
        /// <param name="basePath"></param>
        /// <summary>
        /// Initializes a new instance of the <see cref="ACT_Package_Header"/> class.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="basePath">The base path.</param>
        /// <param name="textEncoding">The text encoding.</param>
        public ACT_Package_Header(string password, string basePath, System.Text.Encoding textEncoding = null)
        {
            if (textEncoding == null) { textEncoding = System.Text.Encoding.Unicode; }

            CreateVersionData();
            CreationDate = BitConverter.GetBytes(DateTime.Now.ToUnixTime());
            NumberOfFileHeaders = new byte[] { 0, 0, 0, 0 };
            NumberOfPathHeaders = new byte[] { 0, 0, 0, 0 };
            NumberOfDataHeaders = new byte[] { 0, 0, 0, 0 };
            EncodingData = BitConverter.GetBytes(textEncoding.CodePage);
            BasePath = textEncoding.GetBytes(basePath);
            BasePathLength = BitConverter.GetBytes(BasePath.Length);
            PWCharacters = textEncoding.GetBytes(password);
            PWLength = BitConverter.GetBytes(PWCharacters.Length);
        }

        /// <summary>
        /// Black Constructor
        /// </summary>
        /// <summary>
        /// Initializes a new instance of the <see cref="ACT_Package_Header"/> class.
        /// </summary>
        public ACT_Package_Header()
        {

        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Checks the Compatibility of the Version
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Checks the compatibility.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CheckCompatibility()
        {
            // TODO WRITE CODE TO IMPLEMENT LOGIC CHECKS
            // TODO Create Version Compatibility Matrix
            return true;
        }

        /// <summary>
        /// Creates the Version Data
        /// UPDATE AS NEEDED
        /// </summary>
        /// <summary>
        /// Creates the version data.
        /// </summary>
        private void CreateVersionData()
        {
            //byte 1-2 Version
            //byte 3-4 Client
            byte _1;
            byte _2;
            byte _3;
            byte _4;

            _1 = 1;
            _2 = 1;
            _3 = 1;
            _4 = 75;

            VersionID[0] = _1; VersionID[1] = _2; VersionID[2] = _3; VersionID[3] = _4;
            VersionIDCheck[0] = _1; VersionIDCheck[1] = _2; VersionIDCheck[2] = _3; VersionIDCheck[3] = _4;
        }

        /// <summary>
        /// Calculate The Hash
        /// </summary>
        /// <summary>
        /// Calculates the hash.
        /// </summary>
        private void CalculateHash()
        {
            var _HashNumber = VersionID_Value * 6 + PasswordLength_Value * (uint)4 + CreationDate_Value.ToUnixTime() * 4;
            HashCharacters = BitConverter.GetBytes(_HashNumber);
            HashLength = BitConverter.GetBytes(HashCharacters.Length);
        }

        #endregion

        #region Value Converter Readers

        /// <summary>
        /// Encoding Value (Default To UTF16)
        /// </summary>
        /// <summary>
        /// Gets the encoding value.
        /// </summary>
        /// <value>The encoding value.</value>
        public UInt32 Encoding_Value
        {
            get
            {
                var _tV = BitConverter.ToUInt32(EncodingData, 0);
                if (_tV == 0) { _tV = (uint)System.Text.Encoding.Unicode.CodePage; }
                return _tV;
            }
        }

        /// <summary>
        /// Version ID Value 
        /// </summary>
        /// <summary>
        /// Gets the version identifier value.
        /// </summary>
        /// <value>The version identifier value.</value>
        public UInt32 VersionID_Value
        {
            get
            {
                var _tV = BitConverter.ToUInt32(VersionID, 0);
                return _tV;
            }
        }

        /// <summary>
        /// Hash Length
        /// </summary>
        /// <summary>
        /// Gets the hash length value.
        /// </summary>
        /// <value>The hash length value.</value>
        public UInt16 HashLength_Value
        {
            get
            {
                var _tV = BitConverter.ToUInt16(HashLength, 0);
                return _tV;
            }
        }

        /// <summary>
        /// Password Length Value
        /// </summary>
        /// <summary>
        /// Gets the password length value.
        /// </summary>
        /// <value>The password length value.</value>
        public UInt16 PasswordLength_Value
        {
            get
            {
                var _tV = BitConverter.ToUInt16(PWLength, 0);
                return _tV;
            }
        }

        /// <summary>
        /// Creation Date Value
        /// </summary>
        /// <summary>
        /// Gets the creation date value.
        /// </summary>
        /// <value>The creation date value.</value>
        public DateTime CreationDate_Value
        {
            get
            {
                var _tV = BitConverter.ToUInt64(CreationDate, 0);
                return _tV.FromUnixTime();
            }
        }

        /// <summary>
        /// Number Of File Headers Value 
        /// </summary>
        /// <summary>
        /// Gets the number of file headers value.
        /// </summary>
        /// <value>The number of file headers value.</value>
        public UInt32 NumberOfFileHeaders_Value
        {
            get
            {
                var _tV = BitConverter.ToUInt32(NumberOfFileHeaders, 0);
                return _tV;
            }
        }

        /// <summary>
        /// Number Of Data Headers Value 
        /// </summary>
        /// <summary>
        /// Gets the number of data headers value.
        /// </summary>
        /// <value>The number of data headers value.</value>
        public UInt32 NumberOfDataHeaders_Value
        {
            get
            {
                var _tV = BitConverter.ToUInt32(NumberOfDataHeaders, 0);
                return _tV;
            }
        }

        /// <summary>
        /// Number Of Path Headers Value 
        /// </summary>
        /// <summary>
        /// Gets the number of path headers value.
        /// </summary>
        /// <value>The number of path headers value.</value>
        public UInt32 NumberOfPathHeaders_Value
        {
            get
            {
                var _tV = BitConverter.ToUInt32(NumberOfPathHeaders, 0);
                return _tV;
            }
        }

        /// <summary>
        /// Base Path Length Value 
        /// </summary>
        /// <summary>
        /// Gets the base path length value.
        /// </summary>
        /// <value>The base path length value.</value>
        public UInt32 BasePathLength_Value
        {
            get
            {
                var _tV = BitConverter.ToUInt32(BasePathLength, 0);
                return _tV;
            }
        }

        /// <summary>
        /// Base Path
        /// </summary>
        /// <summary>
        /// Gets the base path value.
        /// </summary>
        /// <value>The base path value.</value>
        public string BasePathValue
        {
            get
            {
                return System.Text.Encoding.GetEncoding(Encoding_Value.ToInt()).GetString(BasePath);
            }
        }

        #endregion

        #region Hard Data

        /// <summary>
        /// Version ID (Release Manager Version)
        /// </summary>
        /// <summary>
        /// The version identifier
        /// </summary>
        public byte[] VersionID = new byte[4];

        /// <summary>
        /// Encoding Identifier
        /// </summary>
        /// <summary>
        /// The encoding data
        /// </summary>
        public byte[] EncodingData = new byte[4];

        /// <summary>
        /// Hash Length
        /// </summary>
        /// <summary>
        /// The hash length
        /// </summary>
        public byte[] HashLength = new byte[2];

        /// <summary>
        /// Hash Characters (Actual Value BASE64 String)
        /// </summary>
        /// <summary>
        /// The hash characters
        /// </summary>
        public byte[] HashCharacters;

        /// <summary>
        /// Password Length of the String
        /// </summary>
        /// <summary>
        /// The pw length
        /// </summary>
        public byte[] PWLength = new byte[2];

        /// <summary>
        /// Password Characters used to encrypt the data
        /// </summary>
        public byte[] PWCharacters;

        /// <summary>
        /// Creation Date IN UNIX TIME (Seconds Since 1/1/1970
        /// </summary>
        public byte[] CreationDate = new byte[8];

        /// <summary>
        /// Number of File Headers
        /// </summary>
        public byte[] NumberOfFileHeaders = new byte[4];

        /// <summary>
        /// Number of Path Headers
        /// </summary>
        public byte[] NumberOfPathHeaders = new byte[4];

        /// <summary>
        /// Number of Data Identifiers
        /// </summary>
        public byte[] NumberOfDataHeaders = new byte[4];

        /// <summary>
        /// Base Path Length
        /// </summary>
        public byte[] BasePathLength = new byte[4];

        /// <summary>
        /// Base Path
        /// </summary>
        public byte[] BasePath;

        /// <summary>
        /// VersionIDCheck (Release Manager Version)
        /// </summary>
        public byte[] VersionIDCheck = new byte[4];

        #endregion

        /// <summary>
        /// Export to a BYTE Array for Storing in Package File
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] Export()
        {
            /*
             *  Order = Version , Hash Length, Hash, PW Length, PW, CreationDate, NumberOfFileHeaders,
             *  Number of PathHeaders, BasePathLength, BasePathCharacters
             */
            CalculateHash();

            List<byte> _tmpReturn = new List<byte>();
            _tmpReturn.AddRange(VersionID);
            _tmpReturn.AddRange(EncodingData);
            _tmpReturn.AddRange(HashLength);
            _tmpReturn.AddRange(HashCharacters);
            _tmpReturn.AddRange(PWLength);
            _tmpReturn.AddRange(PWCharacters);
            _tmpReturn.AddRange(CreationDate);
            _tmpReturn.AddRange(NumberOfFileHeaders);
            _tmpReturn.AddRange(NumberOfPathHeaders);
            _tmpReturn.AddRange(NumberOfDataHeaders);
            _tmpReturn.AddRange(BasePathLength);
            _tmpReturn.AddRange(BasePath);
            _tmpReturn.AddRange(VersionIDCheck);

            return _tmpReturn.ToArray();
        }

        /// <summary>
        /// Import from a byte[]
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <returns>ACT_Package_Header.</returns>
        public static ACT_Package_Header Import(byte[] Data)
        {
            ACT_Package_Header _PackageHeader = new ACT_Package_Header();

            int _Position = 0;
            Buffer.BlockCopy(Data, _Position, _PackageHeader.VersionID, 0, 4);
            _Position = _Position + 4;
            Buffer.BlockCopy(Data, _Position, _PackageHeader.EncodingData, 0, 4);
            _Position = _Position + 4;
            Buffer.BlockCopy(Data, _Position, _PackageHeader.HashLength, 0, 2);
            _Position = _Position + 2;
            _PackageHeader.HashCharacters = new byte[_PackageHeader.HashLength_Value];
            Buffer.BlockCopy(Data, _Position, _PackageHeader.HashCharacters, 0, _PackageHeader.HashLength_Value);

            _Position = _Position + _PackageHeader.HashLength_Value;
            Buffer.BlockCopy(Data, _Position, _PackageHeader.PWLength, 0, 2);
            _Position = _Position + 2;
            _PackageHeader.PWCharacters = new byte[_PackageHeader.PasswordLength_Value];
            Buffer.BlockCopy(Data, _Position, _PackageHeader.PWCharacters, 0, _PackageHeader.PasswordLength_Value);

            _Position = _Position + _PackageHeader.PasswordLength_Value;
            Buffer.BlockCopy(Data, _Position, _PackageHeader.CreationDate, 0, 8);
            _Position = _Position + 8;
            Buffer.BlockCopy(Data, _Position, _PackageHeader.NumberOfFileHeaders, 0, 4);
            _Position = _Position + 4;
            Buffer.BlockCopy(Data, _Position, _PackageHeader.NumberOfPathHeaders, 0, 4);
            _Position = _Position + 4;
            Buffer.BlockCopy(Data, _Position, _PackageHeader.NumberOfDataHeaders, 0, 4);
            _Position = _Position + 4;
            Buffer.BlockCopy(Data, _Position, _PackageHeader.BasePathLength, 0, 4);
            _Position = _Position + _PackageHeader.BasePathLength_Value.ToInt();
            Buffer.BlockCopy(Data, _Position, _PackageHeader.VersionIDCheck, 0, 4);

            return _PackageHeader;
        }
    }
}
