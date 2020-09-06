// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="ExcelHelper.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using ACT.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using OfficeOpenXml;


namespace ACT.Core.Helper.Excel
{
    /// <summary>
    /// ExcelHelper Class
    /// </summary>
    [CustomAttributes.ClassID("ExcelHelper.cs - ClosedXML")]
    public static class ExcelHelper
    {

        
        /// <summary>
        /// Reads to data table.
        /// </summary>
        /// <param name="ExcelFile">The excel file.</param>
        /// <param name="WorkSheetName">Name of the work sheet.</param>
        /// <param name="WorkSheetNumber">The work sheet number.</param>
        /// <returns>System.Data.DataTable.</returns>
        public static System.Data.DataTable ReadToDataTable(string ExcelFile, string WorkSheetName = "", int WorkSheetNumber = 1)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(ExcelFile))
                {
                    pck.Load(stream);
                }

                ExcelWorksheet ws = default(ExcelWorksheet);

                if (WorkSheetName == "") { ws = pck.Workbook.Worksheets[WorkSheetNumber]; }
                else { ws = pck.Workbook.Worksheets[WorkSheetName]; }

                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(true ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = true ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }          
        }

        /// <summary>
        /// Reads to data table fast.
        /// </summary>
        /// <param name="ExcelFile">The excel file.</param>
        /// <param name="WorkSheetName">Name of the work sheet.</param>
        /// <param name="FirstRowWithDataHasIsHeaders">if set to <c>true</c> [first row with data has is headers].</param>
        /// <returns>System.Data.DataTable.</returns>
        public static System.Data.DataTable ReadToDataTableFast(string ExcelFile, string WorkSheetName, bool FirstRowWithDataHasIsHeaders)
        {
            return ReadToDataTable(ExcelFile, WorkSheetName, 1);
        }

        /// <summary>
        /// Reads to data table fast.
        /// </summary>
        /// <param name="ExcelFile">The excel file.</param>
        /// <param name="WorkSheetName">Name of the work sheet.</param>
        /// <returns>System.Data.DataTable.</returns>
        public static System.Data.DataTable ReadToDataTableFast(System.IO.MemoryStream ExcelFile, string WorkSheetName)
        {
            string _tmpFileName = RandomHelper.Random_Helper.GetRandomFileNameString(10, "xlsx");
            string _tmpDir = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat(true) + "tmpExcel\\";
            if (_tmpDir.DirectoryExists(true) == false)
            {
                Helper.ErrorLogger.VLogError("ACT.Core.Helper.ExcelHelper.ReadToDataTableFast", "Error Saving MemStream", null, Enums.ErrorLevel.Severe);
            }


            System.IO.File.WriteAllBytes(_tmpDir + _tmpFileName, ExcelFile.ToArray());

            return ReadToDataTable(_tmpDir + _tmpFileName, WorkSheetName);
        }

        /// <summary>
        /// Read And Create A Database Table From An Excel Document.  IF Table Exists Append The Data To The Table
        /// </summary>
        /// <param name="FileName">XLSX File</param>
        /// <param name="SheetName">Blank (First Sheet) or Name Of Sheet</param>
        /// <param name="TableName">Table Name</param>
        /// <param name="ConnectionStringName">DatabaseConnectionStringName</param>
        /// <returns></returns>
        public static DataTable ReadAndCreateDatabaseTable(string FileName, string SheetName = "", string TableName = "", string ConnectionStringName = "")
        {
            string _ConnectionName = "";
            string _TableName = "";

            if (ConnectionStringName.NullOrEmpty()) { _ConnectionName = "DefaultConnectionString"; }
            else { _ConnectionName = ConnectionStringName; }
            
            if (ACT.Core.SystemSettings.GetSettingByName(_ConnectionName).Value == "")
            {
                ACT.Core.Helper.ErrorLogger.VLogError("ACT.Core.Helper.Excel.ReadAndCreateDatabaseTable()", "Error Connection Setting Not Found: " + _ConnectionName, null, Enums.ErrorLevel.Critical);
                return null;
            }

            var _DT = ReadToDataTableOpenXML(FileName, SheetName);

            if (TableName.NullOrEmpty())
            {
                if (SheetName.NullOrEmpty())
                {
                    _TableName = FileName.Replace(".", "").EnsureValidWindowsFileName("");
                }
                else
                {
                    _TableName = SheetName;
                }
            }
            else
            {
                _TableName = TableName;
            }
            // TODO MARK

            string _SQL = "CREATE TABLE[dbo].[" + _TableName + "](" + Environment.NewLine;
            _SQL = _SQL + "[ID][uniqueidentifier] NOT NULL," + Environment.NewLine;

            foreach (System.Data.DataColumn Column in _DT.Columns)
            {
                string _Name = Column.ColumnName;

                int _MaxLength = 0;
                foreach (System.Data.DataRow RowData in _DT.Rows)
                {
                    if (RowData[Column.ColumnName].ToString().NullOrEmpty() == false)
                    {
                        if (RowData[Column.ColumnName].ToString().Length > _MaxLength)
                        {
                            _MaxLength = RowData[Column.ColumnName].ToString().Length;
                        }
                    }
                }

                if (_MaxLength > 500) { _SQL = _SQL + "[" + _Name + "] [nvarchar] (MAX) NOT NULL," + Environment.NewLine; }
                else
                {
                    _MaxLength = _MaxLength * 2;
                    _SQL = _SQL + "[" + _Name + "] [nvarchar] (" + _MaxLength.ToString() + ") NOT NULL," + Environment.NewLine;
                }
            }

            _SQL = _SQL + "[DateAdded] [datetime] NOT NULL," + Environment.NewLine;
            _SQL = _SQL + "[DateModified] [datetime] NOT NULL," + Environment.NewLine;
            _SQL = _SQL + "CONSTRAINT[PK_" + _TableName + "] PRIMARY KEY CLUSTERED ( [ID] ASC )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]";

            // RUN SQL
            var _DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent();
            _DataAccess.Open(ACT.Core.SystemSettings.GetSettingByName(_ConnectionName).Value);            
            _DataAccess.RunCommand(_SQL, new List<IDataParameter>(), false, CommandType.StoredProcedure);

            // RUN SQL
            _SQL = "ALTER TABLE[dbo].[" + _TableName + "] ADD CONSTRAINT[DF_" + _TableName + "_ID]  DEFAULT(newid()) FOR[ID]";
            _DataAccess.RunCommand(_SQL, new List<IDataParameter>(), false, CommandType.StoredProcedure);

            // RUN SQL
            _SQL = "ALTER TABLE[dbo].[" + _TableName + "] ADD CONSTRAINT[DF_" + _TableName + "_DateAdded]  DEFAULT(getdate()) FOR[DateAdded]";
            _DataAccess.RunCommand(_SQL, new List<IDataParameter>(), false, CommandType.StoredProcedure);

            // RUN SQL
            _SQL = "ALTER TABLE[dbo].[" + _TableName + "] ADD CONSTRAINT[DF_" + _TableName + "_DateModified]  DEFAULT(getdate()) FOR[DateModified]";
            _DataAccess.RunCommand(_SQL, new List<IDataParameter>(), false, CommandType.StoredProcedure);

            return _DT;
        }

        /// <summary>
        /// Read To DataTable OpenXML
        /// </summary>
        /// <returns>DataTable or NULL on Error</returns>
        public static DataTable ReadToDataTableOpenXML(string FileName, string SheetName = "")
        {
            try
            {
                return ReadToDataTable(FileName, SheetName, 1);
            }
            catch (Exception ex)
            {
                Helper.ErrorLogger.VLogError("ACT.Core.Helper.Excel.ExcelHelper.ReadToDataTableOpenXML", "Error Reading XLSX File", ex, Enums.ErrorLevel.Severe);
                return null;
            }
        }

        /// <summary>
        /// Gets a Cell Value
        /// </summary>
        /// <param name="document"></param>
        /// <param name="cell"></param>
        /// <returns>string containing the Cell Value</returns>
        internal static string GetCellValue(object document, object cell)//SpreadsheetDocument document, Cell cell)
        {
            //TODO REMOVED ORIGINAL CODE;
            return null;
        }

     //   static string ExecuteSQLIntoExcel()
    }
}
