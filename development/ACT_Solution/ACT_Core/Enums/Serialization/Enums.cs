// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Enums.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Enums.Serialization
{
    /// <summary>
    /// Represents the State Of a CacheEngine
    /// </summary>
    public enum CacheEngineState
    {
        Live, Initializing, Rebuilding, Saving, Error
    }

    /// <summary>
    /// Enum SerializationType
    /// </summary>
    public enum SerializationType
    {
        /// <summary>
        /// The XML
        /// </summary>
        XML,
        /// <summary>
        /// The json
        /// </summary>
        JSON,
        /// <summary>
        /// The CSV
        /// </summary>
        CSV,
        /// <summary>
        /// The binary
        /// </summary>
        BINARY,
        /// <summary>
        /// The actbinary
        /// </summary>
        ACTBINARY
    }
}
