///-------------------------------------------------------------------------------------------------
// file:	Web\WebForms\ACT_WEB_CORE_SITE_SETTINGS.cs
//
// summary:	Implements the act web core site settings class
///-------------------------------------------------------------------------------------------------

#if DOTNETFRAMEWORK

using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ACT.Core.Web.WebForms
{
    /// <summary>
    /// Site Validation Settings
    /// </summary>
    public class SiteValidationSettings
    {
        /// <summary>
        /// Override ACT Verbose Debugging
        /// </summary>
        [JsonProperty("verbosedebugging")]
        public bool VerboseDebugging { get; set; }

        /// <summary>
        /// Log All Data Sent To The Page
        /// </summary>
        [JsonProperty("logalldata")]
        public bool LogAllData { get; set; }

        /// <summary>
        /// Require Authentication (Is Authenticated Only)
        /// </summary>
        [JsonProperty("requiredauthentication")]
        public bool RequireAuthentication { get; set; }

        /// <summary>
        /// OverRide UserID Session Variable Name
        /// </summary>
        [JsonProperty("useridsessionvariablename")]
        public string UserIDSessionVariableName { get; set; }
        
        /// <summary>
        /// OverRide URL to send the user to if the Request Variables Fail
        /// </summary>
        [JsonProperty("pagerequestvariableserrorurl")]
        public string PageRequestVariablesErrorUrl { get; set; }

        /// <summary>
        /// OverRide URL to send the user to if the Request Variables Fail
        /// </summary>
        [JsonProperty("authenticationloginurl")]
        public string AuthenticationLoginUrl { get; set; }
                
        /// <summary>
        /// Base Path of the website.
        /// </summary>
        [JsonProperty("basepath")]
        public string BasePath { get; set; }

        /// <summary>
        /// The Site Name 
        /// </summary>
        [JsonProperty("sitename")]
        public string Sitename { get; set; }

        /// <summary>
        /// All of the defined pages that are controled by the ACT Web Form
        /// </summary>
        [JsonProperty("pages")]
        public List<Page> Pages { get; set; }

        /// <summary>
        /// Configured Virtual Paths
        /// </summary>
        [JsonProperty("sitevirtualpaths")]
        public List<VirtualPaths> SiteVirtualPaths { get; set; }

        /// <summary>
        /// Export Class To JSON
        /// </summary>
        /// <returns></returns>
        public string ToJson() => JsonConvert.SerializeObject(this, JSONConverterSettings);

        /// <summary>
        /// Json Converter Object
        /// </summary>
        public static readonly JsonSerializerSettings JSONConverterSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        /// <summary>
        /// From JSON
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static SiteValidationSettings FromJson(string json) => JsonConvert.DeserializeObject<SiteValidationSettings>(json, JSONConverterSettings);

        /// <summary>
        /// Get Validation Data By The FullPath
        /// </summary>
        /// <param name="PageFullPath"></param>
        /// <returns></returns>
        public List<ACT_WEB_PAGE.Form_Validator_Data> GetValidationData(string PageFullPath)
        {
            var _Page = Pages.First(x => x.Path == PageFullPath);
            if (_Page == null) { return null; }

            List<ACT_WEB_PAGE.Form_Validator_Data> _TmpReturn = new List<ACT_WEB_PAGE.Form_Validator_Data>();
            foreach (var valItem in _Page.Formvalidation)
            {
                foreach (var lfd in valItem.Fields)
                {
                    ACT_WEB_PAGE.Form_Validator_Data _tmpItem = new ACT_WEB_PAGE.Form_Validator_Data();
                    _tmpItem.GroupName = valItem.Groupname;
                    _tmpItem.AllowBlank = lfd.Allowblank;
                    _tmpItem.AllowNulls = lfd.Allownulls;
                    _tmpItem.ControlID = lfd.Controlid;
                    _tmpItem.DataType = Type.GetType(lfd.Datatype);
                    _tmpItem.ErrorMessage = lfd.Errormessage;
                    _tmpItem.IsEmailAddress = lfd.Isemailaddress;
                    _tmpItem.Max = lfd.Max;
                    _tmpItem.Min = lfd.Min;
                    _tmpItem.Optional = lfd.Optional;
                    _tmpItem.RegEx = lfd.Regex;
                    _tmpItem.Required = lfd.FieldRequired;
                    _TmpReturn.Add(_tmpItem);
                }
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Get request Required Fields
        /// </summary>
        /// <param name="PageFullPath"></param>
        /// <returns></returns>
        public List<ACT_WEB_PAGE.Request_RequiredFields_Data> GetRequestRequiredFields(string PageFullPath)
        {
            var _Page = Pages.First(x => x.Path == PageFullPath);
            if (_Page == null) { return null; }

            List<ACT_WEB_PAGE.Request_RequiredFields_Data> _TmpReturn = new List<ACT_WEB_PAGE.Request_RequiredFields_Data>();

            foreach (var fld in _Page.Requestfields)
            {
                ACT_WEB_PAGE.Request_RequiredFields_Data _tmpItem = new ACT_WEB_PAGE.Request_RequiredFields_Data();
                _tmpItem.AllowBlank = fld.Allowblanks;
                _tmpItem.AllowNulls = fld.Allownulls;
                _tmpItem.DataType = Type.GetType(fld.Datatype);
                _tmpItem.DependsOn = fld.Dependson;
                _tmpItem.FieldName = fld.Fieldname;
                _tmpItem.Optional = fld.Optional;
                _tmpItem.RedirectURL = fld.Redirecturl;
                _tmpItem.RegEx = fld.Regex;
                _tmpItem.ValidStrings = fld.Validstrings;
                _TmpReturn.Add(_tmpItem);
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Gets the Page Settings
        /// </summary>
        /// <param name="PageFullPath"></param>
        /// <returns></returns>
        public PageSettings GetPageSettings(string PageFullPath)
        {
            var _Page = Pages.First(x => x.Path == PageFullPath);
            if (_Page == null) { return null; }

            return _Page.Pagesettings;
        }

        /// <summary>
        /// Manages the virtual Friendly paths the user will experience
        /// </summary>
        public class VirtualPaths
        {
            /// <summary>
            /// The Relative Path From the Root /pages/test/test.html
            /// </summary>
            [JsonProperty("relativepath")]
            public string RelativePath { get; set; }

            /// <summary>
            /// Represents the Relative Friendly Path - /testpage
            /// </summary>
            [JsonProperty("friendlypath")]
            public string FriendlyPath { get; set; }

            /// <summary>
            /// Allow Posts
            /// </summary>
            [JsonProperty("allowpost")]
            public bool AllowPost { get; set; }

            /// <summary>
            /// Allow Get
            /// </summary>
            [JsonProperty("allowget")]
            public bool AllowGet { get; set; }

            /// <summary>
            /// Allow Posts
            /// </summary>
            [JsonProperty("requirepostvalidation")]
            public bool RequirePostValidation { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Page
        {
            /// <summary>
            /// The Virtual Path to the Page = (Settings Root Path + Virtual Path)
            /// </summary>
            [JsonProperty("path")]
            public string Path { get; set; }

            /// <summary>
            /// List of Form Validation Settings To Ensure
            /// </summary>
            [JsonProperty("formvalidation")]
            public List<Formvalidation> Formvalidation { get; set; }

            /// <summary>
            /// List of Request Fields (Data Submitted to Page)
            /// </summary>
            [JsonProperty("requestfields")]
            public List<RequestField> Requestfields { get; set; }

            /// <summary>
            /// Page Specific Settings
            /// </summary>
            [JsonProperty("pagesettings")]
            public PageSettings Pagesettings { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Formvalidation
        {
            /// <summary>
            /// Group Name
            /// </summary>
            [JsonProperty("groupname")]
            public string Groupname { get; set; }

            /// <summary>
            /// Fields
            /// </summary>
            [JsonProperty("fields")]
            public List<Field> Fields { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Field
        {
            /// <summary>
            /// Control ID
            /// </summary>
            [JsonProperty("controlid")]
            public string Controlid { get; set; }

            /// <summary>
            /// Allow Nulls
            /// </summary>
            [JsonProperty("allownulls")]
            public bool Allownulls { get; set; }

            /// <summary>
            /// Optional
            /// </summary>
            [JsonProperty("optional")]
            public bool Optional { get; set; }

            /// <summary>
            /// Data Type
            /// </summary>
            [JsonProperty("datatype")]
            public string Datatype { get; set; }

            /// <summary>
            /// Field is Required
            /// </summary>
            [JsonProperty("required")]
            public bool FieldRequired { get; set; }

            /// <summary>
            /// Allow Blanks
            /// </summary>
            [JsonProperty("allowblank")]
            public bool Allowblank { get; set; }

            /// <summary>
            /// Min valud For Integer Types
            /// </summary>
            [JsonProperty("min")]
            public long Min { get; set; }

            /// <summary>
            /// Max Valud For Integer Types
            /// </summary>
            [JsonProperty("max")]
            public long Max { get; set; }

            /// <summary>
            /// Regular Expression To Validate Against
            /// </summary>
            [JsonProperty("regex")]
            public string Regex { get; set; }

            /// <summary>
            /// Error Message
            /// </summary>
            [JsonProperty("errormessage")]
            public string Errormessage { get; set; }

            /// <summary>
            /// Is Email Address
            /// </summary>
            [JsonProperty("isemailaddress")]
            public bool Isemailaddress { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class PageSettings
        {
            /// <summary>
            /// Override ACT Verbose Debugging
            /// </summary>
            [JsonProperty("verbosedebugging")]
            public bool VerboseDebugging { get; set; }

            /// <summary>
            /// Log All Data Sent To The Page
            /// </summary>
            [JsonProperty("logalldata")]
            public bool LogAllData { get; set; }

            /// <summary>
            /// Require Authentication (Is Authenticated Only)
            /// </summary>
            [JsonProperty("requiredauthentication")]
            public bool RequireAuthentication { get; set; }

            /// <summary>
            /// OverRide URL to send the user to if the Request Variables Fail
            /// </summary>
            [JsonProperty("pagerequestvariableserrorurl")]
            public string PageRequestVariablesErrorUrl { get; set; }

            /// <summary>
            /// OverRide UserID Session Variable Name
            /// </summary>
            [JsonProperty("useridsessionvariablename")]
            public string UserIDSessionVariableName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("handlervalidationvariable")]
            public string HandlerValidationVariable { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class RequestField
        {
            /// <summary>
            /// Optional
            /// </summary>
            [JsonProperty("optional")]
            public bool Optional { get; set; }

            /// <summary>
            /// Field Name
            /// </summary>
            [JsonProperty("fieldname")]
            public string Fieldname { get; set; }

            /// <summary>
            /// Allow Blanks
            /// </summary>
            [JsonProperty("allowblanks")]
            public bool Allowblanks { get; set; }

            /// <summary>
            /// Data Type
            /// </summary>
            [JsonProperty("datatype")]
            public string Datatype { get; set; }

            /// <summary>
            /// Regular Expression
            /// </summary>
            [JsonProperty("regex")]
            public string Regex { get; set; }

            /// <summary>
            /// Redirect URL
            /// </summary>
            [JsonProperty("redirecturl")]
            public string Redirecturl { get; set; }

            /// <summary>
            /// Allow Nulls
            /// </summary>
            [JsonProperty("allownulls")]
            public bool Allownulls { get; set; }

            /// <summary>
            /// Depends On
            /// </summary>
            [JsonProperty("dependson")]
            public List<string> Dependson { get; set; }

            /// <summary>
            /// Valid Strings
            /// </summary>
            [JsonProperty("validstrings")]
            public List<string> Validstrings { get; set; }
        }
    }
}
#endif