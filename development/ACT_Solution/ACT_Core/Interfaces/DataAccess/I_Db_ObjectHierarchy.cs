///-------------------------------------------------------------------------------------------------
// file:	Interfaces\DataAccess\I_Db_ObjectHierarchy.cs
//
// summary:	Declares the I_Db_ObjectHierarchy interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.DataAccess
{
    /// <summary>
    /// I DB Object Hierarchy
    /// Represents the Database Hierarchy Of Objects that depended on one another
    /// </summary>
    public interface I_Db_ObjectHierarchy
    {

        List<string> OrphanTables { get; set; }
        Dictionary<string, List<string>> Layers { get; set; }

    }
}
