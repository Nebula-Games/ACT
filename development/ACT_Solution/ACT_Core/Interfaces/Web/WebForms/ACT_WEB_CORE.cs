// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_WEB_CORE.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACT.Core.Extensions;
#if DOTNETFRAMEWORK
using System.Web.UI;
using System.Web.UI.WebControls;
using ACT.Core.Interfaces.Web;

namespace ACT.Core.Web.WebForms
{
    /// <summary>
    /// ACT WEB PAGE Allows you to create amazing easy functionality without the muss and fuss
    /// Implements the <see cref="System.Web.UI.Page" />
    /// Implements the <see cref="ACT.Core.Interfaces.Web.I_ACT_Web_Security_Page" />
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    /// <seealso cref="ACT.Core.Interfaces.Web.I_ACT_Web_Security_Page" />
    public class ACT_WEB_PAGE : System.Web.UI.Page, I_ACT_Web_Security_Page
    {
        /// <summary>
        /// Constructor to Setup Event Handlers
        /// </summary>
        public ACT_WEB_PAGE() : base()
        {
            this.Load += ACT_WEB_PAGE_Load;
            this.LoadComplete += ACT_WEB_PAGE_LoadComplete;
            this.InitComplete += ACT_WEB_PAGE_InitComplete;
            this.PreInit += ACT_WEB_PAGE_PreInit;
            this.PreLoad += ACT_WEB_PAGE_PreLoad;
            this.PreRenderComplete += ACT_WEB_PAGE_PreRenderComplete;
        }

        /// <summary>
        /// Event that happens before the page initialization
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ACT_WEB_PAGE_PreInit(object sender, EventArgs e)
        {
            if (this.OnPageInitializing != null) { OnPageInitializing(new object[] { }); }

        }

#region Public Events

        /// <summary>
        /// Object Array = { IsPostBack, IsCallback, IsAsync }
        /// Executed
        /// </summary>
        public event ACT.Core.Delegates.OnComplete OnPageInitialized;

        /// <summary>
        /// Processes Custom Events Triggered By AddCustomEvent
        /// </summary>
        public event ACT.Core.Delegates.OnComplete OnCustomPostbackEvent;

        /// <summary>
        /// Attach to this event to initialize the page
        /// </summary>
        public event ACT.Core.Delegates.OnComplete OnInitializePageData;

        /// <summary>
        /// Occurs before the Page is initialized.
        /// </summary>
        public event ACT.Core.Delegates.OnComplete OnPageInitializing;

        /// <summary>
        /// On Load
        /// </summary>
        public event ACT.Core.Delegates.OnComplete OnPageLoad;

        /// <summary>
        /// Custom Pre Load Handler
        /// </summary>
        public event ACT.Core.Delegates.OnComplete OnPagePreLoad;

        /// <summary>
        /// Custom Pre Load Handler
        /// </summary>
        public event ACT.Core.Delegates.OnComplete OnPagePreRenderComplete;

#endregion

#region Event Handlers

        /// <summary>
        /// Runs the Pre Render Complete Event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ACT_WEB_PAGE_PreRenderComplete(object sender, EventArgs e)
        {
            if (ViewState["CustomEvents"] == null) { ViewState.Add("CustomEvents", Custom_Postback_Events); }
            else { ViewState["CustomEvents"] = Custom_Postback_Events; }

            if (ViewState["ValidationData"] == null) { ViewState.Add("ValidationData", ValidationData); }
            else { ViewState["ValidationData"] = ValidationData; }

            if (ViewState["RequestData"] == null) { ViewState.Add("RequestData", RequiredRequestVariables); }
            else { ViewState["RequestData"] = RequiredRequestVariables; }

            if (ViewState["PageSettings"] == null) { ViewState.Add("PageSettings", PageSettings); }
            else { ViewState["PageSettings"] = PageSettings; }

            if (OnPagePreRenderComplete != null) { OnPagePreRenderComplete(new object[] { sender, e }); }
        }

