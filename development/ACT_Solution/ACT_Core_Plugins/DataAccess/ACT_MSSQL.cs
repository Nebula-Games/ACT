using ACT.Core;
using ACT.Core.Enums;
using ACT.Core.Extensions;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.Interfaces.Security.Authentication;
using ACT.Core.Interfaces.Security.Encryption;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ACT.Plugins.DataAccess
{

    /// <summary>
    /// SQL Server IDataAccess Class
    /// </summary>
    public partial class ACT_MSSQL : ACT_Core, I_DataAccess
    {
        /// <summary>
        /// Get Views
        /// </summary>
        /// <param name="_DB"></param>
        public void GetViews(I_Db _DB)
        {
            #region Tables
            string[] restrictions = new string[4];

            // Catalog
            restrictions[0] = _DB.Name;
            // Owner - We want All
            restrictions[1] = null;
            // Table - We want all, so null
            restrictions[2] = null;
            // Table Type - Only Views
            restrictions[3] = "View";

            System.Data.DataTable _Schema = _Connection.GetSchema("Tables", restrictions);

            foreach (System.Data.DataRow _SchemaDataRows in _Schema.Rows)
            {
                string _TmpTableOwner = _SchemaDataRows["TABLE_SCHEMA"].ToString();
                string _TmpTableShortName = _SchemaDataRows["TABLE_NAME"].ToString();

                string _TmpTableName = "[" + _DB.Name + "].[" + _TmpTableOwner + "].[" + _TmpTableShortName + "]";
                string _TmpTableMediumName = "[" + _TmpTableOwner + "].[" + _TmpTableShortName + "]";
                string _TmpTableDescription = "";

                #region Retrieve The SQL Table Description

                string _TableDescSQL = "SELECT  distinct	u.name + '.' + t.name AS [table], td.value AS [table_desc],tb.[modify_date]    	FROM    	sysobjects t INNER JOIN  sysusers u     ON		u.uid = t.uid INNER JOIN sys.tables tb ON tb.[object_id] = t.[id] LEFT OUTER JOIN sys.extended_properties td    ON		td.major_id = t.id    AND 	td.minor_id = 0    AND		td.name = 'MS_Description' WHERE t.type = 'v' and '[' + u.name + '].[' + t.name +']' = '" + _TmpTableMediumName + "'";

                _Command = new System.Data.SqlClient.SqlCommand();
                _Command.Connection = _Connection;
                _Command.CommandType = System.Data.CommandType.Text;

                _Command.CommandText = _TableDescSQL;
                int _tmpAge = -1;

                using (System.Data.SqlClient.SqlDataReader _DescriptionDr = _Command.ExecuteReader())
                {
                    using (System.Data.DataTable _TableDescriptionTable = _DescriptionDr.ToDataTable())
                    {
                        if (_TableDescriptionTable.Rows.Count == 1)
                        {
                            _TmpTableDescription = _TableDescriptionTable.Rows[0]["table_desc"].ToString();
                            try
                            {
                                var _modDate = (_TableDescriptionTable.Rows[0]["modify_date"].ToDateTime() - DateTime.Now);
                                _tmpAge = (Math.Abs(_modDate.Value.Days));
                            }
                            catch { }
                        }
                    }
                }

                #endregion


                if (!_TmpTableShortName.StartsWith("sys"))
                {
                    I_DbView _Table = CurrentCore<ACT.Core.Interfaces.DataAccess.I_DbView>.GetCurrent();

                    _Table.ParentDatabase = _DB;
                    _Table.Name = _TmpTableName;
                    _Table.ShortName = _TmpTableShortName;
                    _Table.Owner = _TmpTableOwner;
                    _Table.Description = _TmpTableDescription;
                    _Table.AgeInDays = _tmpAge;

                    #region Load All Columns

                    _Command = new System.Data.SqlClient.SqlCommand();
                    _Command.Connection = _Connection;
                    _Command.CommandType = System.Data.CommandType.Text;
                    string _TmpSQL = SystemSettings.GetSettingByName("MSSQLColumnInfoQuery").Value;
                    _TmpSQL = _TmpSQL.Replace("#TABLENAME#", _Table.ShortName);
                    _TmpSQL = _TmpSQL.Replace("#OWNER#", _TmpTableOwner);
                    _TmpSQL = _TmpSQL.Replace("From sys.tables st", "From sys.views st");

                    _Command.CommandText = _TmpSQL;

                    System.Data.SqlClient.SqlDataReader _dr = _Command.ExecuteReader();

                    System.Data.DataTable _SchemaTable = _dr.ToDataTable();
                    _dr.Close();

                    foreach (System.Data.DataRow _SchemaDataRow in _SchemaTable.Rows)
                    {
                        I_DbColumn _TmpDBColumn = CurrentCore<ACT.Core.Interfaces.DataAccess.I_DbColumn>.GetCurrent();

                        _TmpDBColumn.Name = (string)_SchemaDataRow["COLUMN_NAME"];

                        if ((int)_SchemaDataRow["IDENTITYCOLUMN"] == 1)
                        {
                            _TmpDBColumn.AutoIncrement = true;
                        }
                        else
                        {
                            _TmpDBColumn.AutoIncrement = false;
                        }
                        _TmpDBColumn.Description = Convert.ToString(_SchemaDataRow["DESCRIPTION"]);

                        _TmpDBColumn.DataType = _SchemaDataRow["DATA_TYPE"].ToString().ToDbType();

                        try
                        {
                            string _TmpDefault = _SchemaDataRow["COLUMN_DEFAULT"].ToString();
                            if (_TmpDefault.NullOrEmpty() == false)
                            {
                                _TmpDBColumn.Default = _TmpDefault.Substring(1, _TmpDefault.Length - 2);
                            }
                            else
                            {
                                _TmpDBColumn.Default = "";
                            }
                        }

                        catch { _TmpDBColumn.Default = ""; }


                        if ((int)_SchemaDataRow["IDENTITYCOLUMN"] == 1)
                        {
                            _TmpDBColumn.Identity = true;
                        }
                        else
                        {
                            _TmpDBColumn.Identity = false;
                        }


                        _TmpDBColumn.IdentityIncrement = (int)_SchemaDataRow["IDENTITYINCREMENT"];
                        _TmpDBColumn.IdentitySeed = (int)_SchemaDataRow["IDENTITYSEED"];

                        //_TmpDBColumn.IsPrimaryKey Is Set Below in the Next Function

                        // Name is Set First

                        if ((int)_SchemaDataRow["NULLABLE"] == 1)
                        {
                            _TmpDBColumn.Nullable = true;
                        }
                        else
                        {
                            _TmpDBColumn.Nullable = false;
                        }

                        _TmpDBColumn.Precision = (int)_SchemaDataRow["COLUMN_PRECISION"];
                        _TmpDBColumn.Scale = (int)_SchemaDataRow["COLUMN_SCALE"];
                        _TmpDBColumn.ShortName = _TmpDBColumn.Name;
                        _TmpDBColumn.Size = (int)_SchemaDataRow["COLUMN_LENGTH"];

                        _Table.AddColumn(_TmpDBColumn);
                    }
                    #endregion



                    _DB.AddView(_Table);
                }
            }
            #endregion


        }

        /// <summary>
        /// Create Table From ICoreObject
        /// </summary>
        /// <param name="Class"></param>
        /// <param name="SQL"></param>
        /// <param name="AutoExecute"></param>
        /// <param name="CreateIdentity"></param>
        /// <param name="Recursive"></param>
        public void CreateTableFromICoreObject(object Class, out string SQL, bool AutoExecute = false, bool CreateIdentity = true, bool Recursive = false)
        {
            if (_Connection.State == ConnectionState.Open)
            {
                ACT.Core.Interfaces.I_Core _ICoreObj = (ACT.Core.Interfaces.I_Core)Class;
                string _TableName = Class.GetType().ToString().Replace(".", "_");

                string _AppendedSQL = "";

                string _SQL = "Create Table " + _TableName + "\n\r";
                _SQL += "([PKID] [uniqueidentifier] NOT NULL, \n\r";

                foreach (string _prop in _ICoreObj.PublicProperties)
                {
                    Type _tmpProp = _ICoreObj.ReturnPropertyType(_prop);

                    // IGNORE UNFAMILER TYPES FOR NOW
                    try
                    {
                        _SQL += "[" + _prop + "] " + _tmpProp.ToSQLDataType(true) + " NULL,";
                    }
                    catch
                    {
                        if (_tmpProp.IsGenericType)
                        {
                            var _listtype = _tmpProp.GetGenericArguments();
                            try
                            {
                                if (_listtype.Count() > 1) { throw new Exception("Only Generic Lists are Supported"); }

                                var _o = Activator.CreateInstance(_listtype[0]);
                                if (_o is ACT.Core.Interfaces.I_Core)
                                {
                                    string _tmps;
                                    CreateTableFromICoreObject(_o, out _tmps, false, true, true);
                                    _AppendedSQL += _tmps;
                                }
                            }
                            catch
                            {
                                // IGNORE ODD CLASSES NOT DEFINED BY ICORE
                            }
                        }
                        else
                        {
                            try
                            {
                                var _o = Activator.CreateInstance(_tmpProp);
                                if (_o is ACT.Core.Interfaces.I_Core)
                                {
                                    string _tmps;
                                    CreateTableFromICoreObject(_o, out _tmps, false, true, true);
                                    _AppendedSQL += _tmps;
                                }
                            }
                            catch
                            {
                                // IGNORE ODD CLASSES NOT DEFINED BY ICORE
                            }
                        }
                    }
                }
                if (CreateIdentity)
                {
                    _SQL += "\n\rCONSTRAINT [PK_" + Class.GetType().ToString() + " PRIMARY KEY CLUSTERED\n\r";
                    _SQL += "(\n\r";
                    _SQL += "[PKID] ASC\n\r";
                    _SQL += ") WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]\n\r";
                }
                else
                {
                    _SQL = _SQL.Trim().TrimEnd(",".ToCharArray());
                }

                _SQL += ") ON [PRIMARY]\n\rGO\n\r";

                _SQL += "ALTER TABLE " + Class.GetType().ToString() + " ADD  CONSTRAINT [DF_" + Class.GetType().ToString() + "_ID]  DEFAULT (newid()) FOR [PKID] \n\r GO";

                if (AutoExecute)
                {
                    RunCommand(_SQL, null, false, CommandType.Text);
                }

                SQL = _SQL + _AppendedSQL;
            }
            else { throw new Exception("Data Connection Not Open.  Call Open(*) First"); }


        }

        public I_UserInfo AuthenticateUser(string UserName, string Password)
        {
            return null;
        }
        public bool AuthenticateUser(I_UserInfo UserInfo)
        {
            return false;
        }

        internal List<I_DbDataType> GetDataTypes()
        {
            List<string> _IgnoreList = new List<string>();
            try
            {
                _IgnoreList = ACT.Core.SystemSettings.GetSettingByName("GlobalIgnoreList").Value.SplitString(",", StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            }
            catch
            {
            }

            List<I_DbDataType> _TmpReturn = new List<I_DbDataType>();

            string _GetTypesQuery = "select * from sys.types";

            var QR = RunCommand(_GetTypesQuery, null, true, CommandType.Text);

            /// Loop through Each Stored Procedure Found
            foreach (DataRow TR in QR.Tables[0].Rows)
            {
                var _TmpType = ACT.Core.CurrentCore<I_DbDataType>.GetCurrent();

                _TmpType.IsNullable = (bool)TR["is_nullable"];
                _TmpType.IsTableType = (bool)TR["is_table_type"];
                _TmpType.IsUserType = (bool)TR["is_user_defined"];
                _TmpType.MaxLength = Convert.ToInt32(TR["max_length"]);
                _TmpType.Precision = Convert.ToInt32(TR["precision"]);
                _TmpType.Scale = Convert.ToInt32(TR["scale"]);
                _TmpType.SystemTypeID = Convert.ToInt32(TR["system_type_id"]);
                _TmpType.UserTypeID = Convert.ToInt32(TR["user_type_id"]);
                _TmpType.Name = TR["name"].ToString();

                _TmpReturn.Add(_TmpType);
            }

            return _TmpReturn;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets stored procedures. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/4/2019. </remarks>
        ///
        /// <param name="ExistingDB">   The existing database. </param>
        ///
        /// <returns>   The stored procedures. </returns>
        ///-------------------------------------------------------------------------------------------------

        internal List<I_DbStoredProcedure> GetStoredProcedures(I_Db ExistingDB)
        {
            List<string> _IgnoreList = new List<string>();
            try
            {
                _IgnoreList = ACT.Core.SystemSettings.GetSettingByName("GlobalIgnoreList").Value.SplitString(",", StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            }
            catch
            {
            }

            List<I_DbStoredProcedure> _TmpReturn = new List<I_DbStoredProcedure>();

            string _GetProceduresQuery = "SELECT * FROM sys.procedures";

            var QR = RunCommand(_GetProceduresQuery, null, true, CommandType.Text);

            /// Loop through Each Stored Procedure Found
            foreach (DataRow TR in QR.Tables[0].Rows)
            {
                var ProcDetailsQR = RunCommand("exec sp_Help " + TR["name"].ToString(), null, true, CommandType.Text);

                var _TmpProc = ACT.Core.CurrentCore<I_DbStoredProcedure>.GetCurrent();

                _TmpProc.Name = ProcDetailsQR.Tables[0].Rows[0]["Name"].ToString();
                _TmpProc.Owner = ProcDetailsQR.Tables[0].Rows[0]["Owner"].ToString();
                int _tmpAge = -1;
                try
                {
                    var _modDate = (TR["modify_date"].ToDateTime() - DateTime.Now);
                    _tmpAge = (Math.Abs(_modDate.Value.Days));
                }
                catch { }
                _TmpProc.AgeInDays = _tmpAge;


                if (_IgnoreList.Contains(_TmpProc.Name)) { continue; }

                if (ProcDetailsQR.Tables.Count == 2)
                {
                    foreach (DataRow ProcParamsR in ProcDetailsQR.Tables[1].Rows)
                    {

                        var _TmpProcParam = ACT.Core.CurrentCore<I_DbStoredProcedureParameter>.GetCurrent();

                        _TmpProcParam.Name = ProcParamsR["Parameter_name"].ToString();
                        _TmpProcParam.Length = ProcParamsR["Length"].ToString().ToInt(0);
                        string _BaseDataType = ProcParamsR["Type"].ToString();

                        string _BaseColumnTypeName = "";

                        var _FoundType = ExistingDB.Types.Where(x => x.Name.ToLower() == _BaseDataType.ToLower());

                        if (ExistingDB.Types.Where(x => x.Name.ToLower() == _BaseDataType.ToLower()).Count() == 1)
                        {
                            var _FoundTypeSingle = _FoundType.First();

                            if (_FoundTypeSingle.IsTableType == true)
                            {
                                _BaseColumnTypeName = "DataTable";
                            }
                            else if (_FoundTypeSingle.IsUserType == true)
                            {
                                var _SecondFoundType = ExistingDB.Types.Where(x => x.SystemTypeID == _FoundTypeSingle.SystemTypeID && x.IsUserType == false).First();

                                _BaseColumnTypeName = _SecondFoundType.Name;
                            }
                            else
                            {
                                _BaseColumnTypeName = _FoundTypeSingle.Name;
                            }
                        }
                        else
                        {
                            _BaseColumnTypeName = _BaseDataType;
                        }


                        _TmpProcParam.DataTypeName = _BaseColumnTypeName;
                        _TmpProcParam.DataType = _BaseColumnTypeName.ToDbType();
                        _TmpProcParam.Order = ProcParamsR["Param_Order"].ToString().ToInt(0);

                        _TmpProc.Parameters.Add(_TmpProcParam);
                    }
                }

                string _getProcComments = "exec sp_helptext '" + _TmpProc.Name + "'";

                var _ProcText = RunCommand(_getProcComments, null, true, CommandType.Text).FirstDataTable_WithRows();

                if (_ProcText != null)
                {
                    string _comment = "";
                    string _procRaw = "";
                    foreach (DataRow rowData in _ProcText.Rows)
                    {
                        _procRaw += rowData[0];                        
                    }

                    int _startI = _procRaw.IndexOf("-- intellisense-start");
                    if (_startI > -1)
                    {
                        int _endI = _procRaw.IndexOf("-- intellisense-end", _startI);
                        if (_endI > -1)
                        {
                            _comment = _procRaw.Substring(_startI, _endI - _startI);
                            _comment = _comment.Replace("-- intellisense-start", "");
                            _comment = _comment.Replace("--", "");
                            _comment = _comment.Replace(Environment.NewLine, Environment.NewLine + "///");
                            _comment = _comment.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
                        }
                    }
                    
                    _TmpProc.Comments = _comment;
                }
                _TmpReturn.Add(_TmpProc);
            }


            return _TmpReturn;

        }

        /// <summary>
        /// Generate Where Statement Parameters
        /// </summary>
        /// <param name="Where"></param>
        /// <returns></returns>
        public List<System.Data.IDataParameter> GenerateWhereStatementParameters(I_DbWhereStatement Where)
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
        /// Returns a Where Statement Appened to the WhereStatement Passed In
        /// </summary>
        /// <param name="WhereStatement"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public string GenerateWhereStatement(I_DbWhereStatement Where, string WhereStatement = "")
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


        private static Dictionary<string, I_Db> _DatabaseCache = new Dictionary<string, I_Db>();

        private int _CommandTimeoutInSeconds = -1;

        private string _ConnectionString = "";

        private System.Data.SqlClient.SqlConnection _Connection = new System.Data.SqlClient.SqlConnection();
        private System.Data.SqlClient.SqlCommand _Command = new System.Data.SqlClient.SqlCommand();
        private System.Data.SqlClient.SqlTransaction _Transaction;
        private bool _TransactionStarted = false;

        /// <summary>
        /// Generic Constructor.  Attempts to Set the Connection String to the Setting Name "DefaultConnectionString"
        /// </summary>     
        /// <exception cref="InvalidOperationException">When the Connection String is Missing or Blank</exception>
        public ACT_MSSQL()
        {
            _ConnectionString = SystemSettings.GetSettingByName("DefaultConnectionString").Value;

            //    if (_ConnectionString == "")
            //    {
            //       throw new InvalidOperationException("Error Locating DefaultConnectionString");
            //   }
        }

        #region IDataAccess Members

        public void SetCommandTimeout(int Seconds)
        {
            _CommandTimeoutInSeconds = Seconds;
        }

        /// <summary>
        /// Runs a  SQL Command Against a Database
        /// </summary>
        /// <param name="CommandText">Command Text</param>
        /// <param name="Params">List of Params</param>
        /// <param name="ReturnsRows">Return Rows Test</param>
        /// <param name="CmdType">Command Type</param>
        /// <returns>IQueryResult</returns>
        public I_QueryResult RunCommand(string CommandText, List<IDataParameter> Params, bool ReturnsRows, CommandType CmdType)
        {
            _Command = new System.Data.SqlClient.SqlCommand();
            _Command.Connection = _Connection;
            _Command.Parameters.Clear();
            _Command.CommandType = CmdType;
            _Command.CommandText = CommandText;

            if (_CommandTimeoutInSeconds != -1)
            {
                _Command.CommandTimeout = _CommandTimeoutInSeconds;
            }

            if (Params != null)
            {
                foreach (System.Data.IDataParameter _Param in Params)
                {

                    System.Data.SqlDbType _DbType = _Param.DbType.ToSQLDataType();

                    System.Data.SqlClient.SqlParameter _NewParam = new System.Data.SqlClient.SqlParameter(_Param.ParameterName, _DbType);
                    _NewParam.Direction = _Param.Direction;
                    _NewParam.Value = _Param.Value;

                    _Command.Parameters.Add(_NewParam);
                }
            }

            I_QueryResult _QR = CurrentCore<ACT.Core.Interfaces.DataAccess.I_QueryResult>.GetCurrent();

            var s = (from x in _Command.Parameters.Cast<System.Data.IDataParameter>() select x).ToList<System.Data.IDataParameter>();

            _QR.Params.Add(s);

            try
            {
                if (ReturnsRows)
                {
                    System.Data.SqlClient.SqlDataReader _readertemp = _Command.ExecuteReader();

                    _QR.Tables.Add(_readertemp.ToDataTable());

                    while (_readertemp.NextResult())
                    {
                        _QR.Tables.Add(_readertemp.ToDataTable());
                    }

                    _readertemp.Close();
                    _readertemp.Dispose();
                    _QR.RecordsEffected.Add(-1);
                }
                else
                {
                    if (_Command.CommandText.Contains("SCOPE_IDENTITY()"))
                    {
                        object _ResultIdent = _Command.ExecuteScalar();
                        _QR.IdentitiesCaptured.Add(_ResultIdent.ToString());
                        _QR.RecordsEffected.Add(1);
                    }
                    else
                    {
                        _QR.RecordsEffected.Add(_Command.ExecuteNonQuery());
                    }
                }

                _QR.Exceptions.Add(null);
                _QR.Errors.Add(false);
            }
            catch (Exception ex)
            {
                LogError(this.GetType().FullName, "Error Executing Command (" + _Command.CommandText + ")", ex, "", ErrorLevel.Severe);
                _QR.Exceptions.Add(ex);
                _QR.Errors.Add(true);
            }

            _Command.Dispose();

            return _QR;
        }

        /// <summary>
        /// Runs a List of SQL Command Against a Database
        /// </summary>
        /// <param name="CommandTexts">List of Command Texts</param>
        /// <param name="Params">List of a List of Params</param>
        /// <param name="ReturnsRows">List of Return Rows Test</param>
        /// <param name="CmdTypes">List of Command Types</param>
        /// <returns>IQueryResult</returns>
        public I_QueryResult RunCommand(List<string> CommandTexts, List<List<IDataParameter>> Params, List<bool> ReturnsRows, List<CommandType> CmdTypes)
        {
            I_QueryResult _ReturnQR = CurrentCore<ACT.Core.Interfaces.DataAccess.I_QueryResult>.GetCurrent();

            if (CommandTexts.Count == Params.Count && ReturnsRows.Count == CmdTypes.Count && CommandTexts.Count == ReturnsRows.Count)
            { }
            else
            {
                throw new Exception("Item count error!  The number of items do not match in all of the parameters.");
            }

            for (int _ItemCount = 0; _ItemCount < CommandTexts.Count; _ItemCount++)
            {
                I_QueryResult _QR = RunCommand(CommandTexts[_ItemCount], Params[_ItemCount], ReturnsRows[_ItemCount], CmdTypes[_ItemCount]);

                _ReturnQR.Errors.Add(_QR.Errors[0]);
                _ReturnQR.Exceptions.Add(_QR.Exceptions[0]);
                _ReturnQR.Tables.Add(_QR.Tables[0]);
                _ReturnQR.IdentitiesCaptured.Add(_QR.IdentitiesCaptured[0]);
                _ReturnQR.RecordsEffected.Add(_QR.RecordsEffected[0]);
            }

            return _ReturnQR;
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

        /// <summary>
        /// Runs a SQL Command Against a Database
        /// </summary>
        /// <param name="CommandTexts">List of Command Texts</param>
        /// <param name="Params">List of Params</param>
        /// <param name="ReturnsRows">List of Return Rows Test</param>
        /// <param name="UseTransactions">Use Transactions for this List of Commands</param>
        /// <param name="AutoRollback">Rollback Transaction on Failure</param>
        /// <param name="CmdTypes">List of Command Types</param>
        /// <returns>IQueryResult</returns>
        public I_QueryResult RunCommand(List<string> CommandTexts, List<List<IDataParameter>> Params, List<bool> ReturnsRows, bool UseTransactions, bool AutoRollback, List<CommandType> CmdTypes)
        {
            I_QueryResult _ReturnQR = CurrentCore<ACT.Core.Interfaces.DataAccess.I_QueryResult>.GetCurrent();

            if (CommandTexts.Count == Params.Count && ReturnsRows.Count == CmdTypes.Count && CommandTexts.Count == ReturnsRows.Count)
            { }
            else
            {
                throw new Exception("Item count error!  The number of items do not match in all of the parameters.");
            }

            if (UseTransactions) { if (!_TransactionStarted) { BeginTransaction(); } }

            for (int _ItemCount = 0; _ItemCount < CommandTexts.Count; _ItemCount++)
            {
                I_QueryResult _QR = RunCommand(CommandTexts[_ItemCount], Params[_ItemCount], ReturnsRows[_ItemCount], CmdTypes[_ItemCount]);

                _ReturnQR.Errors.Add(_QR.Errors[0]);
                _ReturnQR.Exceptions.Add(_QR.Exceptions[0]);
                _ReturnQR.Tables.Add(_QR.Tables[0]);
                _ReturnQR.IdentitiesCaptured.Add(_QR.IdentitiesCaptured[0]);
                _ReturnQR.RecordsEffected.Add(_QR.RecordsEffected[0]);
            }

            if (UseTransactions)
            {
                if (AutoRollback)
                {
                    if (_ReturnQR.Errors.Contains(true)) { RollbackTransaction(); _ReturnQR.RolledBack = true; }
                    else
                    {
                        CommitTransaction();
                        _ReturnQR.Commited = true;
                    }
                }

            }

            return _ReturnQR;
        }

        public I_QueryResult ExecuteBulkInsert(System.Data.DataTable BulkData, string TableName = null, System.Data.SqlClient.SqlBulkCopyOptions BulkCopyOptions = System.Data.SqlClient.SqlBulkCopyOptions.TableLock | System.Data.SqlClient.SqlBulkCopyOptions.FireTriggers | System.Data.SqlClient.SqlBulkCopyOptions.UseInternalTransaction)
        {
            I_QueryResult _QR = CurrentCore<ACT.Core.Interfaces.DataAccess.I_QueryResult>.GetCurrent();

            // make sure to enable triggers more on triggers in next post
            System.Data.SqlClient.SqlBulkCopy bulkCopy = new System.Data.SqlClient.SqlBulkCopy(_Connection, BulkCopyOptions, null);

            // set the destination table name
            bulkCopy.DestinationTableName = TableName;

            try
            {
                // write the data in the "dataTable"
                bulkCopy.WriteToServer(BulkData);
            }
            catch (Exception ex)
            {
                _QR.Exceptions.Add(ex);
            }

            return _QR;
        }

        public I_QueryResult ExecuteBulkInsert(System.Data.DataTable BulkData, string TableName = null, System.Data.SqlClient.SqlBulkCopyOptions BulkCopyOptions = System.Data.SqlClient.SqlBulkCopyOptions.TableLock | System.Data.SqlClient.SqlBulkCopyOptions.FireTriggers | System.Data.SqlClient.SqlBulkCopyOptions.UseInternalTransaction, bool addColumnMapping = true)
        {
            I_QueryResult _QR = CurrentCore<ACT.Core.Interfaces.DataAccess.I_QueryResult>.GetCurrent();

            // make sure to enable triggers more on triggers in next post
            System.Data.SqlClient.SqlBulkCopy bulkCopy = new System.Data.SqlClient.SqlBulkCopy(_Connection, BulkCopyOptions, null);

            // set the destination table name
            bulkCopy.DestinationTableName = TableName;

            foreach (DataColumn c in BulkData.Columns)
            {
                string s = c.ColumnName;
                bulkCopy.ColumnMappings.Add(s, s);
            }

            try
            {
                // write the data in the "dataTable"
                bulkCopy.WriteToServer(BulkData);
            }
            catch (Exception ex)
            {
                _QR.Exceptions.Add(ex);
            }

            return _QR;
        }


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

            if (_Connection.ConnectionString == "") { return false; }
            bool _Success = false;

            try
            {
                _Connection.Open();
                if (_Connection.State == System.Data.ConnectionState.Open)
                {
                    _Success = true;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 18487 || ex.Number == 18488)
                {
                    LogError(this.GetType().FullName, "UserName Specified Needs to Change Password.", ex, ex.Number.ToString(), ErrorLevel.Critical);
                    throw new Exception("Error opening Database.  Check Log file", ex);
                }
                else
                {
                    LogError(this.GetType().FullName, "General Error Opening Database.", ex, ex.Number.ToString(), ErrorLevel.Critical);
                    throw new Exception("Error opening Database.  Check Log file", ex);
                }
            }
            catch (InvalidOperationException ex)
            {
                if (_Connection.State == System.Data.ConnectionState.Open)
                {
                    _Success = true;
                }
                else
                {
                    LogError(this.GetType().FullName, "General Error Opening Database.", ex, "", ErrorLevel.Critical);
                    throw new Exception("Error opening Database.  Check Log file", ex);
                }
            }

            _Command.Connection = _Connection;

            if (_Success)
            {
                if (EncryptConnectionString)
                {
                    I_Encryption _Encryptor = CurrentCore<I_Encryption>.GetCurrent();
                    _ConnectionString = _Encryptor.Encrypt(_ConnectionString);
                }
                return true;
            }
            else
            {
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

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public string GetStoredSQLQuery(string Name, string GroupName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Start a Transaction
        /// </summary>
        public void BeginTransaction()
        {
            if (Connected)
            {
                _Transaction = _Connection.BeginTransaction();
                _TransactionStarted = true;
            }
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

        /// <summary>
        /// Trys to Commit The Transaction
        /// </summary>
        public void CommitTransaction()
        {
            if (_TransactionStarted)
            {
                try
                {
                    _Transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        RollbackTransaction();
                    }
                    catch (Exception exsub) { LogError(this.GetType().FullName, "Error Rolling Transaction Back.", exsub, "", ErrorLevel.Severe); };

                    DisposeObjects();

                    LogError(this.GetType().FullName, "Error Commiting Transaction.", ex, "", ErrorLevel.Critical);
                    throw ex;
                }
            }
            _TransactionStarted = false;
        }

        /// <summary>
        /// Attempts to Rollback the Transaction
        /// </summary>
        public void RollbackTransaction()
        {
            if (_TransactionStarted)
            {
                try
                {
                    _Transaction.Rollback();
                }
                catch (Exception ex)
                {
                    DisposeObjects();
                    LogError(this.GetType().FullName, "Error Rolling Back Transaction.", ex, "", ErrorLevel.Critical);
                    throw ex;
                }
            }
            _TransactionStarted = false;
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

        //public string ExportDatabaseTableData(string TableName)
        //{
        //string _TmpReturn = "";

        //}



        /// <summary>
        /// Inserts Data into the Exportable Database Table.  Replaces Null with DBNull and throws error on missing data. Also Inserts defaults on null
        /// </summary>
        /// <param name="TableName">Table to Insert Data To</param>
        /// <param name="FieldsAndValues">Fields and Values of the Table You Want to InsertData</param>
        /// <returns>IQueryResult</returns>
        public I_QueryResult InsertData(string TableName, Dictionary<string, object> FieldsAndValues)
        {
            I_Db _TmpDB = ExportDatabase();

            I_DbTable _TmpTable = _TmpDB.GetTable(TableName, true);
            List<string> _Fields = new List<string>();
            //List of DB Params
            List<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();

            foreach (string _key in FieldsAndValues.Keys)
            {
                I_DbColumn _TmpColumn = _TmpTable.GetColumn(_key, true);
                if (_TmpColumn == null)
                {
                    LogError(this.GetType().FullName, "Error Loading Column From DB", null, _TmpDB.ExportXMLData(), ErrorLevel.Critical);
                }

                _Fields.Add(_key);
                System.Data.IDataParameter _tmpnew = new System.Data.SqlClient.SqlParameter();
                _tmpnew.ParameterName = "@" + _key;

                if (FieldsAndValues[_key] == null)
                {
                    if ((_TmpColumn.Nullable == true))
                    {
                        _tmpnew.Value = DBNull.Value;
                    }
                    else
                    {
                        if (_TmpColumn.Default != "")
                        {
                            _Fields.Remove(_key);
                        }
                        else
                        {
                            LogError(this.GetType().FullName, "Column :" + _key + " Can't be null!", null, "", ErrorLevel.Critical);
                        }
                    }
                }
                else
                {
                    _tmpnew.Value = FieldsAndValues[_key];
                }
                if (_Fields.Contains(_key))
                {
                    _Params.Add(_tmpnew);
                }
            }
            string _InsertSQL = _TmpTable.GetInsertDataSQL(_Fields);
            _InsertSQL = _InsertSQL + "; SELECT SCOPE_IDENTITY()";
            //BeginTransaction();
            I_QueryResult _QR = RunCommand(_InsertSQL, _Params, false, System.Data.CommandType.Text);
            //CommitTransaction();

            try
            {
                return _QR;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Updates Data in a Exportable Database Table. 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldsAndValues"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public I_QueryResult UpdateData(string TableName, Dictionary<string, object> FieldsAndValues, I_DbWhereStatement Where)
        {
            I_Db _TmpDB = ExportDatabase();

            I_DbTable _TmpTable = _TmpDB.GetTable(TableName, true);
            List<string> _Fields = new List<string>();
            //List of DB Params
            List<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();

            foreach (string _key in FieldsAndValues.Keys)
            {
                I_DbColumn _TmpColumn = _TmpTable.GetColumn(_key, true);
                if (_TmpColumn == null)
                {
                    LogError(this.GetType().FullName, "Error Loading Column From DB", null, _TmpDB.ExportXMLData(), ErrorLevel.Critical);
                }

                _Fields.Add(_key);
                System.Data.IDataParameter _tmpnew = new System.Data.SqlClient.SqlParameter();
                _tmpnew.ParameterName = "@" + _key;

                if (FieldsAndValues[_key] == null)
                {
                    if ((_TmpColumn.Nullable == true))
                    {
                        _tmpnew.Value = DBNull.Value;
                    }
                    else
                    {
                        if (_TmpColumn.Default != "")
                        {
                            _Fields.Remove(_key);
                        }
                        else
                        {
                            LogError(this.GetType().FullName, "Column :" + _key + " Can't be null!", null, "", ErrorLevel.Critical);
                        }
                    }
                }
                else
                {
                    _tmpnew.Value = FieldsAndValues[_key];
                }
                if (_Fields.Contains(_key))
                {
                    _Params.Add(_tmpnew);
                }
            }
            string _UpdateSQL = _TmpTable.GetUpdateDataSQL(_Fields);
            string _WhereStatement = "";

            if (Where != null)
            {
                GenerateWhereStatement(Where, _WhereStatement);

                _UpdateSQL = _UpdateSQL + " Where " + _WhereStatement;

                System.Data.IDataParameter _tmpnewWhereParam = new System.Data.SqlClient.SqlParameter();
                _tmpnewWhereParam.ParameterName = "@" + Where.Column.Name;
                _tmpnewWhereParam.Value = Where.Value;
                _Params.Add(_tmpnewWhereParam);
            }

            //BeginTransaction();
            I_QueryResult _QR = RunCommand(_UpdateSQL, _Params, false, System.Data.CommandType.Text);
            //CommitTransaction();

            try
            {
                return _QR;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Updates Data in a Exportable Database Table. 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldsAndValues"></param>
        /// <param name="WhereStatements"></param>
        /// <returns></returns>
        public I_QueryResult UpdateData(string TableName, Dictionary<string, object> FieldsAndValues, List<I_DbWhereStatement> WhereStatements)
        {
            I_Db _TmpDB = ExportDatabase();

            I_DbTable _TmpTable = _TmpDB.GetTable(TableName, true);
            List<string> _Fields = new List<string>();
            //List of DB Params
            List<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();

            foreach (string _key in FieldsAndValues.Keys)
            {
                I_DbColumn _TmpColumn = _TmpTable.GetColumn(_key, true);
                if (_TmpColumn == null)
                {
                    LogError(this.GetType().FullName, "Error Loading Column From DB", null, _TmpDB.ExportXMLData(), ErrorLevel.Critical);
                }

                _Fields.Add(_key);
                System.Data.IDataParameter _tmpnew = new System.Data.SqlClient.SqlParameter();
                _tmpnew.ParameterName = "@" + _key;

                if (FieldsAndValues[_key] == null)
                {
                    if ((_TmpColumn.Nullable == true))
                    {
                        _tmpnew.Value = DBNull.Value;
                    }
                    else
                    {
                        if (_TmpColumn.Default != "")
                        {
                            _Fields.Remove(_key);
                        }
                        else
                        {
                            LogError(this.GetType().FullName, "Column :" + _key + " Can't be null!", null, "", ErrorLevel.Critical);
                        }
                    }
                }
                else
                {
                    _tmpnew.Value = FieldsAndValues[_key];
                }
                if (_Fields.Contains(_key))
                {
                    _Params.Add(_tmpnew);
                }
            }
            string _UpdateSQL = _TmpTable.GetUpdateDataSQL(_Fields);
            string _WhereStatement = "";

            if (WhereStatements != null)
            {
                foreach (I_DbWhereStatement _tmpWhere in WhereStatements)
                {
                    _WhereStatement = " Where ";
                    _WhereStatement = GenerateWhereStatement(_tmpWhere, _WhereStatement);
                    System.Data.IDataParameter _tmpnewWhereParam = new System.Data.SqlClient.SqlParameter();
                    _tmpnewWhereParam.ParameterName = "@" + _tmpWhere.Column.Name;
                    _tmpnewWhereParam.Value = _tmpWhere.Value;
                    _Params.Add(_tmpnewWhereParam);
                }

                _UpdateSQL = _UpdateSQL + " " + _WhereStatement;
            }

            //BeginTransaction();
            I_QueryResult _QR = RunCommand(_UpdateSQL, _Params, false, System.Data.CommandType.Text);
            //CommitTransaction();

            try
            {
                return _QR;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes Data From a Exportable Database Table
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public I_QueryResult DeleteData(string TableName, I_DbWhereStatement Where)
        {
            I_Db _TmpDB = ExportDatabase();


            I_DbTable _TmpTable = _TmpDB.GetTable(TableName, true);
            string _DeleteSQL = _TmpTable.GetDeleteDataSQL(Where);

            List<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();

            _Params = GenerateWhereStatementParameters(Where);

            I_QueryResult _TmpReturn = RunCommand(_DeleteSQL, _Params, false, System.Data.CommandType.Text);

            return _TmpReturn;
        }


        public I_QueryResult DuplicateRow(string TableName, I_DbWhereStatement Where)
        {
            throw new NotImplementedException();
        }

        public I_QueryResult DuplicateRow(string TableName, I_DbWhereStatement Where, int Number)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserts Data into a Exportable Database Table
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="FieldsAndValues"></param>
        /// <returns></returns>
        public I_QueryResult InsertData(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues)
        {
            I_Db _TmpDB = ExportDatabase();

            I_DbTable _TmpTable = _TmpDB.GetTable(Table.Name, true);
            List<string> _Fields = new List<string>();
            //List of DB Params
            List<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();

            foreach (I_DbColumn _key in FieldsAndValues.Keys)
            {
                _Fields.Add(_key.Name);
                System.Data.IDataParameter _tmpnew = new System.Data.SqlClient.SqlParameter();
                _tmpnew.ParameterName = "@" + _key;

                if (FieldsAndValues[_key] == null)
                {
                    if ((_key.Nullable == true))
                    {
                        _tmpnew.Value = DBNull.Value;
                    }
                    else
                    {
                        if (_key.Default != "")
                        {
                            _Fields.Remove(_key.Name);
                        }
                        else
                        {
                            LogError(this.GetType().FullName, "Column :" + _key.Name + " Can't be null!", null, "", ErrorLevel.Critical);
                        }
                    }
                }
                else
                {
                    _tmpnew.Value = FieldsAndValues[_key];
                }
                if (_Fields.Contains(_key.Name))
                {
                    _Params.Add(_tmpnew);
                }
            }
            string _InsertSQL = _TmpTable.GetInsertDataSQL(_Fields);
            _InsertSQL = _InsertSQL + "; SELECT SCOPE_IDENTITY()";
            //BeginTransaction();
            I_QueryResult _QR = RunCommand(_InsertSQL, _Params, false, System.Data.CommandType.Text);
            //CommitTransaction();

            try
            {
                return _QR;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Updates a Exportable Database Table
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="FieldsAndValues"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public I_QueryResult UpdateData(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues, I_DbWhereStatement Where)
        {
            I_Db _TmpDB = ExportDatabase();

            I_DbTable _TmpTable = _TmpDB.GetTable(Table.Name, true);
            List<string> _Fields = new List<string>();
            //List of DB Params
            List<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();

            foreach (I_DbColumn _key in FieldsAndValues.Keys)
            {

                _Fields.Add(_key.Name);
                System.Data.IDataParameter _tmpnew = new System.Data.SqlClient.SqlParameter();
                _tmpnew.ParameterName = "@" + _key.Name;

                if (FieldsAndValues[_key] == null)
                {
                    if ((_key.Nullable == true))
                    {
                        _tmpnew.Value = DBNull.Value;
                    }
                    else
                    {
                        if (_key.Default != "")
                        {
                            _Fields.Remove(_key.Name);
                        }
                        else
                        {
                            LogError(this.GetType().FullName, "Column :" + _key.Name + " Can't be null!", null, "", ErrorLevel.Critical);
                        }
                    }
                }
                else
                {
                    _tmpnew.Value = FieldsAndValues[_key];
                }
                if (_Fields.Contains(_key.Name))
                {
                    _Params.Add(_tmpnew);
                }
            }


            string _UpdateSQL = _TmpTable.GetUpdateDataSQL(_Fields, Where);


            _Params = GenerateWhereStatementParameters(Where);

            //BeginTransaction();
            I_QueryResult _QR = RunCommand(_UpdateSQL, _Params, false, System.Data.CommandType.Text);
            //CommitTransaction();

            try
            {
                return _QR;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Updates an Exportable Database Table
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="FieldsAndValues"></param>
        /// <returns></returns>
        public I_QueryResult UpdateData(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues)
        {
            I_Db _TmpDB = ExportDatabase();

            I_DbTable _TmpTable = _TmpDB.GetTable(Table.Name, true);
            List<string> _Fields = new List<string>();
            //List of DB Params
            List<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();

            foreach (I_DbColumn _key in FieldsAndValues.Keys)
            {

                _Fields.Add(_key.Name);
                System.Data.IDataParameter _tmpnew = new System.Data.SqlClient.SqlParameter();
                _tmpnew.ParameterName = "@" + _key;

                if (FieldsAndValues[_key] == null)
                {
                    if ((_key.Nullable == true))
                    {
                        _tmpnew.Value = DBNull.Value;
                    }
                    else
                    {
                        if (_key.Default != "")
                        {
                            _Fields.Remove(_key.Name);
                        }
                        else
                        {
                            LogError(this.GetType().FullName, "Column :" + _key + " Can't be null!", null, "", ErrorLevel.Critical);
                        }
                    }
                }
                else
                {
                    _tmpnew.Value = FieldsAndValues[_key];
                }
                if (_Fields.Contains(_key.Name))
                {
                    _Params.Add(_tmpnew);
                }
            }
            string _UpdateSQL = _TmpTable.GetUpdateDataSQL(_Fields);

            //BeginTransaction();
            I_QueryResult _QR = RunCommand(_UpdateSQL, _Params, false, System.Data.CommandType.Text);
            //CommitTransaction();

            try
            {
                return _QR;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes Data From a Exportable Database Table
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public I_QueryResult DeleteData(I_DbTable Table, I_DbWhereStatement Where)
        {
            I_Db _TmpDB = ExportDatabase();


            I_DbTable _TmpTable = _TmpDB.GetTable(Table.Name, true);
            string _DeleteSQL = _TmpTable.GetDeleteDataSQL(Where);

            List<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();

            _Params = GenerateWhereStatementParameters(Where);

            I_QueryResult _TmpReturn = RunCommand(_DeleteSQL, _Params, false, System.Data.CommandType.Text);

            return _TmpReturn;
        }

        /// <summary>
        /// Recursive DELETE NOT IMPLEMENTED
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="FieldsAndValues"></param>
        /// <param name="RecursiveDelete"></param>
        /// <returns></returns>
        public I_QueryResult DeleteData(I_DbTable Table, Dictionary<I_DbColumn, object> FieldsAndValues, bool RecursiveDelete)
        {
            I_DbWhereStatement _tmpWhere = new ACT_IDbWhereStatemet();

            _tmpWhere = _tmpWhere.GenerateFrom(FieldsAndValues);

            I_Db _TmpDB = ExportDatabase();


            I_DbTable _TmpTable = _TmpDB.GetTable(Table.Name, true);
            string _DeleteSQL = _TmpTable.GetDeleteDataSQL(_tmpWhere);

            List<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();

            _Params = GenerateWhereStatementParameters(_tmpWhere);

            I_QueryResult _TmpReturn = RunCommand(_DeleteSQL, _Params, false, System.Data.CommandType.Text);

            return _TmpReturn;
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
            string wherestatement = "";
            List<IDataParameter> _Params = new List<IDataParameter>();

            _Params = GenerateWhereStatementParameters(Where);

            string _Where = "";

            if (Where != null)
            {
                _Where = " Where ";
                _Where = GenerateWhereStatement(Where, wherestatement);
            }
            I_QueryResult _QR = RunCommand("select * from " + Table.Name + _Where, _Params, true, CommandType.Text);
            return _QR;
        }

        #endregion

        #region IPlugin Members


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


            try
            {
                if (_Command != null)
                {
                    _Command.Dispose();
                }
            }
            catch { }
            try
            {
                if (_Transaction != null)
                {
                    _Transaction.Dispose();
                }
            }
            catch { }
            try
            {
                if (_Connection != null)
                {
                    _Connection.Dispose();
                }
            }
            catch { }
        }

        #endregion

        public I_Db ExportDatabase()
        {
            // First Try and Return the Database Cache
            if (_DatabaseCache.ContainsKey(_ConnectionString))
            {
                if (_DatabaseCache[_ConnectionString] != null)
                {
                    if (_DatabaseCache[_ConnectionString].HasChanged == false)
                    {
                        // return _DatabaseCache[_ConnectionString];
                    }
                }
            }

            if (Connected == false)
            {
                LogError(this.GetType().FullName, "Error Exporting Database. Database is not connected.", null, "", ErrorLevel.Warning);
                throw new Exception("Error Exporting Database. Database is not connected.");
            }

            I_Db _DB = CurrentCore<ACT.Core.Interfaces.DataAccess.I_Db>.GetCurrent();

            //_DB.DataAccessClass = //(CurrentCore.GetCurrent();
            _DB.Name = _Connection.Database;

            _DB.Types = GetDataTypes();

            _DB.StoredProcedures = GetStoredProcedures(_DB);

            #region Tables
            string[] restrictions = new string[4];

            // Catalog
            restrictions[0] = _DB.Name;
            // Owner - We want All
            restrictions[1] = null;
            // Table - We want all, so null
            restrictions[2] = null;
            // Table Type - Only tables and not views
            restrictions[3] = "BASE TABLE";

            System.Data.DataTable _Schema = _Connection.GetSchema("Tables", restrictions);

            foreach (System.Data.DataRow _SchemaDataRows in _Schema.Rows)
            {
                string _TmpTableOwner = _SchemaDataRows["TABLE_SCHEMA"].ToString();
                string _TmpTableShortName = _SchemaDataRows["TABLE_NAME"].ToString();

                string _TmpTableName = "[" + _DB.Name + "].[" + _TmpTableOwner + "].[" + _TmpTableShortName + "]";
                string _TmpTableMediumName = "[" + _TmpTableOwner + "].[" + _TmpTableShortName + "]";
                string _TmpTableDescription = "";

                #region Retrieve The SQL Table Description

                string _TableDescSQL = "SELECT  distinct 	u.name + '.' + t.name AS [table], td.value AS [table_desc], tb.[modify_date]    	FROM    	sysobjects t INNER JOIN  sysusers u     ON		u.uid = t.uid INNER JOIN sys.tables tb ON tb.[object_id] = t.[id] LEFT OUTER JOIN sys.extended_properties td    ON		td.major_id = t.id    AND 	td.minor_id = 0    AND		td.name = 'MS_Description' WHERE t.type = 'u' and '[' + u.name + '].[' + t.name +']' = '" + _TmpTableMediumName.Replace("'", "''") + "'";

                _Command = new System.Data.SqlClient.SqlCommand();
                _Command.Connection = _Connection;
                _Command.CommandType = System.Data.CommandType.Text;

                _Command.CommandText = _TableDescSQL;

                int _tmpAge = -1;
                using (System.Data.SqlClient.SqlDataReader _DescriptionDr = _Command.ExecuteReader())
                {
                    using (System.Data.DataTable _TableDescriptionTable = _DescriptionDr.ToDataTable())
                    {
                        if (_TableDescriptionTable.Rows.Count == 1)
                        {
                            _TmpTableDescription = _TableDescriptionTable.Rows[0]["table_desc"].ToString();

                            try
                            {
                                var _modDate = (_TableDescriptionTable.Rows[0]["modify_date"].ToDateTime() - DateTime.Now);
                                _tmpAge = (Math.Abs(_modDate.Value.Days));
                            }
                            catch { }

                        }
                    }
                }

                #endregion


                if (!_TmpTableShortName.StartsWith("sys"))
                {
                    I_DbTable _Table = CurrentCore<ACT.Core.Interfaces.DataAccess.I_DbTable>.GetCurrent();

                    _Table.ParentDatabase = _DB;
                    _Table.Name = _TmpTableName;
                    _Table.ShortName = _TmpTableShortName;
                    _Table.Owner = _TmpTableOwner;
                    _Table.Description = _TmpTableDescription;
                    _Table.AgeInDays = _tmpAge;

                    #region Load All Columns

                    _Command = new System.Data.SqlClient.SqlCommand();
                    _Command.Connection = _Connection;
                    _Command.CommandType = System.Data.CommandType.Text;
                    string _TmpSQL = SystemSettings.GetSettingByName("MSSQLColumnInfoQuery").Value;
                    _TmpSQL = _TmpSQL.Replace("#TABLENAME#", _Table.ShortName.Replace("'", "''"));
                    _TmpSQL = _TmpSQL.Replace("#OWNER#", _TmpTableOwner);

                    _Command.CommandText = _TmpSQL;

                    System.Data.SqlClient.SqlDataReader _dr = _Command.ExecuteReader();

                    System.Data.DataTable _SchemaTable = _dr.ToDataTable();
                    _dr.Close();

                    foreach (System.Data.DataRow _SchemaDataRow in _SchemaTable.Rows)
                    {
                        I_DbColumn _TmpDBColumn = CurrentCore<ACT.Core.Interfaces.DataAccess.I_DbColumn>.GetCurrent();

                        _TmpDBColumn.Name = (string)_SchemaDataRow["COLUMN_NAME"];

                        if ((int)_SchemaDataRow["IDENTITYCOLUMN"] == 1)
                        {
                            _TmpDBColumn.AutoIncrement = true;
                        }
                        else
                        {
                            _TmpDBColumn.AutoIncrement = false;
                        }
                        _TmpDBColumn.Description = Convert.ToString(_SchemaDataRow["DESCRIPTION"]);

                        string _BaseDataType = _SchemaDataRow["DATA_TYPE"].ToString();

                        string _BaseColumnTypeName = "";

                        var _FoundType = _DB.Types.Where(x => x.Name.ToLower() == _BaseDataType.ToLower());

                        if (_DB.Types.Where(x => x.Name.ToLower() == _BaseDataType.ToLower()).Count() == 1)
                        {
                            var _FoundTypeSingle = _FoundType.First();

                            if (_FoundTypeSingle.IsTableType == true)
                            {
                                _BaseColumnTypeName = "DataTable";
                            }
                            else if (_FoundTypeSingle.IsUserType == true)
                            {
                                var _SecondFoundType = _DB.Types.Where(x => x.SystemTypeID == _FoundTypeSingle.SystemTypeID && x.IsUserType == false).First();

                                _BaseColumnTypeName = _SecondFoundType.Name;
                            }
                            else
                            {
                                _BaseColumnTypeName = _FoundTypeSingle.Name;
                            }
                        }
                        else
                        {
                            _BaseColumnTypeName = _BaseDataType;
                        }

                        _TmpDBColumn.DataType = _BaseColumnTypeName.ToDbType();

                        try
                        {
                            string _TmpDefault = _SchemaDataRow["COLUMN_DEFAULT"].ToString();
                            if (_TmpDefault.NullOrEmpty() == false)
                            {
                                _TmpDBColumn.Default = _TmpDefault.Substring(1, _TmpDefault.Length - 2);
                            }
                            else
                            {
                                _TmpDBColumn.Default = "";
                            }
                        }
                        catch { _TmpDBColumn.Default = ""; }


                        if ((int)_SchemaDataRow["IDENTITYCOLUMN"] == 1)
                        {
                            _TmpDBColumn.Identity = true;
                        }
                        else
                        {
                            _TmpDBColumn.Identity = false;
                        }


                        _TmpDBColumn.IdentityIncrement = (int)_SchemaDataRow["IDENTITYINCREMENT"];
                        _TmpDBColumn.IdentitySeed = (int)_SchemaDataRow["IDENTITYSEED"];

                        //_TmpDBColumn.IsPrimaryKey Is Set Below in the Next Function

                        // Name is Set First

                        if ((int)_SchemaDataRow["NULLABLE"] == 1)
                        {
                            _TmpDBColumn.Nullable = true;
                        }
                        else
                        {
                            _TmpDBColumn.Nullable = false;
                        }

                        _TmpDBColumn.Precision = (int)_SchemaDataRow["COLUMN_PRECISION"];
                        _TmpDBColumn.Scale = (int)_SchemaDataRow["COLUMN_SCALE"];
                        _TmpDBColumn.ShortName = _TmpDBColumn.Name;
                        _TmpDBColumn.Size = (int)_SchemaDataRow["COLUMN_LENGTH"];

                        _Table.AddColumn(_TmpDBColumn);
                    }
                    #endregion

                    #region Set Primary Keys
                    // LOAD ALL THE PRIMARY KEYS
                    try
                    {
                        I_QueryResult _PKQR = RunCommand("sp_pkeys '" + _Table.ShortName + "','" + _Table.Owner + "'", null, true, System.Data.CommandType.Text);

                        if (_PKQR.Tables.Count == 1)
                        {
                            foreach (DataRow _DataRow in _PKQR.Tables[0].Rows)
                            {
                                _Table.GetColumn(_DataRow["COLUMN_NAME"].ToString(), true).IsPrimaryKey = true;
                            }
                        }

                        _PKQR.Dispose();
                        //else
                        //{
                        //    //LogError(this.GetType().FullName, "Error Locating Primary Key", null, _TmpTableName, ErrorLevel.Warning);
                        //}

                    }
                    catch (Exception ex)
                    {
                        LogError(this.GetType().FullName, "Error Locating Primary Key", ex, _TmpTableName, ErrorLevel.Warning);
                    }

                    #endregion

                    #region Load Forign Keys

                    I_QueryResult _FK = RunCommand("sp_fkeys '" + _Table.ShortName.Replace("'", "''") + "','" + _Table.Owner + "'", null, true, System.Data.CommandType.Text);

                    foreach (System.Data.DataRow _RelationshipRow in _FK.Tables[0].Rows)
                    {

                        // Identify Unique FK_Names to handle multi column primary keys / relationships

                        var _Relationship = _Table.GetRelationship(_RelationshipRow["FK_NAME"].ToString(), true);

                        // Add New Relationship
                        if (_Relationship == null)
                        {
                            I_DbRelationship _TempRelationship = CurrentCore<ACT.Core.Interfaces.DataAccess.I_DbRelationship>.GetCurrent();

                            if (_RelationshipRow["PKTABLE_NAME"].ToString() != _Table.Name)
                            {
                                _TempRelationship.IsForeignKey = true;
                                _TempRelationship.RelationshipName = _RelationshipRow["FK_NAME"].ToString();
                                _TempRelationship.TableName = _Table.Name;
                                _TempRelationship.ShortTableName = _Table.ShortName;
                                _TempRelationship.ColumnName = _RelationshipRow["PKCOLUMN_NAME"].ToString();
                                _TempRelationship.External_TableName = "[" + _RelationshipRow["FKTABLE_QUALIFIER"].ToString() + "].[" + _RelationshipRow["FKTABLE_OWNER"].ToString() + "].[" + _RelationshipRow["FKTABLE_NAME"].ToString() + "]";
                                _TempRelationship.ShortExternal_TableName = _RelationshipRow["FKTABLE_NAME"].ToString();
                                _TempRelationship.External_ColumnName = _RelationshipRow["FKCOLUMN_NAME"].ToString();
                                _TempRelationship.ColumnNames.Add(_RelationshipRow["PKCOLUMN_NAME"].ToString());
                                _TempRelationship.ExternalColumnNames.Add(_RelationshipRow["FKCOLUMN_NAME"].ToString());
                                _Table.AddRelationship(_TempRelationship);
                            }
                        }
                        else
                        {
                            if (_RelationshipRow["PKTABLE_NAME"].ToString() != _Table.Name)
                            {
                                _Relationship.ColumnNames.Add(_RelationshipRow["PKCOLUMN_NAME"].ToString());
                                _Relationship.ExternalColumnNames.Add(_RelationshipRow["FKCOLUMN_NAME"].ToString());
                                _Relationship.MultiFieldRelationship = true;
                            }
                        }


                    }

                    #endregion

                    #region Load Parent Tables

                    I_QueryResult _PFK = RunCommand(ACT.Core.SystemSettings.GetSettingByName("SuperFKQuery").Value.Replace("#TABLENAME#", _Table.Name.Replace("'", "''")), null, true, System.Data.CommandType.Text);

                    foreach (System.Data.DataRow _RelationshipRow in _PFK.Tables[0].Rows)
                    {
                        // Identify Unique FK_Names to handle multi column primary keys / relationships

                        var _Relationship = _Table.GetRelationship(_RelationshipRow["FK_NAME"].ToString(), true);
                        // Add New Relationship
                        if (_Relationship == null)
                        {
                            I_DbRelationship _TempRelationship = CurrentCore<ACT.Core.Interfaces.DataAccess.I_DbRelationship>.GetCurrent();

                            if (_RelationshipRow["PKTABLE_NAME"].ToString() == _Table.ShortName)
                            {
                                _TempRelationship.IsForeignKey = false;
                                _TempRelationship.RelationshipName = _RelationshipRow["FK_NAME"].ToString();
                                _TempRelationship.TableName = _Table.Name;
                                _TempRelationship.ShortTableName = _Table.ShortName;
                                _TempRelationship.ColumnName = _RelationshipRow["PKCOLUMN_NAME"].ToString();
                                _TempRelationship.External_TableName = "[" + _DB.Name + "].[" + _RelationshipRow["FKTABLE_OWNER"].ToString() + "].[" + _RelationshipRow["FKTABLE_NAME"].ToString() + "]";
                                _TempRelationship.ShortExternal_TableName = _RelationshipRow["FKTABLE_NAME"].ToString();
                                _TempRelationship.External_ColumnName = _RelationshipRow["FKCOLUMN_NAME"].ToString();
                                _TempRelationship.ColumnNames.Add(_RelationshipRow["PKCOLUMN_NAME"].ToString());
                                _TempRelationship.ExternalColumnNames.Add(_RelationshipRow["FKCOLUMN_NAME"].ToString());
                                _Table.AddRelationship(_TempRelationship);
                            }
                        }
                        else
                        {
                            if (_RelationshipRow["PKTABLE_NAME"].ToString() == _Table.ShortName)
                            {
                                _Relationship.ColumnNames.Add(_RelationshipRow["PKCOLUMN_NAME"].ToString());
                                _Relationship.ExternalColumnNames.Add(_RelationshipRow["FKCOLUMN_NAME"].ToString());
                                _Relationship.MultiFieldRelationship = true;
                            }
                        }
                    }
                    #endregion

                    _DB.AddTable(_Table);
                }
            }
            #endregion

            GetViews(_DB);

            #region populate the relationship Column Types

            foreach (var t in _DB.Tables)
            {
                foreach (var r in t.AllRelationships)
                {
                    r.ColumnType = t.GetColumn(r.ColumnName, true).DataType;
                    r.External_ColumnType = _DB.GetTable(r.External_TableName, true).GetColumn(r.External_ColumnName, true).DataType;
                }
            }

            #endregion

            //if (_DatabaseCache.ContainsKey(_ConnectionString))
            //{
            //    _DatabaseCache[_ConnectionString] = _DB;
            //}
            //else
            //{
            //    _DatabaseCache.Add(_ConnectionString, _DB);
            //}
            return _DB;
        }

        public void SetImpersonate(object UserInfo)
        {
            throw new NotImplementedException();
        }
    }


}