// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Elevation_Consts.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Consts.Windows.Security
{
    /// <summary>
    /// Class TOKENCONSTANTS.
    /// </summary>
    public class TOKENCONSTANTS
    {


        /// <summary>
        /// The standard rights required
        /// </summary>
        public const UInt32 STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        /// <summary>
        /// The standard rights read
        /// </summary>
        public const UInt32 STANDARD_RIGHTS_READ = 0x00020000;
        /// <summary>
        /// The token assign primary
        /// </summary>
        public const UInt32 TOKEN_ASSIGN_PRIMARY = 0x0001;
        /// <summary>
        /// The token duplicate
        /// </summary>
        public const UInt32 TOKEN_DUPLICATE = 0x0002;
        /// <summary>
        /// The token impersonate
        /// </summary>
        public const UInt32 TOKEN_IMPERSONATE = 0x0004;
        /// <summary>
        /// The token query
        /// </summary>
        public const UInt32 TOKEN_QUERY = 0x0008;
        /// <summary>
        /// The token query source
        /// </summary>
        public const UInt32 TOKEN_QUERY_SOURCE = 0x0010;
        /// <summary>
        /// The token adjust privileges
        /// </summary>
        public const UInt32 TOKEN_ADJUST_PRIVILEGES = 0x0020;
        /// <summary>
        /// The token adjust groups
        /// </summary>
        public const UInt32 TOKEN_ADJUST_GROUPS = 0x0040;
        /// <summary>
        /// The token adjust default
        /// </summary>
        public const UInt32 TOKEN_ADJUST_DEFAULT = 0x0080;
        /// <summary>
        /// The token adjust sessionid
        /// </summary>
        public const UInt32 TOKEN_ADJUST_SESSIONID = 0x0100;
        /// <summary>
        /// The token read
        /// </summary>
        public const UInt32 TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
        /// <summary>
        /// The token all access
        /// </summary>
        public const UInt32 TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED |
            TOKEN_ASSIGN_PRIMARY | TOKEN_DUPLICATE | TOKEN_IMPERSONATE |
            TOKEN_QUERY | TOKEN_QUERY_SOURCE | TOKEN_ADJUST_PRIVILEGES |
            TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT | TOKEN_ADJUST_SESSIONID);


        /// <summary>
        /// The error insufficient buffer
        /// </summary>
        public const Int32 ERROR_INSUFFICIENT_BUFFER = 122;

        /// <summary>
        /// Sets the elevation required state for a specified button or
        /// command link to display an elevated icon.
        /// </summary>
        public const UInt32 BCM_SETSHIELD = 0x160C;

        // Integrity Levels 

        /// <summary>
        /// The security mandatory untrusted rid
        /// </summary>
        public const Int32 SECURITY_MANDATORY_UNTRUSTED_RID = 0x00000000;
        /// <summary>
        /// The security mandatory low rid
        /// </summary>
        public const Int32 SECURITY_MANDATORY_LOW_RID = 0x00001000;
        /// <summary>
        /// The security mandatory medium rid
        /// </summary>
        public const Int32 SECURITY_MANDATORY_MEDIUM_RID = 0x00002000;
        /// <summary>
        /// The security mandatory high rid
        /// </summary>
        public const Int32 SECURITY_MANDATORY_HIGH_RID = 0x00003000;
        /// <summary>
        /// The security mandatory system rid
        /// </summary>
        public const Int32 SECURITY_MANDATORY_SYSTEM_RID = 0x00004000;
    }
}