        /// <summary>
        /// Pre Load Event Handler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ACT_WEB_PAGE_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // OnInitializePageData(new object[] { });
                // OnInit_Complete(e);
                CheckRequired_RequestVariables();
            }
            else
            {
                Custom_Postback_Events = (List<Page_Custom_Postback_Events>)ViewState["CustomEvents"];
                ValidationData = (List<Form_Validator_Data>)ViewState["ValidationData"];
                RequiredRequestVariables = (List<Request_RequiredFields_Data>)ViewState["RequestData"];
                PageSettings = (Page_Settings)ViewState["PageSettings"];
            }


            if (OnPagePreLoad != null) { OnPagePreLoad(new object[] { sender, e }); }

            SaveViewState();

        }

        /// <summary>
        /// Page Load Event Handler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ACT_WEB_PAGE_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, string.Empty);

            if (OnPageLoad != null) { OnPageLoad(new object[] { sender, e }); }
        }

        /// <summary>
        /// Page Init Complete Event Handler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ACT_WEB_PAGE_InitComplete(object sender, EventArgs e)
        {

            // Setup References
            QS = Request.QueryString;
            FD = Request.Form;

            if (this is I_ACT_Web_Security_Page)
            {
                if (this.PageSettings.RequiredAuthentication)
                {
                    var _AuthResults = AuthenticateUser();
                    if (_AuthResults.Item1 == false)
                    {
                        Response.Redirect(this.AuthenticationErrorRedirectURL.Replace("#MSG#", _AuthResults.Item2));
                        return;
                    }
                }
            }

            if (!IsPostBack)
            {
                if (Request.UrlReferrer == null) { ViewState["PreviousPageUrl"] = ""; }
                else { ViewState["PreviousPageUrl"] = Request.UrlReferrer.ToString(); }
            }

            if (this.OnPageInitialized != null)
            {
                this.OnPageInitialized(new object[] { sender, e, IsPostBack.ToString(), IsCallback.ToString(), IsAsync.ToString() });
            }
        }

        /// <summary>
        /// Page Load Complete Event Handler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ACT_WEB_PAGE_LoadComplete(object sender, EventArgs e)
        {
            string _EventTarget = Request["__EVENTTARGET"].ToString(true);
            string _EventArgument = Request["__EVENTARGUMENT"].ToString(true);

            if (!_EventTarget.NullOrEmpty())
            {
                if (Custom_Postback_Events.Count(x => x.EventTarget == _EventTarget && x.EventArgument == _EventArgument) > 0)
                {
                    var EventData = Custom_Postback_Events.First(x => x.EventTarget == _EventTarget && x.EventArgument == _EventArgument);
                    if (OnCustomPostbackEvent != null)
                    {
                        OnCustomPostbackEvent(new object[] { EventData.EventName, EventData.EventTarget + "," + EventData.EventArgument });
                    }
                }
            }
        }

#endregion

