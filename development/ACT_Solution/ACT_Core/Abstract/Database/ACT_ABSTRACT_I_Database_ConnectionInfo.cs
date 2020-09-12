// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_ABSTRACT_I_Database_ConnectionInfo.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Enums;
using ACT.Core.Enums.Database;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.Authentication;

namespace ACT.Core.Abstract.Database
{
    /// <summary>
    /// Abstract Connection Info Class
    /// </summary>
    /// <summary>
    /// Class ACT_ABSTRACT_I_Database_ConnectionInfo.
    /// Implements the <see cref="ACT.Core.Interfaces.DataAccess.I_Database_ConnectionInfo" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.DataAccess.I_Database_ConnectionInfo" />
    public abstract class ACT_ABSTRACT_I_Database_ConnectionInfo : Interfaces.DataAccess.I_Database_ConnectionInfo
    {
        /// <summary>
        /// Gets or sets the type of the database.
        /// </summary>
        /// <value>The type of the database.</value>
        public DatabaseTypes DBType { get; set; }
        /// <summary>
        /// Gets or sets the name of the database connection.
        /// </summary>
        /// <value>The name of the database connection.</value>
        public string DatabaseConnectionName { get; set; }
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public string Provider { get; set; }
        /// <summary>
        /// Gets or sets the initial catalog.
        /// </summary>
        /// <value>The initial catalog.</value>
        public string InitialCatalog { get; set; }
        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public string DataSource { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; set; }
        /// <summary>
        /// Gets or sets the encrypted password.
        /// </summary>
        /// <value>The encrypted password.</value>
        public string EncryptedPassword { get; set; }
        /// <summary>
        /// Gets or sets the default collection.
        /// </summary>
        /// <value>The default collection.</value>
        public string DefaultCollection { get; set; }
        /// <summary>
        /// Gets or sets the driver.
        /// </summary>
        /// <value>The driver.</value>
        public string Driver { get; set; }
        /// <summary>
        /// Gets or sets the odbckey.
        /// </summary>
        /// <value>The odbckey.</value>
        public string ODBCKEY { get; set; }
        /// <summary>
        /// Gets or sets the odbcke y2.
        /// </summary>
        /// <value>The odbcke y2.</value>
        public string ODBCKEY2 { get; set; }
        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>The platform.</value>
        public string Platform { get; set; }
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public string Server { get; set; }
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public string Port { get; set; }
        /// <summary>
        /// Gets or sets the integrated security.
        /// </summary>
        /// <value>The integrated security.</value>
        public string IntegratedSecurity { get; set; }
        /// <summary>
        /// Gets or sets the connection timeout.
        /// </summary>
        /// <value>The connection timeout.</value>
        public string ConnectionTimeout { get; set; }
        /// <summary>
        /// Gets or sets the size of the incr pool.
        /// </summary>
        /// <value>The size of the incr pool.</value>
        public string IncrPoolSize { get; set; }
        /// <summary>
        /// Gets or sets the size of the decr pool.
        /// </summary>
        /// <value>The size of the decr pool.</value>
        public string DecrPoolSize { get; set; }
        /// <summary>
        /// Gets or sets the minimum size of the pool.
        /// </summary>
        /// <value>The minimum size of the pool.</value>
        public string MinPoolSize { get; set; }
        /// <summary>
        /// Gets or sets the maximum size of the pool.
        /// </summary>
        /// <value>The maximum size of the pool.</value>
        public string MaxPoolSize { get; set; }
        /// <summary>
        /// Gets or sets the connection lifetime.
        /// </summary>
        /// <value>The connection lifetime.</value>
        public string ConnectionLifetime { get; set; }
        /// <summary>
        /// Gets or sets the dba privilege.
        /// </summary>
        /// <value>The dba privilege.</value>
        public string DBAPrivilege { get; set; }
        /// <summary>
        /// Gets or sets the trusted connection.
        /// </summary>
        /// <value>The trusted connection.</value>
        public string TrustedConnection { get; set; }
        /// <summary>
        /// Gets or sets the attach database filename.
        /// </summary>
        /// <value>The attach database filename.</value>
        public string AttachDbFilename { get; set; }
        /// <summary>
        /// Gets or sets the failover partner.
        /// </summary>
        /// <value>The failover partner.</value>
        public string Failover_Partner { get; set; }
        /// <summary>
        /// Gets or sets the asynchronous processing.
        /// </summary>
        /// <value>The asynchronous processing.</value>
        public string Asynchronous_Processing { get; set; }
        /// <summary>
        /// Gets or sets the user instance.
        /// </summary>
        /// <value>The user instance.</value>
        public string User_Instance { get; set; }
        /// <summary>
        /// Gets or sets the size of the packet.
        /// </summary>
        /// <value>The size of the packet.</value>
        public string PacketSize { get; set; }
        /// <summary>
        /// Gets or sets the langcode.
        /// </summary>
        /// <value>The langcode.</value>
        public string Langcode { get; set; }
        /// <summary>
        /// Gets or sets the codepage.
        /// </summary>
        /// <value>The codepage.</value>
        public string Codepage { get; set; }
        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>The domain.</value>
        public string Domain { get; set; }
        /// <summary>
        /// Gets or sets the other data.
        /// </summary>
        /// <value>The other data.</value>
        public Dictionary<string, string> OtherData { get; set; }
        /// <summary>
        /// Specifies if the class has changed in any way
        /// </summary>
        /// <value><c>true</c> if this instance has changed; otherwise, <c>false</c>.</value>
        public bool HasChanged { get; set; }

