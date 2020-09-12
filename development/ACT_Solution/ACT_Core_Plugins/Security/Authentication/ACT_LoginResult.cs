using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Enums;
using ACT.Core.Interfaces.Security.Authentication;

namespace ACT.Plugins.Security.Authentication
{
    public class ACT_LoginResult : ACT.Plugins.ACT_Core, I_LoginResult
    {
        public void SetImpersonate(object UserInfo)
        {
            throw new NotImplementedException();
        }
        string _TokenID;
        DateTime _ValidTill;
        Dictionary<string, string> _AdditionalInformation;
        bool _Success = false;
        string _ErrorMessage;
        SecurityAccessError _ErrorCode;
        I_UserInfo _UserInfo;

        public I_UserInfo UserInfo
        {
            get { return _UserInfo; }
            set { _UserInfo = value; }
        }

        public string TokenID
        {
            get { return _TokenID; }
            set { _TokenID = value; }
        }

        public DateTime TokenValidTill
        {
            get { return _ValidTill; }
            set { _ValidTill = value; }
        }

        public Dictionary<string, string> AdditionalInformation
        {
            get { return _AdditionalInformation; }
            set { _AdditionalInformation = value; }
        }

        public bool Success
        {
            get { return _Success; }
            set { _Success = value; }
        }

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        public SecurityAccessError ErrorCode
        {
            get { return _ErrorCode; }
            set { _ErrorCode = value; }
        }

        
    }
}
