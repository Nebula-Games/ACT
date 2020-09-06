// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 03-11-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-11-2019
// ***********************************************************************
// <copyright file="FileSystem_Consts.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using ACT.Core.Extensions;

namespace ACT.Core.Consts.Windows.IO
{
    /// <summary>
    /// Contains Static Constants (FAKE) For the Windows FileSystem
    /// </summary>
    public static class FileSystem_Consts
    {
        /// <summary>
        /// The wdic
        /// </summary>
        private static string _wdic = "\\,/,:,*,?,\",<,>,|";
        /// <summary>
        /// The windows directory illegal characters
        /// </summary>
        private static List<string> _WindowsDirectoryIllegalCharacters = null;
        /// <summary>
        /// The windows file name illegal characters
        /// </summary>
        private static List<string> _WindowsFileNameIllegalCharacters = null;

        /// <summary>
        /// Windows Directory Illegal Characters
        /// </summary>
        /// <value>The windows directory illegal characters.</value>
        public static List<string> WindowsDirectoryIllegalCharacters
        {
            get
            {
                if (_WindowsDirectoryIllegalCharacters == null)
                {
                    _WindowsDirectoryIllegalCharacters = _wdic.SplitString(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                return _WindowsDirectoryIllegalCharacters;
            }
        }

        /// <summary>
        /// Widows
        /// </summary>
        /// <value>The windows file illegal characters.</value>
        public static List<string> WindowsFileIllegalCharacters
        {
            get
            {
                if (_WindowsFileNameIllegalCharacters == null)
                {
                    _WindowsDirectoryIllegalCharacters = _wdic.SplitString(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                return _WindowsFileNameIllegalCharacters;
            }
        }
    }
}
