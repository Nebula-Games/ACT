// ***********************************************************************
// Assembly         : ACTPlugins
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="ACT_CodeGenerator.cs" company="Stonegate Intel">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using ACT.Core.Enums;
using ACT.Core.Extensions;
using ACT.Core.Extensions.CodeGenerator;
using ACT.Core.Interfaces.CodeGeneration;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.TemplateEngine;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
        #region Variables Used By The Generator

        /// <summary>
        /// The requirements
        /// </summary>
        List<string> _Requirements = new List<string>();

        /// <summary>
        /// SET NL = New Line
        /// </summary>
        private static string NL = Environment.NewLine;

        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Generates a CLONE Method Called CopyClass </summary>
        ///
        /// <remarks>   Mark Alicz, 8/22/2019. </remarks>
        ///
        /// <param name="ClassName">    Name of the class. </param>
        ///
        /// <returns>   The clone method. </returns>
        ///-------------------------------------------------------------------------------------------------
        private string GenerateCloneMethod(string ClassName)
        {
            string _Temp = "\t\tpublic " + ClassName + " CopyClass()" + Environment.NewLine;
            _Temp += "\t\t{" + Environment.NewLine;

            _Temp += "\t\t\tusing (MemoryStream ms = new MemoryStream())" + Environment.NewLine;
            _Temp += "\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\tBinaryFormatter formatter = new BinaryFormatter();" + Environment.NewLine;
            _Temp += "\t\t\t\tformatter.Serialize(ms, this);" + Environment.NewLine;
            _Temp += "\t\t\t\tms.Position = 0;" + Environment.NewLine;
            _Temp += "\t\t\t\treturn (" + ClassName + ")formatter.Deserialize(ms);" + Environment.NewLine;
            _Temp += "\t\t\t}" + Environment.NewLine;
            _Temp += "\t\t}" + Environment.NewLine;

            return _Temp;
        }

        #region Generate CODE MEthods (Main MEthods)

        /// <summary>
        /// Generate the Code Base On the IDB Database and The Code Settings Passed In
        /// </summary>
        /// <param name="CodeSettings">ACT.Core.Interfaces.CodeGeneration.ICodeGenerationSettings - Code Generation Settings</param>
        /// <returns>Generated Code in a List</returns>
        /// <exception cref="Exception">
        /// Error Generating Code: Unable To Connect To Database: Using Setting Name: " + CodeSettings.DatabaseConnectionName
        /// or
        /// Error Generating Code: Unable To Connect To Database: Using Setting Name: " + CodeSettings.DatabaseConnectionName
        /// </exception>
        public List<I_GeneratedCode> GenerateCode(I_CodeGenerationSettings CodeSettings)
        {
            EnsureCodeSettings(CodeSettings);

            //  CodeSettings.DatabaseConnectionString = ACT.Core.SystemSettings.GetSettingByName(CodeSettings.DatabaseConnectionName).Value;

            var _TestConnResults = TestCodeSettingsConnection(false, CodeSettings);

            if (_TestConnResults == false)
            {
                LogError("Error Open DB With the ConnectionString Specified By DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "" + Environment.NewLine, "Unable To Open DB" + Environment.NewLine, null, "" + Environment.NewLine, ErrorLevel.Critical);
                throw new Exception("Error Generating Code: Unable To Connect To Database: Using Setting Name: " + CodeSettings.DatabaseConnectionName);
            }

            using (var DataAccess = ACT.Core.CurrentCore<I_DataAccess>.GetCurrent())
            {
                string _ConnectionString = ACT.Core.SystemSettings.GetSettingByName(CodeSettings.DatabaseConnectionName).Value;

                if (DataAccess.Open(_ConnectionString))
                {
                    // Export The Database
                    var DB = DataAccess.ExportDatabase();

                    // Call Internal Generate Code Method With the Database Exported
                    return GenerateCode(DB, CodeSettings);
                }
                else
                {
                    LogError("Error Open DB With the ConnectionString Specified By DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "" + Environment.NewLine, "Unable To Open DB" + Environment.NewLine, null, "" + Environment.NewLine, ErrorLevel.Critical);
                    throw new Exception("Error Generating Code: Unable To Connect To Database: Using Setting Name: " + CodeSettings.DatabaseConnectionName);
                }
            }
        }

        /// <summary>
        /// Generate the Code Base On the IDB Database and The Code Settings Passed In
        /// </summary>
        /// <param name="Database">ACT.Core.Interfaces.DataAccess.IDb - Database Containing the Extracted Meta Data</param>
        /// <param name="CodeSettings">ACT.Core.Interfaces.CodeGeneration.ICodeGenerationSettings - Code Generation Settings</param>
        /// <returns>Generated Code in a List</returns>
        public List<I_GeneratedCode> GenerateCode(I_Db Database, I_CodeGenerationSettings CodeSettings)
        {
            // Load the Correct Settings File
            EnsureCodeSettings(CodeSettings);

            #region Test the Database Configuration Log Errors Only
            using (var DataAccess = ACT.Core.CurrentCore<I_DataAccess>.GetCurrent())
            {
                string _ConnectionString = ACT.Core.SystemSettings.GetSettingByName(CodeSettings.DatabaseConnectionName).Value;

                if (_ConnectionString == "")
                {
                    LogError("Error with the DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "" + Environment.NewLine, "Unable To Locate Setting Name" + Environment.NewLine, null, "" + Environment.NewLine, ErrorLevel.Critical);
                }

                if (ACT.Core.Validation.DatabaseValidations.ValidateConnectionString(_ConnectionString) == false)
                {
                    LogError("ACT_CodeGenerator", "Connection String is Invalid: Trying Again", new ACT.Core.Exceptions.InvalidFormatExceptions("Doesn't Match Connection String"), _ConnectionString, ErrorLevel.Informational);
                }

                if (DataAccess.Open(_ConnectionString) == false)
                {
                    LogError("Error Open DB With the ConnectionString Specified By DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "" + Environment.NewLine, "Unable To Open DB" + Environment.NewLine, null, "" + Environment.NewLine, ErrorLevel.Warning);
                }
            }
            #endregion

            // The Variable that Holds The Generated Code
            List<I_GeneratedCode> _TmpReturn = new List<I_GeneratedCode>();

            // Generate The Static Class This Holds the Connection String and Name
            _TmpReturn.Add(GenerateStaticClass(CodeSettings));
            
            // Generate The Code For Each Table
            foreach (var T in Database.Tables)
            {
                // Skip Tables That Are Not New Enough
                if (CodeSettings.ObjectAgeMax > -1 && T.AgeInDays >= CodeSettings.ObjectAgeMax) { continue; }


                // Ignore Keyword Tables
                if (T.ShortName == "_GeneratorEnums" || T.Description == "ACTIGNORE") { continue; }

                // Ignore tables that Do not have 1 Primary Key
                if (T.GetPrimaryColumnNames.Count() != 1) { continue; }

                // Generate the Code for the Table
                var _GeneratedTableCode = GenerateCode(T, CodeSettings);

                // Add the Generated Table Code IF It has Code Otherwise Log an Error
                if (_GeneratedTableCode.Code.NullOrEmpty() == false) { _TmpReturn.Add(_GeneratedTableCode); }
                else { LogError("GenerateCode(T,CodeSettings)", "Table Name: " + T.Name, null, "", ErrorLevel.Informational); }
            }

            // If Generator Enums Exist Add That File Name.
            if (Database.TableNames.Exists(t => t.ToLower() == "_generatorenums")) { _TmpReturn.Add(GenerateEnums(CodeSettings)); }

            // Compile the Code
            if (CodeSettings.CompileCode == true) { Compile(_TmpReturn, CodeSettings); }

            // Generate The Base Layer
            if (CodeSettings.GenerateBaseLayer == true) { GenerateBaseLayer(_TmpReturn, CodeSettings); }

            // Generate The User Layer
            if (CodeSettings.GenerateUserLayer == true) { GenerateUserLayer(_TmpReturn, CodeSettings); }

            // Generate the Stored Procedures
            if (CodeSettings.GenerateStoredProcedures == true)
            {
                var _StoredProcedureCode = GenerateStoredProcedureClass(CodeSettings, Database);
                _TmpReturn.AddRange(_StoredProcedureCode);
                GenerateStoredProceduresFile(_TmpReturn, CodeSettings);
            }

            // Generate Web Services
            if (CodeSettings.GenerateWebServices == true) { GenerateWebServiceLayer(_TmpReturn, CodeSettings); }

            // Generate The View Access
            if (CodeSettings.GenerateViewAccess == true)
            {
                var _ViewAccessCode = GenerateViewAccess(Database, CodeSettings);
                _TmpReturn.Add(_ViewAccessCode);
                GenerateViewAccessCode(_TmpReturn, CodeSettings);
            }

            // Generate the CS Project If Requested
            if (CodeSettings.OutputSolutionWithProject == true) { GenerateCSProject(_TmpReturn, CodeSettings); }

            

            // Return the Generated Code Structure
            return _TmpReturn;
        }

        /// <summary>
        /// Generate The Tables Code using the Code Settings
        /// </summary>
        /// <param name="Table">ACT.Core.Interfaces.DataAccess.IDbTable - Table Information</param>
        /// <param name="CodeSettings">ACT.Core.Interfaces.DataAccess.ICodeGenerationSettings - Code Settings</param>
        /// <returns>I_GeneratedCode.</returns>
        public I_GeneratedCode GenerateCode(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            EnsureCodeSettings(CodeSettings);

            List<string> _PrimaryKeys = Table.GetPrimaryColumnNames;
            string _Temp = "";
            _Temp += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
            

            _Temp += Environment.NewLine + "" + Environment.NewLine;
            _Temp += "namespace " + CodeSettings.NameSpace + Environment.NewLine + "{" + Environment.NewLine + Environment.NewLine; 
            _Temp += "\t[Serializable()]" + Environment.NewLine;
            _Temp += "\tpublic class " + Table.ShortName.ToCSharpFriendlyName() + "_CoreClass : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.DataAccess.I_DataObject " + Environment.NewLine + "\t{" + Environment.NewLine;

            _Temp += GenerateCloneMethod(Table.ShortName.ToCSharpFriendlyName() + "_CoreClass");
            _Temp += Environment.NewLine + Environment.NewLine;

            #region Private Fields
            _Temp = _Temp + "\t\t#region Private Fields" + Environment.NewLine + Environment.NewLine;
            _Temp = _Temp + "\t\tprivate string _LocalConnectionString = \"\";" + Environment.NewLine;
            _Temp = _Temp + "\t\tprivate bool _LoadedCorrectly = false;" + Environment.NewLine + Environment.NewLine;

            _Temp = _Temp + "\t\tpublic bool LoadedCorrectly { get { return _LoadedCorrectly; } set { _LoadedCorrectly = value; } }" + Environment.NewLine + Environment.NewLine;


            for (int x = 0; x < Table.ColumnCount; x++)
            {
                var c = Table.GetColumn(x);
                _Temp = _Temp + "\t\t\tprivate " + c.DataType.ToCSharpStringNullable() + " __" + c.Name.ToCSharpFriendlyName() + ";" + Environment.NewLine + Environment.NewLine;
                _Temp = _Temp + "\t\t\t[NonSerialized]" + Environment.NewLine;
                _Temp = _Temp + "\t\t\tprivate string __" + c.Name.ToCSharpFriendlyName() + "_Description = ";

                if (c.Description.NullOrEmpty() == true)
                {
                    _Temp = _Temp + "\"\";" + Environment.NewLine;
                }
                else
                {
                    _Temp = _Temp + "\"" + c.Description + "\";" + Environment.NewLine;
                }
                _Temp = _Temp + Environment.NewLine;
            }

            _Temp = _Temp + Environment.NewLine + "\t#endregion " + Environment.NewLine;
            #endregion

            #region  Public Properties

            _Temp = _Temp + "\t\t#region Public Properties" + Environment.NewLine + Environment.NewLine;
            _Temp = _Temp + "\t\tpublic virtual string LocalConnectionString { get { return _LocalConnectionString; } set { _LocalConnectionString = value; } }" + Environment.NewLine + Environment.NewLine;

            string _MainPrimaryKey = "";
            if (_PrimaryKeys.Count() > 0)
            {
                _MainPrimaryKey = _PrimaryKeys[0];
            }

            _Temp = _Temp + "\t\tpublic virtual string PrimaryKey { get { return \"" + _MainPrimaryKey.ToCSharpFriendlyName() + "\"; } }" + Environment.NewLine;

            for (int x = 0; x < Table.ColumnCount; x++)
            {
                var c = Table.GetColumn(x);
                _Temp = _Temp + "\t\t\t" + Environment.NewLine;
                _Temp = _Temp + "\t\t\tpublic virtual " + c.DataType.ToCSharpStringNullable() + " " + c.Name.ToCSharpFriendlyName() + "" + Environment.NewLine;
                _Temp = _Temp + "\t\t\t{" + Environment.NewLine;
                _Temp = _Temp + "\t\t\t\tget { return __" + c.Name.ToCSharpFriendlyName() + "; }" + Environment.NewLine;
                _Temp = _Temp + "\t\t\t\tset { __" + c.Name.ToCSharpFriendlyName() + " = value; }" + Environment.NewLine;
                _Temp = _Temp + "\t\t\t}" + Environment.NewLine + Environment.NewLine;

                
                _Temp = _Temp + "\t\t\tpublic virtual string " + c.Name.ToCSharpFriendlyName() + "_Description" + Environment.NewLine;
                _Temp = _Temp + "\t\t\t{" + Environment.NewLine;
                _Temp = _Temp + "\t\t\t\tget { return __" + c.Name.ToCSharpFriendlyName() + "_Description; }" + Environment.NewLine;
                _Temp = _Temp + "\t\t\t\tset { __" + c.Name.ToCSharpFriendlyName() + "_Description = value; }" + Environment.NewLine;
                _Temp = _Temp + "\t\t\t}" + Environment.NewLine + Environment.NewLine;

            }
            _Temp = _Temp + Environment.NewLine + "\t\t\t#endregion " + Environment.NewLine;

            #endregion

            _Temp += GenerateGenericDBAccess(CodeSettings);

            #region ChildTables
            _Temp = _Temp + "\t\t#region Public Child Tables" + Environment.NewLine + Environment.NewLine;
            foreach (var r in Table.AllRelationships)
            {
                if (r.IsForeignKey == true)
                {
                    var _ParentTableName = Table.ParentDatabase.GetTable(r.External_TableName, true);

                    int RSameCount = Table.AllRelationships.Count(x => x.ShortExternal_TableName == r.ShortExternal_TableName && x.IsForeignKey == true);

                    if (RSameCount > 1)
                    {
                        _Temp = _Temp + Environment.NewLine + "\t\tpublic List<" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "> Get_All_" + r.External_ColumnName + "_" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "() { " + Environment.NewLine;
                    }
                    else
                    {
                        _Temp = _Temp + Environment.NewLine + "\t\tpublic List<" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "> Get_All_" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "() { " + Environment.NewLine;

                    }

                    _Temp = _Temp + Environment.NewLine + "\t\t\tList<" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "> _TmpReturn = new List<" + r.ShortExternal_TableName.ToCSharpFriendlyName() + ">();" + Environment.NewLine;
                    _Temp = _Temp + "\t\t\tusing(var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){" + Environment.NewLine;
                    _Temp = _Temp + "\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine;
                    _Temp = _Temp + "\t\t\tList<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();" + Environment.NewLine;
                    string _TmpWhere = "";
                    for (int x = 0; x < r.ColumnNames.Count(); x++)
                    {
                        var c = Table.GetColumn(r.ColumnNames[x], true);

                        _Temp = _Temp + "\t\t\t_Params.Add(new System.Data.SqlClient.SqlParameter(\"" + c.Name + "Param\", this.ReturnProperty(\"" + c.Name.ToCSharpFriendlyName() + "\")));" + Environment.NewLine;
                        _TmpWhere += r.ExternalColumnNames[x] + " = @" + c.Name + "Param AND ";

                    }

                    _TmpWhere = _TmpWhere.TrimEnd(" AND ");

                    _Temp = _Temp + "\t\t\tvar SQL = \"Select " + _ParentTableName.GeneratePrimaryKeySelectListMSSQL() + " From " + r.ShortExternal_TableName.ToCSharpFriendlyName() + " Where " + _TmpWhere + "\";" + Environment.NewLine;

                    _Temp = _Temp + "\t\t\tvar Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);" + Environment.NewLine + Environment.NewLine;


                    _Temp = _Temp + "\t\t\tfor (int rownumber = 0; rownumber < Result.Tables[0].Rows.Count; rownumber++ )" + Environment.NewLine;
                    _Temp = _Temp + "\t\t\t{" + Environment.NewLine;
                    _Temp = _Temp + "\t\t\t\t_TmpReturn.Add(new " + r.ShortExternal_TableName.ToCSharpFriendlyName() + "(";
                    foreach (var pks in _ParentTableName.GetPrimaryColumnNames)
                    {
                        _Temp = _Temp + "(" + _ParentTableName.GetColumn(pks, true).DataType.ToCSharpStringNullable() + ")Result.Tables[0].Rows[rownumber][\"" + pks + "\"],";
                    }
                    _Temp = _Temp.TrimEnd(",");
                    _Temp = _Temp + "));" + Environment.NewLine;
                    _Temp = _Temp + "\t\t\t}" + Environment.NewLine;
                    _Temp = _Temp + Environment.NewLine + "\t\treturn _TmpReturn;";
                    _Temp = _Temp + Environment.NewLine + "\t\t}}";
                }
            }
            _Temp = _Temp + Environment.NewLine + "\t\t\t#endregion " + Environment.NewLine + Environment.NewLine;
            #endregion

            #region ParentTables
            _Temp = _Temp + "\t#region Public Parent Tables" + Environment.NewLine + Environment.NewLine;
            foreach (var r in Table.AllRelationships)
            {
                //r.External_TableName.Substring(r.External_TableName.IndexOf("[dbo].[") + 7).TrimEnd("]"); 

                var _ParentTableName = Table.ParentDatabase.GetTable(r.External_TableName, true);

                if (r.IsForeignKey == false)
                {
                    int RSameCount = Table.AllRelationships.Count(x => x.ShortExternal_TableName == r.ShortExternal_TableName && x.IsForeignKey == false);

                    if (RSameCount > 1)
                    {
                        _Temp = _Temp + Environment.NewLine + "\tpublic " + r.ShortExternal_TableName.ToCSharpFriendlyName() + " Get_" + r.ColumnName + "_" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "() { " + Environment.NewLine;
                    }
                    else
                    {
                        _Temp = _Temp + Environment.NewLine + "\tpublic " + r.ShortExternal_TableName.ToCSharpFriendlyName() + " Get_" + r.ShortExternal_TableName.ToCSharpFriendlyName() + "() { " + Environment.NewLine;
                    }

                    _Temp = _Temp + Environment.NewLine + "\t\t" + r.ShortExternal_TableName.ToCSharpFriendlyName() + " _TmpReturn = null;" + Environment.NewLine;
                    _Temp = _Temp + "\t\tusing(var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){" + Environment.NewLine;


                    _Temp = _Temp + "\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine;


                    _Temp = _Temp + "\t\tList<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();" + Environment.NewLine;

                    string _TmpWhere = "";
                    for (int x = 0; x < r.ColumnNames.Count(); x++)
                    {
                        var c = Table.GetColumn(r.ColumnNames[x], true);

                        _Temp = _Temp + "\t\t_Params.Add(new System.Data.SqlClient.SqlParameter(\"" + c.Name + "Param\", this.ReturnProperty(\"" + c.Name.ToCSharpFriendlyName() + "\")));" + Environment.NewLine;
                        _TmpWhere += r.ExternalColumnNames[x] + " = @" + c.Name + "Param AND ";

                    }

                    _TmpWhere = _TmpWhere.TrimEnd(" AND ");

                    _Temp = _Temp + "\t\tvar SQL = \"Select top 1 " + _ParentTableName.GeneratePrimaryKeySelectListMSSQL() + " From " + r.ShortExternal_TableName.ToCSharpFriendlyName() + " Where " + _TmpWhere + "\";" + Environment.NewLine;

                    _Temp = _Temp + "\t\tvar Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);" + Environment.NewLine + Environment.NewLine;


                    _Temp = _Temp + "\t\tif (Result.Tables[0].Rows.Count == 1)" + Environment.NewLine;
                    _Temp = _Temp + "\t\t{" + Environment.NewLine;


                    _Temp = _Temp + "\t\t\t_TmpReturn = new " + r.ShortExternal_TableName.ToCSharpFriendlyName() + "(";
                    foreach (var pks in _ParentTableName.GetPrimaryColumnNames)
                    {
                        _Temp = _Temp + "(" + _ParentTableName.GetColumn(pks, true).DataType.ToCSharpStringNullable() + ")Result.Tables[0].Rows[0][\"" + pks + "\"],";
                    }
                    _Temp = _Temp.TrimEnd(",");
                    _Temp = _Temp + ");" + Environment.NewLine;


                    _Temp = _Temp + "\t\t}" + Environment.NewLine;
                    _Temp = _Temp + Environment.NewLine + "\treturn _TmpReturn;" + Environment.NewLine + "}}";
                    //_Temp = _Temp + Environment.NewLine + "\t}";
                }
            }
            _Temp = _Temp + Environment.NewLine + "\t#endregion " + Environment.NewLine + "\t";
            #endregion

            #region Constructors



            if (_PrimaryKeys.Count > 0)
            {
                _Temp += Environment.NewLine + "\t\tpublic " + Table.ShortName.ToCSharpFriendlyName() + "_CoreClass(string localConnectionString = \"\"){ LocalConnectionString = localConnectionString; }" + Environment.NewLine + Environment.NewLine;
            }

            _Temp += Environment.NewLine + "\t\tpublic " + Table.ShortName.ToCSharpFriendlyName() + "_CoreClass(";



            string _PrimaryKeyString = "";
            foreach (string _S in _PrimaryKeys)
            {
                _PrimaryKeyString = _PrimaryKeyString + Table.GetColumn(_S, true).DataType.ToCSharpStringNullable() + " " + Table.GetColumn(_S, true).ShortName.ToCSharpFriendlyName() + ", ";
            }
            // _PrimaryKeyString = _PrimaryKeyString.TrimEnd(", ".ToCharArray());
            _Temp += _PrimaryKeyString;

            _Temp += "string localConnectionString = \"\") " + Environment.NewLine + "\t\t{" + Environment.NewLine;

            if (_PrimaryKeys.Count() == 0)
            {
                _Temp = _Temp + "\t\tLocalConnectionString = localConnectionString;" + Environment.NewLine;
                _Temp = _Temp + "\t\treturn;" + Environment.NewLine + "}\n";
                goto SkipConstructor;
            }

            _Temp = _Temp + "\t\tusing(var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){" + Environment.NewLine;

            _Temp = _Temp + "\t\tif (localConnectionString == \"\") {" + Environment.NewLine + Environment.NewLine;

            _Temp = _Temp + "\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine;
            _Temp = _Temp + "\t\t}" + Environment.NewLine;
            _Temp = _Temp + "\t\telse { " + Environment.NewLine;
            _Temp = _Temp + "\t\tLocalConnectionString = localConnectionString;" + Environment.NewLine;
            _Temp = _Temp + "\t\t\tDataAccess.Open(localConnectionString);" + Environment.NewLine;
            _Temp = _Temp + "\t\t}" + Environment.NewLine + Environment.NewLine;


            _Temp = _Temp + "\t\tList<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();" + Environment.NewLine;

            foreach (string _S in _PrimaryKeys)
            {
                _Temp = _Temp + "\t\t_Params.Add(new System.Data.SqlClient.SqlParameter(\"" + _S + "Param\", " + _S + "));" + Environment.NewLine;
            }

            _Temp = _Temp + "\t\tvar SQL = \"Select * From " + Table.ShortName.ToCSharpFriendlyName() + " Where ";

            foreach (string _S in _PrimaryKeys)
            {
                _Temp = _Temp + "[" + _S + "] = @" + _S + "Param AND ";
            }
            _Temp = _Temp.TrimEnd("AND ".ToCharArray()) + "\";" + Environment.NewLine;

            _Temp = _Temp + Environment.NewLine + "\t\tvar Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);" + Environment.NewLine + Environment.NewLine;

            _Temp = _Temp + "\t\tif (Result.Tables[0].Rows.Count == 1) {" + Environment.NewLine;
            _Temp = _Temp + "\t\t\tfor(int x = 0; x < Result.Tables[0].Columns.Count; x++) {" + Environment.NewLine;
            _Temp = _Temp + "\t\t\tif (Result.Tables[0].Rows[0][x] != DBNull.Value)" + Environment.NewLine;
            _Temp = _Temp + "\t\t\t  {" + Environment.NewLine;
            _Temp = _Temp + "\t\t\tthis.SetProperty(Result.Tables[0].Columns[x].ColumnName.ToCSharpFriendlyName(), Result.Tables[0].Rows[0][x]);" + Environment.NewLine;
            _Temp = _Temp + "}" + Environment.NewLine + "\t\t\t}" + Environment.NewLine;
            _Temp = _Temp + "" + Environment.NewLine + "\t\t\t_LoadedCorrectly = true;" + Environment.NewLine + "\t\t\t}" + Environment.NewLine;
            _Temp = _Temp + "\t\telse { _LoadedCorrectly = false; }" + Environment.NewLine;

            _Temp += Environment.NewLine + "\t\t\t}" + Environment.NewLine + "\t\t}";

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
            _Temp = _Temp + Environment.NewLine + "" + Environment.NewLine + "\t#region Public Static Search Methods" + Environment.NewLine + Environment.NewLine;

            #region Generate Table Search With Where
            _Temp += "\t\tpublic static List<" + Table.ShortName.ToCSharpFriendlyName() + "> Get" + Table.ShortName.ToCSharpFriendlyName() + "(I_DbWhereStatement Where, string sql = \"\", string LocalConnectionString = \"\")" + Environment.NewLine;
            _Temp += "\t\t{" + Environment.NewLine;
            _Temp += "\t\tList<" + Table.ShortName.ToCSharpFriendlyName() + "> _TmpReturn = new List<" + Table.ShortName.ToCSharpFriendlyName() + ">();" + Environment.NewLine;

            _Temp += "\t\tusing(var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){" + Environment.NewLine;

            _Temp = _Temp + "\t\tif (LocalConnectionString == \"\") {" + Environment.NewLine + Environment.NewLine;
            _Temp = _Temp + "\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine;
            _Temp = _Temp + "\t\t}" + Environment.NewLine + Environment.NewLine;
            _Temp = _Temp + "\t\telse { " + Environment.NewLine + Environment.NewLine;
            _Temp = _Temp + "\t\t\tDataAccess.Open(LocalConnectionString);" + Environment.NewLine;
            _Temp = _Temp + "\t\t}" + Environment.NewLine + Environment.NewLine;


            _Temp += "\t\tstring _Where = DataAccess.GenerateWhereStatement(Where, sql);" + Environment.NewLine;
            _Temp += "\t\tvar _Params = DataAccess.GenerateWhereStatementParameters(Where);" + Environment.NewLine;

            _Temp += "\t\tvar QR = DataAccess.RunCommand(\"Select " + _BuilderTemp3 + " From " + Table.ShortName.ToCSharpFriendlyName() + " Where \" + _Where, _Params, true, System.Data.CommandType.Text);" + Environment.NewLine;

            _Temp += "\t\tif (QR.Tables[0].Rows.Count > 0)" + Environment.NewLine;
            _Temp += "\t\t{" + Environment.NewLine;
            _Temp += "\t\t    foreach (System.Data.DataRow r in QR.Tables[0].Rows)" + Environment.NewLine;
            _Temp += "\t\t    {" + Environment.NewLine;



            _Temp += "\t\t        _TmpReturn.Add(new " + Table.ShortName.ToCSharpFriendlyName() + "(" + _BuilderTemp4 + "));" + Environment.NewLine;
            _Temp += "\t\t    }" + Environment.NewLine;
            _Temp += "\t\t}" + Environment.NewLine;

            _Temp += "return _TmpReturn;" + Environment.NewLine + "\t\t}}" + Environment.NewLine + Environment.NewLine;

            _Temp += GenerateSearchMethod(Table, CodeSettings);
            _Temp += "" + Environment.NewLine;

            _Temp += GenerateSearchMethodPaging(Table, CodeSettings);
            _Temp += "" + Environment.NewLine;

            _Temp += GenerateSearchMethodManyParams(Table, CodeSettings);
            _Temp += "" + Environment.NewLine;

            _Temp += GenerateGenericSearchMethod(Table, CodeSettings);
            _Temp += "" + Environment.NewLine;

            _Temp = _Temp + Environment.NewLine + "\t#endregion" + Environment.NewLine + Environment.NewLine;

            #endregion

            #endregion

            _Temp += GenerateExportMethod(Table, CodeSettings);

            _Temp += GenerateCreateMethod(Table, CodeSettings);

            //_Temp += GenerateExcelImportProcess(Table, CodeSettings);
            //_Temp += GenerateExcelExportProcess(Table, CodeSettings);

            _Temp += GenerateGenericCalls(Table, CodeSettings);

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


            _Temp += Environment.NewLine + "\t}";
            _Temp += Environment.NewLine + "}";
            ACT_GeneratedCode _NewREturn = new ACT_GeneratedCode();
            _NewREturn.FileName = Table.ShortName + ".cs";
            _NewREturn.Code = _Temp;

            _Temp = "";
            _Temp += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
            _Temp += Environment.NewLine + "" + Environment.NewLine;
            _Temp += "namespace " + CodeSettings.NameSpace + Environment.NewLine + "{" + Environment.NewLine;



            _Temp += "\tpublic class " + Table.ShortName.ToCSharpFriendlyName() + " : " + Table.ShortName.ToCSharpFriendlyName() + "_CoreClass";
            _Temp += Environment.NewLine + "\t{" + Environment.NewLine;

            if (_PrimaryKeys.Count > 0)
            {
                _Temp += Environment.NewLine + "\t\tpublic " + Table.ShortName.ToCSharpFriendlyName() + "(string Conn = \"\"){ }" + Environment.NewLine + Environment.NewLine;
            }
            _Temp += "public " + Table.ShortName.ToCSharpFriendlyName() + "(" + _BuilderTemp + ", string Conn = \"\") : base (" + _BuilderTemp2 + ", Conn)" + Environment.NewLine + " { } " + Environment.NewLine;

            _Temp += Environment.NewLine + "}" + Environment.NewLine + "}";

            _NewREturn.UserCode = _Temp;

            // WEBSERVICES
            if (CodeSettings.GenerateWebServices)
            {
                string _TmpWebServicesCode = "";
                _TmpWebServicesCode = GenerateWebServices(Table, CodeSettings);
                _NewREturn.WebServiceCode = _TmpWebServicesCode;
            }

            return _NewREturn;
        }

        #endregion

        #region Generate MISC Methods

        /// <summary>
        /// Method to Test the connection String Setting
        /// </summary>
        /// <param name="throwErrors">if set to <c>true</c> [throw errors].</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="Exception">
        /// Error with the Database connection Name
        /// or
        /// Invalid Connection String
        /// </exception>
        /// <exception cref="ACT.Core.Exceptions.DatabaseConnectionError">Error Opening The Database: " + CodeSettings.DatabaseConnectionName</exception>
        private bool TestCodeSettingsConnection(bool throwErrors, I_CodeGenerationSettings CodeSettings)
        {
            #region Test the Database Configuration Log Errors Only
            using (var DataAccess = ACT.Core.CurrentCore<I_DataAccess>.GetCurrent())
            {
                ///Get the Encrypted Connection String
                string _ConnectionString = CodeSettings.DatabaseConnectionString;

                if (_ConnectionString == "")
                {
                    LogError("Error with the DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "" + Environment.NewLine, "Unable To Locate Setting Name" + Environment.NewLine, null, "" + Environment.NewLine, ErrorLevel.Critical);
                    if (throwErrors)
                    {
                        throw new Exception("Error with the Database connection Name");
                    }
                    else
                    {
                        return false;
                    }
                }

                if (ACT.Core.Validation.DatabaseValidations.ValidateConnectionString(_ConnectionString) == false)
                {
                    LogError("ACT_CodeGenerator", "Connection String is Invalid: Trying Again", new ACT.Core.Exceptions.InvalidFormatExceptions("Doesn't Match Connection String"), _ConnectionString, ErrorLevel.Informational);
                    if (throwErrors)
                    {
                        throw new Exception("Invalid Connection String");
                    }
                    else
                    {
                        return false;
                    }
                }

                if (DataAccess.Open(_ConnectionString) == false)
                {
                    LogError("Error Open DB With the ConnectionString Specified By DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "" + Environment.NewLine, "Unable To Open DB" + Environment.NewLine, null, "" + Environment.NewLine, ErrorLevel.Warning);
                    if (throwErrors)
                    {
                        throw new ACT.Core.Exceptions.DatabaseConnectionError("Error Opening The Database: " + CodeSettings.DatabaseConnectionName);
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            #endregion
        }

        /// <summary>
        /// Ensure The CodeSettings List Loaded
        /// </summary>
        /// <param name="CodeSettings">The code settings.</param>
        private void EnsureCodeSettings(I_CodeGenerationSettings CodeSettings)
        {
            if (CodeSettings.SettingsFileLocation.NullOrEmpty() == false)
            {
                if (ACT.Core.SystemSettings.LoadedSettingsDirectory.EnsureDirectoryFormat() != CodeSettings.SettingsFileLocation.GetDirectoryFromFileLocation().EnsureDirectoryFormat())
                {
                    ACT.Core.SystemSettings.LoadSystemSettings(CodeSettings.SettingsFileLocation);
                }
            }
        }

        /// <summary>
        /// Generates the VIEW Access Class
        /// </summary>
        /// <param name="Database">The database.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>I_GeneratedCode.</returns>
        public I_GeneratedCode GenerateViewAccess(I_Db Database, I_CodeGenerationSettings CodeSettings)
        {
            string _TemplateData = TemplateEngine.GetTextTemplate(_TemplatePath, "AccessViewTemplate.ngt");
            string _TemplateDataMethods = TemplateEngine.GetTextTemplate(_TemplatePath, "AccessViewTemplateMethods.ngt");
            string _FinalFile = _TemplateData;
            string _AllMethods = "";

            _FinalFile = _FinalFile.Replace("##NAMESPACE##", CodeSettings.NameSpace);
            foreach (string _viewName in Database.ViewNames)
            {
                string _Temp = _TemplateDataMethods;

                _Temp = _Temp.Replace("##VIEWNAME##", _viewName);
                _AllMethods += _Temp + Environment.NewLine;
            }
            _FinalFile = _FinalFile.Replace("##METHODS##", _AllMethods);

            ACT_GeneratedCode _TmpReturn = new ACT_GeneratedCode() { FileName = "DatabaseViews.cs", Code = _AllMethods };

            return _TmpReturn;
        }

        /// <summary>
        /// Generates a Static Class To Hold Public Variables For All Other Classes
        /// </summary>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>I_GeneratedCode.</returns>
        public I_GeneratedCode GenerateStaticClass(I_CodeGenerationSettings CodeSettings)
        {
            string _Temp = TemplateEngine.GetTextTemplate(_TemplatePath, "GenerateStaticClass.ngt");
            _Temp = _Temp.Replace("##NAMESPACE##", CodeSettings.NameSpace);
            _Temp = _Temp.Replace("##DATABASECONNECTIONNAME##", CodeSettings.DatabaseConnectionName);

            ACT_GeneratedCode _TmpReturn = new ACT_GeneratedCode() { FileName = "GenericStaticClass.cs", Code = _Temp };

            return _TmpReturn;
        }

        /// <summary>
        /// Generates the Default Data access
        /// </summary>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        public string GenerateGenericDBAccess(I_CodeGenerationSettings CodeSettings)
        {
            StringBuilder _TmpBuilder = new StringBuilder(Environment.NewLine + "" + Environment.NewLine);

            _TmpBuilder.Append("public static ACT.Core.Interfaces.DataAccess.I_DataAccess GetDataAccess()" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);
            _TmpBuilder.Append("\tusing( var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){ " + Environment.NewLine);
            _TmpBuilder.Append("\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("" + Environment.NewLine);
            _TmpBuilder.Append("\treturn DataAccess;}" + Environment.NewLine);

            _TmpBuilder.Append("}" + Environment.NewLine);

            return _TmpBuilder.ToString();
            "".ToCSharpFriendlyName();

        }

        /// <summary>
        /// TODO - Write Code To Recursivly Delete Data From Tables 
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        private string GenerateChildDeleteMethods(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            return "";
        }


        #endregion

        #region COMPILE CODE

        /// <summary>
        /// Compiles the Code Into a DLL
        /// </summary>
        /// <param name="Code">The code.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <exception cref="Exception">Error Compiling..</exception>
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

        #endregion

        #region I_Plugin / ACT_Core Methods 

        /// <summary>
        /// Health Check Runs the Requirement Check.  It also checks other items like other Interfaces (TODO)
        /// </summary>
        /// <returns>Test Result</returns>
        public override I_TestResult HealthCheck() { return ValidatePluginRequirements(); }

        /// <summary>
        /// Returns a list of System Settings Required In Order For This Plugin To Work
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> ReturnSystemSettingRequirements()
        {
            return new string[] { "C#AssemblyInfoClassTemplate", "C#SolutionTemplate", "ACTCodeGenerator_Namespace", "ACTCodeGenerator_UsingStatements", "C#ProjectTemplate", "ASPNETTemplate", "ASPNETTemplateRow", "ASPNETTemplateCodeFile", "AccessViewClassTemplate" }.ToList();
        }

        #endregion

        #region Security Methods

        /// <summary>
        /// The active user information
        /// </summary>
        private ACT.Core.Interfaces.Security.Authentication.I_UserInfo _ActiveUserInfo;

        /// <summary>
        /// Impersonate a User
        /// </summary>
        /// <param name="Info">I_UserInfo that represents the user to impersonate and execute under</param>
        public override void SetImpersonate(ACT.Core.Interfaces.Security.Authentication.I_UserInfo Info)
        {
            _ActiveUserInfo = Info;
        }

        #endregion
    }
}
