using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Plugins.DataAccess
{
    public class ACT_DbDataType : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.DataAccess.I_DbDataType
    {

        public int SystemTypeID { get; set; }
        public int UserTypeID { get; set; }
        public bool IsUserType { get; set; }
        public bool IsTableType { get; set; }
        public bool IsNullable { get; set; }
        public int MaxLength { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }

        public string Name { get; set; }
    }
}
