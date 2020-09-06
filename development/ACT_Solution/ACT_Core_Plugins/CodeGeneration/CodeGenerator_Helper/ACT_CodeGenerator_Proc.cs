// ***********************************************************************
// Assembly         : ACTPlugins
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="ACT_CodeGenerator.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using ACT.Core.Extensions;
using ACT.Core.Interfaces.CodeGeneration;
using ACT.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ACT.Plugins.CodeGeneration
{
    /// <summary>
    /// Internal Code Generation Class Generates C# Code
    /// Implements the <see cref="ACT.Plugins.ACT_Core" />
    /// Implements the <see cref="ACT.Core.Interfaces.CodeGeneration.I_CodeGenerator" />
    /// </summary>
    /// <seealso cref="ACT.Plugins.ACT_Core" />
    /// <seealso cref="ACT.Core.Interfaces.CodeGeneration.I_CodeGenerator" />
    public partial class ACT_CodeGenerator : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.CodeGeneration.I_CodeGenerator
    {
        #region STORED PROCEDURE CODE

        static readonly string PROCCOMMENT = @"/// <summary> 
        /// ###COMMENT###
        /// </summary>
        /// <author>###AUTHOR###</author>"+ Environment.NewLine;

        /// <summary>
        /// Placeholder for the stored procedures
        /// </summary>        
        private Dictionary<string, string> namespace_StoredProcedures = new Dictionary<string, string>();

        /// <summary>
        /// Parse the Stored Procedures
        /// </summary>
        /// <param name="CodeSettings">The code settings.</param>
        /// <param name="DataBase">The data base.</param>
        private void ParseStoredProcedures(I_CodeGenerationSettings CodeSettings, I_Db DataBase)
        {
            string _Delimiter = CodeSettings.NamespaceDrivenProceduresDelimiter;

            foreach (var Proc in DataBase.StoredProcedures)
            {
                if (Proc.Name.Contains(_Delimiter))
                {
                    string[] _Parts = Proc.Name.SplitString(_Delimiter, StringSplitOptions.RemoveEmptyEntries);
                    string _NameSpace = "";
                    bool _Start = true;
                    foreach (string p in _Parts)
                    {
                        if (p.ToInt(-1) == -1)
                        {
                            if (_Start)
                            {
                                if (p == "ACT") { _NameSpace += "ACTPROC."; }
                                else { _NameSpace += p + "."; }
                                _Start = false;
                            }
                            else { _NameSpace += p + "."; }
                        }
                        else // Namespace Can't Contain Numeric Values As Identifiers
                        {
                            _NameSpace = _NameSpace.TrimEnd(".");
                            _NameSpace += p + ".";
                        }
                    }
                    _NameSpace = _NameSpace.TrimEnd(".");
                    namespace_StoredProcedures.Add(Proc.Name, _NameSpace);
                }
                else
                {
                    namespace_StoredProcedures.Add(Proc.Name, Proc.Name);
                }
            }
        }

        /// <summary>
        /// Generates All The Stored Procedures
        /// </summary>
        /// <param name="CodeSettings">The code settings.</param>
        /// <param name="DataBase">The data base.</param>
        /// <returns>List&lt;I_GeneratedCode&gt;.</returns>
        public List<I_GeneratedCode> GenerateStoredProcedureClass(I_CodeGenerationSettings CodeSettings, I_Db DataBase)
        {
            List<I_GeneratedCode> _TmpReturn = new List<I_GeneratedCode>();

            var DataAccess = ACT.Core.CurrentCore<I_DataAccess>.GetCurrent();
            string _ConnectionString = ACT.Core.SystemSettings.GetSettingByName(CodeSettings.DatabaseConnectionName).Value;

            DataAccess.Open(_ConnectionString);
            string _StoredProcedureCode = "";

            #region Create Bulk Proc Class & EXEC BULK PROC EXECUTION
            _StoredProcedureCode += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
            _StoredProcedureCode += Environment.NewLine + "" + Environment.NewLine;
            _StoredProcedureCode += "namespace " + CodeSettings.NameSpace + ".StoredProcedures " + Environment.NewLine + "{" + Environment.NewLine;

            _StoredProcedureCode += "\tpublic class BulkDatabaseProcedureData" + Environment.NewLine;
            _StoredProcedureCode += "\t{" + Environment.NewLine;
            _StoredProcedureCode += "\tpublic string ProcName;" + Environment.NewLine;
            _StoredProcedureCode += "\tpublic Dictionary<string, System.Data.IDataParameter> Params = new Dictionary<string, System.Data.IDataParameter>();" + Environment.NewLine;

            _StoredProcedureCode += "\tpublic static List<bool> EXEC_sp_EXEC_BULK_Procedures(List<BulkDatabaseProcedureData> Params, string Conn=\"\")" + Environment.NewLine;
            _StoredProcedureCode += "\t{" + Environment.NewLine;
            _StoredProcedureCode += "\t\tList<bool> _TmpReturn = new List<bool>();" + Environment.NewLine;
            _StoredProcedureCode += "\t\tusing (var DataAccess = ACT.Core.CurrentCore<I_DataAccess>.GetCurrent())" + Environment.NewLine;
            _StoredProcedureCode += "\t\t{" + Environment.NewLine;
            _StoredProcedureCode += "\t\tif (Conn == \"\")" + Environment.NewLine;
            _StoredProcedureCode += "\t\t{" + Environment.NewLine;
            _StoredProcedureCode += "\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine;
            _StoredProcedureCode += "\t\t}" + Environment.NewLine;
            _StoredProcedureCode += "\t\telse" + Environment.NewLine;
            _StoredProcedureCode += "\t\t{" + Environment.NewLine;
            _StoredProcedureCode += "\t\t\tDataAccess.Open(Conn);" + Environment.NewLine;
            _StoredProcedureCode += "\t\t}" + Environment.NewLine;

            _StoredProcedureCode += "\t\tList<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();" + Environment.NewLine;

            _StoredProcedureCode += "\t\tforeach (var data in Params)" + Environment.NewLine;
            _StoredProcedureCode += "\t\t{" + Environment.NewLine;
            _StoredProcedureCode += "\t\t_Params.Clear();" + Environment.NewLine;
            _StoredProcedureCode += "\t\tforeach (var param in data.Params)" + Environment.NewLine;
            _StoredProcedureCode += "\t\t{" + Environment.NewLine;
            _StoredProcedureCode += "\t\t_Params.Add(new System.Data.SqlClient.SqlParameter(param.Key, param.Value.Value));" + Environment.NewLine;
            _StoredProcedureCode += "\t\t}" + Environment.NewLine;

            _StoredProcedureCode += "\t\tvar _Result = DataAccess.RunCommand(\"[dbo].\" + data.ProcName, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.StoredProcedure);" + Environment.NewLine;

            _StoredProcedureCode += "\t\tif (_Result.Exceptions[0] == null)" + Environment.NewLine;
            _StoredProcedureCode += "\t\t{" + Environment.NewLine;
            _StoredProcedureCode += "\t\t_TmpReturn.Add(true);" + Environment.NewLine;
            _StoredProcedureCode += "\t\t}" + Environment.NewLine;
            _StoredProcedureCode += "\t\t}" + Environment.NewLine;
            _StoredProcedureCode += "\t\t}" + Environment.NewLine;
            _StoredProcedureCode += "\t\treturn _TmpReturn;" + Environment.NewLine;
            _StoredProcedureCode += "\t\t}" + Environment.NewLine;
            _StoredProcedureCode += "\t}" + Environment.NewLine;
            _StoredProcedureCode += "}" + Environment.NewLine;
            #endregion

            /// ADD BULK EXECUTION AS ITS OWN FILE
            ACT_GeneratedCode _tmpBulkFile = new ACT_GeneratedCode() { FileName = "DBSP_Bulk_Stored_Procedures.cs", Code = _StoredProcedureCode };
            _TmpReturn.Add(_tmpBulkFile);
            _tmpBulkFile = null;


            if (CodeSettings.NamespaceDrivenProcedures)
            {
                namespace_StoredProcedures.Clear();
                ParseStoredProcedures(CodeSettings, DataBase);

                // Loop over all the stored procedures
                foreach (var proc in namespace_StoredProcedures)
                {
                    if (CodeSettings.ObjectAgeMax > -1 && DataBase.StoredProcedures.First(x => x.Name == proc.Key).AgeInDays >= CodeSettings.ObjectAgeMax) { continue; }

                    var _ProcInfo = DataBase.StoredProcedures.First(x => x.Name == proc.Key);

                    //_ProcInfo.Comments

                    string _tmpCode = "";
                    _tmpCode += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;

                    _tmpCode += "namespace " + CodeSettings.NameSpace + "." + proc.Value.ToUpper() + Environment.NewLine + "{" + Environment.NewLine;

                    _tmpCode += "/// <summary>" + Environment.NewLine;
                    _tmpCode += "/// Execute " + proc.Key + Environment.NewLine;
                    _tmpCode += "/// </summary>" + Environment.NewLine;
                    _tmpCode += "/// <returns>I_QueryResult</returns>" + Environment.NewLine + Environment.NewLine;
                    _tmpCode += GenerateStoredProcedureCode(DataBase.StoredProcedures.First(x => x.Name == proc.Key), CodeSettings);
                    _tmpCode += "}" + Environment.NewLine;
                    // adding "DBSP_" so that the file writer will know to put it in a sub dir of DBSP
                    ACT_GeneratedCode _tmpFile = new ACT_GeneratedCode() { FileName = "DBSP_" + proc.Key.Replace(".", "_") + ".cs", Code = _tmpCode };
                    _TmpReturn.Add(_tmpFile);
                }
            }
            else
            {
                _StoredProcedureCode = ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
                _StoredProcedureCode += Environment.NewLine + "" + Environment.NewLine;
                _StoredProcedureCode += "namespace " + CodeSettings.NameSpace + ".StoredProcedures " + Environment.NewLine + "{" + Environment.NewLine;
                _StoredProcedureCode += "public static class DatabaseStoredProcedures" + Environment.NewLine;
                _StoredProcedureCode += "{" + Environment.NewLine;

                foreach (var Proc in DataBase.StoredProcedures)
                {
                    ///if (CodeSettings.ObjectAgeMax > -1 && Proc.AgeInDays > CodeSettings.ObjectAgeMax) { continue; }

                    _StoredProcedureCode += GenerateStoredProcedureCode(Proc, CodeSettings);
                }

                ACT_GeneratedCode _NewREturn = new ACT_GeneratedCode() { FileName = "DBStoredProcedures.cs", Code = _StoredProcedureCode + Environment.NewLine + "}" + Environment.NewLine + "}" };
                _TmpReturn.Add(_NewREturn);
            }
            return _TmpReturn;
        }

        /// <summary>
        /// Generate a Stored Procedure Code To Execute
        /// </summary>
        /// <param name="Proc">The proc.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        public string GenerateStoredProcedureCode(I_DbStoredProcedure Proc, I_CodeGenerationSettings CodeSettings)
        {
            string _ParamList = "";
            string _ParamAddList = "";

            foreach (var ProcParm in Proc.Parameters)
            {
                if (ProcParm.DataTypeName == "DataTable")
                {
                    _ParamList += "System.Data.DataTable " + ProcParm.Name + "_VALUE, ";
                }
                else
                {
                    _ParamList += ProcParm.DataType.ToCSharpStringNullable() + " " + ProcParm.Name + "_VALUE, ";
                }

                _ParamAddList += "\tif (" + ProcParm.Name + "_VALUE == null) " + Environment.NewLine + "\t { " + Environment.NewLine;
                _ParamAddList += "\t_Params.Add(new System.Data.SqlClient.SqlParameter(\"" + ProcParm.Name + "\", DBNull.Value));" + Environment.NewLine;
                _ParamAddList += "\t}" + Environment.NewLine + "\telse " + Environment.NewLine + "\t{" + Environment.NewLine;
                _ParamAddList += "\t_Params.Add(new System.Data.SqlClient.SqlParameter(\"" + ProcParm.Name + "\", " + ProcParm.Name + "_VALUE));" + Environment.NewLine;
                _ParamAddList += "\t}" + Environment.NewLine;
            }

            var p = Proc.Name.SplitString("_", StringSplitOptions.RemoveEmptyEntries);
            string className = "", methodName = "";

            string _ProcMethodString = "";
            string _tComment = "";
            if (Proc.Comments.NullOrEmpty() == false)
            {
                _tComment = PROCCOMMENT.Replace("###COMMENT###", Proc.Comments).Replace("###AUTHOR###", "TBD");
            }

            if (CodeSettings.NamespaceDrivenProcedures)
            {
                className = "Execute";
                methodName = "Proc";
                
                _ProcMethodString = _ProcMethodString+ "public static class " + className + " {" + Environment.NewLine + _tComment + "public static I_QueryResult " + methodName + "(" + _ParamList + "string Conn = \"\")" + Environment.NewLine + "";
            }
            else
            {
                className = Proc.Name;
                methodName = Proc.Name;
                _ProcMethodString = _ProcMethodString+ Environment.NewLine + _tComment + "\t\tpublic static I_QueryResult EXEC_" + methodName.ToUpper() + "(" + _ParamList + "string Conn = \"\")" + Environment.NewLine + "";
            }

            #region COMMON METHOD GUTS
            _ProcMethodString += "{" + Environment.NewLine + "";

            _ProcMethodString += "\tusing(var DataAccess = CurrentCore<I_DataAccess>.GetCurrent()){" + Environment.NewLine + "";
            _ProcMethodString += "\tif (Conn == \"\") {" + Environment.NewLine + "";
            _ProcMethodString += "\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine + "";
            _ProcMethodString += "\t } " + Environment.NewLine + "";
            _ProcMethodString += "\t else " + Environment.NewLine + " \t { " + Environment.NewLine + "\t";
            _ProcMethodString += "\tDataAccess.Open(Conn);" + Environment.NewLine + "";
            _ProcMethodString += "}" + Environment.NewLine + "";
            _ProcMethodString += "\tList<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();" + Environment.NewLine;
            _ProcMethodString += "\t#region Param Values" + Environment.NewLine;
            _ProcMethodString += _ParamAddList;
            _ProcMethodString += "\t#endregion" + Environment.NewLine;

            string _ExeString = "[" + Proc.Owner + "]." + Proc.Name;

            _ProcMethodString += "\tvar _Result = DataAccess.RunCommand(\"" + _ExeString + "\", _Params, true, System.Data.CommandType.StoredProcedure);" + Environment.NewLine;
            _ProcMethodString += "\treturn _Result;" + Environment.NewLine;

            #endregion

            if (CodeSettings.NamespaceDrivenProcedures)
            {
                _ProcMethodString += "}}}" + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                _ProcMethodString += "}}" + Environment.NewLine + Environment.NewLine;
            }

            return _ProcMethodString;
        }

        #endregion
    }
}
