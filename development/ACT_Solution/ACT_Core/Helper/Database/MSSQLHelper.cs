///-------------------------------------------------------------------------------------------------
// file:	Helper\Database\MSSQLHelper.cs
//
// summary:	Implements the mssql helper class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Helper.Database
{
    public class DBObject
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public string ObjectType { get; set; }

        public List<DBObject> DependsOn { get; set; }

        public DBObject() { DependsOn = new List<DBObject>(); }
               
    }
    public static class MSSQLHelper
    {

        public static List<DBObject> LoadDatabaseObjects(string ConnectionName)
        {
            List<DBObject> _tmpReturn = new List<DBObject>();
            // Call Methods to get all Objects etc
            return _tmpReturn;
        }

        //public static void ImportData(string DatabaseConnectionName, string FileName, )
    }
}
