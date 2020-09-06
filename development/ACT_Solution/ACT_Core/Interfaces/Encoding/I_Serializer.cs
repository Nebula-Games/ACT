// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Serializer.cs" company="Nebula Entertainment LLC">
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

namespace ACT.Core.Interfaces.Encoding
{
    /// <summary>
    /// Interface I_Serializer
    /// </summary>
    public interface I_Serializer
    {
        /// <summary>
        /// Serializes to string.
        /// </summary>
        /// <returns>System.String.</returns>
        string SerializeToString();
        /// <summary>
        /// Serializes to byte array.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        byte[] SerializeToByteArray();

        /// <summary>
        /// Deserializes from string.
        /// </summary>
        /// <param name="SerializedData">The serialized data.</param>
        void DeserializeFromString(string SerializedData);
        /// <summary>
        /// Deserializes from byte array.
        /// </summary>
        /// <param name="SerializedData">The serialized data.</param>
        void DeserializeFromByteArray(byte[] SerializedData);

    }
}
