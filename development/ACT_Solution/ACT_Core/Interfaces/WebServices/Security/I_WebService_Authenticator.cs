// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_WebServiceAuthenticate.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Interfaces.WebServices.Security
{
    /// <summary>
    /// Interface I_WebServiceAuthenticate
    /// </summary>
    public interface I_WebService_Authenticator
    {
        /// <summary>
        /// Authenticates the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        string Authenticate(object[] args);
        
        string Authenticate(string EmailAddress, string Password);
        
        string Company_Authenticate(string Token, string APIKey, string APISecret, uint? PermissionLevelRequest = null);

        bool Registration_Confirm_ConfirmationCode(string Token, string ConfirmationCode);

        bool Company_AcceptInvitation(string InvitationCode, string Token);

    }
}
