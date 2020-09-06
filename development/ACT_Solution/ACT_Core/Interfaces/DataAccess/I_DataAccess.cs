// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_DataAccess.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.Authentication;
using ACT.Core.Interfaces.Security.UserData;

namespace ACT.Core.Interfaces.DataAccess
{
    /// <summary>
    /// Represents the DataAccess Class
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    public interface I_DataAccess : I_Plugin
    {
        /// <summary>
        /// Execute a Stored Procedure Return the Type Indicated from the Column Indicated.
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="Name">Stored Procedure Name</param>
        /// <param name="Parameters">SQL Parameters</param>
        /// <param name="ColumnToReturn">Column Ordinal Position to return</param>
        /// <returns>T Single Value</returns>
        T ExecuteDynamicProcedure<T>(string Name, Dictionary<string, object> Parameters, int ColumnToReturn = 0);


        /// <summary>
        /// Create Table from ICoreObject - creates All the Columns based on Properties in the ICore Object.
        /// </summary>
        /// <param name="Class">The class.</param>
        /// <param name="SQL">The SQL.</param>
        /// <param name="AutoExecute">if set to <c>true</c> [automatic execute].</param>
        /// <param name="CreateIdentity">if set to <c>true</c> [create identity].</param>
        /// <param name="Recursive">if set to <c>true</c> [recursive].</param>
        void CreateTableFromICoreObject(object Class, out string SQL, bool AutoExecute = false, bool CreateIdentity = true, bool Recursive = false);


        /// <summary>
        /// Generates The Where Statement Based On the IDbWhereStatement
        /// </summary>
        /// <param name="Where">The where.</param>
        /// <param name="WhereStatement">The where statement.</param>
        /// <returns>System.String.</returns>
        string GenerateWhereStatement(I_DbWhereStatement Where, string WhereStatement);

        /// <summary>
        /// Generates a list of Parameters use in the SQL Action
        /// </summary>
        /// <param name="Where">The where.</param>
        /// <returns>List&lt;System.Data.IDataParameter&gt;.</returns>
        List<System.Data.IDataParameter> GenerateWhereStatementParameters(I_DbWhereStatement Where);

        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <returns>I_UserInfo.</returns>
        I_UserInfo AuthenticateUser(string UserName, string Password);
        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool AuthenticateUser(I_UserInfo User);


        /// <summary>
        /// Performs a Database Action
        /// </summary>
        /// <param name="CommandText">SQL Command to Execute</param>
        /// <param name="Params">Params to Pass - System.Data.IDataParameter</param>
        /// <param name="ReturnsRows">Trap the return tables?</param>
        /// <param name="CmdType">Type of Command - System.Data.CommandType</param>
        /// <returns>IQueryResult</returns>
        I_QueryResult RunCommand(string CommandText, List<System.Data.IDataParameter> Params, bool ReturnsRows, System.Data.CommandType CmdType);
        /// <summary>
        /// Performs a Database Action.  All Indexes must match- i.e Command[0] with Param[0]
        /// </summary>
        /// <param name="CommandTexts">SQL Commands to Execute</param>
        /// <param name="Params">List of a List of Params to Pass - System.Data.IDataParameter</param>
        /// <param name="ReturnsRows">List of Trap the return tables?</param>
        /// <param name="CmdTypes">The command types.</param>
        /// <returns>IQueryResult</returns>
        I_QueryResult RunCommand(List<string> CommandTexts, List<List<System.Data.IDataParameter>> Params, List<bool> ReturnsRows, List<System.Data.CommandType> CmdTypes);
        /// <summary>
        /// Performs a Database Action.  All Indexes must match- i.e Command[0] with Param[0]
        /// </summary>
        /// <param name="CommandTexts">SQL Commands to Execute</param>
        /// <param name="Params">List of a List of Params to Pass - System.Data.IDataParameter</param>
        /// <param name="ReturnsRows">List of Trap the return tables?</param>
        /// <param name="UseTransactions">This will wrap the Commands in 1 Transaction</param>
        /// <param name="AutoRollback">This will auto rollback the transaction on a failer</param>
        /// <param name="CmdTypes">The command types.</param>
        /// <returns>IQueryResult</returns>
        I_QueryResult RunCommand(List<string> CommandTexts, List<List<System.Data.IDataParameter>> Params, List<bool> ReturnsRows, bool UseTransactions, bool AutoRollback, List<System.Data.CommandType> CmdTypes);

        /// <summary>
        /// Executes the bulk insert.
        /// </summary>
        /// <param name="BulkData">The bulk data.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="BulkCopyOptions">The bulk copy options.</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult ExecuteBulkInsert(System.Data.DataTable BulkData, string TableName, System.Data.SqlClient.SqlBulkCopyOptions BulkCopyOptions);

        /// <summary>
        /// Executes the bulk insert.
        /// </summary>
        /// <param name="BulkData">The bulk data.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="BulkCopyOptions">The bulk copy options.</param>
        /// <param name="addColumnMapping">if set to <c>true</c> [add column mapping].</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult ExecuteBulkInsert(System.Data.DataTable BulkData, string TableName, System.Data.SqlClient.SqlBulkCopyOptions BulkCopyOptions, bool addColumnMapping);

        /// <summary>
        /// Open the default connection.  Defined in SystemConfigurationFile
        /// </summary>
        /// <returns>Success Result</returns>
        bool Open();

        /// <summary>
        /// Open a Specific Connection.
        /// </summary>
        /// <param name="ConnectionString">Connection String</param>
        /// <returns>Success Result</returns>
        bool Open(string ConnectionString);

        /// <summary>
        /// Open a Specific Connection.
        /// </summary>
        /// <param name="ConnectionString">Connection String</param>
        /// <param name="EncryptConnectionString">Set to true if you want to Encrypt the Connection String using the default settings in configuration file</param>
        /// <returns>Success Result</returns>
        bool Open(string ConnectionString, bool EncryptConnectionString);

        /// <summary>
        /// Returns true if Database Connection is Active
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        bool Connected { get; }

        /// <summary>
        /// Returns the Connection String.
        /// </summary>
        /// <value>The connection string.</value>
        string ConnectionString { get; }

        /// <summary>
        /// Gets the stored SQL query.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="GroupName">Name of the group.</param>
        /// <returns>System.String.</returns>
        string GetStoredSQLQuery(string Name, string GroupName);

        /// <summary>
        /// Start a Transaction
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commit A Transaction
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Roll Current Transaction Back
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Sets the Current Command Timeout In Seconds
        /// </summary>
        /// <param name="Seconds">The seconds.</param>
        void SetCommandTimeout(int Seconds);

        /// <summary>
        /// Export the Current Database
        /// </summary>
        /// <returns>I_Db.</returns>
        I_Db ExportDatabase();

        /// <summary>
        /// Modify Table
        /// </summary>
        /// <param name="OriginalTable">Original Table Structure</param>
        /// <param name="NewTable">New Table Structure</param>
        /// <returns>True on Success</returns>
        bool ModifyTable(I_DbTable OriginalTable, I_DbTable NewTable);

        /// <summary>
        /// Create A Table
        /// </summary>
        /// <param name="NewTable">New Table Structure</param>
        /// <returns>True on Success</returns>
        bool CreateTable(I_DbTable NewTable);

        /// <summary>
        /// Drop / Remove a Table
        /// </summary>
        /// <param name="TableToDrop">Table To Drop</param>
        /// <returns>True on Success</returns>
        bool DropTable(I_DbTable TableToDrop);

        /// <summary>
        /// Inserts Data Into the Table
        /// </summary>
        /// <param name="TableName">Table Name To Insert Data Into</param>
        /// <param name="FieldsAndValues">Fields and Values To Insert</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult InsertData(string TableName, Dictionary<string, object> FieldsAndValues);

        /// <summary>
        /// Updates the data in the table specified
        /// </summary>
        /// <param name="TableName">Table To Update data In</param>
        /// <param name="FieldsAndValues">Fields And Values</param>
        /// <param name="Where">Where statement defining scope of update</param>
        /// <returns>IQueryResult</returns>
        I_QueryResult UpdateData(string TableName, Dictionary<string, object> FieldsAndValues, I_DbWhereStatement Where);


        /// <summary>
        /// Deletes the data in the table specified
        /// </summary>
        /// <param name="TableName">Table To Delete data From</param>
        /// <param name="Where">Where statement defining scope of delete</param>
        /// <returns>IQueryResult</returns>
        I_QueryResult DeleteData(string TableName, I_DbWhereStatement Where);


        /// <summary>
        /// Duplicates the row.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="Where">The where.</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult DuplicateRow(string TableName, I_DbWhereStatement Where);
        /// <summary>
        /// Duplicates the row.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="Where">The where.</param>
        /// <param name="Number">The number.</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult DuplicateRow(string TableName, I_DbWhereStatement Where, int Number);



        /// <summary>
        /// Inserts the data.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="FieldsAndValues">The fields and values.</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult InsertData(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues);
        /// <summary>
        /// Updates the data.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="FieldsAndValues">The fields and values.</param>
        /// <param name="Where">The where.</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult UpdateData(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues, I_DbWhereStatement Where);
        /// <summary>
        /// Updates the data.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="FieldsAndValues">The fields and values.</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult UpdateData(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues);
        /// <summary>
        /// Deletes the data.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="Where">The where.</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult DeleteData(I_DbTable Table, I_DbWhereStatement Where);
        /// <summary>
        /// Special Function.  You can use the to clean databases when you are really deleting items.  FK Erros would occur otherwise
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="FieldsAndValues">The fields and values.</param>
        /// <param name="RecursiveDelete">if set to <c>true</c> [recursive delete].</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult DeleteData(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues, bool RecursiveDelete);
        /// <summary>
        /// Duplicates the row.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="FieldsAndValues">The fields and values.</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult DuplicateRow(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues);
        /// <summary>
        /// Duplicates the row.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="FieldsAndValues">The fields and values.</param>
        /// <param name="Number">The number.</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult DuplicateRow(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues, int Number);
        /// <summary>
        /// Gets the table data.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="Where">The where.</param>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult GetTableData(I_DbTable Table, I_DbWhereStatement Where);
    }   
   
}
