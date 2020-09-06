// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-09-2019
// ***********************************************************************
// <copyright file="PackedFileHeader.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using ACT.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Encoding.ACTPackaging
{
    /// <summary>
    /// Represents the Packed File Header Describing the File Information
    /// Each File In the Package Gets a Single PackedFileHeader
    /// </summary>
    /// <summary>
    /// Struct PackedFileHeader
    /// </summary>
    public struct PackedFileHeader
    {
        /// <summary>
        /// Path ID
        /// </summary>
        /// <summary>
        /// Gets or sets the path identifier.
        /// </summary>
        /// <value>The path identifier.</value>
        public ushort PathID { get; set; }

        /// <summary>
        /// Data ID
        /// </summary>
        /// <summary>
        /// Gets or sets the data identifier.
        /// </summary>
        /// <value>The data identifier.</value>
        public uint DataID { get; set; }

        /// <summary>
        /// File Name Character Length
        /// </summary>
        /// <value>The length of the file name character.</value>
        public ushort FileNameCharacterLength { get; set; }

        /// <summary>
        /// FileName
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Export the Class to a Byte[]
        /// </summary>
        /// <returns>{byte[]}</returns>
        public byte[] Export()
        {
            List<byte> _data = new List<byte>();
            _data.AddRange(PathID.ToByteArray());
            _data.AddRange(DataID.ToByteArray());
            FileNameCharacterLength = FileName.ToCharArray().GetBytes().Length.ToUShort();
            _data.AddRange(FileNameCharacterLength.ToByteArray());
            _data.AddRange(FileName.ToCharArray().GetBytes());

            return _data.ToArray();
        }

        /// <summary>
        /// Import from a Binary Reader
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>PackedFileHeader.</returns>
        public static PackedFileHeader Import(ACT.Core.Types.IO.ACTBinaryReader reader)
        {
            PackedFileHeader _tmpReturn = new PackedFileHeader();
            _tmpReturn.PathID = reader.ReadUInt16();
            _tmpReturn.DataID = reader.ReadUInt32();
            _tmpReturn.FileNameCharacterLength = reader.ReadUInt16();
            _tmpReturn.FileName = reader.ReadBytes(_tmpReturn.FileNameCharacterLength).ToCharArray().ToString();

            return _tmpReturn;
        }
    }

}
