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
using ACT.Core.Enums;
using ACT.Core.Extensions;
using ACT.Core.Interfaces.CodeGeneration;
using ACT.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Extensions.CodeGenerator;

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

        /// <summary>
        /// Generates the search method.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        private string GenerateSearchMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            if (Table.GetPrimaryColumnNames.Count() == 0) { return ""; }
            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public static List<" + Table.ShortName.ToCSharpFriendlyName() + "> Search(string FieldName, string SearchText, bool UseContains = false, string LocalConnectionString = \"\")" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);
            _TmpBuilder.Append("List<" + Table.ShortName.ToCSharpFriendlyName() + "> _TmpReturn = new List<" + Table.ShortName.ToCSharpFriendlyName() + ">();" + Environment.NewLine);

            _TmpBuilder.Append("using (var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\telse { " + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);

            _TmpBuilder.Append("List<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();" + Environment.NewLine);

            _TmpBuilder.Append("_Params.Add(new System.Data.SqlClient.SqlParameter(\"SearchParam\", SearchText));" + Environment.NewLine);


            _TmpBuilder.Append("var SQL = \"\";" + Environment.NewLine);

            _TmpBuilder.Append("if (UseContains == true) { " + Environment.NewLine);
            _TmpBuilder.Append("SQL = SQL + \"Select " + Table.GeneratePrimaryKeySelectListMSSQL() + " From " + Table.ShortName.ToCSharpFriendlyName() + " Where [\" + FieldName + \"] like '%' + @SearchParam + '%'\";" + Environment.NewLine);
            _TmpBuilder.Append("} " + Environment.NewLine);
            _TmpBuilder.Append("else { " + Environment.NewLine);
            _TmpBuilder.Append("SQL = SQL + \"Select " + Table.GeneratePrimaryKeySelectListMSSQL() + " From " + Table.ShortName.ToCSharpFriendlyName() + " Where [\" + FieldName + \"] = @SearchParam\";" + Environment.NewLine);
            _TmpBuilder.Append("}" + Environment.NewLine + Environment.NewLine);

            _TmpBuilder.Append("var Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);" + Environment.NewLine);

            _TmpBuilder.Append("if (Result.Tables[0].Rows.Count > 0)" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);
            _TmpBuilder.Append("for (int x = 0; x < Result.Tables[0].Rows.Count; x++)" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);
            _TmpBuilder.Append("if (Result.Tables[0].Rows[x][0] != DBNull.Value)" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);
            _TmpBuilder.Append("_TmpReturn.Add(new " + Table.ShortName.ToCSharpFriendlyName() + "(");


            foreach (string pkcn in Table.GetPrimaryColumnNames)
            {

                _TmpBuilder.Append("(" + Table.GetColumn(pkcn, true).DataType.ToCSharpString() + ")Result.Tables[0].Rows[x][\"" + pkcn + "\"],");
            }
            _TmpBuilder.Remove(_TmpBuilder.Length - 1, 1);
            _TmpBuilder.Append("));" + Environment.NewLine);
            _TmpBuilder.Append("}" + Environment.NewLine);
            _TmpBuilder.Append("}" + Environment.NewLine);
            _TmpBuilder.Append("}}" + Environment.NewLine);

            _TmpBuilder.Append("return _TmpReturn;" + Environment.NewLine);
            _TmpBuilder.Append("}" + Environment.NewLine);

            return _TmpBuilder.ToString();
        }

        /// <summary>
        /// Generates the search method many parameters.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        private string GenerateSearchMethodManyParams(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            if (Table.GetPrimaryColumnNames.Count() == 0) { return ""; }
            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public static List<" + Table.ShortName.ToCSharpFriendlyName() + "> Search(Dictionary<string,string> FieldValues, string LocalConnectionString = \"\")" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);
            _TmpBuilder.Append("List<" + Table.ShortName.ToCSharpFriendlyName() + "> _TmpReturn = new List<" + Table.ShortName.ToCSharpFriendlyName() + ">();" + Environment.NewLine);

            _TmpBuilder.Append("using (var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\telse { " + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);

            _TmpBuilder.Append("List<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();" + Environment.NewLine);

            _TmpBuilder.Append("foreach(var skey in FieldValues.Keys) { _Params.Add(new System.Data.SqlClient.SqlParameter(skey, FieldValues[skey]));" + Environment.NewLine + " } " + Environment.NewLine);

            _TmpBuilder.Append("var SQL = \"\";" + Environment.NewLine);
            _TmpBuilder.Append("SQL = SQL + \"Select " + Table.GeneratePrimaryKeySelectListMSSQL() + " From " + Table.ShortName.ToCSharpFriendlyName() + " where \";" + Environment.NewLine);

            _TmpBuilder.Append("foreach (var skey in FieldValues.Keys)" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);
            _TmpBuilder.Append("SQL = SQL + \"[\" + skey + \"] = @\" + skey + \" AND \";" + Environment.NewLine);
            _TmpBuilder.Append("}" + Environment.NewLine);

            _TmpBuilder.Append("SQL = SQL.Substring(0, SQL.LastIndexOf(\"AND\"));" + Environment.NewLine);

            _TmpBuilder.Append(Environment.NewLine + "" + Environment.NewLine);

            _TmpBuilder.Append("var Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);" + Environment.NewLine);

            _TmpBuilder.Append("if (Result.Tables[0].Rows.Count > 0)" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);
            _TmpBuilder.Append("for (int x = 0; x < Result.Tables[0].Rows.Count; x++)" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);
            _TmpBuilder.Append("if (Result.Tables[0].Rows[x][0] != DBNull.Value)" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);
            _TmpBuilder.Append("_TmpReturn.Add(new " + Table.ShortName.ToCSharpFriendlyName() + "(");

            foreach (string pkcn in Table.GetPrimaryColumnNames)
            {

                _TmpBuilder.Append("(" + Table.GetColumn(pkcn, true).DataType.ToCSharpString() + ")Result.Tables[0].Rows[x][\"" + pkcn + "\"],");
            }
            _TmpBuilder.Remove(_TmpBuilder.Length - 1, 1);
            _TmpBuilder.Append("));" + Environment.NewLine);
            _TmpBuilder.Append("}" + Environment.NewLine);
            _TmpBuilder.Append("}" + Environment.NewLine);
            _TmpBuilder.Append("}}" + Environment.NewLine);

            _TmpBuilder.Append("return _TmpReturn;" + Environment.NewLine);
            _TmpBuilder.Append("}" + Environment.NewLine);

            return _TmpBuilder.ToString();
        }

        /// <summary>
        /// Generates the generic search method.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        private string GenerateGenericSearchMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public static System.Data.DataTable Search(string FieldName, string SearchText, string OrderByStatement, bool UseContains = false, string LocalConnectionString = \"\")" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);

            _TmpBuilder.Append("using(var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){" + Environment.NewLine);
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\telse { " + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);

            _TmpBuilder.Append("List<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();" + Environment.NewLine);

            _TmpBuilder.Append("_Params.Add(new System.Data.SqlClient.SqlParameter(\"SearchParam\", SearchText));" + Environment.NewLine);


            _TmpBuilder.Append("var SQL = \"\";" + Environment.NewLine);

            _TmpBuilder.Append("if (UseContains == true) { " + Environment.NewLine);
            _TmpBuilder.Append("SQL = SQL + \"Select * From " + Table.ShortName.ToCSharpFriendlyName() + " Where [\" + FieldName + \"] like '%' + @SearchParam + '%'\";");
            _TmpBuilder.Append("} " + Environment.NewLine);
            _TmpBuilder.Append("else { " + Environment.NewLine);
            _TmpBuilder.Append("SQL = SQL + \"Select * From " + Table.ShortName.ToCSharpFriendlyName() + " Where [\" + FieldName + \"] = @SearchParam\";");
            _TmpBuilder.Append("}" + Environment.NewLine + Environment.NewLine);

            _TmpBuilder.Append("var Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);" + Environment.NewLine);
            _TmpBuilder.Append("return Result.Tables[0];" + Environment.NewLine);
            _TmpBuilder.Append("}" + Environment.NewLine + Environment.NewLine);


            _TmpBuilder.Append("}" + Environment.NewLine);

            return _TmpBuilder.ToString();
        }

        /// <summary>
        /// Generates the search method paging.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        private string GenerateSearchMethodPaging(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {

            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public static System.Data.DataTable Search(string FieldName, string SearchText, string OrderByStatement, int Start, int Increment, bool UseContains = false, string LocalConnectionString = \"\")" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);

            _TmpBuilder.Append("using(var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){" + Environment.NewLine);
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\telse { " + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);

            _TmpBuilder.Append("List<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();" + Environment.NewLine);

            _TmpBuilder.Append("_Params.Add(new System.Data.SqlClient.SqlParameter(\"SearchParam\", SearchText));" + Environment.NewLine);


            _TmpBuilder.Append("var SQL = \"\";" + Environment.NewLine);

            _TmpBuilder.Append("if (UseContains == true) { " + Environment.NewLine);
            _TmpBuilder.Append("SQL = SQL + \"Select * From ( Select *, ROW_NUMBER() Over(order by \" + OrderByStatement + \") as RowNum From " + Table.ShortName.ToCSharpFriendlyName() + " Where [\" + FieldName + \"] like '%' + @SearchParam + '%') as T Where T.RowNum > \" + (Start-1).ToString() + \" AND T.RowNum < \" + (Start + Increment).ToString();" + Environment.NewLine);
            _TmpBuilder.Append("} " + Environment.NewLine);
            _TmpBuilder.Append("else { " + Environment.NewLine);
            _TmpBuilder.Append("SQL = SQL + \"Select * From ( Select *, ROW_NUMBER() Over(order by \" + OrderByStatement + \") as RowNum From " + Table.ShortName.ToCSharpFriendlyName() + " Where [\" + FieldName + \"] = @SearchParam) as T Where T.RowNum > \" + (Start-1).ToString() + \" AND T.RowNum < \" + (Start + Increment).ToString();" + Environment.NewLine);
            _TmpBuilder.Append("}" + Environment.NewLine + Environment.NewLine);

            _TmpBuilder.Append("var Result = DataAccess.RunCommand(SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);" + Environment.NewLine);
            _TmpBuilder.Append("return Result.Tables[0];" + Environment.NewLine);
            _TmpBuilder.Append("}" + Environment.NewLine + Environment.NewLine);


            _TmpBuilder.Append("}" + Environment.NewLine);

            return _TmpBuilder.ToString();
        }

    }
}
