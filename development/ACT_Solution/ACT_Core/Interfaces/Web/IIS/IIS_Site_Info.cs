// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="IIS_Site_Info.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Web.IIS
{
    /// <summary>
    /// Class IISSiteInfo.
    /// </summary>
    public class IISSiteInfo
    {
        /// <summary>
        /// The site name
        /// </summary>
        public string SiteName;
        /// <summary>
        /// The application pool name
        /// </summary>
        public string AppPoolName;
        /// <summary>
        /// The site identifier
        /// </summary>
        public string SiteID;
        /// <summary>
        /// The application name
        /// </summary>
        public string AppName;
        /// <summary>
        /// The directories
        /// </summary>
        public string Directories;
    }
}
