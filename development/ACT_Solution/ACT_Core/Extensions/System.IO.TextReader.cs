// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.IO.TextReader.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class SystemIOTextReaderExtensions.
    /// </summary>
    public static class SystemIOTextReaderExtensions
    {
        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="TextFilePath">The text file path.</param>
        /// <param name="FileEncoding">The file encoding.</param>
        /// <returns>System.IO.TextReader.</returns>
        public static System.IO.TextReader OpenFile(this string TextFilePath,System.Text.Encoding FileEncoding)
        {
            return new System.IO.StreamReader(TextFilePath, FileEncoding) as System.IO.TextReader;
        }
    }
}
