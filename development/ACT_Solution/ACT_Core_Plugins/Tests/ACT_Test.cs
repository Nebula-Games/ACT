using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Common.Code;
using ACT.Core.Extensions;

namespace ACT.Plugins.Tests
{
    public class ACT_Test : ACT.Core.Interfaces.Common.I_Test
    {

        Dictionary<string, List<string>> _Messages = new Dictionary<string, List<string>>();
        public virtual Dictionary<string, List<string>> Messages { get { return _Messages; } }

        Dictionary<string, CodeSignature> _MethodDefinitions = new Dictionary<string, CodeSignature>();
        public virtual Dictionary<string, CodeSignature> MethodDefinitions { get { return _MethodDefinitions; } }

        List<string> _MethodNames = new List<string>();
        public virtual List<string> MethodNames { get { return _MethodNames; } }

        Dictionary<string, bool> _MethodTestResults = new Dictionary<string, bool>();
        public virtual Dictionary<string, bool> MethodTestResults { get { return _MethodTestResults; } }

        bool _OverallSuccess = false;
        public virtual bool OverallSuccess { get { return _OverallSuccess; } }

        public virtual void ExecuteTests()
        {
            throw new NotImplementedException();
        }

        public virtual void ExecuteTests(List<string> TestsToPerform)
        {
            throw new NotImplementedException();
        }

        public virtual void ExecuteTests(List<CodeSignature> TestsToPerform)
        {
            throw new NotImplementedException();
        }
    }


}
