using ACT.Core.Enums.Code;
using ACT.Core.Enums.Database;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Plugins.Code
{
    public class CSharp_Code : ACT.Core.Interfaces.Code.I_CodeFile
    {
        public Language Language { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public I_TestResult CreateDatabaseTables(string connectionName, DatabaseTypes DBType)
        {
            throw new NotImplementedException();
        }

        public List<I_DbTable> GenerateDatabaseTables(DatabaseTypes DBType)
        {
            throw new NotImplementedException();
        }

        public string GenerateDatabaseTablesTSQL(DatabaseTypes DBType)
        {
            throw new NotImplementedException();
        }
    }
}
