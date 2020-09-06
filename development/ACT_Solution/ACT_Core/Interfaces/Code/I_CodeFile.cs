using ACT.Core.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Interfaces.Code
{
    public interface I_CodeFile
    {

        Enums.Code.Language Language { get; set; }

        List<DataAccess.I_DbTable> GenerateDatabaseTables(Enums.Database.DatabaseTypes DBType);

        string GenerateDatabaseTablesTSQL(Enums.Database.DatabaseTypes DBType);

        I_TestResult CreateDatabaseTables(string connectionName, Enums.Database.DatabaseTypes DBType);



    }
}