        /// <summary>
        /// Get all of the Public Properties in the class
        /// </summary>
        /// <value>The public properties.</value>
        /// <exception cref="NotImplementedException"></exception>
        public List<string> PublicProperties => throw new NotImplementedException();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Exports the current class to XML
        /// </summary>
        /// <returns>XML Representation of class</returns>
        /// <exception cref="NotImplementedException"></exception>
        public string ExportXMLData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the Errors stored in the local variable
        /// </summary>
        /// <returns><![CDATA[List<Exception>]]></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Exception> GetErrors()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks The Health Of The Class.  Use this to return missing configuration.  Invalid Permissions Etc..
        /// </summary>
        /// <returns>I_TestResult - Specifying changes needed to be made to obtain a good health report.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public I_TestResult HealthCheck()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Imports the variable XML into the current class
        /// </summary>
        /// <param name="XML">XML Data to Import</param>
        /// <returns>true on success</returns>
        /// <exception cref="NotImplementedException"></exception>
        public I_TestResult ImportXMLData(string XML)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Logs an Error
        /// </summary>
        /// <param name="className"></param>
        /// <param name="summary"></param>
        /// <param name="ex"></param>
        /// <param name="additionInformation"></param>
        /// <param name="errorType"></param>
        public virtual void LogError(string className, string summary, Exception ex, string additionInformation, ErrorLevel errorType)
        {
            var _CurrentErrorLogger = ACT.Core.CurrentCore<ACT.Core.Interfaces.Common.I_ErrorLoggable>.GetCurrent();
            _CurrentErrorLogger.LogError(className, summary, ex, additionInformation, errorType);
        }

        /// <summary>
        /// Return a property value by name
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ReturnProperty(string PropertyName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the type of the property
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        /// <returns>Type.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Type ReturnPropertyType(string PropertyName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return System Setting Requirements
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> ReturnSystemSettingRequirements()
        {
            return new List<string>() { DatabaseConnectionName };
        }

        /// <summary>
        /// Sets the Impersonation of the Executing User Level
        /// </summary>
        /// <param name="UserInfo">The user information.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetImpersonate(object UserInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Trys to set a property using the propertyname and the value. Case sensitive people.
        /// </summary>
        /// <param name="PropertyName">Case Sensitive Property Name</param>
        /// <param name="value">value</param>
        /// <returns>I_TestResult - Specifying if the Set was successfull</returns>
        /// <exception cref="NotImplementedException"></exception>
        public I_TestResult SetProperty(string PropertyName, object value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Standard Text Replacement Functionality
        /// </summary>
        /// <param name="instr">The instr.</param>
        /// <param name="InputStandard">The input standard.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public string StandardReplaceMent(string instr, RepacementStandard InputStandard)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validates the plugin requirements.
        /// </summary>
        /// <returns>I_TestResult.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public I_TestResult ValidatePluginRequirements()
        {
            throw new NotImplementedException();
        }
    }
}
