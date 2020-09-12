using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Extensions;
using ACT.Core.Interfaces.CodeGeneration;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.Interfaces.Security;
using ACT.Core.Interfaces.Common;
using ACT.Core.Enums;
using ACT.Core.Extensions.CodeGenerator;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.CSharp;


namespace ACT.Plugins.CodeGeneration
{
    /// <summary>
    /// Internal Code Generation Class Generates C# Code
    /// </summary>
    public partial class ACT_CodeGeneratorSQLite : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.CodeGeneration.I_CodeGenerator
    {
        List<string> _Requirements = new List<string>();
        public static string NL = Environment.NewLine;


        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="CodeSettings"></param>
        /// <returns></returns>
        public List<I_GeneratedCode> GenerateWebFormCode(I_CodeGenerationSettings CodeSettings)
        {
            List<I_GeneratedCode> _TmpReturn = new List<I_GeneratedCode>();

            var DataAccess = ACT.Core.CurrentCore<I_DataAccess>.GetCurrent();

            string _ConnectionString = ACT.Core.SystemSettings.GetSettingByName(CodeSettings.DatabaseConnectionName).Value;
            string _ASPTemplate = ACT.Core.SystemSettings.GetSettingByName("ASPNETTemplate").Value;
            string _ASPTemplateRow = ACT.Core.SystemSettings.GetSettingByName("ASPNETTemplateRow").Value;
            string _ASPTemplateCodeFile = ACT.Core.SystemSettings.GetSettingByName("ASPNETTemplateCodeFile").Value;

            if (_ConnectionString == "")
            {
                LogError("Error with the DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "\n\r", "Unable To Locate Setting Name\n\r", null, "\n\r", ErrorLevel.Critical);
            }

            if (DataAccess.Open(_ConnectionString))
            {
                var DB = DataAccess.ExportDatabase();

                foreach (var T in DB.Tables)
                {
                    string p1, p2, r;

                    p1 = _ASPTemplate;
                    p2 = _ASPTemplateCodeFile;


                    p1 = T.StandardReplaceMent(p1, RepacementStandard.UPPERCASE);
                    p2 = T.StandardReplaceMent(p2, RepacementStandard.UPPERCASE);

                    string _rowoutput = "";

                    foreach (var C in T.Columns)
                    {
                        r = _ASPTemplateRow;
                        r = C.StandardReplaceMent(r, RepacementStandard.UPPERCASE);

                        string _ControlTemplate = "";
                        if (C.DataType.IsStringType())
                        {
                            _ControlTemplate = "<asp:TextBox ID=\"#NAME#_TextBox\" runat=\"server\"></asp:TextBox>";
                        }
                        else if (C.DataType == System.Data.DbType.Guid)
                        {
                            var rel = (from x in T.AllRelationships where x.ColumnName == C.Name select x).First();

                            //rel.External_TableName



                        }

                        r = r.Replace("#INPUTCONTROL#", C.StandardReplaceMent(_ControlTemplate, RepacementStandard.UPPERCASE));
                    }

                    p1.Replace("#ROWDATA#", _rowoutput);
                }

            }
            else
            {
                LogError("Error Open DB With the ConnectionString Specified By DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "\n\r", "Unable To Open DB\n\r", null, "\n\r", ErrorLevel.Critical);
                return null;
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Generates the Update Method based on the table
        /// </summary>
        /// <param name="Table">ACT.Core.Interfaces.DataAccess.IDbTable - Table Information</param>
        /// <param name="CodeSettings">ACT.Core.Interfaces.DataAccess.ICodeGenerationSettings - Code Settings</param>
        /// <returns></returns>
        internal string GenerateUpdateMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {

            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public virtual IQueryResult Update()\n\r");
            _TmpBuilder.Append("{\n\r");
            _TmpBuilder.Append("using (var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.IDataAccess>.GetCurrent()){ \n\r");
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {\n\r\n\r");
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\n\r");
            _TmpBuilder.Append("\t\t}\n\r\n\r");
            _TmpBuilder.Append("\t\telse { \n\r\n\r");
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);\n\r");
            _TmpBuilder.Append("\t\t}\n\r\n\r");
            _TmpBuilder.Append("\n\r");
            _TmpBuilder.Append("List<System.Data.SQLite.SQLiteParameter> _Params = new List<System.Data.SQLite.SQLiteParameter>();\n\r");

            _TmpBuilder.Append("string SQLInsertFields = \"\";\n\r");
            _TmpBuilder.Append("string SQLValues = \"\";\n\r");

            foreach (var C in Table.Columns)
            {
                _TmpBuilder.Append("if (this." + C.ShortName.ToCSharpFriendlyName() + " != null){ \n\r");

                if (C.Name.ToLower() == "datemodified")
                {
                    _TmpBuilder.Append("_Params.Add(new System.Data.SQLite.SQLiteParameter(\"" + C.ShortName.ToCSharpFriendlyName() + "_Param\", DateTime.Now));\n\r");
                }
                else
                {
                    _TmpBuilder.Append("_Params.Add(new System.Data.SQLite.SQLiteParameter(\"" + C.ShortName.ToCSharpFriendlyName() + "_Param\", this." + C.ShortName.ToCSharpFriendlyName() + "));\n\r");
                }

                if (C.IsPrimaryKey)
                {
                    _TmpBuilder.Append("SQLValues += \"[" + C.ShortName + "] = @" + C.ShortName.ToCSharpFriendlyName() + "_Param AND \";\n\r");
                }
                else
                {
                    _TmpBuilder.Append("SQLInsertFields += \"[" + C.ShortName + "] = @" + C.ShortName.ToCSharpFriendlyName() + "_Param, \";\n\r");
                }



                _TmpBuilder.Append("}\n\r");
            }

            _TmpBuilder.Append("SQLInsertFields = SQLInsertFields.TrimEnd(\", \".ToCharArray());\n\r");
            _TmpBuilder.Append("SQLValues = SQLValues.TrimEnd(\"AND \".ToCharArray());\n\r");

            _TmpBuilder.Append("string _SQL = \"Update [" + Table.ShortName.ToCSharpFriendlyName() + "] set \" + SQLInsertFields + \" Where \" + SQLValues;\n\r");

            _TmpBuilder.Append("var _TmpReturn = DataAccess.RunCommand(_SQL, _Params.ToList<System.Data.IDataParameter>(), false, System.Data.CommandType.Text);\n\r");
            _TmpBuilder.Append("\n\rreturn _TmpReturn;\n\r");
            _TmpBuilder.Append("\t}\n\r}\n\r");


            return _TmpBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Table">ACT.Core.Interfaces.DataAccess.IDbTable - Table Information</param>
        /// <param name="CodeSettings">ACT.Core.Interfaces.DataAccess.ICodeGenerationSettings - Code Settings</param>
        /// <returns></returns>
        internal string GenerateDeleteMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {

            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public virtual void Delete()\n\r");
            _TmpBuilder.Append("{\n\r");
            _TmpBuilder.Append("using (var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.IDataAccess>.GetCurrent()){ \n\r");
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {\n\r\n\r");
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\n\r");
            _TmpBuilder.Append("\t\t}\n\r\n\r");
            _TmpBuilder.Append("\t\telse { \n\r\n\r");
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);\n\r");
            _TmpBuilder.Append("\t\t}\n\r\n\r");
            _TmpBuilder.Append("\n\r");
            _TmpBuilder.Append("List<System.Data.SQLite.SQLiteParameter> _Params = new List<System.Data.SQLite.SQLiteParameter>();\n\r");

            // _TmpBuilder.Append("string SQLInsertFields = \"\";\n\r");
            _TmpBuilder.Append("string SQLValues = \"\";\n\r");

            foreach (var C in Table.Columns)
            {
                _TmpBuilder.Append("if (this." + C.ShortName.ToCSharpFriendlyName() + " != null){ \n\r");


                if (C.IsPrimaryKey)
                {
                    _TmpBuilder.Append("_Params.Add(new System.Data.SQLite.SQLiteParameter(\"" + C.ShortName.ToCSharpFriendlyName() + "_Param\", this." + C.ShortName.ToCSharpFriendlyName() + "));\n\r");

                    _TmpBuilder.Append("SQLValues += \"[" + C.ShortName + "] = @" + C.ShortName.ToCSharpFriendlyName() + "_Param AND \";\n\r");
                }

                _TmpBuilder.Append("}\n\r");
            }

            // _TmpBuilder.Append("SQLInsertFields = SQLInsertFields.TrimEnd(\", \".ToCharArray());\n\r");
            _TmpBuilder.Append("SQLValues = SQLValues.TrimEnd(\"AND \".ToCharArray());\n\r");

            _TmpBuilder.Append("string _SQL = \"Delete From [" + Table.ShortName.ToCSharpFriendlyName() + "] Where \" + SQLValues;\n\r");

            _TmpBuilder.Append("DataAccess.RunCommand(_SQL, _Params.ToList<System.Data.IDataParameter>(), false, System.Data.CommandType.Text);\n\r");
            _TmpBuilder.Append("}}\n\r");


            return _TmpBuilder.ToString();
        }

        /// <summary>
        /// Generates the Create Method for the Table
        /// </summary>
        /// <param name="Table">ACT.Core.Interfaces.DataAccess.IDbTable - Table Information</param>
        /// <param name="CodeSettings">ACT.Core.Interfaces.DataAccess.ICodeGenerationSettings - Code Settings</param>
        /// <returns>System.String.</returns>
        internal string GenerateCreateMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {

            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public virtual I_QueryResult Create()" + Environment.NewLine);
            _TmpBuilder.Append("\t{" + Environment.NewLine);
            _TmpBuilder.Append("using (var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){ " + Environment.NewLine);
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\telse { " + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("" + Environment.NewLine);
            _TmpBuilder.Append("List<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();" + Environment.NewLine);

            _TmpBuilder.Append("string SQLInsertFields = \"\";" + Environment.NewLine);
            _TmpBuilder.Append("string SQLValues = \"\";" + Environment.NewLine);

            foreach (var C in Table.Columns)
            {
                _TmpBuilder.Append("if (this." + C.ShortName.ToCSharpFriendlyName() + " != null){ " + Environment.NewLine);

                _TmpBuilder.Append("_Params.Add(new System.Data.SqlClient.SqlParameter(\"" + C.ShortName.ToCSharpFriendlyName() + "_Param\", this." + C.ShortName.ToCSharpFriendlyName() + "));" + Environment.NewLine);

                _TmpBuilder.Append("SQLInsertFields += \"[" + C.ShortName + "], \";" + Environment.NewLine);
                _TmpBuilder.Append("SQLValues += \"@" + C.ShortName.ToCSharpFriendlyName() + "_Param, \";" + Environment.NewLine);
                _TmpBuilder.Append("}" + Environment.NewLine);
            }

            _TmpBuilder.Append("SQLInsertFields = SQLInsertFields.TrimEnd(\", \".ToCharArray());" + Environment.NewLine);
            _TmpBuilder.Append("SQLValues = SQLValues.TrimEnd(\", \".ToCharArray());" + Environment.NewLine);

            _TmpBuilder.Append("string _SQL = \"Insert Into [" + Table.ShortName.ToCSharpFriendlyName() + "] ( \" + SQLInsertFields + \") Values (\" + SQLValues + \")\";" + Environment.NewLine);

            _TmpBuilder.Append("var _TmpReturn = DataAccess.RunCommand(_SQL, _Params.ToList<System.Data.IDataParameter>(), false, System.Data.CommandType.Text);" + Environment.NewLine);
            _TmpBuilder.Append("\n\rreturn _TmpReturn;" + Environment.NewLine);
            _TmpBuilder.Append("\t}" + Environment.NewLine + "}" + Environment.NewLine);


            return _TmpBuilder.ToString();
        }
        /// <summary>
        /// Generate the Code Base On the IDB Database and The Code Settings Passed In
        /// </summary>
        /// <param name="Database">ACT.Core.Interfaces.DataAccess.IDb - Database Containing the Extracted Meta Data</param>
        /// <param name="CodeSettings">ACT.Core.Interfaces.CodeGeneration.ICodeGenerationSettings - Code Generation Settings</param>
        /// <returns>Generated Code in a List</returns>
        public List<I_GeneratedCode> GenerateCode(I_CodeGenerationSettings CodeSettings)
        {
            
            var DataAccess = new ACT.Plugins.DataAccess.ACT_SQLite3(); //ACT.Core.CurrentCore<IDataAccess>.GetCurrent();

            string _ConnectionString = ACT.Core.SystemSettings.GetSettingByName(CodeSettings.DatabaseConnectionName).Value;

            if (_ConnectionString == "")
            {
                LogError("Error with the DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "\n\r", "Unable To Locate Setting Name\n\r", null, "\n\r", ErrorLevel.Critical);
            }

            if (DataAccess.Open(_ConnectionString))
            {
                var DB = DataAccess.ExportDatabase();
                return GenerateCode(DB, CodeSettings);
            }
            else
            {
                LogError("Error Open DB With the ConnectionString Specified By DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "\n\r", "Unable To Open DB\n\r", null, "\n\r", ErrorLevel.Critical);
                return null;
            }
        }

        /// <summary>
        /// Generate The Tables Code using the Code Settings
        /// </summary>
        /// <param name="Table">ACT.Core.Interfaces.DataAccess.IDbTable - Table Information</param>
        /// <param name="CodeSettings">ACT.Core.Interfaces.DataAccess.ICodeGenerationSettings - Code Settings</param>
        /// <returns></returns>
        public I_GeneratedCode GenerateCode(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            // bool _UseDatabaseConnectionName = false;


            string _Temp = "";
            _Temp += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
            _Temp += "\n\r\n\r";
            _Temp += "namespace " + CodeSettings.NameSpace + "\n\r{\n\r";
            _Temp += "\tpublic class " + Table.ShortName.ToCSharpFriendlyName() + "_CoreClass : ACT.Plugins.ACTCore \n\r\t{\n\r";

            #region Private Fields
            _Temp = _Temp + "\t#region Private Fields\n\r\n\r";
            _Temp = _Temp + "\tprivate string _LocalConnectionString = \"\";\n\r\n\r";

            for (int x = 0; x < Table.ColumnCount; x++)
            {
                var c = Table.GetColumn(x);
                _Temp = _Temp + "\t\tprivate " + c.DataType.ToCSharpStringNullable() + " __" + c.Name.ToCSharpFriendlyName() + ";\n\r";
                _Temp = _Temp + "\t\tprivate string __" + c.Name.ToCSharpFriendlyName() + "_Description = ";

                if (c.Description.NullOrEmpty() == true)
                {
                    _Temp = _Temp + "\"\";\n\r";
                }
                else
                {
                    _Temp = _Temp + "\"" + c.Description + "\";\n\r";
                }
            }

            _Temp = _Temp + "\n\r\t#endregion \n\r";
            #endregion

            #region  Public Properties

            _Temp = _Temp + "\t#region Public Properties\n\r\n\r";
            _Temp = _Temp + "\tpublic virtual string LocalConnectionString { get { return _LocalConnectionString; } set { _LocalConnectionString = value; } }\n\r\n\r";

            for (int x = 0; x < Table.ColumnCount; x++)
            {
                var c = Table.GetColumn(x);
                _Temp = _Temp + "\t\tpublic virtual " + c.DataType.ToCSharpStringNullable() + " " + c.Name.ToCSharpFriendlyName() + "\n\r";
                _Temp = _Temp + "\t\t{\n\r";
                _Temp = _Temp + "\t\t\tget { return __" + c.Name.ToCSharpFriendlyName() + "; }\n\r";
                _Temp = _Temp + "\t\t\tset { __" + c.Name.ToCSharpFriendlyName() + " = value; }\n\r";
                _Temp = _Temp + "\t\t}\n\r\n\r";

                _Temp = _Temp + "\t\tpublic virtual string " + c.Name.ToCSharpFriendlyName() + "_Description\n\r";
                _Temp = _Temp + "\t\t{\n\r";
                _Temp = _Temp + "\t\t\tget { return __" + c.Name.ToCSharpFriendlyName() + "_Description; }\n\r";
                _Temp = _Temp + "\t\t\tset { __" + c.Name.ToCSharpFriendlyName() + "_Description = value; }\n\r";
                _Temp = _Temp + "\t\t}\n\r\n\r";

            }
            _Temp = _Temp + "\n\r\t#endregion \n\r";

            #endregion

            _Temp += GenerateGenericDBAccess(CodeSettings);

            //#region ChildTables
            //_Temp = _Temp + "\t#region Public Child Tables\n\r\n\r";
            //foreach (var r in Table.AllRelationships)
            //{
            //    if (r.IsForeignKey == true)
            //    {
            //        var _ParentTableName = Table.ParentDatabase.GetTable(r.External_TableName, true);

            //        int RSameCount = Table.AllRelationships.Count(x => x.ShortExternal_TableName == r.ShortExternal_TableName && x.IsForeignKey == true);

            //        if (RSameCount > 1)
            //        {
            //            _Temp = _Temp + "\n\r\tpublic List<" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "> Get_All_" + r.External_ColumnName + "_" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "() { \n\r";
            //        }
            //        else
            //        {
            //            _Temp = _Temp + "\n\r\tpublic List<" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "> Get_All_" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "() { \n\r";

            //        }

            //        _Temp = _Temp + "\n\r\t\tList<" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "> _TmpReturn = new List<" + r.ShortExternal_TableName.ToCSharpFriendlyName() + ">();\n\r";
            //        _Temp = _Temp + "\t\tusing(var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.IDataAccess>.GetCurrent()){\n\r";
            //        _Temp = _Temp + "\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\n\r";
            //        _Temp = _Temp + "\t\tList<System.Data.SQLite.SQLiteParameter> _Params = new List<System.Data.SQLite.SQLiteParameter>();\n\r";
            //        string _TmpWhere = "";
            //        for (int x = 0; x < r.ColumnNames.Count(); x++)
            //        {
            //            var c = Table.GetColumn(r.ColumnNames[x], true);

            //            _Temp = _Temp + "\t\t_Params.Add(new System.Data.SQLite.SQLiteParameter(\"" + c.Name + "Param\", this.ReturnProperty(\"" + c.Name.ToCSharpFriendlyName() + "\")));\n\r";
            //            _TmpWhere += r.ExternalColumnNames[x] + " = @" + c.Name + "Param AND ";

            //        }

            //        _TmpWhere = _TmpWhere.TrimEnd(" AND ");

            //        _Temp = _Temp + "\t\tvar SQL = \"Select " + _ParentTableName.GeneratePrimaryKeySelectListMSSQL() + " From " + r.ShortExternal_TableName.ToCSharpFriendlyName() + " Where " + _TmpWhere + "\";\n\r";

            //        _Temp = _Temp + "\t\tvar Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);\n\r\n\r";


            //        _Temp = _Temp + "\t\tfor (int rownumber = 0; rownumber < Result.Tables[0].Rows.Count; rownumber++ )\n\r";
            //        _Temp = _Temp + "\t\t{\n\r";
            //        _Temp = _Temp + "\t\t\t_TmpReturn.Add(new " + r.ShortExternal_TableName.ToCSharpFriendlyName() + "(";
            //        foreach (var pks in _ParentTableName.GetPrimaryColumnNames)
            //        {
            //            _Temp = _Temp + "(" + _ParentTableName.GetColumn(pks, true).DataType.ToCSharpStringNullable() + ")Result.Tables[0].Rows[rownumber][\"" + pks + "\"],";
            //        }
            //        _Temp = _Temp.TrimEnd(",");
            //        _Temp = _Temp + "));\n\r";
            //        _Temp = _Temp + "\t\t}\n\r";
            //        _Temp = _Temp + "\n\r\treturn _TmpReturn;";
            //        _Temp = _Temp + "\n\r\t}}";
            //    }
            //}
            //_Temp = _Temp + "\n\r\t#endregion \n\r\n\r";
            //#endregion

            //#region ParentTables
            //_Temp = _Temp + "\t#region Public Parent Tables\n\r\n\r";
            //foreach (var r in Table.AllRelationships)
            //{
            //    //r.External_TableName.Substring(r.External_TableName.IndexOf("[dbo].[") + 7).TrimEnd("]"); 

            //    var _ParentTableName = Table.ParentDatabase.GetTable(r.External_TableName, true);

            //    if (r.IsForeignKey == false)
            //    {
            //        int RSameCount = Table.AllRelationships.Count(x => x.ShortExternal_TableName == r.ShortExternal_TableName && x.IsForeignKey == false);

            //        if (RSameCount > 1)
            //        {
            //            _Temp = _Temp + "\n\r\tpublic " + r.ShortExternal_TableName.ToCSharpFriendlyName() + " Get_" + r.ColumnName + "_" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "() { \n\r";
            //        }
            //        else
            //        {
            //            _Temp = _Temp + "\n\r\tpublic " + r.ShortExternal_TableName.ToCSharpFriendlyName() + " Get_" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "() { \n\r";
            //        }

            //        _Temp = _Temp + "\n\r\t\t" + r.ShortExternal_TableName.ToCSharpFriendlyName() + " _TmpReturn = null;\n\r";
            //        _Temp = _Temp + "\t\tusing(var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.IDataAccess>.GetCurrent()){\n\r";


            //        _Temp = _Temp + "\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\n\r";


            //        _Temp = _Temp + "\t\tList<System.Data.SQLite.SQLiteParameter> _Params = new List<System.Data.SQLite.SQLiteParameter>();\n\r";

            //        string _TmpWhere = "";
            //        for (int x = 0; x < r.ColumnNames.Count(); x++)
            //        {
            //            var c = Table.GetColumn(r.ColumnNames[x], true);

            //            _Temp = _Temp + "\t\t_Params.Add(new System.Data.SQLite.SQLiteParameter(\"" + c.Name + "Param\", this.ReturnProperty(\"" + c.Name.ToCSharpFriendlyName() + "\")));\n\r";
            //            _TmpWhere += r.ExternalColumnNames[x] + " = @" + c.Name + "Param AND ";

            //        }

            //        _TmpWhere = _TmpWhere.TrimEnd(" AND ");

            //        _Temp = _Temp + "\t\tvar SQL = \"Select top 1 " + _ParentTableName.GeneratePrimaryKeySelectListMSSQL() + " From " + r.ShortExternal_TableName.ToCSharpFriendlyName() + " Where " + _TmpWhere + "\";\n\r";

            //        _Temp = _Temp + "\t\tvar Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);\n\r\n\r";


            //        _Temp = _Temp + "\t\tif (Result.Tables[0].Rows.Count == 1)\n\r";
            //        _Temp = _Temp + "\t\t{\n\r";


            //        _Temp = _Temp + "\t\t\t_TmpReturn = new " + r.ShortExternal_TableName.ToCSharpFriendlyName() + "(";
            //        foreach (var pks in _ParentTableName.GetPrimaryColumnNames)
            //        {
            //            _Temp = _Temp + "(" + _ParentTableName.GetColumn(pks, true).DataType.ToCSharpStringNullable() + ")Result.Tables[0].Rows[0][\"" + pks + "\"],";
            //        }
            //        _Temp = _Temp.TrimEnd(",");
            //        _Temp = _Temp + ");\n\r";


            //        _Temp = _Temp + "\t\t}\n\r";
            //        _Temp = _Temp + "\n\r\treturn _TmpReturn;\n\r}}";
            //        //_Temp = _Temp + "\n\r\t}";
            //    }
            //}
            //_Temp = _Temp + "\n\r\t#endregion \n\r\t";
            //#endregion

            #region Constructors

            List<string> _PrimaryKeys = Table.GetPrimaryColumnNames;

            if (_PrimaryKeys.Count > 0)
            {
                _Temp += "\n\r\t\tpublic " + Table.ShortName.ToCSharpFriendlyName() + "_CoreClass(string localConnectionString = \"\"){ LocalConnectionString = localConnectionString; }\n\r\n\r";
            }

            _Temp += "\n\r\t\tpublic " + Table.ShortName.ToCSharpFriendlyName() + "_CoreClass(";



            string _PrimaryKeyString = "";
            foreach (string _S in _PrimaryKeys)
            {
                _PrimaryKeyString = _PrimaryKeyString + Table.GetColumn(_S, true).DataType.ToCSharpStringNullable() + " " + Table.GetColumn(_S, true).ShortName.ToCSharpFriendlyName() + ", ";
            }
            // _PrimaryKeyString = _PrimaryKeyString.TrimEnd(", ".ToCharArray());
            _Temp += _PrimaryKeyString;

            _Temp += "string localConnectionString = \"\") \n\r\t\t{\n\r";

            if (_PrimaryKeys.Count() == 0)
            {
                _Temp = _Temp + "\t\tLocalConnectionString = localConnectionString;\n\r";
                _Temp = _Temp + "\t\treturn;\n\r}\n";
                goto SkipConstructor;
            }

            _Temp = _Temp + "\t\tusing(var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.IDataAccess>.GetCurrent()){\n\r";

            _Temp = _Temp + "\t\tif (localConnectionString == \"\") {\n\r\n\r";

            _Temp = _Temp + "\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\n\r";
            _Temp = _Temp + "\t\t}\n\r";
            _Temp = _Temp + "\t\telse { \n\r";
            _Temp = _Temp + "\t\tLocalConnectionString = localConnectionString;\n\r";
            _Temp = _Temp + "\t\t\tDataAccess.Open(localConnectionString);\n\r";
            _Temp = _Temp + "\t\t}\n\r\n\r";


            _Temp = _Temp + "\t\tList<System.Data.SQLite.SQLiteParameter> _Params = new List<System.Data.SQLite.SQLiteParameter>();\n\r";

            foreach (string _S in _PrimaryKeys)
            {
                _Temp = _Temp + "\t\t_Params.Add(new System.Data.SQLite.SQLiteParameter(\"" + _S + "Param\", " + _S + "));\n\r";
            }

            _Temp = _Temp + "\t\tvar SQL = \"Select * From " + Table.ShortName.ToCSharpFriendlyName() + " Where ";

            foreach (string _S in _PrimaryKeys)
            {
                _Temp = _Temp + "[" + _S + "] = @" + _S + "Param AND ";
            }
            _Temp = _Temp.TrimEnd("AND ".ToCharArray()) + "\";\n\r";

            _Temp = _Temp + "\n\r\t\tvar Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);\n\r\n\r";

            _Temp = _Temp + "\t\tif (Result.Tables[0].Rows.Count == 1) {\n\r";
            _Temp = _Temp + "\t\t\tfor(int x = 0; x < Result.Tables[0].Columns.Count; x++) {\n\r";
            _Temp = _Temp + "\t\t\tif (Result.Tables[0].Rows[0][x] != DBNull.Value)\n\r";
            _Temp = _Temp + "\t\t\t  {\n\r";
            _Temp = _Temp + "\t\t\tthis.SetProperty(Result.Tables[0].Columns[x].ColumnName.ToCSharpFriendlyName(), Result.Tables[0].Rows[0][x]);\n\r";
            _Temp = _Temp + "}\n\r\t\t\t}\n\r";
            _Temp = _Temp + "\t\t}}\n\r";


            _Temp += "\n\r\t\t}";

        SkipConstructor:


            #endregion

            #region GenerateParameters

            _PrimaryKeys = Table.GetPrimaryColumnNames;
            string _BuilderTemp = "";
            foreach (string _S in _PrimaryKeys)
            {
                _BuilderTemp = _BuilderTemp + Table.GetColumn(_S, true).DataType.ToCSharpStringNullable() + " " + Table.GetColumn(_S, true).ShortName.ToCSharpFriendlyName() + ", ";
            }
            _BuilderTemp = _BuilderTemp.TrimEnd(", ".ToCharArray());
            string _BuilderTemp2 = "";
            foreach (string _S in _PrimaryKeys)
            {
                _BuilderTemp2 = _BuilderTemp2 + Table.GetColumn(_S, true).ShortName.ToCSharpFriendlyName() + ", ";
            }
            _BuilderTemp2 = _BuilderTemp2.TrimEnd(", ".ToCharArray());
            string _BuilderTemp3 = "";
            foreach (string _S in _PrimaryKeys)
            {
                _BuilderTemp3 = _BuilderTemp3 + "[" + Table.GetColumn(_S, true).ShortName + "], ";
            }
            _BuilderTemp3 = _BuilderTemp3.TrimEnd(", ".ToCharArray());
            string _BuilderTemp4 = "";
            foreach (string _S in _PrimaryKeys)
            {
                _BuilderTemp4 = _BuilderTemp4 + "(" + Table.GetColumn(Table.GetPrimaryColumnNames[0], true).DataType.ToCSharpStringNullable() + ")r[\"" + Table.GetColumn(Table.GetPrimaryColumnNames[0], true).ShortName.ToCSharpFriendlyName() + "\"], ";
            }
            _BuilderTemp4 = _BuilderTemp4.TrimEnd(", ".ToCharArray());
            #endregion

            #region Generate Search Methods
            _Temp = _Temp + "\n\r\n\r\t#region Public Static Search Methods\n\r\n\r";

            #region Generate Table Search With Where
            _Temp += "\t\tpublic static List<" + Table.ShortName.ToCSharpFriendlyName() + "> Get" + Table.ShortName.ToCSharpFriendlyName() + "(IDbWhereStatement Where, string sql = \"\", string LocalConnectionString = \"\")\n\r";
            _Temp += "\t\t{\n\r";
            _Temp += "\t\tList<" + Table.ShortName.ToCSharpFriendlyName() + "> _TmpReturn = new List<" + Table.ShortName.ToCSharpFriendlyName() + ">();\n\r";

            _Temp += "\t\tusing(var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.IDataAccess>.GetCurrent()){\n\r";

            _Temp = _Temp + "\t\tif (LocalConnectionString == \"\") {\n\r\n\r";
            _Temp = _Temp + "\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\n\r";
            _Temp = _Temp + "\t\t}\n\r\n\r";
            _Temp = _Temp + "\t\telse { \n\r\n\r";
            _Temp = _Temp + "\t\t\tDataAccess.Open(LocalConnectionString);\n\r";
            _Temp = _Temp + "\t\t}\n\r\n\r";


            _Temp += "\t\tstring _Where = DataAccess.GenerateWhereStatement(Where, sql);\n\r";
            _Temp += "\t\tvar _Params = DataAccess.GenerateWhereStatementParameters(Where);\n\r";

            _Temp += "\t\tvar QR = DataAccess.RunCommand(\"Select " + _BuilderTemp3 + " From " + Table.ShortName.ToCSharpFriendlyName() + " Where \" + _Where, _Params, true, System.Data.CommandType.Text);\n\r";

            _Temp += "\t\tif (QR.Tables[0].Rows.Count > 0)\n\r";
            _Temp += "\t\t{\n\r";
            _Temp += "\t\t    foreach (System.Data.DataRow r in QR.Tables[0].Rows)\n\r";
            _Temp += "\t\t    {\n\r";



            _Temp += "\t\t        _TmpReturn.Add(new " + Table.ShortName.ToCSharpFriendlyName() + "(" + _BuilderTemp4 + "));\n\r";
            _Temp += "\t\t    }\n\r";
            _Temp += "\t\t}\n\r";

            _Temp += "return _TmpReturn;\n\r\t\t}}\n\r\n\r";

            _Temp += GenerateSearchMethod(Table, CodeSettings);
            _Temp += "\n\r";

            _Temp += GenerateSearchMethodPaging(Table, CodeSettings);
            _Temp += "\n\r";

            _Temp += GenerateSearchMethodManyParams(Table, CodeSettings);
            _Temp += "\n\r";

            _Temp += GenerateGenericSearchMethod(Table, CodeSettings);
            _Temp += "\n\r";

            _Temp = _Temp + "\n\r\t#endregion\n\r\n\r";

            #endregion

            #endregion

            _Temp += GenerateExportMethod(Table, CodeSettings);

            _Temp += GenerateCreateMethod(Table, CodeSettings);

            if (Table.GetPrimaryColumnNames.Count() > 0)
            {
                _Temp += GenerateUpdateMethod(Table, CodeSettings);
                _Temp += GenerateDeleteMethod(Table, CodeSettings);
            }
            else
            {
                _Temp += GenerateBlankUpdateMethod(Table, CodeSettings);
                _Temp += GenerateBlankDeleteMethod(Table, CodeSettings);
            }


            _Temp += "\n\r\t}";
            _Temp += "\n\r}";
             ACT_GeneratedCode _NewREturn = new ACT_GeneratedCode();
            _NewREturn.FileName = Table.ShortName + ".cs";
            _NewREturn.Code = _Temp;

            _Temp = "";
            _Temp += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
            _Temp += "\n\r\n\r";
            _Temp += "namespace " + CodeSettings.NameSpace + "\n\r{\n\r";



            _Temp += "\tpublic class " + Table.ShortName.ToCSharpFriendlyName() + " : " + Table.ShortName.ToCSharpFriendlyName() + "_CoreClass";
            _Temp += "\n\r\t{\n\r";

            if (_PrimaryKeys.Count > 0)
            {
                _Temp += Environment.NewLine + "\t\tpublic " + Table.ShortName.ToCSharpFriendlyName() + "(string Conn = \"\"){ }" + Environment.NewLine + Environment.NewLine;
            }
            _Temp += "\tpublic " + Table.ShortName.ToCSharpFriendlyName() + "(" + _BuilderTemp + ", string Conn = \"\") : base (" + _BuilderTemp2 + ", Conn)\n\r { }" + Environment.NewLine;

            _Temp += Environment.NewLine + "}" + Environment.NewLine + "}";

            _NewREturn.UserCode = _Temp;

            // WEBSERVICES
            if (CodeSettings.GenerateWebServices)
            {
                string _TmpWebServicesCode = "";
                //  _TmpWebServicesCode = GenerateWebServices(Table, CodeSettings);
                _NewREturn.WebServiceCode = _TmpWebServicesCode;
            }

            return _NewREturn;
        }

        public I_GeneratedCode GenerateViewAccess(I_Db Database, I_CodeGenerationSettings CodeSettings)
        {
            // TODO MARK
            string _ViewTemplate = ACT.Core.SystemSettings.GetSettingByName("AccessViewClassTemplate").Value;
            string _CodeOutput = "";
            _CodeOutput += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
            _CodeOutput += Environment.NewLine + Environment.NewLine;
            _CodeOutput += "namespace " + CodeSettings.NameSpace + ".Views " + Environment.NewLine + "{" + Environment.NewLine;

            _CodeOutput += "\tpublic static class ViewAccess\n\r";
            _CodeOutput += "{\n\r";

            foreach (I_DbView view in Database.Views)
            {
                string _TmpTemplate = _ViewTemplate;
                _TmpTemplate = _TmpTemplate.Replace("#VIEWNAME#", view.ShortName);
                _CodeOutput += _TmpTemplate;
            }

            _CodeOutput += "\t}" + Environment.NewLine + "}";


            ACT_GeneratedCode _NewREturn = new ACT_GeneratedCode();
            _NewREturn.FileName = "ViewAccess.cs";
            _NewREturn.Code = _CodeOutput;

            return _NewREturn;
        }

        /// <summary>
        /// Generates All The Stored Procedures
        /// </summary>
        /// <param name="CodeSettings"></param>
        /// <param name="DataBase"></param>
        /// <returns></returns>
        public I_GeneratedCode GenerateStoredProcedureClass(I_CodeGenerationSettings CodeSettings, I_Db DataBase)
        {
            var DataAccess = ACT.Core.CurrentCore<I_DataAccess>.GetCurrent();
            string _ConnectionString = ACT.Core.SystemSettings.GetSettingByName(CodeSettings.DatabaseConnectionName).Value;

            DataAccess.Open(_ConnectionString);
            string _StoredProcedureCode = "";

            _StoredProcedureCode += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
            _StoredProcedureCode += "\n\r\n\r";
            _StoredProcedureCode += "namespace " + CodeSettings.NameSpace + ".StoredProcedures \n\r{\n\r";

            #region Create Bulk Proc Class
            _StoredProcedureCode += "\tpublic class BulkDatabaseProcedureData\r\n";
            _StoredProcedureCode += "\t{\r\n";
            _StoredProcedureCode += "\tpublic string ProcName;\r\n";
            _StoredProcedureCode += "\tpublic Dictionary<string, System.Data.IDataParameter> Params = new Dictionary<string, System.Data.IDataParameter>();\r\n";
            _StoredProcedureCode += "\t}\r\n";
            #endregion

            _StoredProcedureCode += "public static class DatabaseStoredProcedures\n\r";
            _StoredProcedureCode += "{\n\r";

            #region Create EXEC BULK PROC EXECUTION
            _StoredProcedureCode += "\tpublic static List<bool> EXEC_sp_EXEC_BULK_Procedures(List<BulkDatabaseProcedureData> Params, string Conn=\"\")\r\n";
            _StoredProcedureCode += "\t{\r\n";
            _StoredProcedureCode += "\t\tList<bool> _TmpReturn = new List<bool>();\r\n";
            _StoredProcedureCode += "\t\tusing (var DataAccess = ACT.Core.CurrentCore<IDataAccess>.GetCurrent())\r\n";
            _StoredProcedureCode += "\t\t{\r\n";
            _StoredProcedureCode += "\t\tif (Conn == \"\")\r\n";
            _StoredProcedureCode += "\t\t{\r\n";
            _StoredProcedureCode += "\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\r\n";
            _StoredProcedureCode += "\t\t}\r\n";
            _StoredProcedureCode += "\t\telse\r\n";
            _StoredProcedureCode += "\t\t{\r\n";
            _StoredProcedureCode += "\t\t\tDataAccess.Open(Conn);\r\n";
            _StoredProcedureCode += "\t\t}\r\n";

            _StoredProcedureCode += "\t\tList<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();\r\n";

            _StoredProcedureCode += "\t\tforeach (var data in Params)\r\n";
            _StoredProcedureCode += "\t\t{\r\n";
            _StoredProcedureCode += "\t\t_Params.Clear();\r\n";
            _StoredProcedureCode += "\t\tforeach (var param in data.Params)\r\n";
            _StoredProcedureCode += "\t\t{\r\n";
            _StoredProcedureCode += "\t\t_Params.Add(new System.Data.SQLite.SQLiteParameter(param.Key, param.Value.Value));\r\n";
            _StoredProcedureCode += "\t\t}\r\n";

            _StoredProcedureCode += "\t\tvar _Result = DataAccess.RunCommand(\"[dbo].\" + data.ProcName, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.StoredProcedure);\r\n";

            _StoredProcedureCode += "\t\tif (_Result.Exceptions[0] == null)\r\n";
            _StoredProcedureCode += "\t\t{\r\n";
            _StoredProcedureCode += "\t\t_TmpReturn.Add(true);\r\n";
            _StoredProcedureCode += "\t\t}\r\n";
            _StoredProcedureCode += "\t\t}\r\n";
            _StoredProcedureCode += "\t\t}\r\n";
            _StoredProcedureCode += "\t\treturn _TmpReturn;\r\n";
            _StoredProcedureCode += "\t\t}\r\n";
            #endregion

            foreach (var Proc in DataBase.StoredProcedures)
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

                    _ParamAddList += "\t_Params.Add(new System.Data.SQLite.SQLiteParameter(\"" + ProcParm.Name + "\", " + ProcParm.Name + "_VALUE));\n\r";
                }

                //                _ParamList = _ParamList.TrimEnd(", ");

                string _ProcMethodString = "public static IQueryResult EXEC_" + Proc.Name + "(" + _ParamList + "string Conn = \"\")\n\r";
                _ProcMethodString += "{\n\r";

                _ProcMethodString += "\tusing(var DataAccess = ACT.Core.CurrentCore<IDataAccess>.GetCurrent()){\n\r";

                _ProcMethodString += "\tif (Conn == \"\") {\n\r";

                _ProcMethodString += "\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\n\r";

                _ProcMethodString += "\t } \n\r";

                _ProcMethodString += "\t else \n\r \t { \n\r\t";

                _ProcMethodString += "\tDataAccess.Open(Conn);\n\r";

                _ProcMethodString += "}\n\r";

                _ProcMethodString += "\tList<System.Data.IDataParameter> _Params = new List<System.Data.IDataParameter>();\n\r";

                _ProcMethodString += _ParamAddList;

                string _ExeString = "[" + Proc.Owner + "]." + Proc.Name;

                _ProcMethodString += "\tvar _Result = DataAccess.RunCommand(\"" + _ExeString + "\", _Params, true, System.Data.CommandType.StoredProcedure);\n\r";
                // _ProcMethodString += "\tDataAccess.Dispose();\n\r";

                _ProcMethodString += "\treturn _Result;\n\r";
                _ProcMethodString += "}}\n\r\n\r";
                _StoredProcedureCode += _ProcMethodString;
            }


            // GENERATE BULK EXEC CODE


            ACT_GeneratedCode _NewREturn = new ACT_GeneratedCode() { FileName = "DBStoredProcedures.cs", Code = _StoredProcedureCode + "\n\r}\n\r}" };

            return _NewREturn;
        }


        /// <summary>
        /// Generates a Static Class To Hold Public Variables For All Other Classes
        /// </summary>
        /// <param name="CodeSettings"></param>
        /// <returns></returns>
        public I_GeneratedCode GenerateStaticClass(I_CodeGenerationSettings CodeSettings)
        {

            string _Temp = "";
            _Temp += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
            _Temp += "\n\r\n\r";
            _Temp += "namespace " + CodeSettings.NameSpace + "\n\r{\n\r";
            _Temp += "public static class GenericStaticClass\n\r";
            _Temp += "{\n\r";
            if (CodeSettings.DatabaseConnectionName.NullOrEmpty())
            {
                _Temp += "public static string DatabaseConnectionName = \"DefaultConnectionString\";\n\r";
            }
            else
            {
                _Temp += "public static string DatabaseConnectionName = \"" + CodeSettings.DatabaseConnectionName + "\";\n\r";
            }

            _Temp += "public static string DatabaseConnectionString { get { return ACT.Core.SystemSettings.GetSettingByName(DatabaseConnectionName).Value; } }\n\r";

            _Temp += "}\n\r}\n\r";

            ACT_GeneratedCode _NewREturn = new ACT_GeneratedCode() { FileName = "GenericStaticClass.cs", Code = _Temp };

            return _NewREturn;
        }

        /// <summary>
        /// Generate the Code Base On the IDB Database and The Code Settings Passed In
        /// </summary>
        /// <param name="Database">ACT.Core.Interfaces.DataAccess.IDb - Database Containing the Extracted Meta Data</param>
        /// <param name="CodeSettings">ACT.Core.Interfaces.CodeGeneration.ICodeGenerationSettings - Code Generation Settings</param>
        /// <returns>Generated Code in a List</returns>
        public List<I_GeneratedCode> GenerateCode(I_Db Database, I_CodeGenerationSettings CodeSettings)
        {

            #region Test the Database Configuration Log Errors Only
            var DataAccess = new ACT.Plugins.DataAccess.ACT_SQLite3();

            string _ConnectionString = ACT.Core.SystemSettings.GetSettingByName(CodeSettings.DatabaseConnectionName).Value;

            if (_ConnectionString == "")
            {
                LogError("Error with the DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "\n\r", "Unable To Locate Setting Name\n\r", null, "\n\r", ErrorLevel.Warning);
            }

            if (DataAccess.Open(_ConnectionString) == false)
            {
                LogError("Error Open DB With the ConnectionString Specified By DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "\n\r", "Unable To Open DB\n\r", null, "\n\r", ErrorLevel.Warning);
            }

            #endregion

            // The Variable that Holds The Generated Code
            List<I_GeneratedCode> _TmpReturn = new List<I_GeneratedCode>();

            // Generate The Static Class
            _TmpReturn.Add(GenerateStaticClass(CodeSettings));



            // Generate The Code For Each Table
            foreach (var T in Database.Tables)
            {
                if (T.ShortName == "_GeneratorEnums") { continue; }

                if (T.Description == "ACTIGNORE") { continue; }

                _TmpReturn.Add(GenerateCode(T, CodeSettings));
            }

            // Generate The Enums Table if Setup
            _TmpReturn.Add(GenerateEnums(CodeSettings));


            if (CodeSettings.CompileCode == true)
            {
                Compile(_TmpReturn, CodeSettings);
            }

            if (CodeSettings.OutputSolutionWithProject == true)
            {
                GenerateCSProject(_TmpReturn, CodeSettings);
            }
            else
            {
                if (CodeSettings.GenerateBaseLayer == true)
                {
                    GenerateBaseLayer(_TmpReturn, CodeSettings);
                }

                if (CodeSettings.GenerateUserLayer == true)
                {
                    GenerateUserLayer(_TmpReturn, CodeSettings);
                }

                //if (CodeSettings.GenerateStoredProcedures == true)
                //{
                //    // Generate The Stored Procedures class
                //    _TmpReturn.Add(GenerateStoredProcedureClass(CodeSettings, Database));

                //    GenerateStoredProcedures(_TmpReturn, CodeSettings);
                //}

                //if (CodeSettings.GenerateWebServices == true)
                //{
                //    GenerateWebServiceLayer(_TmpReturn, CodeSettings);
                //}

                //if (CodeSettings.GenerateViewAccess == true)
                //{
                //    _TmpReturn.Add(GenerateViewAccess(Database, CodeSettings));
                //    GenerateViewAccessCode(_TmpReturn, CodeSettings);
                //}
            }

            return _TmpReturn;
        }

        #region Code To Create Files
        private void GenerateStoredProcedures(List<I_GeneratedCode> Code, I_CodeGenerationSettings CodeSettings)
        {
            if (!System.IO.Directory.Exists(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Base\\"))
            {
                System.IO.Directory.CreateDirectory(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Base\\");
            }

            foreach (var c in Code)
            {
                if (c.FileName.Contains("DBStoredProcedures"))
                {
                    System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Base\\" + c.FileName.Replace(".cs", "") + "_Base.cs", c.Code);
                }
            }
        }

        private void GenerateUserLayer(List<I_GeneratedCode> Code, I_CodeGenerationSettings CodeSettings)
        {
            if (System.IO.Directory.Exists(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "User\\"))
            {
                System.IO.Directory.Delete(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "User\\", true);
            }

            System.Threading.Thread.Sleep(500);

            System.IO.Directory.CreateDirectory(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "User\\");
            foreach (var c in Code)
            {
                System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "User\\" + c.FileName.Replace(".cs", "") + "_User" + ".cs", c.UserCode);
            }
        }

        private void GenerateBaseLayer(List<I_GeneratedCode> Code, I_CodeGenerationSettings CodeSettings)
        {
            if (System.IO.Directory.Exists(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Base\\"))
            {
                System.IO.Directory.Delete(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Base\\", true);
            }

            /// Wait For Windows to Clean Up

            System.Threading.Thread.Sleep(500);

            System.IO.Directory.CreateDirectory(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Base\\");
            foreach (var c in Code)
            {
                System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Base\\" + c.FileName.Replace(".cs", "") + "_Base" + ".cs", c.Code);
            }
        }
        private void GenerateCSProject(List<I_GeneratedCode> Code, I_CodeGenerationSettings CodeSettings)
        {
            if (System.IO.Directory.Exists(CodeSettings.RootOutputDirectory + "Project\\"))
            {
                System.IO.Directory.Delete(CodeSettings.RootOutputDirectory + "Project\\", true);

            }

            System.IO.Directory.CreateDirectory(CodeSettings.RootOutputDirectory + "Project\\");
            System.IO.Directory.CreateDirectory(CodeSettings.RootOutputDirectory + "Project\\Base\\");
            System.IO.Directory.CreateDirectory(CodeSettings.RootOutputDirectory + "Project\\User\\");
            System.IO.Directory.CreateDirectory(CodeSettings.RootOutputDirectory + "Project\\Bin\\");
            string _BaseItems = "";
            string _UserItems = "";
            foreach (var c in Code)
            {
                System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory + "\\Project\\Base\\" + c.FileName.Replace(".cs", "") + "_Base" + ".cs", c.Code);
                System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory + "\\Project\\User\\" + c.FileName.Replace(".cs", "") + "_User" + ".cs", c.UserCode);

                _BaseItems += "\t<Compile Include=\"Base\\" + c.FileName.Replace(".cs", "") + "_Base" + ".cs" + "\" />\n\r";
                _UserItems += "\t<Compile Include=\"User\\" + c.FileName.Replace(".cs", "") + "_User" + ".cs" + "\" />\n\r";
            }

            string _ProjectCode = ACT.Core.SystemSettings.GetSettingByName("C#ProjectTemplate").Value;
            _ProjectCode = _ProjectCode.Replace("#ASSEMBLYNAME#", CodeSettings.DLLName);
            _ProjectCode = _ProjectCode.Replace("#ITEMS#", _BaseItems + _UserItems);
            _ProjectCode = _ProjectCode.Replace("#NEWGUID#", Guid.NewGuid().ToString());

            System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory + "\\Project\\CSharpProject.csproj", _ProjectCode);
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ACT.Core.dll"))
            {
                System.IO.File.Copy(AppDomain.CurrentDomain.BaseDirectory + "ACT.Core.dll", CodeSettings.RootOutputDirectory + "\\Project\\Bin\\ACT.Core.dll");
            }
            else if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Bin\\ACT.Core.dll"))
            {
                System.IO.File.Copy(AppDomain.CurrentDomain.BaseDirectory + "Bin\\ACT.Core.dll", CodeSettings.RootOutputDirectory + "\\Project\\Bin\\ACT.Core.dll");
            }

            //TODO Fix ME
            //System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory + "\\Project\\Bin\\SystemConfiguration.xml", ACT.Core.SystemSettings.ExportXMLData());

        }

        private void GenerateViewAccessCode(List<I_GeneratedCode> Code, I_CodeGenerationSettings CodeSettings)
        {
            if (!System.IO.Directory.Exists(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Base\\"))
            {
                System.IO.Directory.CreateDirectory(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Base\\");
            }

            foreach (var c in Code)
            {
                if (c.FileName.Contains("ViewAccess"))
                {
                    System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Base\\" + c.FileName.Replace(".cs", "") + "_Base.cs", c.Code);
                }
            }
        }

        #endregion

        /// <summary>
        /// Compiles the Code Into a DLL
        /// </summary>
        /// <param name="OnlyBase"></param>
        /// <param name="Code"></param>
        /// <param name="CodeSettings"></param>
        private void Compile(List<I_GeneratedCode> Code, I_CodeGenerationSettings CodeSettings)
        {

            List<string> _BaseCode = new List<string>();
            foreach (var c in Code)
            {
                _BaseCode.Add(c.Code);
            }

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = false;
            parameters.WarningLevel = 3;
            parameters.CompilerOptions = "/optimize";
            parameters.OutputAssembly = CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + CodeSettings.DLLName;
            parameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("System.Data.dll");
            parameters.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
            parameters.ReferencedAssemblies.Add("System.XML.dll");
            parameters.ReferencedAssemblies.Add("System.XML.Linq.dll");
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ACT.Core.dll"))
            {
                parameters.ReferencedAssemblies.Add(AppDomain.CurrentDomain.BaseDirectory + "ACT.Core.dll");
            }
            else if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Bin\\ACT.Core.dll"))
            {
                parameters.ReferencedAssemblies.Add(AppDomain.CurrentDomain.BaseDirectory + "Bin\\ACT.Core.dll");
            }


            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, _BaseCode.ToArray());

            if (results.Errors.Count > 0)
            {
                LogError(results.Errors[0].ErrorText, "Error Compiling", null, "", ErrorLevel.Critical);
                throw new Exception("Error Compiling..");
            }
        }

        /// <summary>
        /// Generates The Enums
        /// </summary>
        /// <param name="NameValues">Name And Values To Generate</param>
        /// <param name="TableName">Table Name of EnumTable</param>
        /// <returns></returns>
        public string GenerateEnum(Dictionary<string, int> NameValues, string TableName)
        {
            string _TmpReturn = "";
            _TmpReturn = "public enum " + TableName.ToCSharpFriendlyName() + " \n\r\t{ \n\r";

            foreach (string k in NameValues.Keys)
            {
                _TmpReturn += "\n\r\t\t" + k.Replace(" ", "_").ToCSharpFriendlyName() + " = " + NameValues[k] + ",";
            }
            _TmpReturn = _TmpReturn.TrimEnd(",");
            _TmpReturn += "\t}\n\r";

            return _TmpReturn;
        }

        /// <summary>
        /// Generates The Enums..  This is a Specific Class That Utilizes a Table Named _GeneratorEnums
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="CodeSettings"></param>
        /// <returns></returns>
        public I_GeneratedCode GenerateEnums(I_CodeGenerationSettings CodeSettings)
        {
            bool _UseDatabaseConnectionName = false;
            if (CodeSettings.DatabaseConnectionName != "")
            {
                _UseDatabaseConnectionName = true;
            }

            string _Temp = "";
            _Temp += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
            _Temp += "\n\r\n\r";
            _Temp += "namespace " + CodeSettings.NameSpace + "\n\r{\n\r";
            _Temp += "\tpublic class Enums : ACT.Plugins.ACTCore \n\r\t{\n\r";

            using (var DataAccess = new ACT.Plugins.DataAccess.ACT_SQLite3())
            {
                if (_UseDatabaseConnectionName)
                {
                    DataAccess.Open(ACT.Core.SystemSettings.GetSettingByName(CodeSettings.DatabaseConnectionName).Value);
                }
                else
                {

                    DataAccess.Open();

                }

                var QR = DataAccess.RunCommand("Select * From _GeneratorEnums", null, true, System.Data.CommandType.Text);

                if (QR.Errors.Count() > 0 && QR.Errors[0] == true)
                {
                    LogError(this.GetType().FullName, "Error Generating Enums. Missing Meta Data Table: _GeneratorEnums", QR.Exceptions[0], "", ErrorLevel.Warning);
                }
                else
                {
                    foreach (System.Data.DataRow DRow in QR.Tables[0].Rows)
                    {
                        string _tmpDataSQL = "select " + DRow["FieldName"].ToString() + ", " + DRow["ValueFieldName"].ToString() + " from " + DRow["TableName"].ToString();

                        var DQR = DataAccess.RunCommand(_tmpDataSQL, null, true, System.Data.CommandType.Text);

                        Dictionary<string, int> _TmpEnumData = new Dictionary<string, int>();
                        foreach (System.Data.DataRow DDRow in DQR.Tables[0].Rows)
                        {
                            _TmpEnumData.Add(DDRow[DRow["FieldName"].ToString()].ToString(), Convert.ToInt32(DDRow[DRow["ValueFieldName"].ToString()]));
                        }

                        _Temp += GenerateEnum(_TmpEnumData, DRow["TableName"].ToString());
                    }
                }
            }

            _Temp += "\t}\n\r}\n\r";  //Close Namespace and Class

            ACT_GeneratedCode _NewREturn = new ACT_GeneratedCode();
            _NewREturn.FileName = "Enums.cs";
            _NewREturn.Code = _Temp;

            return _NewREturn;
        }

        /// <summary>
        /// Generates the Default Data access
        /// </summary>
        /// <param name="CodeSettings"></param>
        /// <returns></returns>
        public string GenerateGenericDBAccess(I_CodeGenerationSettings CodeSettings)
        {
            StringBuilder _TmpBuilder = new StringBuilder("\n\r\n\r");

            _TmpBuilder.Append("public static ACT.Core.Interfaces.DataAccess.IDataAccess GetDataAccess()\n\r");
            _TmpBuilder.Append("{\n\r");
            _TmpBuilder.Append("\tusing( var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.IDataAccess>.GetCurrent()){ \n\r");
            _TmpBuilder.Append("\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\n\r");
            _TmpBuilder.Append("\n\r");
            _TmpBuilder.Append("\treturn DataAccess;}\n\r");

            _TmpBuilder.Append("}\n\r");

            return _TmpBuilder.ToString();


        }


        /// <summary>
        /// Generate The Export Method Currently Supports XML and JSON
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="CodeSettings"></param>
        /// <returns></returns>
        private string GenerateExportMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            string _TmpReturn = "";
            _TmpReturn = "\tpublic string Export(string Format)\n\r";
            _TmpReturn = _TmpReturn += "\t{\n\r";
            _TmpReturn = _TmpReturn += "\t    if (Format.ToLower() == \"xml\")\n\r";
            _TmpReturn = _TmpReturn += "\t    {\n\r";
            _TmpReturn = _TmpReturn += "\t        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(this.GetType() );\n\r";
            _TmpReturn = _TmpReturn += "\t        System.IO.StringWriter _Output = new System.IO.StringWriter();\n\r";
            _TmpReturn = _TmpReturn += "\t        x.Serialize(_Output, this);\n\r";
            _TmpReturn = _TmpReturn += "\t        return _Output.ToString();\n\r";
            _TmpReturn = _TmpReturn += "\t    }\n\r";
            _TmpReturn = _TmpReturn += "\t    else if (Format.ToLower() == \"json\")\n\r";
            _TmpReturn = _TmpReturn += "\t    {\n\r";
            _TmpReturn = _TmpReturn += "\t        return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);\n\r";
            _TmpReturn = _TmpReturn += "\t    }\n\r";
            _TmpReturn = _TmpReturn += "\t\n\r";
            _TmpReturn = _TmpReturn += "\t    return \"\";\n\r";
            _TmpReturn = _TmpReturn += "\t}\n\r";
            return _TmpReturn;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="CodeSettings"></param>
        /// <returns></returns>
        private string GenerateBlankUpdateMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            StringBuilder _TmpBuilder = new StringBuilder();
            _TmpBuilder.Append("public virtual void Update()\n\r");
            _TmpBuilder.Append("{\n\r // No Primary Keys Found Must Define Manually \n }");
            return _TmpBuilder.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="CodeSettings"></param>
        /// <returns></returns>
        private string GenerateBlankDeleteMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            StringBuilder _TmpBuilder = new StringBuilder();
            _TmpBuilder.Append("public virtual void Delete()\n\r");
            _TmpBuilder.Append("{\n\r // No Primary Keys Found Must Define Manually \n }");
            return _TmpBuilder.ToString();
        }
 
        // TODO
        private string GenerateChildDeleteMethods(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            return "";
        }

        private string GenerateSearchMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            if (Table.GetPrimaryColumnNames.Count() == 0) { return ""; }
            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public static List<" + Table.ShortName.ToCSharpFriendlyName() + "> Search(string FieldName, string SearchText, bool UseContains = false, string LocalConnectionString = \"\")\n\r");
            _TmpBuilder.Append("{\n\r");
            _TmpBuilder.Append("List<" + Table.ShortName.ToCSharpFriendlyName() + "> _TmpReturn = new List<" + Table.ShortName.ToCSharpFriendlyName() + ">();\n\r");

            _TmpBuilder.Append("using (var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.IDataAccess>.GetCurrent()){\n\r\n\r");
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {\n\r\n\r");
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\n\r");
            _TmpBuilder.Append("\t\t}\n\r\n\r");
            _TmpBuilder.Append("\t\telse { \n\r\n\r");
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);\n\r");
            _TmpBuilder.Append("\t\t}\n\r\n\r");

            _TmpBuilder.Append("List<System.Data.SQLite.SQLiteParameter> _Params = new List<System.Data.SQLite.SQLiteParameter>();\n\r");

            _TmpBuilder.Append("_Params.Add(new System.Data.SQLite.SQLiteParameter(\"SearchParam\", SearchText));\n\r");


            _TmpBuilder.Append("var SQL = \"\";\n\r");

            _TmpBuilder.Append("if (UseContains == true) { \n\r");
            _TmpBuilder.Append("SQL = SQL + \"Select " + Table.GeneratePrimaryKeySelectListMSSQL() + " From " + Table.ShortName.ToCSharpFriendlyName() + " Where [\" + FieldName + \"] like '%' + @SearchParam + '%'\";\n\r");
            _TmpBuilder.Append("} \n\r");
            _TmpBuilder.Append("else { \n\r");
            _TmpBuilder.Append("SQL = SQL + \"Select " + Table.GeneratePrimaryKeySelectListMSSQL() + " From " + Table.ShortName.ToCSharpFriendlyName() + " Where [\" + FieldName + \"] = @SearchParam\";\n\r");
            _TmpBuilder.Append("}\n\r\n\r");

            _TmpBuilder.Append("var Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);\n\r");

            _TmpBuilder.Append("if (Result.Tables[0].Rows.Count > 0)\n\r");
            _TmpBuilder.Append("{\n\r");
            _TmpBuilder.Append("for (int x = 0; x < Result.Tables[0].Rows.Count; x++)\n\r");
            _TmpBuilder.Append("{\n\r");
            _TmpBuilder.Append("if (Result.Tables[0].Rows[x][0] != DBNull.Value)\n\r");
            _TmpBuilder.Append("{\n\r");
            _TmpBuilder.Append("_TmpReturn.Add(new " + Table.ShortName.ToCSharpFriendlyName() + "(");


            foreach (string pkcn in Table.GetPrimaryColumnNames)
            {

                _TmpBuilder.Append("(" + Table.GetColumn(pkcn, true).DataType.ToCSharpString() + ")Result.Tables[0].Rows[x][\"" + pkcn + "\"],");
            }
            _TmpBuilder.Remove(_TmpBuilder.Length - 1, 1);
            _TmpBuilder.Append("));\n\r");
            _TmpBuilder.Append("}\n\r");
            _TmpBuilder.Append("}\n\r");
            _TmpBuilder.Append("}}\n\r");

            _TmpBuilder.Append("return _TmpReturn;\n\r");
            _TmpBuilder.Append("}\n\r");

            return _TmpBuilder.ToString();
        }

        private string GenerateSearchMethodManyParams(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            if (Table.GetPrimaryColumnNames.Count() == 0) { return ""; }
            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public static List<" + Table.ShortName.ToCSharpFriendlyName() + "> Search(Dictionary<string,string> FieldValues, string LocalConnectionString = \"\")\n\r");
            _TmpBuilder.Append("{\n\r");
            _TmpBuilder.Append("List<" + Table.ShortName.ToCSharpFriendlyName() + "> _TmpReturn = new List<" + Table.ShortName.ToCSharpFriendlyName() + ">();\n\r");

            _TmpBuilder.Append("using (var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.IDataAccess>.GetCurrent()){\n\r\n\r");
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {\n\r\n\r");
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\n\r");
            _TmpBuilder.Append("\t\t}\n\r\n\r");
            _TmpBuilder.Append("\t\telse { \n\r\n\r");
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);\n\r");
            _TmpBuilder.Append("\t\t}\n\r\n\r");

            _TmpBuilder.Append("List<System.Data.SQLite.SQLiteParameter> _Params = new List<System.Data.SQLite.SQLiteParameter>();\n\r");

            _TmpBuilder.Append("foreach(var skey in FieldValues.Keys) { _Params.Add(new System.Data.SQLite.SQLiteParameter(skey, FieldValues[skey]));\n\r } \n\r");

            _TmpBuilder.Append("var SQL = \"\";\n\r");
            _TmpBuilder.Append("SQL = SQL + \"Select " + Table.GeneratePrimaryKeySelectListMSSQL() + " From " + Table.ShortName.ToCSharpFriendlyName() + " where \";\n\r");

            _TmpBuilder.Append("foreach (var skey in FieldValues.Keys)\n\r");
            _TmpBuilder.Append("{\n\r");
            _TmpBuilder.Append("SQL = SQL + \"[\" + skey + \"] = @\" + skey + \" AND \";\n\r");
            _TmpBuilder.Append("}\n\r");

            _TmpBuilder.Append("SQL = SQL.Substring(0, SQL.LastIndexOf(\"AND\"));\n\r");

            _TmpBuilder.Append("\n\r\n\r");

            _TmpBuilder.Append("var Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);\n\r");

            _TmpBuilder.Append("if (Result.Tables[0].Rows.Count > 0)\n\r");
            _TmpBuilder.Append("{\n\r");
            _TmpBuilder.Append("for (int x = 0; x < Result.Tables[0].Rows.Count; x++)\n\r");
            _TmpBuilder.Append("{\n\r");
            _TmpBuilder.Append("if (Result.Tables[0].Rows[x][0] != DBNull.Value)\n\r");
            _TmpBuilder.Append("{\n\r");
            _TmpBuilder.Append("_TmpReturn.Add(new " + Table.ShortName.ToCSharpFriendlyName() + "(");

            foreach (string pkcn in Table.GetPrimaryColumnNames)
            {

                _TmpBuilder.Append("(" + Table.GetColumn(pkcn, true).DataType.ToCSharpString() + ")Result.Tables[0].Rows[x][\"" + pkcn + "\"],");
            }
            _TmpBuilder.Remove(_TmpBuilder.Length - 1, 1);
            _TmpBuilder.Append("));\n\r");
            _TmpBuilder.Append("}\n\r");
            _TmpBuilder.Append("}\n\r");
            _TmpBuilder.Append("}}\n\r");

            _TmpBuilder.Append("return _TmpReturn;\n\r");
            _TmpBuilder.Append("}\n\r");

            return _TmpBuilder.ToString();
        }

        private string GenerateGenericSearchMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public static System.Data.DataTable Search(string FieldName, string SearchText, string OrderByStatement, bool UseContains = false, string LocalConnectionString = \"\")\n\r");
            _TmpBuilder.Append("{\n\r");

            _TmpBuilder.Append("using(var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.IDataAccess>.GetCurrent()){\n\r");
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {\n\r\n\r");
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\n\r");
            _TmpBuilder.Append("\t\t}\n\r\n\r");
            _TmpBuilder.Append("\t\telse { \n\r\n\r");
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);\n\r");
            _TmpBuilder.Append("\t\t}\n\r\n\r");

            _TmpBuilder.Append("List<System.Data.SQLite.SQLiteParameter> _Params = new List<System.Data.SQLite.SQLiteParameter>();\n\r");

            _TmpBuilder.Append("_Params.Add(new System.Data.SQLite.SQLiteParameter(\"SearchParam\", SearchText));\n\r");


            _TmpBuilder.Append("var SQL = \"\";\n\r");

            _TmpBuilder.Append("if (UseContains == true) { \n\r");
            _TmpBuilder.Append("SQL = SQL + \"Select * From " + Table.ShortName.ToCSharpFriendlyName() + " Where [\" + FieldName + \"] like '%' + @SearchParam + '%'\";");
            _TmpBuilder.Append("} \n\r");
            _TmpBuilder.Append("else { \n\r");
            _TmpBuilder.Append("SQL = SQL + \"Select * From " + Table.ShortName.ToCSharpFriendlyName() + " Where [\" + FieldName + \"] = @SearchParam\";");
            _TmpBuilder.Append("}\n\r\n\r");

            _TmpBuilder.Append("var Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);\n\r");
            _TmpBuilder.Append("return Result.Tables[0];\n\r");
            _TmpBuilder.Append("}\n\r\n\r");


            _TmpBuilder.Append("}\n\r");

            return _TmpBuilder.ToString();
        }

        private string GenerateSearchMethodPaging(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {

            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public static System.Data.DataTable Search(string FieldName, string SearchText, string OrderByStatement, int Start, int Increment, bool UseContains = false, string LocalConnectionString = \"\")\n\r");
            _TmpBuilder.Append("{\n\r");

            _TmpBuilder.Append("using(var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.IDataAccess>.GetCurrent()){\n\r");
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {\n\r\n\r");
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);\n\r");
            _TmpBuilder.Append("\t\t}\n\r\n\r");
            _TmpBuilder.Append("\t\telse { \n\r\n\r");
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);\n\r");
            _TmpBuilder.Append("\t\t}\n\r\n\r");

            _TmpBuilder.Append("List<System.Data.SQLite.SQLiteParameter> _Params = new List<System.Data.SQLite.SQLiteParameter>();\n\r");

            _TmpBuilder.Append("_Params.Add(new System.Data.SQLite.SQLiteParameter(\"SearchParam\", SearchText));\n\r");


            _TmpBuilder.Append("var SQL = \"\";\n\r");

            _TmpBuilder.Append("if (UseContains == true) { \n\r");
            _TmpBuilder.Append("SQL = SQL + \"Select * From ( Select *, ROW_NUMBER() Over(order by \" + OrderByStatement + \") as RowNum From " + Table.ShortName.ToCSharpFriendlyName() + " Where [\" + FieldName + \"] like '%' + @SearchParam + '%') as T Where T.RowNum > \" + (Start-1).ToString() + \" AND T.RowNum < \" + (Start + Increment).ToString();\n\r");
            _TmpBuilder.Append("} \n\r");
            _TmpBuilder.Append("else { \n\r");
            _TmpBuilder.Append("SQL = SQL + \"Select * From ( Select *, ROW_NUMBER() Over(order by \" + OrderByStatement + \") as RowNum From " + Table.ShortName.ToCSharpFriendlyName() + " Where [\" + FieldName + \"] = @SearchParam) as T Where T.RowNum > \" + (Start-1).ToString() + \" AND T.RowNum < \" + (Start + Increment).ToString();\n\r");
            _TmpBuilder.Append("}\n\r\n\r");

            _TmpBuilder.Append("var Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);\n\r");
            _TmpBuilder.Append("return Result.Tables[0];\n\r");
            _TmpBuilder.Append("}\n\r\n\r");


            _TmpBuilder.Append("}\n\r");

            return _TmpBuilder.ToString();
        }



        #region OverRidden Methods
        public override I_TestResult HealthCheck()
        {
            return ValidatePluginRequirements();
        }

        public override List<string> ReturnSystemSettingRequirements()
        {
            string[] _S = new string[] { "ACTCodeGenerator_Namespace", "ACTCodeGenerator_UsingStatements", "C#ProjectTemplate", "ASPNETTemplate", "ASPNETTemplateRow", "ASPNETTemplateCodeFile", "AccessViewClassTemplate" };
            return _S.ToList<string>();
        }

        public override I_TestResult ValidatePluginRequirements()
        {
            var _TmpReturn = ACT.Core.SystemSettings.MeetsExpectations((ACT.Core.Interfaces.Common.I_Plugin)this);

            return _TmpReturn;
        }

        public override void SetImpersonate(ACT.Core.Interfaces.Security.Authentication.I_UserInfo Info)
        {
            throw new Exception("Not Implemented");
        }

        public void SetImpersonate(object UserInfo)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
