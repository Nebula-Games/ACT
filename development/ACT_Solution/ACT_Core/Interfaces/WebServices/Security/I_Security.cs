// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Security.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ACT.Core.Interfaces.WebServices.Security
{

    /// <summary>
    /// Interface I_Security
    /// </summary>
    [ServiceContract]
    public interface I_Security
    {
        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="AccessToken">The access token.</param>
        /// <param name="DataToEncrypt">The data to encrypt.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string EncryptString(string AccessToken, string DataToEncrypt, string APIKey);

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="AccessToken">The access token.</param>
        /// <param name="DataToDecrypt">The data to decrypt.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string DecryptString(string AccessToken, string DataToDecrypt, string APIKey);

        /// <summary>
        /// Returns the encryption methods.
        /// </summary>
        /// <param name="APIKey">The API key.</param>
        /// <param name="Language">The language.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string ReturnEncryptionMethods(string APIKey, ACT.Core.Enums.ProgrammingLanguages Language);

        /// <summary>
        /// Keeps the token alive.
        /// </summary>
        /// <param name="AccessToken">The access token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [OperationContract]
        bool KeepTokenAlive(string AccessToken, string APIKey);

        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string Login(string UserName, string Password, string APIKey);

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string CreateUser(string Email, string UserName, string Password, string FirstName, string LastName, string APIKey);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="AccessToken">The access token.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string UpdateUser(string AccessToken, string UserName, string FirstName, string LastName, string APIKey);

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="AccessToken">The access token.</param>
        /// <param name="OldPassword">The old password.</param>
        /// <param name="NewPassword">Creates new password.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string UpdatePassword(string AccessToken, string OldPassword, string NewPassword, string APIKey);

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="Token">The token.</param>
        /// <param name="API">The API.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string DeleteUser(string Token, string API);

        /// <summary>
        /// Sends the confirmation email.
        /// </summary>
        /// <param name="AccessToken">The access token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string SendConfirmationEmail(string AccessToken, string APIKey);

        /// <summary>
        /// Sends the forgot password email.
        /// </summary>
        /// <param name="EmailAddress">The email address.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [OperationContract]
        bool SendForgotPasswordEmail(string EmailAddress, string APIKey);

        /// <summary>
        /// Assigns the user to application.
        /// </summary>
        /// <param name="AccessToken">The access token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [OperationContract]
        bool AssignUserToApplication(string AccessToken, string APIKey);

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="AccessToken">The access token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string GetUserInfo(string AccessToken, string APIKey);

    }
}
