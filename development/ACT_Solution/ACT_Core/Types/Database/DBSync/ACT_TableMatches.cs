///-------------------------------------------------------------------------------------------------
// file:	Types\Database\DBSync\ACT_TableMatches.cs
//
// summary:	Implements the act table matches class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Types.Database.DBSync
{
    /// <summary>
    /// ACT Table Matches
    /// </summary>
    public class ACT_TableMatches
    {
        public Dictionary<Guid, Interfaces.DataAccess.I_DbColumn> _SourceDBColumns = new Dictionary<Guid, Interfaces.DataAccess.I_DbColumn>();
        public Dictionary<Guid, Interfaces.DataAccess.I_DbColumn> _DestinationDBColumns = new Dictionary<Guid, Interfaces.DataAccess.I_DbColumn>();

        public bool Warning { get; set; }
        public bool Error { get; set; }
        public bool ThrowErrorOnTruncate { get; set; }
        public string TypeConversionPlugin { get; set; }

    }
}
