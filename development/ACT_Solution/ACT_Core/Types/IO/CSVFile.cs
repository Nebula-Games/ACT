///-------------------------------------------------------------------------------------------------
// file:	Types\IO\CSVFile.cs
//
// summary:	Implements the CSV file class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Extensions;

namespace ACT.Core.Types.IO
{
    /// <summary>
    /// CSV File
    /// </summary>
    public class CSVFile
    {
        /// <summary>
        /// CSV File
        /// </summary>
        public CSVFile(string FileLocation, bool HasColumnNames = false)
        {
            Location = FileLocation;
            Name = FileLocation.GetFileNameWithoutExtension();
            Data = FileLocation.ReadAllText();

            string _tmpData = Data;

            if (HasColumnNames)
            {
                string _FirstRow = Data.ReadTill(Environment.NewLine);

                List<string> Columns = new List<string>();

                bool inString = false;
                string tmpColName = "";

                foreach (char ch in _FirstRow)
                {
                    if (inString)
                    {
                        if (ch == '"') { inString = false; }
                    }
                    else
                    {
                        if (tmpColName.NullOrEmpty())
                        {
                            if (ch == '"') { inString = true; }
                            else { tmpColName.Append(ch); }
                        }
                        else if (ch == ',') { Columns.Add(tmpColName); tmpColName = ""; }
                        else { tmpColName.Append(ch); }
                    }
                }

                if (tmpColName.NullOrEmpty() == false)
                {
                    Columns.Add(tmpColName);
                }

                Data = Data.Replace(_FirstRow, "");
            }

            var _RowData = Data.SplitString(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string _FirstRow in _RowData)
            {
                bool inString = false;
                string tmpData = "";

                foreach (char ch in _FirstRow)
                {
                    if (inString) { if (ch == '"') { inString = false; } }
                    else
                    {
                        if (tmpData.NullOrEmpty()) { if (ch == '"') { inString = true; } else { tmpData.Append(ch); } }
                        else if (ch == ',')
                        {
                            // TODO
                            //  RowData.Add(tmpData);
                            tmpData = "";
                        }
                        else { tmpData.Append(ch); }
                    }
                }

                if (tmpData.NullOrEmpty() == false)
                {
                    // TODO
                    //  RowData.Add(tmpData);
                }
            }
        }

        /// <summary>
        /// File Location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File Data
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Column Names For The Data
        /// </summary>
        public List<(string, System.Data.DbType)> ColumnNames { get; set; }

        /// <summary>
        /// Column Names For The Data
        /// </summary>
        public Dictionary<int, List<string>> RowData { get; set; }

        /// <summary>
        /// Import Into The Database
        /// </summary>
        /// <param name="ConnectionName">Connection Name In SystemConfig</param>
        /// <param name="TableName">Table Name</param>
        /// <param name="ConvertToDbNullOnBlank">Convert Data To Null If Error Converting To Type</param>
        /// <param name="PKColumnName">Primary Key Name</param>
        /// <param name="GenerateGuidPK">Create New Guid</param>
        public (string, int) ImportIntoDatabase(string ConnectionName, string TableName, bool ConvertToDbNullOnBlank, string PKColumnName, bool GenerateGuidPK)
        {
            var _Conn = ACT.Core.SystemSettings.GetSettingByName(ConnectionName);
            if (_Conn.Valid == false) { return ("Invalid Connection - Missing", -1); }

            string _DatabaseName = _Conn.TryGrabDatabaseName;
            if (_DatabaseName.NullOrEmpty()) { return ("Unable To Decifer Database Name", -2); }

            using (var _DataAccess = ACT.Core.CurrentCore<Interfaces.DataAccess.I_DataAccess>.GetCurrent())
            {
                _DataAccess.Open(_Conn.Value);

                if (_DatabaseName.NullOrEmpty()) { return ("Unable To Decifer Database Name", -2); }
                if (_DataAccess.Connected == false) { return ("Unable To Connect To The Database", -3); }

                bool _TableExists = false;

                var _TableTest = _DataAccess.RunCommand("Select Top 1 * From " + TableName, null, true, System.Data.CommandType.Text);

                if (_TableTest.Exceptions[0] == null) { _TableExists = true; }
                if (_TableExists == false) { throw new Exception("Table Not Found: Please Implement Me: Line 72ish CSVFile.cs"); }

                bool _AllFound = true;
                foreach (string key in ColumnNames.Select(x => x.Item1))
                {
                    bool _foundcol = false;
                    foreach (System.Data.DataColumn col in _TableTest.FirstDataTable_WithRows().Columns) { if (col.ColumnName.ToLower() == key.ToLower()) { _foundcol = true; break; } }
                    if (_foundcol == false) { _AllFound = false; break; }
                }

                if (_AllFound == false) { throw new Exception("Table doesnt match required fields"); }

                List<string> InsertData = new List<string>();

                string _InsertRecordTemplate = "Insert Into [" + TableName + "] (###FIELDS###) VALUES (###VALUES###)";

                string _Keys = "";
                string _Values = "";

                foreach (string key in ColumnNames.Select(x => x.Item1)) { _Keys += "[" + key + "],"; }
                _Keys = _Keys.Substring(0, _Keys.Length - 1);

                foreach (int row in RowData.Keys)
                {
                    foreach (string data in RowData[row]) { _Values = _Values + "'" + data.Replace("'", "''") + "',"; }
                    _Values = _Values.Substring(0, _Values.Length - 1);

                    InsertData.Add(_InsertRecordTemplate.Replace("###FIELDS###", _Keys).Replace("###VALUES###", _Values));

                    _Values = "";
                }

                // TODO RUN INSERT STATEMENTS


            }

            return ("", 0);
        }
    }
}
