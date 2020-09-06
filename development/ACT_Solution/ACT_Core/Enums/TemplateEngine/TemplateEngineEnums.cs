// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="TemplateEngineEnums.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Enums.TemplateEngine
{
    /// <summary>
    /// Cache Methods Available
    /// </summary>
    public enum CacheMethod
    {
        /// <summary>
        /// Database caching
        /// </summary>
        Database = 1,
        /// <summary>
        /// DLL Caching using external DLL for Caching
        /// </summary>
        DLL = 2,
        /// <summary>
        /// Use Internal Caching
        /// </summary>
        Memory = 4
    }

    /// <summary>
    /// Enum PackageType
    /// </summary>
    public enum PackageType
    {
        /// <summary>
        /// Nebula Template Standard Package
        /// </summary>
        NGT = 1,
        /// <summary>
        /// Nebula Database Template Package
        /// </summary>
        DBNGT = 2,
        /// <summary>
        /// Nebula Custom Package Definition - Plugin Based Execution
        /// </summary>
        CUSTOM = 3
    }
          

    /// <summary>
    /// Enum TemplateType
    /// </summary>
    public enum TemplateType
    {
        /// <summary>
        /// Advanced Nebula Template
        /// </summary>
        NGT = 1,
        /// <summary>
        /// Simple Nebula Template
        /// </summary>
        SIMPLE = 2,
        /// <summary>
        /// Web Platform Specific Structure
        /// </summary>
        WEBPLATFORM = 3,
        /// <summary>
        /// Reporting Engine Structure
        /// </summary>
        REPORTENGINE = 4,
        /// <summary>
        /// Query Engine Structure
        /// </summary>
        QUERYENGINE=5,
        /// <summary>
        /// Email Engine Structure
        /// </summary>
        EMAILENGINE=6,
        /// <summary>
        /// Javascript Engine
        /// </summary>
        JAVASCRIPTENGINE = 7,
        /// <summary>   An enum constant representing the other option. </summary>
        OTHER = 8000
    }
}
