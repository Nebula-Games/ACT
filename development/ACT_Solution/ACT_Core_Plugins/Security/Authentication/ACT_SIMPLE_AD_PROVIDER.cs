using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Enums;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.Authentication;

namespace ACT.Plugins.Security.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class ACT_SIMPLE_AD_PROVIDER : ACT.Core.Abstract.Security.Authentication.ACT_ABSTRACT_I_SIMPLE_SECURITY_PROVIDER_ACTIVE_DIRECTORY_LOGIN
    {
        public override bool HasChanged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
     
        public override List<string> PublicProperties => throw new NotImplementedException();

        public override I_TestResult CreateUser(string UserName, string PassWord, Dictionary<string, string> AdditionalData, I_UserInfo NewUserInfo)
        {
            throw new NotImplementedException();
        }

        public override I_TestResult CreateUser(string TokenID, Dictionary<string, string> AdditionalData, I_UserInfo NewUserInfo)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override string ExportXMLData()
        {
            throw new NotImplementedException();
        }

        public override List<Exception> GetErrors()
        {
            throw new NotImplementedException();
        }

        public override I_TestResult HealthCheck()
        {
            throw new NotImplementedException();
        }

        public override I_TestResult ImportXMLData(string XML)
        {
            throw new NotImplementedException();
        }

        public override void LogError(string className, string summary, Exception ex, string additionInformation, ErrorLevel errorType)
        {
            throw new NotImplementedException();
        }

        public override object ReturnProperty(string PropertyName)
        {
            throw new NotImplementedException();
        }

        public override Type ReturnPropertyType(string PropertyName)
        {
            throw new NotImplementedException();
        }

        public override List<string> ReturnSystemSettingRequirements()
        {
            throw new NotImplementedException();
        }
           
        public override void SetImpersonate(object UserInfo)
        {
            throw new NotImplementedException();
        }

        public override I_TestResult SetProperty(string PropertyName, object value)
        {
            throw new NotImplementedException();
        }

        public override string StandardReplaceMent(string instr, RepacementStandard InputStandard)
        {
            throw new NotImplementedException();
        }

        public override I_TestResult ValidatePluginRequirements()
        {
            throw new NotImplementedException();
        }
    }
}
