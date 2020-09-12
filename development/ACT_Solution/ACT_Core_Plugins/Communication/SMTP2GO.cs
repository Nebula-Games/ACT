using ACT.Core.Enums;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Plugins.Communication
{
    public class SMTP2GO : ACT.Core.Interfaces.Communication.I_Emails
    {
        public bool HasChanged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<string> PublicProperties => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string ExportXMLData()
        {
            throw new NotImplementedException();
        }

        public List<Exception> GetErrors()
        {
            throw new NotImplementedException();
        }

        public I_TestResult HealthCheck()
        {
            throw new NotImplementedException();
        }
        public void SetImpersonate(object UserInfo)
        {
            throw new NotImplementedException();
        }
        public I_TestResult ImportXMLData(string XML)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Log Error Using ACT - Helper
        /// </summary>
        /// <param name="className"></param>
        /// <param name="summary"></param>
        /// <param name="ex"></param>
        /// <param name="additionInformation"></param>
        /// <param name="errorType"></param>
        public void LogError(string className, string summary, Exception ex, string additionInformation, ErrorLevel errorType)
        {
            ACT.Core.Helper.ErrorLogger.LogError("ACT.Plugins.Communication.SMTP2GO", summary + "_" + additionInformation, ex, errorType);
        }

        public object ReturnProperty(string PropertyName)
        {
            throw new NotImplementedException();
        }

        public Type ReturnPropertyType(string PropertyName)
        {
            throw new NotImplementedException();
        }

        public List<string> ReturnSystemSettingRequirements()
        {
            throw new NotImplementedException();
        }

        public bool SendBatch()
        {
            // Get Latest Batch To Send
            throw new NotImplementedException();
        }

        public I_TestResult SendEmail(List<string> To, List<string> CC, List<string> BCC, string ReplyTo, string Subject, string Body)
        {
            throw new NotImplementedException();
        }

        public I_TestResult SendEmail(string To, string CC, string BCC, string ReplyTo, string Subject, string Body)
        {
            throw new NotImplementedException();
        }

        public I_TestResult SendEmail(string To, string CC, string BCC, string ReplyTo, string Subject, string Body, string FileName)
        {
            return null;
        }
        public Core.Interfaces.Common.I_TestResult SendEmail(string To, string CC, string BCC, string ReplyTo, string Subject, string Body, string Host, int Port, string UserName, string Password)
        { return null; }

        public Core.Interfaces.Common.I_TestResult SendEmail(List<string> To, List<string> CC, List<string> BCC, string ReplyTo, string ReplyToDisplayName, string Subject, string Body, string Host, int Port, string UserName, string Password)
        { return null; }


        public void SetImpersonate(I_UserInfo UserInfo)
        {
            throw new NotImplementedException();
        }

        public I_TestResult SetProperty(string PropertyName, object value)
        {
            throw new NotImplementedException();
        }

        public string StandardReplaceMent(string instr, RepacementStandard InputStandard)
        {
            throw new NotImplementedException();
        }

        public Guid StartBatch()
        {
            throw new NotImplementedException();
        }

        public I_TestResult ValidatePluginRequirements()
        {
            throw new NotImplementedException();
        }
    }
}
