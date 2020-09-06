// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="IOExceptions.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Extensions;
using ACT.Core.Interfaces;

namespace ACT.Core.Exceptions
{
    /// <summary>
    /// Class InvalidPathException.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidPathException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPathException"/> class.
        /// </summary>
        /// <param name="Path">The path.</param>
        public InvalidPathException(string Path) : base("Invalid Path Detected: " + Path)
        {            
        }
    }
}
