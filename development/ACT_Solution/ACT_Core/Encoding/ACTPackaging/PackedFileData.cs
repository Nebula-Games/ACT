// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="PackedFileData.cs" company="Stonegate Intel LLC">
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
using System.IO;

namespace ACT.Core.Encoding.ACTPackaging
{
    /// <summary>
    /// Represents a File Data - Reducing the Storage Total Size (Deduplication)
    /// </summary>
    public struct PackedFileData
    {
        /// <summary>
        /// Data ID (Unique Identifier)
        /// </summary>
        /// <value>The data identifier.</value>
        public uint DataID { get; set; }

        /// <summary>
        /// Data Length in Bytes
        /// </summary>
        /// <value>The length of the data.</value>
        public uint DataLength { get; set; }

        /// <summary>
        /// Content Start Position
        /// </summary>
        /// <value>The content start position.</value>
        public ulong ContentStartPosition { get; set; }

        /// <summary>
        /// Export to a BYTE Array for Storing in Package File
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] Export()
        {
            List<byte> _tmpReturn = new List<byte>();
            _tmpReturn.AddRange(DataID.ToByteArray());
            _tmpReturn.AddRange(DataLength.ToByteArray());
            _tmpReturn.AddRange(ContentStartPosition.ToByteArray());
            return _tmpReturn.ToArray();
        }

        /// <summary>
        /// Import From the Binary Reader
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>PackedFileData.</returns>
        public static PackedFileData Import(ACT.Core.Types.IO.ACTBinaryReader reader)
        {
            PackedFileData _TmpReturn = new PackedFileData();
            _TmpReturn.DataID = reader.ReadUInt32();
            _TmpReturn.DataLength = reader.ReadUInt32();
            _TmpReturn.ContentStartPosition = reader.ReadUInt64();
            return _TmpReturn;
        }
    }
}