#region Support Classes

        /// <summary>
        /// Form Validator Data Class.
        /// Used in Validating The Data In A Form Automatically.
        /// </summary>
        [Serializable()]
        public class Form_Validator_Data
        {
            /// <summary>
            /// The group name
            /// </summary>
            public string GroupName;
            /// <summary>
            /// The control identifier
            /// </summary>
            public string ControlID;
            /// <summary>
            /// The equal to control identifier
            /// </summary>
            public string EqualToControlID;
            /// <summary>
            /// The allow nulls
            /// </summary>
            public bool AllowNulls = false;
            /// <summary>
            /// The optional
            /// </summary>
            public bool Optional = false;
            /// <summary>
            /// The data type
            /// </summary>
            private string _DataType = "System.String";
            /// <summary>
            /// Gets or sets the type of the data.
            /// </summary>
            /// <value>The type of the data.</value>
            public Type DataType { get { return Type.GetType(_DataType); } set { _DataType = value.FullName; } }
            /// <summary>
            /// The required
            /// </summary>
            public bool Required = false;
            /// <summary>
            /// The allow blank
            /// </summary>
            public bool AllowBlank = true;
            /// <summary>
            /// Determines the minimum of the parameters.
            /// </summary>
            public decimal Min = 0;
            /// <summary>
            /// Determines the maximum of the parameters.
            /// </summary>
            public decimal Max = 100;
            /// <summary>
            /// The reg ex
            /// </summary>
            public string RegEx = "";
            /// <summary>
            /// The error message
            /// </summary>
            public string ErrorMessage = "";
            /// <summary>
            /// The is email address
            /// </summary>
            public bool IsEmailAddress = false;
            /// <summary>
            /// The includes value
            /// </summary>
            public List<string> IncludesValue = new List<string>();
            /// <summary>
            /// The date latest part
            /// </summary>
            public string DateLatestPart = "";
            /// <summary>
            /// The date latest term
            /// </summary>
            public string DateLatestTerm = "";
            /// <summary>
            /// The date earliest part
            /// </summary>
            public string DateEarliestPart = "";
            /// <summary>
            /// The date earliest term
            /// </summary>
            public string DateEarliestTerm = "";
        }

        /// <summary>
        /// The class describes the required fields in request object and actions to take when not found
        /// </summary>
        [Serializable()]
        public class Request_RequiredFields_Data
        {
            /// <summary>
            /// The optional
            /// </summary>
            public bool Optional = false;
            /// <summary>
            /// The field name
            /// </summary>
            public string FieldName;
            /// <summary>
            /// The allow blank
            /// </summary>
            public bool AllowBlank = true;
            /// <summary>
            /// The data type
            /// </summary>
            private string _DataType = "System.String";
            /// <summary>
            /// Gets or sets the type of the data.
            /// </summary>
            /// <value>The type of the data.</value>
            public Type DataType { get { return Type.GetType(_DataType); } set { _DataType = value.FullName; } }
            /// <summary>
            /// The reg ex
            /// </summary>
            public string RegEx = "";
            /// <summary>
            /// The redirect URL
            /// </summary>
            public string RedirectURL = "/ErrorPage.aspx?MSG=#MSG#";
            /// <summary>
            /// The error message
            /// </summary>
            public string ErrorMessage = "Required Field is Missing!";
            /// <summary>
            /// The allow nulls
            /// </summary>
            public bool AllowNulls = false;
            /// <summary>
            /// The depends on
            /// </summary>
            public List<string> DependsOn = new List<string>();
            /// <summary>
            /// The valid strings
            /// </summary>
            public List<string> ValidStrings = new List<string>();
        }

        /// <summary>
        /// Page General Settings
        /// </summary>
        [Serializable()]
        public class Page_Settings
        {
            /// <summary>
            /// The verbose debugging
            /// </summary>
            public bool VerboseDebugging = false;
            /// <summary>
            /// The log all data
            /// </summary>
            public bool LogAllData = false;
            /// <summary>
            /// The required authentication
            /// </summary>
            public bool RequiredAuthentication = true;
            /// <summary>
            /// The page request variables error URL
            /// </summary>
            public string PageRequestVariablesErrorURL = "/Error.aspx";
            /// <summary>
            /// The user identifier session variable
            /// </summary>
            public string UserIDSessionVariable = "UserID";
            /// <summary>
            /// The handler validation variable
            /// </summary>
            public string HandlerValidationVariable = "PageValidationCode";
            /// <summary>
            /// The user identifier is unique identifier
            /// </summary>
            public bool UserID_IsGuid = true;
            /// <summary>
            /// The user identifier is int
            /// </summary>
            public bool UserID_IsInt = false;
            /// <summary>
            /// The authentication error redirect URL
            /// </summary>
            public string AuthenticationErrorRedirectURL = "/Login.aspx";
        }

        /// <summary>
        /// Holds the Page Custom Postback Events
        /// </summary>
        [Serializable()]
        public class Page_Custom_Postback_Events
        {
            /// <summary>
            /// The event name
            /// </summary>
            public string EventName;
            /// <summary>
            /// The event target
            /// </summary>
            public string EventTarget;
            /// <summary>
            /// The event argument
            /// </summary>
            public string EventArgument;
        }

#endregion

#region Shortcut References
        /// <summary>
        /// Quick Reference to the Query String Collection
        /// </summary>
        public System.Collections.Specialized.NameValueCollection QS;

        /// <summary>
        /// Quick reference to the Form Collection
        /// </summary>
        public System.Collections.Specialized.NameValueCollection FD;
