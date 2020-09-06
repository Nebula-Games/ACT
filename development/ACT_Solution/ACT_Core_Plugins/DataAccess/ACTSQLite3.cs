using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.Interfaces.Security.Authentication;
using ACT.Core.Interfaces.Security.Encryption;
using ACT.Plugins;
using System.Reflection;
using ACT.Core.Extensions;
using ACT.Core;
using ACT.Core.Enums;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace ACT.Plugins.DataAccess
{
    public class ACT_SQLite3 : ACT_Core, I_DataAccess
    {

        public ACT_SQLite3()
        {
            _ConnectionString = SystemSettings.GetSettingByName("DefaultConnectionString").Value;
        }

        #region Private Variables
        private SQLiteConnection _Connection = new SQLiteConnection();
        private SQLiteCommand _Command = new SQLiteCommand();
        //private SQLiteTransaction _Transaction = new SQLiteTransaction();
        private string _ConnectionString;
      //  private int _CommandTimeoutInSeconds = -1;
     //   private bool _TransactionStarted = false;

        #endregion

        #region Not Implemented Methods

        public I_QueryResult InsertData(string TableName, Dictionary<string, object> FieldsAndValues)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult UpdateData(string TableName, Dictionary<string, object> FieldsAndValues, I_DbWhereStatement Where)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult DeleteData(string TableName, I_DbWhereStatement Where)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult DuplicateRow(string TableName, I_DbWhereStatement Where)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult DuplicateRow(string TableName, I_DbWhereStatement Where, int Number)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult InsertData(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult UpdateData(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues, I_DbWhereStatement Where)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult UpdateData(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult DeleteData(I_DbTable Table, I_DbWhereStatement Where)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult DeleteData(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues, bool RecursiveDelete)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult DuplicateRow(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult DuplicateRow(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues, int Number)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult GetTableData(I_DbTable Table, I_DbWhereStatement Where)
        {
            throw new NotImplementedException();
        }

        public void CreateTableFromICoreObject(object Class, out string SQL, bool AutoExecute = false, bool CreateIdentity = true, bool Recursive = false)
        {
            throw new NotImplementedException();
        }
        public I_UserInfo AuthenticateUser(string UserName, string Password)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(I_UserInfo User)
        {
            throw new NotImplementedException();
        }

        public bool ModifyTable(I_DbTable OriginalTable, I_DbTable NewTable)
        {
            throw new NotImplementedException();
        }

        public bool CreateTable(I_DbTable NewTable)
        {
            throw new NotImplementedException();
        }

        public bool DropTable(I_DbTable TableToDrop)
        {
            throw new NotImplementedException();
        }

        public string GetStoredSQLQuery(string Name, string GroupName)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Database Connection Methods
        /// <summary>
        /// Opens the Connection using the Set Connection String
        /// </summary>
        /// <returns>True if Connection False If Not</returns>
        public bool Open()
        {
            return Open(_ConnectionString, true);
        }

        /// <summary>
        /// Opens the Specified Connection String
        /// </summary>
        /// <param name="ConnectionString">DB Connection String - Encrypts this value once connected.</param>
        /// <returns></returns>
        public bool Open(string ConnectionString)
        {
            return Open(ConnectionString, true);
        }

        /// <summary>
        /// Opens the specified Connection
        /// </summary>
        /// <param name="ConnectionString">DB Connection String</param>
        /// <param name="EncryptConnectionString">Do you want to Encrypt the Connection String Using Internal Encrypt Methods?</param>
        /// <returns>True if Connected</returns>
        public bool Open(string ConnectionString, bool EncryptConnectionString)
        {
            _ConnectionString = ConnectionString;

            _Connection.ConnectionString = _ConnectionString;

            if (_Connection == null) { _Connection = new SQLiteConnection(); }

            if (_Connection.State == System.Data.ConnectionState.Closed) { _Connection.Open(); }

            int _TryCount = 0;
        TryAgain:

            if (_Connection.State == System.Data.ConnectionState.Connecting)
            {
                _TryCount++;

                if (_TryCount > 10) { _Connection.Dispose(); return false; }
                // Sleep for 1/10 second to give file system time to process message que.
                System.Threading.Thread.Sleep(100);
                goto TryAgain;
            }

            if (_Connection.State == System.Data.ConnectionState.Open)
            {
                _Command.Connection = _Connection;
                if (EncryptConnectionString)
                {
                    I_Encryption _Encryptor = CurrentCore<I_Encryption>.GetCurrent();
                    _ConnectionString = _Encryptor.Encrypt(_ConnectionString);
                }
                return true;
            }
            else
            {
                _Connection.Dispose();
                LogError(this.GetType().FullName, "General Error Opening Database.", null, _ConnectionString, ErrorLevel.Informational);

                return false;
            }


        }

        /// <summary>
        /// Test if the Current Connection is Open
        /// </summary>
        public bool Connected
        {
            get
            {
                if (_Connection.State == System.Data.ConnectionState.Open)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns the currently stored Connection String.  By default this value is encrypted
        /// </summary>
        public string ConnectionString
        {
            get { return _ConnectionString; }
        }

        public void SetCommandTimeout(int Seconds)
        {
            _Command.CommandTimeout = Seconds;
        }
        #endregion

        #region Helper Methods
        public string GenerateWhereStatement(I_DbWhereStatement Where, string WhereStatement)
        {
            if (Where == null) { return "1=1"; };
            if (WhereStatement != null && WhereStatement != "")
            {
                if (Where.UseAND)
                {
                    WhereStatement += " AND ";
                }
                else
                {
                    WhereStatement += " OR ";
                }
            }
            else
            {
                WhereStatement = " Where ";
            }

            if (Where.HasChildren)
            {
                WhereStatement += " (";
            }


            if (Where.StatementOperators == Operators.Equals)
            {
                WhereStatement += "[" + Where.ColumnName + "] = @" + Where.ColumnName;
            }
            else if (Where.StatementOperators == Operators.GreaterThan)
            {
                WhereStatement += "[" + Where.ColumnName + "] > @" + Where.ColumnName;
            }
            else if (Where.StatementOperators == Operators.LessThan)
            {
                WhereStatement += "[" + Where.ColumnName + "] < @" + Where.ColumnName;
            }
            else if (Where.StatementOperators == Operators.Contains)
            {
                WhereStatement += "[" + Where.ColumnName + "] Like '%' + @" + Where.ColumnName + " + '%'";
            }

            if (Where.HasChildren)
            {
                foreach (var w in Where.Children)
                {
                    WhereStatement = GenerateWhereStatement(w, WhereStatement);
                }
                WhereStatement += ")";
            }

            if (String.IsNullOrEmpty(WhereStatement))
            {
                return "1=1";
            }
            else
            {
                return WhereStatement;
            }
        }
        public List<IDataParameter> GenerateWhereStatementParameters(I_DbWhereStatement Where)
        {
            if (Where == null) { return null; }

            List<System.Data.IDataParameter> _TmpReturn = new List<IDataParameter>();

            var t = new System.Data.SqlClient.SqlParameter("@" + Where.ColumnName, Where.Value);
            t.DbType = Where.ColumnDataType;

            _TmpReturn.Add(t);

            foreach (var _ChildWhere in Where.Children)
            {
                var _TList = GenerateWhereStatementParameters(_ChildWhere);

                _TmpReturn.AddRange(_TList);
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Disposes the Current Connection and Command
        /// </summary>
        private void DisposeObjects()
        {
            try
            {
                _Connection.Dispose();
                _Command.Dispose();
            }
            catch { }
        }
        #endregion

        #region Execute Query Commands
        public I_QueryResult RunCommand(string CommandText, List<IDataParameter> Params, bool ReturnsRows, CommandType CmdType)
        {
            I_QueryResult _TmpReturn = ACT.Core.CurrentCore<I_QueryResult>.GetCurrent();

            try
            {
                using (SQLiteCommand _Command = new SQLiteCommand(_Connection))
                {
                    _Command.CommandText = CommandText;
                    _Command.CommandType = CmdType;
                    _Command.Parameters.Clear();
                    _Command.Connection = _Connection;

                    if (Params != null)
                    {
                        foreach (var p in Params) { _Command.Parameters.Add(p); }
                    }

                    if (ReturnsRows == true)
                    {
                        var _TmpDataTable = GetDataTableFromReader(_Command.ExecuteReader(System.Data.CommandBehavior.Default));

                        _TmpReturn.Tables.Add(_TmpDataTable);
                    }
                    else
                    {
                        _TmpReturn.RecordsEffected.Add(_Command.ExecuteNonQuery());
                    }
                }
            }
            catch (Exception ex)
            {
                _TmpReturn.Tables.Add(new DataTable());
                _TmpReturn.Exceptions.Add(ex);
                _TmpReturn.Errors.Add(true);
            }
            return _TmpReturn;
        }

        public I_QueryResult RunCommand(List<string> CommandTexts, List<List<IDataParameter>> Params, List<bool> ReturnsRows, List<CommandType> CmdTypes)
        {
            I_QueryResult _TmpReturn = ACT.Core.CurrentCore<I_QueryResult>.GetCurrent();

            for (int cx = 0; cx < CommandTexts.Count(); cx++)
            {
                try
                {
                    using (SQLiteCommand _Command = new SQLiteCommand(_Connection))
                    {
                        _Command.CommandText = CommandTexts[cx];
                        _Command.CommandType = CmdTypes[cx];
                        _Command.Parameters.Clear();
                        _Command.Connection = _Connection;

                        if (Params[cx] != null)
                        {
                            foreach (var p in Params[cx]) { _Command.Parameters.Add(p); }
                        } 
                        

                        if (ReturnsRows[cx] == true)
                        {
                            var _TmpDataTable = GetDataTableFromReader(_Command.ExecuteReader(System.Data.CommandBehavior.Default));
                            _TmpReturn.Tables.Add(_TmpDataTable);
                        }
                        else
                        {
                            _TmpReturn.RecordsEffected.Add(_Command.ExecuteNonQuery());
                        }
                    }
                }
                catch (Exception ex)
                {
                    _TmpReturn.Exceptions.Add(ex);
                }
            }

            return _TmpReturn;
        }

        public I_QueryResult RunCommand(List<string> CommandTexts, List<List<IDataParameter>> Params, List<bool> ReturnsRows, bool UseTransactions, bool AutoRollback, List<CommandType> CmdTypes)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Transaction Support Code
        /// <summary>
        /// Start a Transaction
        /// </summary>
        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Trys to Commit The Transaction
        /// </summary>
        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attempts to Rollback the Transaction
        /// </summary>
        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Export Database Command
        private System.Data.DbType GetType(string TypeName)
        {
            if (TypeName.ToLower()=="integer")
            {
                return System.Data.DbType.Int64;
            }
            else if (TypeName.ToLower() == "int")
            {
                return System.Data.DbType.Int32;
            }
            else if (TypeName.ToLower() == "bigint")
            {
                return System.Data.DbType.Int64;
            }
            else if (TypeName.ToLower() == "boolean")
            {
                return System.Data.DbType.Boolean;
            }
            else if (TypeName.ToLower() == "text")
            {
                return System.Data.DbType.String;
            }
            else if (TypeName.ToLower() == "blob")
            {
                return System.Data.DbType.Object;
            }
            else
            {
                return DbType.String;
            }
        }

        public I_Db ExportDatabase()
        {
            if (Connected == false)
            {
                LogError(this.GetType().FullName, "Error Exporting Database. Database is not connected.", null, "", ErrorLevel.Warning);
                throw new Exception("Error Exporting Database. Database is not connected.");
            }
            
            I_Db _DB = CurrentCore<ACT.Core.Interfaces.DataAccess.I_Db>.GetCurrent();

            var _TableQuery = RunCommand("SELECT name FROM sqlite_master WHERE type='table'", null, true, CommandType.Text);

            foreach (System.Data.DataRow Row in _TableQuery.Tables[0].Rows)
            {
                I_DbTable _Table = CurrentCore<ACT.Core.Interfaces.DataAccess.I_DbTable>.GetCurrent();

                _Table.ParentDatabase = _DB;
                _Table.Name = Row["Name"].ToString();
                _Table.ShortName = Row["Name"].ToString();
                _Table.Owner = "";
                _Table.Description = "";

                var _ColumnQuery = RunCommand("pragma table_info(" + Row["Name"].ToString() + ");", null, true, CommandType.Text);

                foreach (System.Data.DataRow colRow in _ColumnQuery.Tables[0].Rows)
                {
                    I_DbColumn _TmpDBColumn = CurrentCore<ACT.Core.Interfaces.DataAccess.I_DbColumn>.GetCurrent();

                    _TmpDBColumn.DataType = GetType(colRow["type"].ToString());
                    _TmpDBColumn.Default = colRow["dflt_value"].ToString();
                    _TmpDBColumn.Description = "";
                    _TmpDBColumn.Name = colRow["name"].ToString();
                    _TmpDBColumn.Nullable = !colRow["notnull"].ToBool().Value;
                    _TmpDBColumn.ShortName = colRow["name"].ToString();
                    _TmpDBColumn.IsPrimaryKey = colRow["pk"].ToBool().Value;

                    _Table.Columns.Add(_TmpDBColumn);
                }

                _DB.Tables.Add(_Table);
            }

            return _DB;
        }
        #endregion

        #region IPlugin Methods

        /// <summary>
        /// Returns a List of SystemSettingRequirements
        /// </summary>
        /// <returns></returns>
        public override List<string> ReturnSystemSettingRequirements()
        {
            List<string> _tmpReturn = new List<string>();
            _tmpReturn.Add("DefaultConnectionString");
            _tmpReturn.Add("MSSQLColumnInfoQuery");
            _tmpReturn.Add("SuperFKQuery");
            return _tmpReturn;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Overrides and Implements the Dispose Method.
        /// </summary>
        public override void Dispose()
        {
            if (_Connection != null)
            {
                if (_Connection.State != System.Data.ConnectionState.Closed)
                {
                    _Connection.Close();
                }
            }

            try
            {
                _Command.Dispose();
            }
            catch { }
            
            try
            {
                _Connection.Dispose();
            }
            catch { }
        }

        #endregion
                     
        private static System.Data.DataTable GetDataTableFromReader(SQLiteDataReader Reader)
        {
            System.Data.DataTable _TmpReturn;
            using (ACT.Core.Helper.Database.InternalDataAdapter _IDA = new ACT.Core.Helper.Database.InternalDataAdapter())
            {
                _TmpReturn = _IDA.ConvertToDataTable((System.Data.IDataReader)Reader);

            }

            return _TmpReturn;
        }

        public I_QueryResult ExecuteBulkInsert(DataTable BulkData, string TableName, SqlBulkCopyOptions BulkCopyOptions)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult ExecuteBulkInsert(DataTable BulkData, string TableName, SqlBulkCopyOptions BulkCopyOptions, bool addColumnMapping)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute a Stored Procedure Return the Type Indicated from the Column Indicated.
        /// </summary>
        /// <typeparam name="T">Return Type - MUST BE VALUE TYPE ATM</typeparam>
        /// <param name="Name">Stored Procedure Name</param>
        /// <param name="Parameters">SQL Parameters</param>
        /// <param name="ColumnToReturn">Column Ordinal Position to return</param>
        /// <returns>T Single Value</returns>
        public T ExecuteDynamicProcedure<T>(string Name, Dictionary<string, object> Parameters, int ColumnToReturn = 0)
        {
            if (typeof(T).IsValueType == false) { throw new InvalidExpressionException(typeof(T).ToString() + " MUST BE A VALUE TYPE"); }

            object _tempReturn = null;

            using (var DataAccess = ACT.Core.CurrentCore<I_DataAccess>.GetCurrent())
            {
                List<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();

                #region Param Values
                foreach (string key in Parameters.Keys) { _Params.Add(new System.Data.SqlClient.SqlParameter(key, Parameters[key])); }
                #endregion

                var _Result = RunCommand(Name, _Params, true, System.Data.CommandType.StoredProcedure);

                if (_Result.Exceptions[0] != null) { throw _Result.Exceptions[0]; }
                if (_Result.FirstDataTable_WithRows() == null) { return default(T); }
                if (_Result.FirstDataTable_WithRows().Rows.Count != 1) { return default(T); }
                if (_Result.FirstDataTable_WithRows().Columns.Count < (ColumnToReturn + 1)) { throw new Exception("Invalid Column Position"); }
                _tempReturn = _Result.FirstDataTable_WithRows().Rows[0][ColumnToReturn];
            }

            if (typeof(T).ToString() == "System.Guid") { _tempReturn = Guid.Parse(_tempReturn.ToString()); }

            return (T)_tempReturn;
        }
    }
}
