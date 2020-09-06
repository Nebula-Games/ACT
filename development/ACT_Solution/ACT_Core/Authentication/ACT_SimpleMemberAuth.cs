using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Authentication
{
    public class ACT_SimpleMemberAuth
    {
        public ACT_SimpleMemberAuth(string ConnectionStringName, bool CreateTables = false, bool IncludeDemoData = false)
        {
            if (ACT.Core.SystemSettings.HasSettingWithValue(ConnectionStringName) == false)
            {
                throw new Exception("Error Missing Simple Member Auth Connection Information");
            }
            else
            {
                var _DataBase = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent();
            }

        }

        public Member.ACT_BasicMemberInfo Login(string UserName, string Password, string SecurityMethod = "SHA256")
        {
            return null;
        }
    }
}
