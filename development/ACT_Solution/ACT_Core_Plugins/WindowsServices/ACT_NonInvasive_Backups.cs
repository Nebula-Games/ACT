using ACT.Core.Extensions;
using ACT.Core.Interfaces.Common;
using System.Linq;

namespace ACT.Plugins.WindowsServices
{
    public class ACT_NonInvasive_Backups : ACT.Core.Interfaces.Common.I_ExecuteWithParameters
    {
        public string Name => "ACT Non Invasive File/Drive Backup Utility";

        public bool NeedsExecute { get { return true; } set { } }

        public I_TestResult Execute(object[] Params)
        {
            foreach (string op in Params.Select(x => x.ToString()))
            {
                if (op.DirectoryExists(false))
                {
                    continue;
                }
            }

            return null;
        }

        public I_TestResult BackuoDirectory(string Dir, bool SubDirectories)
        {
            return null;
        }
    }
}
