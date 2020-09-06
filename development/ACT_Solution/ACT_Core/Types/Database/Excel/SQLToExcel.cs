///-------------------------------------------------------------------------------------------------
// file:	Types\Database\Excel\SQLToExcel.cs
//
// summary:	Implements the SQL to excel class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Types.Database.Excel
{
    /// <summary>
    /// Converts a SQL Datatable To Excel Using The Defined Mappings
    /// </summary>
    public class SQLToExcel
    {
        public System.Data.DataTable SQLData = null;

        public Dictionary<string, (string, int, int)> HeaderMappingMapping = new Dictionary<string, (string, int, int)>();

        public Dictionary<string, (string, int, int)> FieldMapping = new Dictionary<string, (string, int, int)>();

        public Dictionary<string, (bool, bool)> IncrementPosition = new Dictionary<string, (bool, bool)>();

        //public bool LoadMappings(string JSONData)
        //{

        //}

    }
}
