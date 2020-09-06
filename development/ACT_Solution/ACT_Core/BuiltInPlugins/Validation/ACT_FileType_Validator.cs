///-------------------------------------------------------------------------------------------------
// file:	BuiltInPlugins\Validation\ACT_FileType_Validator.cs
//
// summary:	Implements the act file type validator class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using ACT.Core.Enums;
using ACT.Core.Interfaces;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.Authentication;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ACT.Core.BuiltInPlugins.Validation
{
    public class JSON : ACT.Core.Interfaces.Validation.I_FileType_Validator<string>
    {

        #region ValidationMethods
        public bool IsValid(string FileContents)
        {
            if (string.IsNullOrWhiteSpace(FileContents)) { return false; }

            var value = FileContents.Trim();

            if ((value.StartsWith("{") && value.EndsWith("}")) || //For object
                (value.StartsWith("[") && value.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(value);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
            }

            return false;
        }

        public bool IsValid(object FileContents)
        {
            return IsValid(FileContents.ToString());
        }

        public bool IsValid(List<string> FileContents)
        {
            foreach(string file in FileContents)
            {
                if (IsValid(file) == false) { return false; }
            }
            return true;
        }

        public bool IsValid(List<object> FileContents)
        {
            foreach (string file in FileContents)
            {
                if (IsValid(file.ToString()) == false) { return false; }
            }
            return true;
        }

        #endregion



        public bool HasChanged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<string> PublicProperties => throw new NotImplementedException();

        public void Dispose()
        {
            return;
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

        public I_TestResult ImportXMLData(string XML)
        {
            throw new NotImplementedException();
        }

       


        public void LogError(string className, string summary, Exception ex, string additionInformation, ErrorLevel errorType)
        {
            throw new NotImplementedException();
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

        public I_TestResult ValidatePluginRequirements()
        {
            throw new NotImplementedException();
        }

        public void SetImpersonate(object UserInfo)
        {
            throw new NotImplementedException();
        }
    }
}