#endregion

#region Helper Methods

        /// <summary>
        /// Request the IP Address
        /// </summary>
        /// <value>The request ip address.</value>
        public string RequestIPAddress
        {
            get
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    string[] addresses = ipAddress.Split(',');
                    if (addresses.Length != 0)
                    {
                        return addresses[0];
                    }
                }

                return context.Request.ServerVariables["REMOTE_ADDR"];
            }
        }

        /// <summary>
        /// Get the previous URL saved from the Initialization
        /// </summary>
        /// <value>The get previous URL.</value>
        public string GetPreviousURL
        {
            get
            {
                try { return (string)ViewState["PreviousPageUrl"]; } catch { return ""; }
            }
        }

        /// <summary>
        /// Generate Redirect URL
        /// </summary>
        /// <returns>System.String.</returns>
        public string GenerateRedirectURL()
        {
            return "RedirectURL=" + Request.RawUrl.ToBase16();
        }

        /// <summary>
        /// Generate Redurect URL From Embedded URL
        /// </summary>
        /// <returns>System.String.</returns>
        public string Parse_Redirect_URL()
        {
            if (Request.QueryString["RedirectURL"] != null) { return Request.QueryString["RedirectURL"].FromBase16(); }
            else { return ""; }
        }

#endregion

#region Page Variables Fields / Properties

#region Private Fields

        /// <summary>
        /// You can set the Required Fields for this Page. This is the Simple Implementation
        /// </summary>
        private List<Request_RequiredFields_Data> _RequiredRequestVariables = new List<Request_RequiredFields_Data>();

        /// <summary>
        /// List of the Form Validation Data
        /// </summary>
        private List<Form_Validator_Data> _ValidationData = new List<Form_Validator_Data>();

        /// <summary>
        /// Holds the Page Settings data
        /// </summary>
        private Page_Settings _Settings = new Page_Settings();

        /// <summary>
        /// list of Custom Postback Events Documented for this Page
        /// </summary>
        private List<Page_Custom_Postback_Events> _customPostbackEvents = new List<Page_Custom_Postback_Events>();

#endregion

#region Public Property
        /// <summary>
        /// Custom Page Events
        /// </summary>
        /// <value>The custom postback events.</value>
        public List<Page_Custom_Postback_Events> Custom_Postback_Events { get { return _customPostbackEvents; } set { _customPostbackEvents = value; } }

        /// <summary>
        /// Required Request Variables (Load)
        /// </summary>
        /// <value>The required request variables.</value>
        public List<Request_RequiredFields_Data> RequiredRequestVariables { get { return _RequiredRequestVariables; } set { _RequiredRequestVariables = value; } }

        /// <summary>
        /// Validation Data (Forms)
        /// </summary>
        /// <value>The validation data.</value>
        public List<Form_Validator_Data> ValidationData { get { return _ValidationData; } set { _ValidationData = value; } }

        /// <summary>
        /// Page Settings
        /// </summary>
        /// <value>The page settings.</value>
        public Page_Settings PageSettings { get { return _Settings; } set { _Settings = value; } }
#endregion

#region ACT Web Security Page Region

        /// <summary>
        /// Can be overridden to perform additional checks.
        /// </summary>
        /// <returns>System.ValueTuple&lt;System.Boolean, System.String&gt;.</returns>
        public virtual (bool, string) AuthenticateUser()
        {
            if (MemberID == null) { return (false, "User not logged in."); }
            if (MemberID.Value == Guid.Empty) { return (false, "User not logged in."); }

            return (true, "");
        }

        /// <summary>
        /// Member ID
        /// </summary>
        /// <value>The member identifier.</value>
        public Guid? MemberID
        {
            get { return Session[PageSettings.UserIDSessionVariable].ToGuid(); }
        }

        /// <summary>
        /// Create the Encrypted Security Data - JS Validation
        /// </summary>
        /// <value>The security data encrypted.</value>
        public string SecurityDataEncrypted { get { return SecurityDataRaw.ToString().ToSHA512().ToBase16(); } }

        /// <summary>
        /// Raw Security Data - JS Validation
        /// </summary>
        /// <value>The security data raw.</value>
        public string SecurityDataRaw { get; set; }

        /// <summary>
        /// Redirect URL for Unauthorized Users
        /// </summary>
        /// <value>The authentication error redirect URL.</value>
        public string AuthenticationErrorRedirectURL { get { return this.PageSettings.AuthenticationErrorRedirectURL; } }

