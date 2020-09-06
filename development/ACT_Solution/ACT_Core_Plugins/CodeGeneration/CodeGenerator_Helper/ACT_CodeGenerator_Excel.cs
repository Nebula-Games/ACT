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
        /// <summary>
        /// Generates the excel export process.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        public string GenerateExcelExportProcess(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            string _Temp = "";
            _Temp += Environment.NewLine + Environment.NewLine;
            _Temp += "/// <summary>EXPORT Class To Excel in Horizontal Format</summary>" + Environment.NewLine;
            _Temp += "///" + Environment.NewLine;
            _Temp += "/// <remarks>   Mark Alicz, 9/28/2016. </remarks>" + Environment.NewLine;
            _Temp += "///" + Environment.NewLine;
            _Temp += "/// <exception cref=\"Exception\">    Thrown when an exception error condition occurs. </exception>" + Environment.NewLine;
            _Temp += "///" + Environment.NewLine;
            _Temp += "/// <param name=\"ExcelFile\">        The excel file Location. </param>" + Environment.NewLine;
            _Temp += "/// <param name=\"IgnoreFields\">    (Optional) the ignore fields. </param>" + Environment.NewLine;
            _Temp += "///" + Environment.NewLine;
            _Temp += "/// <returns>   bool Success if True</returns>" + Environment.NewLine;
            _Temp += Environment.NewLine + Environment.NewLine;
            _Temp += "public ClosedXML.Excel.XLWorkbook ExportToExcel(List<string> IgnoreFields = null)" + Environment.NewLine;
            _Temp += "{" + Environment.NewLine;
            _Temp += "\tvar workbook = new ClosedXML.Excel.XLWorkbook();" + Environment.NewLine;
            _Temp += "\tvar worksheet = workbook.Worksheets.Add(\"" + Table.ShortName.ToCSharpFriendlyName() + "\");" + Environment.NewLine;
            _Temp += Environment.NewLine;
            _Temp += "\tIgnoreFields = IgnoreFields ?? new List<string>();" + Environment.NewLine;

            _Temp += "\tstring property = \"\";" + Environment.NewLine;
            _Temp += "\tint _ColPos = 1;" + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            _Temp += "\tbool _Add = false;" + Environment.NewLine + Environment.NewLine;

            _Temp += "\t#region Property Selection" + Environment.NewLine + Environment.NewLine;
            foreach (var col in Table.Columns)
            {
                string property = col.ShortName.ToCSharpFriendlyName();
                _Temp += "\t_Add = false;" + Environment.NewLine + Environment.NewLine;

                _Temp += "\tproperty = \"" + col.ShortName.ToCSharpFriendlyName() + "\";" + Environment.NewLine;

                _Temp += "\tif (IgnoreFields.Count == 0)" + Environment.NewLine;
                _Temp += "\t{" + Environment.NewLine;
                //_Temp += "\t\tif (IgnoreFields.Exists(x => x.ToLower() == property.ToLower()) == false) { _Add = true; }" + Environment.NewLine;
                _Temp += "\t\tif (property.ToLower() != \"dateadded\" && property.ToLower() != \"datemodified\" && property.ToLower() != \"id\") { _Add = true; } " + Environment.NewLine;
                _Temp += "\t}" + Environment.NewLine;
                _Temp += "\telse if (IgnoreFields.Exists(x => x.ToLower() == \"" + property.ToLower() + "\") == false)" + Environment.NewLine;
                _Temp += "\t{" + Environment.NewLine;
                _Temp += "\t\t_Add = true;" + Environment.NewLine;
                _Temp += "\t}" + Environment.NewLine;

                _Temp += "\tif (_Add)" + Environment.NewLine;
                _Temp += "\t{" + Environment.NewLine;
                _Temp += "\t\tworksheet.Cell(_ColPos.GetExcelColumnName() + \"1\").DataType = ClosedXML.Excel.XLCellValues.Text;" + Environment.NewLine;
                _Temp += "\t\tworksheet.Cell(_ColPos.GetExcelColumnName() + \"1\").Value = \"" + property + "\";" + Environment.NewLine;
                _Temp += "\t\ttry" + Environment.NewLine;
                _Temp += "\t\t{" + Environment.NewLine;
                _Temp += "\t\t\tif (this.ReturnProperty(\"" + property + "\") == null)" + Environment.NewLine;
                _Temp += "\t\t\t{" + Environment.NewLine;
                _Temp += "\t\t\t\tworksheet.Cell(_ColPos.GetExcelColumnName() + \"2\").DataType = ClosedXML.Excel.XLCellValues.Text;" + Environment.NewLine;
                _Temp += "\t\t\t\tworksheet.Cell(_ColPos.GetExcelColumnName() + \"2\").Value = \"\";" + Environment.NewLine;
                _Temp += "\t\t\t}" + Environment.NewLine;
                _Temp += "\t\t\telse" + Environment.NewLine;
                _Temp += "\t\t\t{" + Environment.NewLine;
                _Temp += "\t\t\t\tworksheet.Cell(_ColPos.GetExcelColumnName() + \"2\").DataType = ClosedXML.Excel.XLCellValues.Text;" + Environment.NewLine;
                _Temp += "\t\t\t\tworksheet.Cell(_ColPos.GetExcelColumnName() + \"2\").Value = this.ReturnProperty(\"" + property + "\").ToString();" + Environment.NewLine;
                _Temp += "\t\t\t}" + Environment.NewLine;
                _Temp += "\t\t}" + Environment.NewLine;
                _Temp += "\t\tcatch" + Environment.NewLine;
                _Temp += "\t\t{" + Environment.NewLine;
                _Temp += "\t\t\t\tworksheet.Cell(_ColPos.GetExcelColumnName() + \"2\").DataType = ClosedXML.Excel.XLCellValues.Text;" + Environment.NewLine;
                _Temp += "\t\t\tworksheet.Cell(_ColPos.GetExcelColumnName() + \"2\").Value = \"\";" + Environment.NewLine;
                _Temp += "\t\t}" + Environment.NewLine;
                _Temp += "\t\t_ColPos++;" + Environment.NewLine;
                _Temp += "\t}" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            }

            _Temp += "\t#endregion" + Environment.NewLine;
            //_Temp += "\tstring Destination = ExcelFile;" + Environment.NewLine;
            //_Temp += "\tif (Destination.EndsWith(\".xlsx\") == false)" + Environment.NewLine;
            //_Temp += "\t{" + Environment.NewLine;
            //_Temp += "\t\tthrow new Exception(\"Error Exporting Excel File Must BE FULL Path including xlsx. Please Check The Location\" + Destination);" + Environment.NewLine;
            //_Temp += "\t}" + Environment.NewLine;

            //_Temp += "\tif (System.IO.Directory.Exists(Destination.GetDirectoryFromFileLocation()) == false)" + Environment.NewLine;
            //_Temp += "\t{" + Environment.NewLine;
            //_Temp += "\t\ttry" + Environment.NewLine;
            //_Temp += "\t\t{" + Environment.NewLine;
            //_Temp += "\t\t\tDestination = AppDomain.CurrentDomain.BaseDirectory + \"\\\\EXPORT\\\\\";" + Environment.NewLine;
            //_Temp += "\t\t\tDestination.CreateDirectoryStructure();" + Environment.NewLine;
            //_Temp += "\t\t\tDestination += Guid.NewGuid().ToString().Left(5) + \" - \" + DateTime.Now.ToShortDateString().Replace(\" / \", \" - \") + \"" + Table.ShortName.ToCSharpFriendlyName() + ".xlsx\";" + Environment.NewLine;
            //_Temp += "\t\t}" + Environment.NewLine;
            //_Temp += "\t\tcatch" + Environment.NewLine;
            //_Temp += "\t\t{" + Environment.NewLine;
            //_Temp += "\t\t\tthrow new Exception(\"Error Exporting Excel File Please Check The Location\" + Destination);" + Environment.NewLine;
            //_Temp += "\t\t}" + Environment.NewLine;
            //_Temp += "\t\t}" + Environment.NewLine;

            //_Temp += "\tworkbook.SaveAs(Destination);" + Environment.NewLine + Environment.NewLine;
            _Temp += "return workbook;" + Environment.NewLine + Environment.NewLine;
            _Temp += "}" + Environment.NewLine + Environment.NewLine;

            return _Temp;
        }

        /// <summary>
        /// Generates the excel import process.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        public string GenerateExcelImportProcess(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            string _Temp = "";
            _Temp += Environment.NewLine + Environment.NewLine;
            _Temp += "/// <summary>Import Many Records ONLY Horizontal Format</summary>" + Environment.NewLine;
            _Temp += "///" + Environment.NewLine;
            _Temp += "/// <remarks>   Mark Alicz, 9/28/2016. </remarks>" + Environment.NewLine;
            _Temp += "///" + Environment.NewLine;
            _Temp += "/// <exception cref=\"Exception\">    Thrown when an exception error condition occurs. </exception>" + Environment.NewLine;
            _Temp += "///" + Environment.NewLine;
            _Temp += "/// <param name=\"ExcelFile\">        The excel file. </param>" + Environment.NewLine;
            _Temp += "/// <param name=\"IgnoreColumns\">    (Optional) the ignore columns. </param>" + Environment.NewLine;
            _Temp += "///" + Environment.NewLine;
            _Temp += "/// <returns>   List<" + Table.ShortName.ToCSharpFriendlyName() + "> </returns>" + Environment.NewLine;
            _Temp += Environment.NewLine + Environment.NewLine;
            _Temp += "public static List<" + Table.ShortName.ToCSharpFriendlyName() + "> ImportFromExcel(string ExcelFile, List<string> IgnoreColumns = null)" + Environment.NewLine;
            _Temp += "{" + Environment.NewLine;
            _Temp += "\tList<" + Table.ShortName.ToCSharpFriendlyName() + "> _TmpReturn = new List<" + Table.ShortName.ToCSharpFriendlyName() + ">();" + Environment.NewLine;
            _Temp += "\tvar _MemStream = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(ExcelFile));" + Environment.NewLine;
            _Temp += "\tExcel.IExcelDataReader _Reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(_MemStream);" + Environment.NewLine;
            _Temp += "\tvar _DataSets = _Reader.AsDataSet();" + Environment.NewLine;
            _Temp += "\t_Reader.IsFirstRowAsColumnNames = true;" + Environment.NewLine;
            _Temp += "\tSystem.Data.DataTable _MainTable = _DataSets.Tables[0];" + Environment.NewLine;
            _Temp += "\tIgnoreColumns = IgnoreColumns ?? new List<string>();" + Environment.NewLine;
            _Temp += "\t" + Table.ShortName.ToCSharpFriendlyName() + " _TmpNew = new " + Table.ShortName.ToCSharpFriendlyName() + "();" + Environment.NewLine;
            _Temp += "\tforeach (System.Data.DataColumn dc in _MainTable.Columns)" + Environment.NewLine;
            _Temp += "\t{" + Environment.NewLine;
            _Temp += "\t\t#region Ignore Certain Columns (Default Ignore dateadded, datemodified, id" + Environment.NewLine;
            _Temp += "\t\tif (IgnoreColumns.Count == 0)" + Environment.NewLine;
            _Temp += "\t\t{" + Environment.NewLine;
            _Temp += "\t\t\tif (dc.ColumnName.ToLower() == \"dateadded\" || dc.ColumnName.ToLower() == \"datemodified\") { continue; }" + Environment.NewLine;
            _Temp += "\t\t\tif (dc.ColumnName.ToLower() == \"id\") { continue; }" + Environment.NewLine;
            _Temp += "\t\t}" + Environment.NewLine;
            _Temp += "\t\telse" + Environment.NewLine;
            _Temp += "\t\t{" + Environment.NewLine;
            _Temp += "\t\t\tforeach (string ignoreCol in IgnoreColumns)" + Environment.NewLine;
            _Temp += "\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\t    if (dc.ColumnName.ToLower() == ignoreCol.ToLower()) { continue; }" + Environment.NewLine;
            _Temp += "\t\t\t}" + Environment.NewLine;
            _Temp += "\t\t}" + Environment.NewLine;
            _Temp += "\t\t#endregion" + Environment.NewLine;
            _Temp += "\t\tif (_TmpNew.PublicProperties.Exists(x => x.ToLower() == dc.ColumnName.ToLower() == false))" + Environment.NewLine;
            _Temp += "\t\t{" + Environment.NewLine;
            _Temp += "\t\t\tthrow new Exception(\"Missing Column: \" + dc.ColumnName);" + Environment.NewLine;
            _Temp += "\t\t}" + Environment.NewLine;
            _Temp += "\t\t}" + Environment.NewLine;
            _Temp += "\t\tforeach (System.Data.DataRow dr in _MainTable.Rows)" + Environment.NewLine;
            _Temp += "\t\t{" + Environment.NewLine;
            _Temp += "\t\t\tint _ColPos = 1;" + Environment.NewLine;
            _Temp += "\t\t\tint _RowPos = 0;" + Environment.NewLine;
            _Temp += "\t\t\tforeach (System.Data.DataColumn dc in _MainTable.Columns)" + Environment.NewLine;
            _Temp += "\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\tvar _TrySet = _TmpNew.SetProperty(dc.ColumnName, _MainTable.Rows[_RowPos][dc.ColumnName]);" + Environment.NewLine;
            _Temp += "\t\t\t\tif (_TrySet.Success == false)" + Environment.NewLine;
            _Temp += "\t\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\t\t    throw new Exception(\"Error Setting Property: \" + dc.ColumnName + \" At Row \" + _RowPos.ToString() + \" & Column Position: \" + _ColPos.ToString());" + Environment.NewLine;
            _Temp += "\t\t\t\t}" + Environment.NewLine;
            _Temp += "\t\t\t\t_ColPos++;" + Environment.NewLine;
            _Temp += "\t\t\t}" + Environment.NewLine;
            _Temp += "\t\t\t_RowPos++;" + Environment.NewLine;
            _Temp += "\t\t\t_TmpReturn.Add(_TmpNew);" + Environment.NewLine;
            _Temp += "\t\t\t_TmpNew = new " + Table.ShortName.ToCSharpFriendlyName() + "();" + Environment.NewLine;
            _Temp += "\t\t}" + Environment.NewLine;

            _Temp += "\t\treturn _TmpReturn;" + Environment.NewLine;
            _Temp += "}" + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            _Temp += "/// <summary>Only Import 1 Record </summary>" + Environment.NewLine;
            _Temp += "///" + Environment.NewLine;
            _Temp += "/// <remarks>   Mark Alicz, 9/28/2016. </remarks>" + Environment.NewLine;
            _Temp += "///" + Environment.NewLine;
            _Temp += "/// <exception cref=\"Exception\">    Thrown when an exception error condition occurs. </exception>" + Environment.NewLine;
            _Temp += "///" + Environment.NewLine;
            _Temp += "/// <param name=\"ExcelFile\">        The excel file. </param>" + Environment.NewLine;
            _Temp += "/// <param name=\"IgnoreColumns\">    (Optional) the ignore columns. </param>" + Environment.NewLine;
            _Temp += "/// <param name=\"Horizontal\">       (Optional) true to horizontal. </param>" + Environment.NewLine;
            _Temp += "///" + Environment.NewLine;
            _Temp += "/// <returns>   The " + Table.ShortName.ToCSharpFriendlyName() + ". </returns>" + Environment.NewLine;
            _Temp += "public static " + Table.ShortName.ToCSharpFriendlyName() + " ImportFromExcel(string ExcelFile, List<string> IgnoreColumns = null, bool Horizontal = true)" + Environment.NewLine;
            _Temp += "{" + Environment.NewLine;
            _Temp += "\t" + Table.ShortName.ToCSharpFriendlyName() + " _TmpReturn = new " + Table.ShortName.ToCSharpFriendlyName() + "();" + Environment.NewLine;

            _Temp += "\tvar _MemStream = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(ExcelFile)); " + Environment.NewLine;

            _Temp += "\tExcel.IExcelDataReader _Reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(_MemStream); " + Environment.NewLine;

            _Temp += "\tvar _DataSets = _Reader.AsDataSet(); " + Environment.NewLine;

            _Temp += "\tif (Horizontal) { _Reader.IsFirstRowAsColumnNames = true; }" + Environment.NewLine;
            _Temp += "\telse { _Reader.IsFirstRowAsColumnNames = false; } " + Environment.NewLine;

            _Temp += "\tSystem.Data.DataTable _MainTable = _DataSets.Tables[0]; " + Environment.NewLine;

            _Temp += "\tIgnoreColumns = IgnoreColumns ?? new List<string>(); " + Environment.NewLine;

            _Temp += "\tif (Horizontal)" + Environment.NewLine;
            _Temp += "\t{" + Environment.NewLine;
            _Temp += "\t\tint _ColPos = 1;" + Environment.NewLine;
            _Temp += "\t\tforeach (System.Data.DataColumn dc in _MainTable.Columns)" + Environment.NewLine;
            _Temp += "\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t#region Ignore Certain Columns (Default Ignore dateadded, datemodified, id" + Environment.NewLine;
            _Temp += "\t\t\tif (IgnoreColumns.Count == 0)" + Environment.NewLine;
            _Temp += "\t\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\t\tif (dc.ColumnName.ToLower() == \"dateadded\" || dc.ColumnName.ToLower() == \"datemodified\") { continue; }" + Environment.NewLine;
            _Temp += "\t\t\t\t\tif (dc.ColumnName.ToLower() == \"id\") { continue; }" + Environment.NewLine;
            _Temp += "\t\t\t\t}" + Environment.NewLine;
            _Temp += "\t\t\t\telse" + Environment.NewLine;
            _Temp += "\t\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\t\tforeach (string ignoreCol in IgnoreColumns)" + Environment.NewLine;
            _Temp += "\t\t\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\t\t\tif (dc.ColumnName.ToLower() == ignoreCol.ToLower()) { continue; }" + Environment.NewLine;
            _Temp += "\t\t\t\t\t}" + Environment.NewLine;
            _Temp += "\t\t\t\t}" + Environment.NewLine;
            _Temp += "\t\t\t#endregion" + Environment.NewLine;

            _Temp += "\t\t\tif (_TmpReturn.PublicProperties.Exists(x => x.ToLower() == dc.ColumnName.ToLower() == false))" + Environment.NewLine;
            _Temp += "\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\tthrow new Exception(\"Missing Column: \" + dc.ColumnName);" + Environment.NewLine;
            _Temp += "\t\t\t\t}" + Environment.NewLine;

            _Temp += "\t\t\tvar _TrySet = _TmpReturn.SetProperty(dc.ColumnName, _MainTable.Rows[0][dc.ColumnName]);" + Environment.NewLine;
            _Temp += "\t\t\tif (_TrySet.Success == false)" + Environment.NewLine;
            _Temp += "\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\tthrow new Exception(\"Error Setting Property: \" + dc.ColumnName + \" At Column Position: \" + _ColPos.ToString());" + Environment.NewLine;
            _Temp += "\t\t\t}" + Environment.NewLine;
            _Temp += "\t\t\t_ColPos++;" + Environment.NewLine;
            _Temp += "\t\t}" + Environment.NewLine;
            _Temp += "\t}" + Environment.NewLine;
            _Temp += "\telse" + Environment.NewLine;
            _Temp += "\t{" + Environment.NewLine;
            _Temp += "\t\tint _RowPos = 1;" + Environment.NewLine;
            _Temp += "\t\tforeach (System.Data.DataRow dr in _MainTable.Rows)" + Environment.NewLine;
            _Temp += "\t\t{" + Environment.NewLine;
            _Temp += "\t\t\tstring _ColName = dr[0].ToString();" + Environment.NewLine;
            _Temp += "\t\t\t/// Ignore Blank Column Names (Typically The First Row" + Environment.NewLine;
            _Temp += "\t\t\tif (_ColName.NullOrEmpty()) { continue; }" + Environment.NewLine;

            _Temp += "\t\t\t#region Ignore Certain Columns (Default Ignore dateadded, datemodified, id" + Environment.NewLine;
            _Temp += "\t\t\tif (IgnoreColumns.Count == 0)" + Environment.NewLine;
            _Temp += "\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\tif (_ColName.ToLower() == \"dateadded\" || _ColName.ToLower() == \"datemodified\") { continue; }" + Environment.NewLine;
            _Temp += "\t\t\t\tif (_ColName.ToLower() == \"id\") { continue; }" + Environment.NewLine;
            _Temp += "\t\t\t}" + Environment.NewLine;
            _Temp += "\t\t\telse" + Environment.NewLine;
            _Temp += "\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\tforeach (string ignoreCol in IgnoreColumns)" + Environment.NewLine;
            _Temp += "\t\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\t\tif (_ColName.ToLower() == ignoreCol.ToLower()) { continue; }" + Environment.NewLine;
            _Temp += "\t\t\t\t}" + Environment.NewLine;
            _Temp += "\t\t\t}" + Environment.NewLine;
            _Temp += "\t\t\t#endregion" + Environment.NewLine;

            _Temp += "\t\t\tif (_TmpReturn.PublicProperties.Exists(x => x.ToLower() == _ColName.ToLower() == false))" + Environment.NewLine;
            _Temp += "\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\tthrow new Exception(\"Missing Column: \" + _ColName);" + Environment.NewLine;
            _Temp += "\t\t\t}" + Environment.NewLine;

            _Temp += "\t\t\tvar _TrySet = _TmpReturn.SetProperty(_ColName, dr[1]);" + Environment.NewLine;
            _Temp += "\t\t\tif (_TrySet.Success == false)" + Environment.NewLine;
            _Temp += "\t\t\t{" + Environment.NewLine;
            _Temp += "\t\t\t\tthrow new Exception(\"Error Setting Property: \" + _ColName + \" At Row: \" + _RowPos.ToString());" + Environment.NewLine;
            _Temp += "\t\t\t}" + Environment.NewLine;
            _Temp += "\t\t\t_RowPos++;" + Environment.NewLine;
            _Temp += "\t\t}" + Environment.NewLine;
            _Temp += "\t}" + Environment.NewLine;

            _Temp += "\treturn _TmpReturn;" + Environment.NewLine;
            _Temp += "}" + Environment.NewLine;

            return _Temp;
        }

    }
}
