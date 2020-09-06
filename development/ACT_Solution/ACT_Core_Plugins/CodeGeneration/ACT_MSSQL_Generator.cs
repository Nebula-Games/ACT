using ACT.Core.Extensions;
using ACT.Core.Interfaces.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Extensions.CodeGenerator;

namespace ACT.Plugins.CodeGeneration
{
    /// <summary>
    /// MSSQL Generator
    /// </summary>
    public class ACT_MSSQL_Generator : ACT.Core.Interfaces.CodeGeneration.I_MSSQL_Generator
    {
        /// <summary>
        /// Generate Basic Stored Proc Code
        /// </summary>
        /// <param name="ConnectionName"></param>
        /// <returns></returns>
        public string GenerateBasicStoredProcCode(string ConnectionName)
        {
            var _DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent();

            _DataAccess.Open(ACT.Core.SystemSettings.GetSettingByName(ConnectionName).Value);

            var DatabaseObject = _DataAccess.ExportDatabase();

            string _tmpReturn = "// ACT CODE GENERATION";

            foreach (var Table in DatabaseObject.Tables)
            {
                if (Table.GetPrimaryColumnNames.Count != 1) { continue; }

                List<string> _DefaultColumns = new List<string>();
                List<string> _Parameters = new List<string>();
                List<string> _ValueParameters = new List<string>();
                List<string> _PrimaryKeys = new List<string>();
                List<string> _DefaultParameters = new List<string>();
                string _ColumnsForInsert = "";
                string _CustomIDCode = "";
                bool _IsIdentifyInt = false;

                //GENERATE THE COLUMNS IN THE DATABASE TABLE
                foreach (var Column in DatabaseObject.Tables.First(x => x.Name == Table.Name).Columns)
                {
                    if (Column.Default.ToLower() == "getdate()")
                    {
                        _DefaultColumns.Add(Column.ShortName);                        
                        string _tmpParameter = "@" + Column.ShortName.ToMSSQLFriendlyName() + " " + Column.DataType.ToDBStringCustom() + " = NULL," + Environment.NewLine;                        
                        _Parameters.Add(_tmpParameter);
                        _ValueParameters.Add("@" + Column.ShortName.ToMSSQLFriendlyName() + ", ");
                    }
                    else if (Column.IsPrimaryKey)
                    {
                        _PrimaryKeys.Add(Column.Name);
                        string _tmpParameter = "@" + Column.ShortName.ToMSSQLFriendlyName() + " " + Column.DataType.ToDBStringCustom() + "," + Environment.NewLine;
                        _Parameters.Add(_tmpParameter);

                        if (Column.IsPrimaryKey)
                        {
                            if (Column.DataType== System.Data.DbType.Guid)
                            {
                                _CustomIDCode += "declare @NEW" + Column.ShortName.ToMSSQLFriendlyName() + " as uniqueidentifier" + Environment.NewLine;
                                _CustomIDCode += "set @NEW" + Column.ShortName.ToMSSQLFriendlyName() + " = NewID()" + Environment.NewLine;
                                _ValueParameters.Add("@NEW" + Column.ShortName.ToMSSQLFriendlyName() + ", ");
                            }
                            else if (Column.DataType == System.Data.DbType.Int32)
                            {
                                _IsIdentifyInt = true;
                            }
                        }
                    }
                    else
                    {
                        string _tmpParameter = "@" + Column.ShortName.ToMSSQLFriendlyName() + " " + Column.DataType.ToDBStringCustom();
                        if (Column.Nullable) { _tmpParameter += " = NULL"; }
                        _Parameters.Add("\t" + _tmpParameter + "," + Environment.NewLine);
                        _ValueParameters.Add("@" + Column.ShortName.ToMSSQLFriendlyName() + ", ");
                    }
                }

                #region Cleanup Parameters

                _Parameters[_Parameters.Count - 1] = _Parameters[_Parameters.Count - 1].TrimEnd(",");
                _ValueParameters[_ValueParameters.Count - 1] = _ValueParameters[_ValueParameters.Count - 1].TrimEnd(",");

                #endregion

                string _AddUpdateProc = ACT.Core.SystemSettings.GetSettingByName("ACT_ADDUPDATEPROC_GENERATECODE").Value;
                _AddUpdateProc = _AddUpdateProc.Replace("###PROCEDURENAME###", Table.ShortName.ToUpper() + "_ADDUPDATE");
                _AddUpdateProc = _AddUpdateProc.Replace("###PARAMETERS###", _Parameters.ToDelimitedString("").TrimEnd(","));
                _AddUpdateProc = _AddUpdateProc.Replace("###COLUMNNAMES###", _ColumnsForInsert);
                string _CustomCode = _CustomIDCode + Environment.NewLine + Environment.NewLine;
                foreach (var item in _DefaultColumns)
                {
                    _CustomCode += "\tif @" + item.ToMSSQLFriendlyName() + "is null" + Environment.NewLine;
                    _CustomCode += "\t\tBEGIN" + Environment.NewLine;
                    _CustomCode += "\t\t\t set @" + item.ToMSSQLFriendlyName() + " = " + Table.Columns.First(x=>x.ShortName==item).Default + Environment.NewLine;
                    _CustomCode += "\t\tEND" + Environment.NewLine + Environment.NewLine;                    
                }

                

                _AddUpdateProc = _AddUpdateProc.Replace("###CUSTOMCODE###", _CustomCode);

                _AddUpdateProc = _AddUpdateProc.Replace("###SCHEMANAME###", Table.Owner);
                _AddUpdateProc = _AddUpdateProc.Replace("###TABLENAME###", Table.ShortName);
              
                _AddUpdateProc = _AddUpdateProc.Replace("###PARAMETERS_VALUES###", _ValueParameters.ToDelimitedString("").TrimEnd(","));

                if (_IsIdentifyInt) {
                    _AddUpdateProc = _AddUpdateProc.Replace("###NEWID###", "@@IDENTITY");
                }
                else
                {
                    _AddUpdateProc = _AddUpdateProc.Replace("###NEWID###", "@NEW" + _PrimaryKeys[0]);
                }

                _tmpReturn += _AddUpdateProc + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            }
            return _tmpReturn;
        }

        /// <summary>
        /// Generate Basic Views Code
        /// </summary>
        /// <param name="ConnectionName"></param>
        /// <returns></returns>
        public string GenerateBasicViewsCode(string ConnectionName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate Code
        /// </summary>
        /// <param name="CodeSettings"></param>
        /// <returns></returns>
        public List<I_GeneratedCode> GenerateCode(I_MSSQL_CodeGenerationSettings CodeSettings)
        {
            throw new NotImplementedException();
        }
    }
}
