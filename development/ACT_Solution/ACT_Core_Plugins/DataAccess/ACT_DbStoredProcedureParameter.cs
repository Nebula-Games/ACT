using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.Interfaces;
using ACT.Core;
using ACT.Core.Enums;

namespace ACT.Plugins.DataAccess
{
    public class ACT_DbStoredProcedureParameter : ACT_Core, I_DbStoredProcedureParameter
    {
        public void SetImpersonate(object UserInfo)
        {
            throw new NotImplementedException();
        }
        public string Name
        {
            get; set;
        }

        public System.Data.DbType DataType
        {
            get; set;
        }

        public int Length
        {
            get; set;
        }

        public int Order
        {
            get; set;
        }

        public string DataTypeName
        {
            get;
            set;
        }

        public override I_TestResult ValidatePluginRequirements()
        {
            var _TR = ACT.Core.CurrentCore<I_TestResult>.GetCurrent();
            _TR.Success = true;
            return _TR;
        }
    }
}
