// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_ABSTRACT_I_SIMPLE_SECURITY_PROVIDER_ACTIVE_DIRECTORY.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.Authentication;
using System.DirectoryServices;
using ACT.Core.Enums;
using System.DirectoryServices.AccountManagement;
using System.Reflection.PortableExecutable;
using DirectoryEntry = System.DirectoryServices.DirectoryEntry;

namespace ACT.Core.Abstract.Security.Authentication
{
    /// <summary>
    /// Simple Security Provider TODO
    /// </summary>
    /// <summary>
    /// Class ACT_ABSTRACT_I_SIMPLE_SECURITY_PROVIDER_ACTIVE_DIRECTORY_LOGIN.
    /// Implements the <see cref="ACT.Core.Interfaces.Security.Authentication.I_Simple_Security_Provider" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Security.Authentication.I_Simple_Security_Provider" />
    public abstract class ACT_ABSTRACT_I_SIMPLE_SECURITY_PROVIDER_ACTIVE_DIRECTORY_LOGIN : ACT.Core.Interfaces.Security.Authentication.I_Simple_Security_Provider
    {
        /// <summary>
        /// Configure the Security Key
        /// </summary>
        /// <value>The API key.</value>
        public virtual string APIKey { get; set; }
        /// <summary>
        /// Configure The Security App
        /// </summary>
        /// <value>The API secret.</value>
        public virtual string APISecret { get; set; }

        /// <summary>
        /// A Unique Provider Identity: ONLY Registered DLL's Obtain a Provider UID.  Others Need to leave This Blank
        /// </summary>
        /// <value>The provider uid.</value>
        public virtual string ProviderUID => "";

        /// <summary>
        /// Specifies if the class has changed in any way
        /// </summary>
        /// <value><c>true</c> if this instance has changed; otherwise, <c>false</c>.</value>
        public abstract bool HasChanged { get; set; }
        /// <summary>
        /// Get all of the Public Properties in the class
        /// </summary>
        /// <value>The public properties.</value>
        public abstract List<string> PublicProperties { get; }

        /// <summary>
        /// Create New User
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="PassWord">The pass word.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <param name="NewUserInfo">Creates new userinfo.</param>
        /// <returns>I_TestResult.</returns>
        public abstract I_TestResult CreateUser(string UserName, string PassWord, Dictionary<string, string> AdditionalData, I_UserInfo NewUserInfo);

        /// <summary>
        /// Create New User
        /// </summary>
        /// <param name="TokenID">The token identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <param name="NewUserInfo">Creates new userinfo.</param>
        /// <returns>I_TestResult.</returns>
        public abstract I_TestResult CreateUser(string TokenID, Dictionary<string, string> AdditionalData, I_UserInfo NewUserInfo);
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public abstract void Dispose();
        /// <summary>
        /// Exports the current class to XML
        /// </summary>
        /// <returns>XML Representation of class</returns>
        public abstract string ExportXMLData();

        /// <summary>
        /// Generates A New Security Token and Populates the UserInfo Class
        /// </summary>
        /// <param name="LoginResult">The login result.</param>
        /// <param name="AdditionalData">Additional Data To Embed in the Token</param>
        /// <returns>System.String.</returns>
        public string GenerateToken(I_LoginResult LoginResult, Dictionary<string, string> AdditionalData)
        {
            if (LoginResult.Success == true)
            {
                LoginResult.TokenID = Guid.NewGuid().ToString();
            }

            return LoginResult.TokenID;
        }

        /// <summary>
        /// Returns the Errors stored in the local variable
        /// </summary>
        /// <returns><![CDATA[List<Exception>]]></returns>
        public abstract List<Exception> GetErrors();

