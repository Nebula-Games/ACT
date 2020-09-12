using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Encoding.ACTPackaging;
using ACT.Core.Enums;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.Authentication;
using ACT.Core.Types.ACTConfig;

namespace ACT.Plugins.DataAccess
{
    /// <summary>
    /// ACT_DB_Table Archive Plugin
    /// </summary>
    public class ACT_DB_Table_Archive : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.DataAccess.I_DB_Table_Archive
    {
        public string ArchiveAllData(string TableName, string EncryptionKey = "", Plugin PluginConfigInfo = null)
        {
            throw new NotImplementedException();
        }

        public ACT_Package ArchiveToPackage(string TableName, string EncryptionKey = "", Plugin PluginConfigInfo = null)
        {
            throw new NotImplementedException();
        }

        public string GenerateArchiveSQL(string TableName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetDependantTables(string TableName)
        {
            throw new NotImplementedException();
        }

        public void SetImpersonate(object UserInfo)
        {
            throw new NotImplementedException();
        }
    }
}