#endregion

#endregion

#region Custom Event Code

        /// <summary>
        /// Generate - Custom Event JS Code
        /// </summary>
        /// <param name="EventName">Name of the event.</param>
        /// <returns>System.String.</returns>
        public string GetCustomEvent_JSCode(string EventName)
        {
            if (Custom_Postback_Events.Exists(x => x.EventName == EventName))
            {
                return "__doPostBack(\"" + Custom_Postback_Events.First(x => x.EventName == EventName).EventTarget + "\", \"" + Custom_Postback_Events.First(x => x.EventName == EventName).EventArgument + "\");";
            }
            else
            {
                return "alert('Missing Event Declaration')";
            }
        }

        /// <summary>
        /// Add a Custom Event To Track and Process.  This will call the event OnCustomPostbackEvent
        /// </summary>
        /// <param name="EventName">Name of the Event (Custom Defined)</param>
        /// <param name="PostbackData">EventTarget and EventArgument specified by the __DoPostback Method</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddCustomEvent(string EventName, (string target, string argument) PostbackData)
        {
            try
            {
                if (Custom_Postback_Events.Exists(x => x.EventName == EventName))
                {
                    Custom_Postback_Events.First(x => x.EventName == EventName).EventTarget = PostbackData.target;
                    Custom_Postback_Events.First(x => x.EventName == EventName).EventArgument = PostbackData.argument;
                }
                else
                {
                    Custom_Postback_Events.Add(new Page_Custom_Postback_Events() { EventName = EventName, EventTarget = PostbackData.target, EventArgument = PostbackData.argument });
                }
            }
            catch { return false; }

            return true;
        }

