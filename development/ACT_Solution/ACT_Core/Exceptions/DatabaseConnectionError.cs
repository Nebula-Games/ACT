// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="DatabaseConnectionError.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Exceptions
{
    /// <summary>
    /// Class DatabaseConnectionError.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class DatabaseConnectionError : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnectionError"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DatabaseConnectionError(string message) : base(message)
        {
            LogError(message, null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnectionError"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public DatabaseConnectionError(string message, Exception innerException) : base(message, innerException)
        {
            LogError(message, innerException);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnectionError"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected DatabaseConnectionError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        private void LogError(string Message, Exception innerException)
        {
            ACT.Core.Helper.ErrorLogger.LogError("DatabaseConnectionError", Message, innerException, Enums.ErrorLevel.Informational);
        }
    }
}
