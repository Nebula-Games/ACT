// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_SecurityProvider.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.Interfaces;
using ACT.Core;
using ACT.Core.Enums;
using ACT.Core.Interfaces.Security.UserData;


namespace ACT.Core.Interfaces.Security.Authentication
{
    /// <summary>
    /// This interface defines the methods needed to Connect To A Security Provider.  Active Directory, Or Custom Base Auth
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    public interface I_SecurityProvider : I_Plugin
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
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="TokenID">The token identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns><c>true</c> if [is token valid] [the specified application identifier]; otherwise, <c>false</c>.</returns>
        bool IsTokenValid(Guid ApplicationID, string TokenID, string AdditionalData);

        /// <summary>
        /// Generates A New Security Token and Populates the UserInfo Class
        /// </summary>
        /// <param name="ApplicationID">ApplicationID to use</param>
        /// <param name="UserInfo">Can be null if only needing to access Application Level Functions</param>
        /// <param name="AdditionalData">Additional Data To Embed in the Token</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool GenerateToken(Guid ApplicationID, I_UserInfo UserInfo, string AdditionalData);

        /// <summary>
        /// Child Security Providers Provide Additional Options For a Multi Tier Authentication Application
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <returns>List&lt;I_SecurityProvider&gt;.</returns>
        List<I_SecurityProvider> GetChildSecurityProviders(Guid ApplicationID);

        /// <summary>
        /// Additional Configuration Settings Defined In the XML File.  Or Loaded Seperatly outside if the configuration file.
        /// </summary>
        /// <value>The configuration settings.</value>
        Dictionary<string, string> ConfigurationSettings { get; set; }

        /// <summary>
        /// Login the User
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="UserName">Clear Text UserName</param>
        /// <param name="Password">Clear Text PassWord</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>I_LoginResult.</returns>
        I_LoginResult LoginUser(Guid ApplicationID, string UserName, string Password, string AdditionalData);

        #region Group Membership Functionality

        /// <summary>
        /// Validates User Group Membership
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="GroupName">Name of the group.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>bool</returns>
        bool IsUserMemberOfGroup(Guid ApplicationID, Guid UserID, string GroupName, string AdditionalData);

        /// <summary>
        /// Get The Group ID From The Name
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="GroupName">Name of the group.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>Guid.</returns>
        Guid GetGroupID(Guid ApplicationID, string GroupName, string AdditionalData);

        /// <summary>
        /// Returns all the available groups
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        List<string> GetAllGroups(Guid ApplicationID, string AdditionalData);

        /// <summary>
        /// Adds A User To The Group
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="GroupName">Name of the group.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>ACT.Core.Enums.ISecurityProviderGenericResult.</returns>
        ACT.Core.Enums.ISecurityProviderGenericResult AddUserToGroup(Guid ApplicationID, Guid UserID, string GroupName, string AdditionalData);

        #endregion

        /// <summary>
        /// Gets the Confirmation Code For The Email Address
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="EmailAddress">The email address.</param>
        /// <returns>System.String.</returns>
        string GetConfirmationCode(Guid ApplicationID, string EmailAddress);

        /// <summary>
        /// Get Encryption Key
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>Encryption Key</returns>
        string GetEncryptionKey(Guid ApplicationID, Guid UserID,string AdditionalData);

        /// <summary>
        /// Get User ID
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="AccessToken">The access token.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>System.String.</returns>
        string GetUserID(Guid ApplicationID, string AccessToken, string AdditionalData);

        /// <summary>
        /// Sends the Forgot Password Email
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="EmailAddress">The email address.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>True if Email Address Found False If Not</returns>
        bool SendForgotPasswordEmail(Guid ApplicationID, string EmailAddress, string AdditionalData);

        /// <summary>
        /// Returns a Delimeted string of User Info.  Delimiters are defined in ACT Configuration Settings.
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>I_UserInfo.</returns>
        I_UserInfo GetUserInfo(Guid ApplicationID, Guid UserID, string AdditionalData);

        /// <summary>
        /// Creates A New User
        /// </summary>
        /// <param name="BasicInfo">The basic information.</param>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>False O</returns>
        ACT.Core.Enums.ISecurityProviderGenericResult CreateUser(I_UserInfo BasicInfo, Guid ApplicationID, string AdditionalData);

        /// <summary>
        /// Updates A User.
        /// </summary>
        /// <param name="BasicInfo">Make Sure You Set UserInfo.UserKey to ID of User</param>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="AdditionalData">APIKEY</param>
        /// <returns>Update Result</returns>
        ACT.Core.Enums.ISecurityProviderGenericResult UpdateUser(I_UserInfo BasicInfo, Guid ApplicationID, string AdditionalData);

        /// <summary>
        /// Update Password
        /// </summary>
        /// <param name="UserID">UserID of Member to be updated</param>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="Password">New Unencrypted Password</param>
        /// <param name="AdditionalData">Additional Data</param>
        /// <returns>ACT.Core.Enums.ISecurityProviderGenericResult.</returns>
        ACT.Core.Enums.ISecurityProviderGenericResult UpdatePassword(Guid UserID, Guid ApplicationID, string Password, string AdditionalData);

        /// <summary>
        /// Update UserName
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>ACT.Core.Enums.ISecurityProviderGenericResult.</returns>
        ACT.Core.Enums.ISecurityProviderGenericResult UpdateUserName(Guid UserID, Guid ApplicationID, string UserName, string AdditionalData);

        /// <summary>
        /// Update Email
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="Email">The email.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>ACT.Core.Enums.ISecurityProviderGenericResult.</returns>
        ACT.Core.Enums.ISecurityProviderGenericResult UpdateEmail(Guid UserID, Guid ApplicationID, string Email, string AdditionalData);

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>ACT.Core.Enums.ISecurityProviderGenericResult.</returns>
        ACT.Core.Enums.ISecurityProviderGenericResult DeleteUser(Guid UserID, Guid ApplicationID, string AdditionalData);

        /// <summary>
        /// Adds a User To The
        /// </summary>
        /// <param name="AccessToken">The access token.</param>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>ACT.Core.Enums.ISecurityProviderGenericResult.</returns>
        ACT.Core.Enums.ISecurityProviderGenericResult AddUserToApplication(string AccessToken, Guid ApplicationID, string AdditionalData);

        /// <summary>
        /// Generates a Code for use in SMS Authentication
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="AccessToken">The access token.</param>
        /// <param name="ExternalID">The external identifier.</param>
        /// <returns>System.String.</returns>
        string GenerateSMSAuthenticationCode(Guid ApplicationID, string AccessToken, string ExternalID);

        /// <summary>
        /// Confirms the Authentication Code
        /// </summary>
        /// <param name="ApplicationID">The application identifier.</param>
        /// <param name="AccessToken">The access token.</param>
        /// <param name="Code">The code.</param>
        /// <param name="ExternalID">The external identifier.</param>
        /// <returns>System.String.</returns>
        string ConfirmSMSAuthenticationCode(Guid ApplicationID, string AccessToken, string Code, string ExternalID);


    }
}