        /// <summary>
        /// Required AdditionalInfo["Domain"]
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="AdditionalInfo"></param>     
        /// <returns>I_UserInfo.</returns>
        public I_UserInfo GetUserInfo(string UserName, string Password, Dictionary<string, string> AdditionalInfo)
        {
            
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, AdditionalInfo["Domain"]))
            {
                // validate the credentials
                bool isValid = pc.ValidateCredentials(UserName, Password);
                if (isValid)
                {

                    DirectoryEntry oDE;
                    oDE = new DirectoryEntry("gamersgather.local", UserName, Password, AuthenticationTypes.Secure);
                    
                    I_UserInfo _TmpReturn = ACT.Core.CurrentCore<I_UserInfo>.GetCurrent();
                    ACT.Core.Windows.ActiveDirectory.ADUserDetail _D = Windows.ActiveDirectory.ADUserDetail.GetUser(oDE);

                    _TmpReturn.FirstName = _D.FirstName;
                    _TmpReturn.LastName = _D.LastName;
                    _TmpReturn.Email = _D.EmailAddress;
                    return _TmpReturn;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the User Information, Token Based Auth
        /// </summary>
        /// <param name="TokenID">The token identifier.</param>
        /// <param name="AdditionalInfo">The additional information.</param>
        /// <returns>I_UserInfo.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public I_UserInfo GetUserInfo(string TokenID, Dictionary<string, string> AdditionalInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks The Health Of The Class.  Use this to return missing configuration.  Invalid Permissions Etc..
        /// </summary>
        /// <returns>I_TestResult - Specifying changes needed to be made to obtain a good health report.</returns>
        public abstract I_TestResult HealthCheck();
        /// <summary>
        /// Imports the variable XML into the current class
        /// </summary>
        /// <param name="XML">XML Data to Import</param>
        /// <returns>true on success</returns>
        public abstract I_TestResult ImportXMLData(string XML);

        /// <summary>
        /// Validates A User Token
        /// </summary>
        /// <param name="TokenID">The token identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns><c>true</c> if [is token valid] [the specified token identifier]; otherwise, <c>false</c>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsTokenValid(string TokenID, Dictionary<string, string> AdditionalData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Logs the Error
        /// </summary>
        /// <param name="className">Name of the class the error occured</param>
        /// <param name="summary">Summary of the Error</param>
        /// <param name="ex">Exception</param>
        /// <param name="additionInformation">Additional Information</param>
        /// <param name="errorType">Error Type</param>
        public abstract void LogError(string className, string summary, Exception ex, string additionInformation, ErrorLevel errorType);

        /// <summary>
        /// Login the User
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="PassWord">The pass word.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <returns>I_LoginResult.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public I_LoginResult LoginUser(string UserName, string PassWord, Dictionary<string, string> AdditionalData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return a property value by name
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        /// <returns>System.Object.</returns>
        public abstract object ReturnProperty(string PropertyName);
        /// <summary>
        /// Returns the type of the property
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        /// <returns>Type.</returns>
        public abstract Type ReturnPropertyType(string PropertyName);
        /// <summary>
        /// Returns all the System Settings Required By The Plugin
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public abstract List<string> ReturnSystemSettingRequirements();
        /// <summary>
        /// Sets the Impersonation of the Executing User Level
        /// </summary>
        /// <param name="UserInfo">The user information.</param>
        public abstract void SetImpersonate(object UserInfo);
        /// <summary>
        /// Trys to set a property using the propertyname and the value. Case sensitive people.
        /// </summary>
        /// <param name="PropertyName">Case Sensitive Property Name</param>
        /// <param name="value">value</param>
        /// <returns>I_TestResult - Specifying if the Set was successfull</returns>
        public abstract I_TestResult SetProperty(string PropertyName, object value);
        /// <summary>
        /// Standard Text Replacement Functionality
        /// </summary>
        /// <param name="instr">The instr.</param>
        /// <param name="InputStandard">The input standard.</param>
        /// <returns>System.String.</returns>
        public abstract string StandardReplaceMent(string instr, RepacementStandard InputStandard);

        /// <summary>
        /// Update the User Info (Full User Authentication)
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="PassWord">The pass word.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <param name="UpdatedUserInfo">The updated user information.</param>
        /// <returns>I_TestResult.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public I_TestResult UpdateUserInfo(string UserName, string PassWord, Dictionary<string, string> AdditionalData, I_UserInfo UpdatedUserInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the User Info (Token Based Auth)
        /// </summary>
        /// <param name="TokenID">The token identifier.</param>
        /// <param name="AdditionalData">The additional data.</param>
        /// <param name="UpdatedUserInfo">The updated user information.</param>
        /// <returns>I_TestResult.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public I_TestResult UpdateUserInfo(string TokenID, Dictionary<string, string> AdditionalData, I_UserInfo UpdatedUserInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validates the plugin requirements.
        /// </summary>
        /// <returns>I_TestResult.</returns>
        public abstract I_TestResult ValidatePluginRequirements();
    }
}
