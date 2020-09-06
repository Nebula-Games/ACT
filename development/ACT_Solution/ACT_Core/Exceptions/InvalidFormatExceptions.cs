// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="InvalidFormatExceptions.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Exceptions
{

    /// <summary>
    /// Class InvalidFormatExceptions.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class InvalidFormatExceptions : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFormatExceptions"/> class.
        /// </summary>
        public InvalidFormatExceptions() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFormatExceptions"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidFormatExceptions(string message) : base(message) { LogError(message, null); }
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFormatExceptions"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public InvalidFormatExceptions(string message, Exception inner) : base(message, inner) { LogError(message, inner); }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        private void LogError(string Message, Exception innerException)
        {
            ACT.Core.Helper.ErrorLogger.LogError("DatabaseConnectionError", Message, innerException, Enums.ErrorLevel.Informational);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFormatExceptions"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        protected InvalidFormatExceptions(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


}
