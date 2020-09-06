using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Interfaces.Data
{
    public interface I_DB_Store
    {
        Common.I_TestResult SaveToDB();
        Common.I_TestResult LoadFromDB(ACT.Core.Interfaces.DataAccess.I_DbWhereStatement whereStatement);
        Common.I_TestResult LoadFromDB();

    }
}
