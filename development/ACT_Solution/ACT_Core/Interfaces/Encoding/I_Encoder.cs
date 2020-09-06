// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Encoder.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Encoding
{
    /// <summary>
    /// Basic Encoding Only
    /// </summary>
    public interface I_Encoder
    {
        /// <summary>
        /// Encodes the text.
        /// </summary>
        /// <param name="Input">The input.</param>
        /// <param name="Rules">The rules.</param>
        /// <returns>System.String.</returns>
        string EncodeText(string Input, IEncoderRules Rules);
        /// <summary>
        /// Decodes the text.
        /// </summary>
        /// <param name="Input">The input.</param>
        /// <param name="Rules">The rules.</param>
        /// <returns>System.String.</returns>
        string DecodeText(string Input, IEncoderRules Rules);

    }

    /// <summary>
    /// Currently Only Supporting Simple Character Replacement
    /// </summary>
    public interface IEncoderRules
    {
        /// <summary>
        /// Gets the replacement characters.
        /// </summary>
        /// <value>The replacement characters.</value>
        Dictionary<string, string> ReplacementCharacters { get; }
        /// <summary>
        /// Gets the reg ex start append.
        /// </summary>
        /// <value>The reg ex start append.</value>
        Dictionary<string, string> RegExStartAppend { get; }
        /// <summary>
        /// Gets the reserved keywords.
        /// </summary>
        /// <value>The reserved keywords.</value>
        string[] ReservedKeywords { get; }
        /// <summary>
        /// Gets the reserved keywords replacement text.
        /// </summary>
        /// <value>The reserved keywords replacement text.</value>
        string ReservedKeywordsReplacementText { get; }
    }
}
