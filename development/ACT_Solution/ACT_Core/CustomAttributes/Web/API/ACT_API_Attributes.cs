// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_API_Attributes.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.CustomAttributes.Web.API
{
    /// <summary>
    /// Tags a Generic Class To Support API Definitions
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class APIDefinition : System.Attribute
    {
        /// <summary>
        /// NameStorage OverRide (INTERNAL ONLY)
        /// </summary>
        /// <value>The name over ride.</value>
        public string NameOverRide { get; set; }

        /// <summary>
        /// OverRide the Default Definition File Location
        /// </summary>
        /// <value>The definition file.</value>
        public string DefinitionFile { get; set; }

        /// <summary>
        /// Is this a public API
        /// </summary>
        /// <value><c>true</c> if public; otherwise, <c>false</c>.</value>
        public bool Public { get; set; }

        /// <summary>
        /// Advanced Authentication Supports
        /// </summary>
        /// <value><c>true</c> if [advanced authentication]; otherwise, <c>false</c>.</value>
        public bool AdvancedAuthentication { get; set; }

        /// <summary>
        /// Session Management Enabled
        /// </summary>
        /// <value><c>true</c> if [session management]; otherwise, <c>false</c>.</value>
        public bool SessionManagement { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public class ACTAuthorization : System.Attribute
    {        
        public string? AuthorizationRole { get; set; }
        public ACT.Core.Enums.AccessLevel AccessLevel { get; set; }
        public ACT.Core.Enums.ErrorLevel AccessDeniedSeverity { get; set; }


    }
}