#endregion

        /// <summary>
        /// Validate the Page data
        /// </summary>
        /// <param name="Ctrl">The control.</param>
        /// <param name="GroupName">Name of the group.</param>
        /// <returns>System.ValueTuple&lt;System.Boolean, System.String, System.String&gt;.</returns>
        public virtual (bool success, string controlID, string errMessage) ValidateFormData(Control Ctrl, string GroupName)
        {
            //Required Fields
            foreach (Form_Validator_Data ReqField in _ValidationData.Where(x => x.GroupName == GroupName))
            {
                Control _FoundControl = null;
                try { _FoundControl = Ctrl.FindControl(ReqField.ControlID); } catch { }
                if (_FoundControl == null) { try { _FoundControl = this.FindControl(ReqField.ControlID); } catch { } }
                if (_FoundControl == null) { return (false, ReqField.ControlID, "Control Not Found"); }

                if (ReqField.Optional == true) { }
                if (ReqField.Required == true)
                {
                    if (_FoundControl is TextBox)
                    {
                        var _txtBox = (TextBox)_FoundControl;
                        if (_txtBox.Text.NullOrEmpty() && ReqField.Optional == false && ReqField.AllowBlank == false && ReqField.AllowNulls == false) { _txtBox.Focus(); return (false, ReqField.ControlID, ReqField.ErrorMessage); }

                        if (ReqField.AllowBlank == false)
                        {
                            if (ReqField.IsEmailAddress)
                            {
                                if (_txtBox.Text.IsValidEmail() == false)
                                {
                                    return (false, ReqField.ControlID, ReqField.ErrorMessage);
                                }
                            }


                            var _typ = ReqField.DataType;

                            if (_typ == typeof(int)) { if (_txtBox.Text.ToInt(-1) == -1) { return (false, ReqField.ControlID, ReqField.ErrorMessage); } }
                            else if (_typ == typeof(DateTime)) { if (_txtBox.Text.ToDateTime(DateTime.MinValue) == DateTime.MinValue) { return (false, ReqField.ControlID, ReqField.ErrorMessage); } }
                            else if (_typ == typeof(float)) { if (_txtBox.Text.ToFloat(-1) == -1) { return (false, ReqField.ControlID, ReqField.ErrorMessage); } }
                            else if (_typ == typeof(decimal)) { if (_txtBox.Text.ToDecimal(-1) == -1) { return (false, ReqField.ControlID, ReqField.ErrorMessage); } }
                            else if (_typ == typeof(bool)) { bool? textV = _txtBox.Text.ToBool(); if (textV == null) { return (false, ReqField.ControlID, ReqField.ErrorMessage); } }


                            if (ReqField.RegEx.NullOrEmpty() == false)
                            {
                                System.Text.RegularExpressions.Regex _RegEx = new System.Text.RegularExpressions.Regex(ReqField.RegEx);
                                if (_RegEx.Match(_txtBox.Text).Success == false)
                                {
                                    return (false, ReqField.ControlID, ReqField.ErrorMessage);
                                }
                            }

                            if (ReqField.Min != ReqField.Max && (ReqField.DataType == typeof(int) || ReqField.DataType == typeof(long) || ReqField.DataType == typeof(float) || ReqField.DataType == typeof(decimal)))
                            {
                                decimal val = _txtBox.Text.ToDecimal(0);
                                if (val < ReqField.Min || val > ReqField.Max) { return (false, ReqField.ControlID, ReqField.ErrorMessage); }
                            }
                        }
                    }
                    else if (_FoundControl is DropDownList)
                    {
                        var _dropDownList = (DropDownList)_FoundControl;
                        if (_dropDownList.SelectedIndex == -1 && ReqField.Optional == false) { _dropDownList.Focus(); return (false, ReqField.ControlID, ReqField.ErrorMessage); }
                    }
                    else if (_FoundControl is RadioButtonList)
                    {
                        var _dropDownList = (RadioButtonList)_FoundControl;
                        if (_dropDownList.SelectedIndex == -1 && ReqField.Optional == false) { _dropDownList.Focus(); return (false, ReqField.ControlID, ReqField.ErrorMessage); }
                    }
                    else if (_FoundControl is CheckBox)
                    {
                        var _dropDownList = (CheckBox)_FoundControl;
                        if (_dropDownList.Checked == false && ReqField.Required == true && ReqField.Optional == false) { _dropDownList.Focus(); return (false, ReqField.ControlID, ReqField.ErrorMessage); }
                    }
                    else
                    {
                        return (false, ReqField.ControlID, "Invalid Control Type.");
                    }
                }
            }

            return (true, "", "");
        }

        /// <summary>
        /// Check the Required Query String Variables
        /// </summary>
        private void CheckRequired_RequestVariables()
        {
            if (RequiredRequestVariables == null) { return; }
            // Check Required Fields
            if (RequiredRequestVariables.Count() > 0)
            {
                foreach (Request_RequiredFields_Data key in _RequiredRequestVariables)
                {
                    bool _right = true;
                    if (key.DataType == typeof(Guid) || key.DataType == typeof(Guid?))
                    {
                        try
                        {
                            var _tmpG = Request[key.FieldName].ToGuid();
                            if (_tmpG == null)
                            {
                                _tmpG = Request[key.FieldName].FromBase16().ToGuid();
                                if (_tmpG == null) { _right = false; }
                            }
                        }
                        catch { _right = false; }
                    }
                    else if (key.DataType == typeof(int) || key.DataType == typeof(int?))
                    {
                        try
                        {
                            var _tmpG = Request[key.FieldName].ToInt(0);
                            if (_tmpG == 0) { _right = false; }
                        }
                        catch { _right = false; }
                    }
                    else if (key.DataType == typeof(long) || key.DataType == typeof(long?))
                    {
                        try
                        {
                            var _tmpG = Request[key.FieldName].ToLong(0);
                            if (_tmpG == 0) { _right = false; }
                        }
                        catch { _right = false; }
                    }
                    else if (key.DataType == typeof(string))
                    {
                        try
                        {
                            var _tmpG = Request[key.FieldName].ToString(false);
                            if (_tmpG.NullOrEmpty()) { _right = false; }
                        }
                        catch { _right = false; }
                    }
                    else if (key.DataType == typeof(float))
                    {
                        try
                        {
                            var _tmpG = Request[key.FieldName].ToFloat(-10101010.1f);
                            if (_tmpG == -10101010.1) { _right = false; }
                        }
                        catch { _right = false; }
                    }
                    else if (key.DataType == typeof(decimal))
                    {
                        try
                        {
                            var _tmpG = Request[key.FieldName].ToDecimal(-10101010.1m);
                            if (_tmpG == -10101010.1m) { _right = false; }
                        }
                        catch { _right = false; }
                    }
                    else if (key.DataType == typeof(double))
                    {
                        try
                        {
                            var _tmpG = Request[key.FieldName].ToDouble(-10101010.1d);
                            if (_tmpG == -10101010.1d) { _right = false; }
                        }
                        catch { _right = false; }
                    }

                    if (_right == false)
                    {
                        Response.Redirect(PageSettings.PageRequestVariablesErrorURL);
                        return;
                    }
                }
            }

        }

        /// <summary>
        /// Initialize the Page Data.  Should Be Called During Page Initialized
        /// </summary>
        /// <param name="Settings">The settings.</param>
        /// <param name="ValidationData">The validation data.</param>
        /// <param name="RequiredRequestVariables">The required request variables.</param>
        public void InitializePageData(Page_Settings Settings, List<Form_Validator_Data> ValidationData = null, List<Request_RequiredFields_Data> RequiredRequestVariables = null)
        {
            _ValidationData = ValidationData;
            _RequiredRequestVariables = RequiredRequestVariables;
            this._Settings = Settings;

            if (this is I_ACT_Web_Security_Page)
            {
                this.SecurityDataRaw = Guid.NewGuid().ToString();
                ConfigureSecurityPage();
            }

            if (this.OnInitializePageData != null)
            {
                this.OnInitializePageData(new object[] { true, this.SecurityDataRaw });
            }
        }

        /// <summary>
        /// Load the Configuration Settings
        /// </summary>
        /// <param name="BaseDirectory">The base directory.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool InitializePageData_FromConfigurationSettings(string BaseDirectory = "")
        {
            if (Cache["ACT_SITE_SETTINGS"] == null)
            {
                if (BaseDirectory.NullOrEmpty()) { BaseDirectory = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat(); }
                string _FilePath = BaseDirectory.FindFileReturnPath("SiteSettings.json", true);
                Cache["ACT_SITE_SETTINGS"] = ACT.Core.Web.WebForms.SiteValidationSettings.FromJson(_FilePath);
            }

            if (Cache["ACT_SITE_SETTINGS"] == null)
            {
                if (this.OnInitializePageData != null) { this.OnInitializePageData(new object[] { false, this.SecurityDataRaw }); }
                return false;
            }
            else
            {
                SiteValidationSettings _tSettings = (SiteValidationSettings)Cache["ACT_SITE_SETTINGS"];
                _ValidationData = _tSettings.GetValidationData(this.Request.Path);
            //    _Settings = _tSettings.GetPageSettings(this.Request.Path);
                _RequiredRequestVariables = _tSettings.GetRequestRequiredFields(this.Request.Path);

                if (this.OnInitializePageData != null) { this.OnInitializePageData(new object[] { true, this.SecurityDataRaw }); }
                return true;
            }

        }

        /// <summary>
        /// Configure the Security Logic sets HandlerValidationVariable
        /// </summary>
        public virtual void ConfigureSecurityPage()
        {
            /// Global Validation Handler Variable
            Session[PageSettings.HandlerValidationVariable] = SecurityDataRaw.ToString().ToSHA512().ToBase16();

            if (this.AuthenticationErrorRedirectURL.NullOrEmpty() == true)
            {
                this.PageSettings.AuthenticationErrorRedirectURL = "~/Error.aspx?MSG=#MSG#";
            }
        }



        /// <summary>
        /// Generate the Javascript Validation
        /// </summary>
        /// <returns>System.String.</returns>
        public string GenerateJavascriptValidation()
        {
            return ACT_WEB_CORE_JSWRITER.GenerateJSValidationScript(ValidationData);
        }
    }
}
#endif