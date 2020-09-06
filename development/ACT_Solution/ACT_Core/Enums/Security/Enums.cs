// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Enums.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Enums.Security
{
    /// <summary>
    /// The security Difficulty of the process
    /// Easy = 6 Characters
    /// Medium = 8, Hard = 12, Impossible = 16, Galaxy = 32, Universe = 64
    /// Phrase + Easy = 2 Words, Phrase + Medium = 2, ... 4... 6... 8... 10...
    /// </summary>
    /// <summary>
    /// Enum SecurityDifficulty
    /// </summary>
    [Flags()]
    public enum SecurityDifficulty
    {
        /// <summary>
        /// The length easy
        /// </summary>
        Length_Easy = 1,
        /// <summary>
        /// The length medium
        /// </summary>
        Length_Medium = 2,
        /// <summary>
        /// The length hard
        /// </summary>
        Length_Hard = 4,
        /// <summary>
        /// The length impossible
        /// </summary>
        Length_Impossible = 8,
        /// <summary>
        /// The length galaxy size
        /// </summary>
        Length_GalaxySize = 16,
        /// <summary>
        /// The length universe size
        /// </summary>
        Length_UniverseSize = 32,
        /// <summary>
        /// The characters alpha lower
        /// </summary>
        Characters_AlphaLower = 64,
        /// <summary>
        /// The characters alpha upper
        /// </summary>
        Characters_AlphaUpper = 128,
        /// <summary>
        /// The characters alpha lower upper
        /// </summary>
        Characters_AlphaLowerUpper = 256,
        /// <summary>
        /// The characters special
        /// </summary>
        Characters_Special = 1012,
        /// <summary>
        /// The phrase dictionary
        /// </summary>
        Phrase_Dictionary = 2024,
        /// <summary>
        /// The phrase songs
        /// </summary>
        Phrase_Songs = 4048,
        /// <summary>
        /// The phrase nonsense
        /// </summary>
        Phrase_Nonsense = 8096
    }

    /// <summary>
    /// Enum PermissionLevel
    /// </summary>
    [Flags()]
    public enum PermissionLevel
    {
        /// <summary>
        /// The read
        /// </summary>
        Read, Write, Modify, Delete, ReadSelf, ModifySelf, DeleteSelf, ReadOther, ModifyOther, DeleteOther
    }

    /// <summary>
    /// Enum HashType
    /// </summary>
    public enum HashType
    {
        /// <summary>
        /// The sh a512
        /// </summary>
        SHA512, SHA384, SHA256, SHA1, MD5
    }

    /// <summary>
    /// KeyStore Source
    /// </summary>
    public enum KeyStoreSource
    {
        ACT,
        Internal,
        External
    }

    /// <summary>
    /// Defines the Isolation Level for various Application Levels
    /// </summary>
    public enum IsolationLevel
    {
        //
        // Summary:
        //     The level cannot be determined.
        Unspecified = -1,
        //
        // Summary:
        //     The pending changes from more highly isolated transactions cannot be overwritten.
        Simple = 1,
        //
        // Summary:
        //     A segmented Memory Space Isolated From all Data in ACT and ACT Applications
        Medium = 2,
        //
        // Summary:
        //     Locked down to specific Access Levels and Specific Protocols
        High = 3

    }
}
