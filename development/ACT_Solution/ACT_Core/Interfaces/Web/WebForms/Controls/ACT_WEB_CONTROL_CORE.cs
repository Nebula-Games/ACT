///-------------------------------------------------------------------------------------------------
// file:	Web\WebForms\Controls\ACT_WEB_CONTROL_CORE.cs
//
// summary:	Implements the act web control core class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACT.Core.Extensions;
#if DOTNETFRAMEWORK
using ACT.Core.Interfaces.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ACT.Core.Web.WebForms
{
    public class ACT_WEB_CONTROL : System.Web.UI.UserControl
    {
        private ACT_WEB_PAGE _PageReference;

        /// <summary>
        /// Represents the ControlID Outside of the ID Value.  Used For Custom Eventing
        /// </summary>
        public string ControlID { get; set; }
        /// <summary>
        /// Constructor to Setup Event Handlers
        /// </summary>
        public ACT_WEB_CONTROL() : base()
        {
            this.Load += ACT_WEB_CONTROL_Load;
            this.DataBinding += ACT_WEB_CONTROL_DataBinding;
            this.Init += ACT_WEB_CONTROL_Init;
        }

          public void InitControl(string ControlID)
        {
           
        }

        private void ACT_WEB_CONTROL_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ACT_WEB_CONTROL_Init(object sender, EventArgs e)
        {
          //  ViewState[""]
        }

        private void ACT_WEB_CONTROL_DataBinding(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

#region Public Events

        /// <summary>
        /// Object Array = { IsPostBack, IsCallback, IsAsync }
        /// </summary>
        public event ACT.Core.Delegates.OnComplete OnControlLoad;

        /// <summary>
        /// Processes Custom Events Triggered By AddCustomEvent
        /// </summary>
        public event ACT.Core.Delegates.OnComplete OnControlDatabind;

        /// <summary>
        /// Attach to this event to initialize the page
        /// </summary>
        public event ACT.Core.Delegates.OnComplete OnControlInit;

#endregion

#region Custom Event Code

        /// <summary>
        /// Generate - Custom Event JS Code
        /// </summary>
        /// <param name="EventName"></param>
        /// <returns></returns>
        public string GetCustomEvent_JSCode(string EventName)
        {
            ACT_WEB_PAGE _Page = (ACT_WEB_PAGE)this.Page;

            if (_Page.Custom_Postback_Events.Exists(x => x.EventName == EventName))
            {
                return "__doPostBack(\"" + _Page.Custom_Postback_Events.First(x => x.EventName == EventName).EventTarget + "\", \"" + _Page.Custom_Postback_Events.First(x => x.EventName == EventName).EventArgument + "\");";
            }
            else { return "alert('Missing Event Declaration')"; }
        }

        /// <summary>
        /// Add a Custom Event To Track and Process.  This will call the event OnCustomPostbackEvent
        /// </summary>
        /// <param name="EventName">Name of the Event (Custom Defined)</param>
        /// <param name="PostbackData">EventTarget and EventArgument specified by the __DoPostback Method</param>
        /// <returns></returns>
        public bool AddCustomEvent(string EventName, (string target, string argument) PostbackData)
        {
            try
            {
                ACT_WEB_PAGE _Page = (ACT_WEB_PAGE)this.Page;

                if (_Page.Custom_Postback_Events.Exists(x => x.EventName == EventName))
                {
                    _Page.Custom_Postback_Events.First(x => x.EventName == EventName).EventTarget = PostbackData.target;
                    _Page.Custom_Postback_Events.First(x => x.EventName == EventName).EventArgument = PostbackData.argument;
                }
                else
                {
                    _Page.Custom_Postback_Events.Add(new ACT_WEB_PAGE.Page_Custom_Postback_Events() { EventName = EventName, EventTarget = PostbackData.target, EventArgument = PostbackData.argument });
                }
            }
            catch { return false; }

            return true;
        }

#endregion
    }
}
#endif