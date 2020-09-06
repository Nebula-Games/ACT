// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-10-2019
// ***********************************************************************
// <copyright file="PackedPathHeader.cs" company="Stonegate Intel LLC">
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
    /// Represents a Path - Reducing the Storage Total Size
    /// </summary>
    /// <summary>
    /// Class PackedPathHeader.
    /// </summary>
    public class PackedPathHeader 
    {
        /// <summary>
        /// The path identifier
        /// </summary>
        private UInt32 _PathID;
        /// <summary>
        /// The path character length
        /// </summary>
        private UInt16 _PathCharacterLength;
        /// <summary>
        /// The path
        /// </summary>
        private string _Path;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="Package"></param>
        /// <summary>
        /// Initializes a new instance of the <see cref="PackedPathHeader"/> class.
        /// </summary>
        /// <param name="Path">The path.</param>
        /// <param name="Package">The package.</param>
        public PackedPathHeader(string Path, ACT_Package Package)
        {
            PathCharacters = System.Text.Encoding.GetEncoding(Package.ACTPackageHeader.Encoding_Value.ToInt()).GetBytes(Path);
            _PathCharacterLength = PathCharacters.Length.ToUShort();
            // TODO GET PATHID
            PathCharacterLength = BitConverter.GetBytes(_PathCharacterLength);
        }


        /// <summary>
        /// Path ID
        /// </summary>
        public byte[] PathID = new byte[4];

        /// <summary>
        /// Path Characters Length
        /// </summary>
        public byte[] PathCharacterLength = new byte[2];

        /// <summary>
        /// Path Characters
        /// </summary>
        public byte[] PathCharacters;

        /// <summary>
        /// Export to a BYTE Array for Storing in Package File
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] Export()
        {
            return null;
            //PathCharacterLength = Path.ToCharArray().GetBytes().Length.ToUShort();

            //List<byte> _tmpReturn = new List<byte>();
            //_tmpReturn.AddRange(PathID.ToByteArray());
            //_tmpReturn.AddRange(PathCharacterLength.ToByteArray());
            //_tmpReturn.AddRange(Path.ToCharArray().GetBytes());

            //return _tmpReturn.ToArray();
        }

        /// <summary>
        /// Import from BinaryReader
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>PackedPathHeader.</returns>
        public static PackedPathHeader Import(ACT.Core.Types.IO.ACTBinaryReader reader)
        {
            return null;
            // PackedPathHeader _tmpReturn = new PackedPathHeader();
            //_tmpReturn.PathID = reader.ReadUInt16();
            //_tmpReturn.PathCharacterLength = reader.ReadUInt16();
            //_tmpReturn.Path = reader.ReadBytes(_tmpReturn.PathCharacterLength).ToCharArray().ToString();
            //return _tmpReturn;
        }
    }

}
