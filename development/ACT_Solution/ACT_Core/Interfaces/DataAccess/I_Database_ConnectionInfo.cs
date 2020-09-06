// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Database_ConnectionInfo.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace ACT.Core.Interfaces.DataAccess
{
    /// <summary>
    /// ConnectionInfo Represents a Database Connection To Any Database
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    public interface I_Database_ConnectionInfo : Common.I_Plugin
    {
        /// <summary>
        /// Gets or sets the type of the database.
        /// </summary>
        /// <value>The type of the database.</value>
        Enums.Database.DatabaseTypes DBType { get; set; }
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        string Provider { get; set; }
        /// <summary>
        /// Gets or sets the initial catalog.
        /// </summary>
        /// <value>The initial catalog.</value>
        string InitialCatalog { get; set; }
        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        string DataSource { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        string UserID { get; set; }
        /// <summary>
        /// Gets or sets the encrypted password.
        /// </summary>
        /// <value>The encrypted password.</value>
        string EncryptedPassword { get; set; }
        /// <summary>
        /// Gets or sets the default collection.
        /// </summary>
        /// <value>The default collection.</value>
        string DefaultCollection { get; set; }
        /// <summary>
        /// Gets or sets the driver.
        /// </summary>
        /// <value>The driver.</value>
        string Driver { get; set; }
        /// <summary>
        /// Gets or sets the odbckey.
        /// </summary>
        /// <value>The odbckey.</value>
        string ODBCKEY { get; set; }
        /// <summary>
        /// Gets or sets the odbcke y2.
        /// </summary>
        /// <value>The odbcke y2.</value>
        string ODBCKEY2 { get; set; }
        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>The platform.</value>
        string Platform { get; set; }
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        string Server { get; set; }
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        string Port { get; set; }
        /// <summary>
        /// Gets or sets the integrated security.
        /// </summary>
        /// <value>The integrated security.</value>
        string IntegratedSecurity { get; set; }
        /// <summary>
        /// Gets or sets the connection timeout.
        /// </summary>
        /// <value>The connection timeout.</value>
        string ConnectionTimeout { get; set; }
        /// <summary>
        /// Gets or sets the size of the incr pool.
        /// </summary>
        /// <value>The size of the incr pool.</value>
        string IncrPoolSize { get; set; }
        /// <summary>
        /// Gets or sets the size of the decr pool.
        /// </summary>
        /// <value>The size of the decr pool.</value>
        string DecrPoolSize { get; set; }
        /// <summary>
        /// Gets or sets the minimum size of the pool.
        /// </summary>
        /// <value>The minimum size of the pool.</value>
        string MinPoolSize { get; set; }
        /// <summary>
        /// Gets or sets the maximum size of the pool.
        /// </summary>
        /// <value>The maximum size of the pool.</value>
        string MaxPoolSize { get; set; }
        /// <summary>
        /// Gets or sets the connection lifetime.
        /// </summary>
        /// <value>The connection lifetime.</value>
        string ConnectionLifetime { get; set; }
        /// <summary>
        /// Gets or sets the dba privilege.
        /// </summary>
        /// <value>The dba privilege.</value>
        string DBAPrivilege { get; set; }
        /// <summary>
        /// Gets or sets the trusted connection.
        /// </summary>
        /// <value>The trusted connection.</value>
        string TrustedConnection { get; set; }
        /// <summary>
        /// Gets or sets the attach database filename.
        /// </summary>
        /// <value>The attach database filename.</value>
        string AttachDbFilename { get; set; }
        /// <summary>
        /// Gets or sets the failover partner.
        /// </summary>
        /// <value>The failover partner.</value>
        string Failover_Partner { get; set; }
        /// <summary>
        /// Gets or sets the asynchronous processing.
        /// </summary>
        /// <value>The asynchronous processing.</value>
        string Asynchronous_Processing { get; set; }
        /// <summary>
        /// Gets or sets the user instance.
        /// </summary>
        /// <value>The user instance.</value>
        string User_Instance { get; set; }
        /// <summary>
        /// Gets or sets the size of the packet.
        /// </summary>
        /// <value>The size of the packet.</value>
        string PacketSize { get; set; }
        /// <summary>
        /// Gets or sets the langcode.
        /// </summary>
        /// <value>The langcode.</value>
        string Langcode { get; set; }
        /// <summary>
        /// Gets or sets the codepage.
        /// </summary>
        /// <value>The codepage.</value>
        string Codepage { get; set; }
        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>The domain.</value>
        string Domain { get; set; }
        /// <summary>
        /// Gets or sets the other data.
        /// </summary>
        /// <value>The other data.</value>
        Dictionary<string,string> OtherData { get; set; }
    }
}
