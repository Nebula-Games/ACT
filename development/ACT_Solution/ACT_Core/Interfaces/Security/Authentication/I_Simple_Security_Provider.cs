// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Simple_Security_Provider.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Security.Authentication
{
    /// <summary>
    /// Interface I_Simple_Security_Provider
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    public interface I_Simple_Security_Provider : Interfaces.Common.I_Plugin
    {
        /// <summary>
        /// Configure the Security Key
        /// </summary>
        /// <value>The API key.</value>
        string APIKey { get; set; }

        /// <summary>
        /// Configure The Security App
        /// </summary>
        /// <value>The API secret.</value>
        string APISecret { get; set; }

        /// <summary>
        /// A Unique Provider Identity: ONLY Registered DLL's Obtain a Provider UID.  Others Need to leave This Blank
        /// </summary>
        /// <value>The provider uid.</value>
        string ProviderUID { get; }

        /// <summary>
        /// Validates A User Token
        /// </summary>
        /// <param name="TokenID">The token identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns><c>true</c> if [is token valid] [the specified token identifier]; otherwise, <c>false</c>.</returns>
        bool IsTokenValid(string TokenID, Dictionary<string, string> AdditionalData);

        /// <summary>
        /// Login the User
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="PassWord">The pass word.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>I_LoginResult.</returns>
        I_LoginResult LoginUser(string UserName, string PassWord, Dictionary<string, string> AdditionalData);

        /// <summary>
        /// Generates A New Security Token and Populates the UserInfo Class
        /// </summary>
        /// <param name="LoginResult">The login result.</param>
        /// <param name="AdditionalData">Additional Data To Embed in the Token</param>
        /// <returns>System.String.</returns>
        string GenerateToken(I_LoginResult LoginResult, Dictionary<string, string> AdditionalData);

        /// <summary>
        /// Gets the User Information Full Login Info Required
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="AdditionalInfo">The additional information.</param>
        /// <returns>I_UserInfo.</returns>
        I_UserInfo GetUserInfo(string UserName, string Password, Dictionary<string, string> AdditionalInfo);

        /// <summary>
        /// Gets the User Information, Token Based Auth
        /// </summary>
        /// <param name="TokenID">The token identifier.</param>
        /// <param name="AdditionalInfo">The additional information.</param>
        /// <returns>I_UserInfo.</returns>
        I_UserInfo GetUserInfo(string TokenID, Dictionary<string,string> AdditionalInfo);

        /// <summary>
        /// Update the User Info (Full User Authentication)
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="PassWord">The pass word.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <param name="UpdatedUserInfo">The updated user information.</param>
        /// <returns>Common.I_TestResult.</returns>
        Common.I_TestResult UpdateUserInfo(string UserName, string PassWord, Dictionary<string, string> AdditionalData, I_UserInfo UpdatedUserInfo);

        /// <summary>
        /// Update the User Info (Token Based Auth)
        /// </summary>
        /// <param name="TokenID">The token identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <param name="UpdatedUserInfo">The updated user information.</param>
        /// <returns>Common.I_TestResult.</returns>
        Common.I_TestResult UpdateUserInfo(string TokenID, Dictionary<string, string> AdditionalData, I_UserInfo UpdatedUserInfo);

        /// <summary>
        /// Create New User
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="PassWord">The pass word.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <param name="NewUserInfo">Creates new userinfo.</param>
        /// <returns>Common.I_TestResult.</returns>
        Common.I_TestResult CreateUser(string UserName, string PassWord, Dictionary<string, string> AdditionalData, I_UserInfo NewUserInfo);

        /// <summary>
        /// Create New User
        /// </summary>
        /// <param name="TokenID">The token identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <param name="NewUserInfo">Creates new userinfo.</param>
        /// <returns>Common.I_TestResult.</returns>
        Common.I_TestResult CreateUser(string TokenID, Dictionary<string, string> AdditionalData, I_UserInfo NewUserInfo);

    }
}
