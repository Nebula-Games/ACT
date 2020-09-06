// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.IO.BinaryReader.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ACT.Core.Types.IO
{
    /// <summary>
    /// ACT Binary Reader - Fixes the Endian Issue
    /// Implements the <see cref="System.IO.BinaryReader" />
    /// </summary>
    /// <seealso cref="System.IO.BinaryReader" />
    public class ACTBinaryReader : BinaryReader
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream">The stream.</param>
        public ACTBinaryReader(System.IO.Stream stream) : base(stream) { }

        /// <summary>
        /// Read an Int32
        /// </summary>
        /// <returns>A 4-byte signed integer read from the current stream.</returns>
        public new int ReadInt32()
        {
            var data = base.ReadBytes(4);
            Array.Reverse(data);
            return BitConverter.ToInt32(data, 0);
        }

        /// <summary>
        /// Read an Int16
        /// </summary>
        /// <returns>A 2-byte signed integer read from the current stream.</returns>
        public new Int16 ReadInt16()
        {
            var data = base.ReadBytes(2);
            Array.Reverse(data);
            return BitConverter.ToInt16(data, 0);
        }

        /// <summary>
        /// Read an Int64
        /// </summary>
        /// <returns>An 8-byte signed integer read from the current stream.</returns>
        public new Int64 ReadInt64()
        {
            var data = base.ReadBytes(8);
            Array.Reverse(data);
            return BitConverter.ToInt64(data, 0);
        }

        /// <summary>
        /// Read a UInt32
        /// </summary>
        /// <returns>A 2-byte unsigned integer read from this stream.</returns>
        public new UInt16 ReadUInt16()
        {
            var data = base.ReadBytes(2);
            Array.Reverse(data);
            return BitConverter.ToUInt16(data, 0);
        }

        /// <summary>
        /// Read a UInt32
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from this stream.</returns>
        public new UInt32 ReadUInt32()
        {
            var data = base.ReadBytes(4);
            Array.Reverse(data);
            return BitConverter.ToUInt32(data, 0);
        }

        /// <summary>
        /// Read a UInt32
        /// </summary>
        /// <returns>UInt64.</returns>
        public new UInt64 ReadUInt64()
        {
            var data = base.ReadBytes(8);
            Array.Reverse(data);
            return BitConverter.ToUInt64(data, 0);
        }

    }
}
