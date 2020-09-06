using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Enums;
using ACT.Core.Enums.Common;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.Authentication;
using ACT.Core.SystemConfiguration;

namespace ACT.Plugins.Storage.Caching
{
    public class ACT_Caching : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.Storage.Caching.I_Caching
    {
        private Dictionary<string, dynamic> _DATA = new Dictionary<string, dynamic>();
        private bool _HasChanged = false;
        private List<string> _PublicProperties = new List<string>();
        private bool _EnsureStateRecovery = false;
        private int _StateRecoveryLevel = 0;
        private Dictionary<string, BasicSetting> _ConfigurationSettings = new Dictionary<string, BasicSetting>();

        public Dictionary<string, dynamic> DATA { get { return _DATA; } set { _DATA = value; } }
        public override bool HasChanged { get { return _HasChanged; } set { _HasChanged = value; } }

        public bool EnsureStateRecovery { get { return _EnsureStateRecovery; } set { _EnsureStateRecovery = value; } }
        public int StateRecoveryLevel { get { return _StateRecoveryLevel; } set { _StateRecoveryLevel = value; } }
        public Dictionary<string, BasicSetting> ConfigurationSettings { get => _ConfigurationSettings; set => _ConfigurationSettings = value; }

        public BasicMethodReturn Add(string key, dynamic value)
        {
            throw new NotImplementedException();
        }

        public bool LoadConfiguration(string JSONData)
        {
            return true;
        }

        public bool SaveConfiguration(string FilePath)
        {
            return true;
        }
    }
}
