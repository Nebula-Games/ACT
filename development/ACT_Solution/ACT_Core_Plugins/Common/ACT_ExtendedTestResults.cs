using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Plugins.Common
{
    /// <summary>
    /// ACT Extended Test Results
    /// </summary>
    public class ACT_ExtendedTestResults : ACT.Core.Interfaces.Common.I_TestResultExpanded
    {
        private List<string> _Messages = new List<string>();
        private List<string> _Warnings = new List<string>();
        private List<Exception> _Exceptions = new List<Exception>();

        public List<Exception> Exceptions { get { return _Exceptions; } set { _Exceptions = value; } }
        public List<string> Messages { get { return _Messages; } set { _Messages = value; } }
        public bool Success { get; set; }
        public List<string> Warnings { get { return _Warnings; } set { _Warnings = value; } }
        public bool HasWarnings { get; set; }
        public bool ExitProcess { get; set; }
        public object[] ReturnData { get; set; }
    }
}
