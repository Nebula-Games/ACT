// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_EncryptionPacket.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Security.Encryption
{
    /// <summary>
    /// Work in progress
    /// </summary>
    public interface IEncryptionPacket
    {
        /// <summary>
        /// Gets or sets the encryption object.
        /// </summary>
        /// <value>The encryption object.</value>
        I_Encryption EncryptionObject { get; set; }
        /// <summary>
        /// Gets or sets the password m d5.
        /// </summary>
        /// <value>The password m d5.</value>
        string PasswordMD5 { get; set; }

    }
}
