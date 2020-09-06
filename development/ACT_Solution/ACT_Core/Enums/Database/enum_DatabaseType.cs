// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="enum_DatabaseType.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Enums.Database
{
    /// <summary>
    /// Enum DatabaseTypes
    /// </summary>
    public enum DatabaseTypes
    {
        /// <summary>
        /// The MSSQL
        /// </summary>
        MSSQL = 1,
        /// <summary>
        /// The msaccess
        /// </summary>
        MSACCESS = 2,
        /// <summary>
        /// The oracle
        /// </summary>
        ORACLE = 3,
        /// <summary>
        /// The mysql
        /// </summary>
        MYSQL = 4,
        /// <summary>
        /// The postgresql
        /// </summary>
        POSTGRESQL = 5,
        /// <summary>
        /// The mongodb
        /// </summary>
        MONGODB = 6,
        /// <summary>
        /// The sqlite
        /// </summary>
        SQLITE = 7,
        /// <summary>
        /// The redis
        /// </summary>
        REDIS = 8,
        /// <summary>
        /// The elasticsearch
        /// </summary>
        ELASTICSEARCH = 9,
        /// <summary>
        /// The mariadb
        /// </summary>
        MARIADB = 10,
        /// <summary>
        /// The azuredb
        /// </summary>
        AZUREDB = 11,
        /// <summary>
        /// The memcached
        /// </summary>
        MEMCACHED = 12,
        /// <summary>
        /// The amazondynamodb
        /// </summary>
        AMAZONDYNAMODB = 13,
        /// <summary>
        /// The amazonrdsaurora
        /// </summary>
        AMAZONRDSAURORA = 14,
        /// <summary>
        /// The cassandra
        /// </summary>
        CASSANDRA = 15,
        /// <summary>
        /// The ibmd b2
        /// </summary>
        IBMDB2 = 16,
        /// <summary>
        /// The ne o4 j
        /// </summary>
        NEO4J = 17,
        /// <summary>
        /// The amazonredshift
        /// </summary>
        AMAZONREDSHIFT = 18        
    }
}
