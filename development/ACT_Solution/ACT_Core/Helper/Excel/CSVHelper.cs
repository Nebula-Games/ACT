///-------------------------------------------------------------------------------------------------
// file:	Helper\Excel\CSVHelper.cs
//
// summary:	Implements the CSV helper class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ACT.Core.Helper.Excel
{
    /// <summary>
    /// CSF File Helper
    /// </summary>
    public static class CSVHelper
    {
        /// <summary>
        /// Convert CSV to DataTable
        /// </summary>
        /// <param name="CSVData"></param>
        /// <param name="HasQuotedStrings"></param>
        /// <param name="HasHeaders"></param>
        /// <returns></returns>
        public static System.Data.DataTable ToDataTable(this string CSVData, bool HasQuotedStrings=true, bool HasHeaders = true)
        {
            int _LineNumber = 0;
            string[] _Headers = null;
            List<string[]> _Data = new List<string[]>();
            System.Data.DataTable _tmpReturn = new System.Data.DataTable();

            using (System.IO.StringReader _csvStream = new System.IO.StringReader(CSVData))
            {
                Microsoft.VisualBasic.FileIO.TextFieldParser _TP = new Microsoft.VisualBasic.FileIO.TextFieldParser(_csvStream);
                _TP.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                _TP.SetDelimiters(",");
                _TP.HasFieldsEnclosedInQuotes = HasQuotedStrings;
                while (_TP.EndOfData == false)
                {
                    if (HasHeaders) { if (_LineNumber == 0) { _Headers = _TP.ReadFields(); } continue; }
                    var _FieldData = _TP.ReadFields();
                    _Data.Add(_FieldData);
                    _LineNumber++;
                }
            }

            // Create DataTable Headers
            if (_Headers != null)
            {
                foreach (string header in _Headers)
                {
                    _tmpReturn.Columns.Add(header, typeof(string));
                }
            }

            // Insert Data Into DataTable
            foreach(string[] data in _Data) { _tmpReturn.Rows.Add(data); }


            return _tmpReturn;
        }

        /// <summary>
        /// Save the CSV FILE to EXCEL
        /// </summary>
        /// <param name="CSVFileName"></param>
        /// <param name="ExcelFileName"></param>
        /// <param name="HasQuotedStrings"></param>
        /// <param name="HasHeaders"></param>
        public static void SaveToExcelFile(string CSVFileName, string ExcelFileName, bool HasQuotedStrings = true, bool HasHeaders=true)
        {
         
            //string worksheetsName = "Data";
            //bool firstRowIsHeader = HasHeaders;
            
            //var format = new ExcelTextFormat();
            //format.Delimiter = ',';

            //if (HasQuotedStrings) { format.TextQualifier = '"'; }

            //using (ExcelPackage package = new ExcelPackage(new FileInfo(ExcelFileName)))
            //{
            //    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetsName);
            //    worksheet.Cells["A1"].LoadFromText(new FileInfo(CSVFileName), format, OfficeOpenXml.Table.TableStyles.Medium27, firstRowIsHeader);
            //    package.Save();
            //}
        }
    }
}
