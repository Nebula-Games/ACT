// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Template_Engine_Settings.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Enums.TemplateEngine;
using ACT.Core.Extensions;

namespace ACT.Core.TemplateEngine
{
    /// <summary>
    /// Template Engine Settings - Used When Initializing The Template Engine
    /// </summary>
    public class Template_Engine_Settings
    {
        #region Properties

        /// <summary>
        /// Use Database
        /// </summary>
        /// <value><c>true</c> if [use database]; otherwise, <c>false</c>.</value>
        public bool UseDatabase { get; set; }

        /// <summary>
        /// ApplicationID if NULL only use templates with NULL ApplicationID
        /// </summary>
        /// <value>The application identifier.</value>
        public Guid ApplicationID { get; set; }

        /// <summary>
        /// Use Cache - cache Tempaltes , Template Data, Parsed Template Etc..
        /// CacheMethod determines scope
        /// </summary>
        /// <value><c>true</c> if [use cache]; otherwise, <c>false</c>.</value>
        public bool UseCache { get; set; }

        /// <summary>
        /// Cache Methods - Scope of the Cache
        /// </summary>
        /// <value>The cache method.</value>
        public CacheMethod CacheMethod { get; set; }

        /// <summary>
        /// Cache Levels Are Defined as Follows
        /// 1 - Template From Source Raw
        /// 2 - Querystring Based Initial Load/Parse (No Post / Callbacks)
        /// 4 - Viewstate Tracker (Expirimental)
        /// 8 - Unique ID Tracker
        /// </summary>
        /// <value>The cache level.</value>
        public int CacheLevel { get; set; }

        /// <summary>
        /// Base folder for File System Based Templates
        /// </summary>
        /// <value>The base folder.</value>
        public string BaseFolder { get; set; }

        /// <summary>
        /// Database Connection Name
        /// </summary>
        /// <value>The name of the database connection.</value>
        public string DatabaseConnectionName { get; set; }

        /// <summary>
        /// Database Table Prefix
        /// </summary>
        /// <value>The database table prefix.</value>
        public string DatabaseTablePrefix { get; set; }

        /// <summary>
        /// Use ACT Cloud as the Source
        /// </summary>
        /// <value><c>true</c> if [use act cloud]; otherwise, <c>false</c>.</value>
        public bool UseACTCloud { get; set; }

        /// <summary>
        /// ACT Connection Information - JSON Object with server, port, service login etc.
        /// </summary>
        /// <value>The act connection information.</value>
        public string ACTConnectionInformation { get; set; }

        /// <summary>
        /// Store Instance Inside ACT
        /// </summary>
        /// <value><c>true</c> if [store instance inside act]; otherwise, <c>false</c>.</value>
        public bool StoreInstanceInsideACT { get; set; }

        #endregion

        #region Methods 

        /// <summary>
        /// Basic Constructor
        /// </summary>
        public Template_Engine_Settings()
        {
            UseDatabase = false;
#if DEBUG
            UseCache = false;
#else
            UseCache = true;
#endif
            CacheMethod = CacheMethod.Memory;
            CacheLevel = 1;
            BaseFolder = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat();
            DatabaseConnectionName = "";
            DatabaseTablePrefix = "";
            UseACTCloud = false;
            ACTConnectionInformation = "";
            StoreInstanceInsideACT = false;
        }

        #endregion

    }
}
