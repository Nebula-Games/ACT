// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="SecurityExceptions.cs" company="Nebula Entertainment LLC">
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
    /// Class SecurityExceptions.
    /// </summary>
    public class SecurityExceptions
    {

        /// <summary>
        /// Class AccessDeniedException.
        /// Implements the <see cref="System.Exception" />
        /// </summary>
        /// <seealso cref="System.Exception" />
        public class AccessDeniedException : Exception
        {
            /// <summary>
            /// Gets the message.
            /// </summary>
            /// <value>The message.</value>
            public override string Message
            {
                get
                {
                    return "Access Denied";
                }
            }

            
        }
    }
}
