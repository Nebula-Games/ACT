// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-11-2019
// ***********************************************************************
// <copyright file="Enums.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Enums
{

    /// <summary>
    /// Mouse Events (ACT Standards)
    /// Enum MouseEvents
    /// </summary>
    public enum MouseEvents
    {
        /// <summary>
        /// The left mouse button click
        /// </summary>
        LeftMouseButtonClick = 0,
        /// <summary>
        /// The left mouse button drag
        /// </summary>
        LeftMouseButtonDrag = 1,
        /// <summary>
        /// The right mouse button click
        /// </summary>
        RightMouseButtonClick = 2,
        /// <summary>
        /// The right mouse button drag
        /// </summary>
        RightMouseButtonDrag = 3,
        /// <summary>
        /// The i os
        /// </summary>
        Hovor = 4
    }


    /// <summary>
    /// Defines Common Error Levels.
    /// Enum Other
    /// </summary>
    public enum ErrorLevel
    {
        /// <summary>
        /// The informational
        /// </summary>
        Informational = 0,
        /// <summary>
        /// Warning. Log Error. Take No Additional Action
        /// The warning
        /// </summary>
        Warning = 1,
        /// <summary>
        /// Severe: Log Error, Notify Admin
        /// The severe
        /// </summary>
        Severe = 2,
        /// <summary>
        /// Critical: Log Error, Notify Admin, Throw Exception
        /// The critical
        /// </summary>
        Critical = 3
    }

    /// <summary>
    /// Defines Operators that can be passed and saved as values
    /// Enum Operators
    /// </summary>
    public enum Operators
    {
        /// <summary>
        /// = 
        /// The equals
        /// </summary>
        Equals = 0,
        /// <summary>    
        /// The less than
        /// </summary>
        LessThan = 1,
        /// <summary>      
        /// The greater than
        /// </summary>
        GreaterThan = 2,
        /// <summary>
        /// Substring, % - %     
        /// Determines whether this instance contains the object.
        /// </summary>
        Contains = 4,
        /// <summary>
        /// X  10 and X  0      
        /// The with in
        /// </summary>
        WithIn = 8
    }

    /// <summary>
    /// Enum AccessLevel
    /// </summary>
    [Flags]
    public enum AccessLevel
    {
        /// <summary>
        /// The read
        /// </summary>
        /// <summary>
        /// The write
        /// </summary>
        /// <summary>
        /// The modify
        /// </summary>
        /// <summary>
        /// The custom
        /// </summary>
        /// <summary>
        /// The create
        /// </summary>
        /// <summary>
        /// The delete
        /// </summary>
        Read, Write, Modify, Custom, Create, Delete
    }

    /// <summary>
    /// Enum SecurityAccessError
    /// </summary>
    public enum SecurityAccessError
    {
        /// <summary>
        /// The frequent attempts
        /// </summary>
        FREQUENT_ATTEMPTS,
        /// <summary>
        /// The invalid credentials
        /// </summary>
        INVALID_CREDENTIALS,
        /// <summary>
        /// The security privledges
        /// </summary>
        SECURITY_PRIVLEDGES,
        /// <summary>
        /// The token failedtocreate
        /// </summary>
        TOKEN_FAILEDTOCREATE,
        /// <summary>
        /// The invalid apikey
        /// </summary>
        INVALID_APIKEY,
        /// <summary>
        /// The other
        /// </summary>
        OTHER
    }

    /// <summary>
    /// Enum RepacementStandard
    /// </summary>
    public enum RepacementStandard
    {
        /// <summary>
        /// The uppercase
        /// </summary>
        UPPERCASE = 1,
        /// <summary>
        /// The ignorecase
        /// </summary>
        IGNORECASE = 2,
        /// <summary>
        /// The usedelimeter
        /// </summary>
        USEDELIMETER = 3
    }

    /// <summary>
    /// Basic Randomization Options
    /// </summary>
    /// <summary>
    /// Enum RandomizationOptions
    /// </summary>
    public enum RandomizationOptions
    {
        /// <summary>
        /// Standard Randomization 
        /// </summary>
        /// <summary>
        /// The standard
        /// </summary>
        Standard,
        /// <summary>
        /// Crypto Based
        /// </summary>
        /// <summary>
        /// The crypto
        /// </summary>
        Crypto,
        /// <summary>
        /// ACT Special Random Engine Patent Pending
        /// </summary>
        /// <summary>
        /// The act special saucce
        /// </summary>
        ACTSpecialSaucce
    }

    /// <summary>
    /// Enum ISecurityProviderGenericResult
    /// </summary>
    public enum ISecurityProviderGenericResult
    {
        /// <summary>
        /// The email address registered already
        /// </summary>
        EmailAddressRegisteredAlready,
        /// <summary>
        /// The user name taken
        /// </summary>
        UserNameTaken,
        /// <summary>
        /// The password complexity invalid
        /// </summary>
        PasswordComplexityInvalid,
        /// <summary>
        /// The registration ok
        /// </summary>
        RegistrationOK,
        /// <summary>
        /// The un known error
        /// </summary>
        UnKnownError,
        /// <summary>
        /// The invalid API key
        /// </summary>
        InvalidAPIKey,
        /// <summary>
        /// The application cant create user
        /// </summary>
        ApplicationCantCreateUser,
        /// <summary>
        /// The user not associated to application
        /// </summary>
        UserNotAssociatedToApp,
        /// <summary>
        /// The action already exists
        /// </summary>
        ActionAlreadyExists,
        /// <summary>
        /// The access denied
        /// </summary>
        AccessDenied,
        /// <summary>
        /// The success
        /// </summary>
        Success
    }

    /// <summary>
    /// List of all Programming Languages
    /// </summary>
    public enum ProgrammingLanguages
    {
        /// <summary>
        /// C Sharp Microsoft .net
        /// </summary>
        CSharp = 0,
        /// <summary>
        /// Javascript Scripting Language
        /// </summary>
        JavaScript = 1,
        /// <summary>
        /// C Language
        /// </summary>
        C = 2,
        /// <summary>
        /// C Plus Plus Language
        /// </summary>
        CPlusPlus = 4
    }

    /// <summary>
    /// List Of Operating systems
    /// </summary>
    public enum OperatingSystems
    {
        /// <summary>
        /// Windows
        /// </summary>
        Windows = 0,
        /// <summary>
        /// All version of Linux
        /// </summary>
        Linux = 1,
        /// <summary>
        /// Mac OS
        /// </summary>
        MaxOS = 2,
        /// <summary>
        /// Android
        /// </summary>
        Android = 4,
        /// <summary>
        /// iOS
        /// </summary>
        iOS = 8,
        /// <summary>
        /// Other
        /// </summary>
        Other = 16
    }


}
