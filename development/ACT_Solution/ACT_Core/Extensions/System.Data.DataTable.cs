// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-18-2019
// ***********************************************************************
// <copyright file="System.Data.DataTable.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using ACT.Core.Helper.JSON;
using System.Security.Permissions;
using System.Dynamic;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Data Table Extensions
    /// </summary>
    public static partial class DataTableExtensions
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// A DataTable extension method that converts a dataTable to a data table.
        /// </summary>
        ///
        /// <remarks>   Mark Alicz, 7/27/2019. </remarks>
        ///
        /// <param name="dataTable">    The dataTable to act on. </param>
        ///
        /// <returns>   The given data converted to a data table. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static List<dynamic> ToDynamicList(this DataTable dataTable)
        {
            var result = new List<dynamic>();
            foreach (DataRow row in dataTable.Rows)
            {
                dynamic dyn = new ExpandoObject();
                foreach (DataColumn column in dataTable.Columns)
                {
                    var dic = (IDictionary<string, object>)dyn;
                    dic[column.ColumnName] = row[column];
                }
                result.Add(dyn);
            }
            return result;
        }

        /// <summary>
        /// Exports the Data Table To A CSV File
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string ToCSV(this System.Data.DataTable value)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = value.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in value.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            return sb.ToString();
        }

        /// <summary>
        /// To JSON
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string ToJSON(this System.Data.DataTable value)
        {
            Type type = value.GetType();

            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();

            json.NullValueHandling = NullValueHandling.Ignore;

            json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
            json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            if (type == typeof(DataRow))
                json.Converters.Add(new DataRowConverter());
            else if (type == typeof(DataTable))
                json.Converters.Add(new DataTableConverter());
            else if (type == typeof(DataSet))
                json.Converters.Add(new DataSetConverter());

            StringWriter sw = new StringWriter();
            Newtonsoft.Json.JsonTextWriter writer = new JsonTextWriter(sw);

            writer.Formatting = Formatting.Indented;

            writer.QuoteChar = '"';
            json.Serialize(writer, value);

            string output = sw.ToString();
            writer.Close();
            sw.Close();

            return output;
        }

#if DOTNETFRAMEWORK
        /// <summary>
        /// Add Records To ComboBox
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="cmboBox">The cmbo box.</param>
        /// <param name="IDField">The identifier field.</param>
        /// <param name="TextField">The text field.</param>
        public static void AddRecordsToComboBox(this System.Data.DataTable dt, ref System.Windows.Forms.ComboBox cmboBox, string IDField, string TextField)
        {
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                var n = dr[TextField].ToString();
                var v = dr[IDField].ToString();

                cmboBox.Items.Add(new ACT.Core.Types.SimpleNameValueClass() { Name = n, Value = v });
            }
        }
#endif

        /// <summary>
        /// DataTable FromDataRowArray
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="Data">The data.</param>
        /// <returns>DataTable.</returns>
        public static DataTable FromDataRowArray(this DataTable x, DataRow[] Data)
        {
            return Data.CopyToDataTable();
        }

        /// <summary>
        /// Datatable To Excel File (xlsx ONLY)
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="XLSXFileLocation">The XLSX file location.</param>
        /// <param name="CreateHeaderRows">if set to <c>true</c> [create header rows].</param>
        public static void ToExcelFile(this DataTable x, string XLSXFileLocation, bool CreateHeaderRows)
        {
            //string ACTExcelTemplate = ACT.Core.SystemSettings.GetSettingByName("ACTExcelTemplate").Value;

            //string _BaseDirectory = XLSXFileLocation.GetDirectoryFromFileLocation();

            //if (_BaseDirectory.DirectoryExists() == false) { _BaseDirectory.CreateDirectoryStructure(); }

            //FastExcel.Worksheet _PrimaryWorksheet = new FastExcel.Worksheet();
            //List<FastExcel.Row> _PrimaryRows = new List<FastExcel.Row>();

            //List<FastExcel.Cell> _Cells = new List<FastExcel.Cell>();
            //int xPos = 1;
            //int yPos = 1;

            //foreach (System.Data.DataColumn DC in x.Columns) { _Cells.Add(new FastExcel.Cell(xPos, DC.ColumnName)); xPos++; }
            //_PrimaryRows.Add(new FastExcel.Row(yPos, _Cells));
            //yPos++; xPos = 1;

            //foreach(System.Data.DataRow dr in x.Rows)
            //{
            //    _Cells.Clear();
            //    foreach (System.Data.DataColumn DC in x.Columns)
            //    {
            //        _Cells.Add(new FastExcel.Cell(xPos, dr[DC.ColumnName]));
            //        xPos++;
            //    }
            //    _PrimaryRows.Add(new FastExcel.Row(yPos, _Cells));
            //    yPos++;
            //}

            //_PrimaryWorksheet.Rows = _PrimaryRows;

            //// Create an instance of FastExcel
            //using (FastExcel.FastExcel fastExcel = new FastExcel.FastExcel(new System.IO.FileInfo(ACTExcelTemplate), new System.IO.FileInfo(XLSXFileLocation)))
            //{
            //    // Write the data to the XLSX Document
            //    fastExcel.Write(_PrimaryWorksheet, 1);
            //}
        }

        /// <summary>
        /// Generates a DataTable Object From the Table Name
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="ConnectionName">Name of the connection.</param>
        /// <returns>DataTable.</returns>
        public static DataTable TableNameToEmptyDataTableClass(this string TableName, string ConnectionName)
        {
            string _query = "Select top 1 * from [" + TableName + "]";

            var _DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent();
            _DataAccess.Open(ACT.Core.SystemSettings.GetSettingByName(ConnectionName).Value);

            var _TmpReturn = _DataAccess.RunCommand(_query, null, true, CommandType.Text);

            DataTable _tmpReturn = new DataTable(TableName);

            foreach(DataColumn col in _TmpReturn.Tables[0].Columns)
            {
                _tmpReturn.Columns.Add(col.ColumnName, col.DataType);
            }

            _DataAccess.Dispose();
            return _tmpReturn;
        }
    }
}
