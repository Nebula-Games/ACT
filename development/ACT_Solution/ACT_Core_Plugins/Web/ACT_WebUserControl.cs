//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using ACT.Core.Delegates;

//namespace ACT.Plugins.Web
//{
//    public class ACT_WebUserControl : System.Web.UI.UserControl , ACT.Core.Interfaces.Web.I_WebUserControl
//    {
//        public virtual void Show(object[] args)
//        {
            
//        }

//        public event OnComplete OnControlComplete;
//        public event OnError OnControlError;

//        protected virtual void OnErrorHandlerMethod(Exception ex, Dictionary<string,string> e)
//        {
//            if (OnControlError != null)
//            {
//                OnControlError(ex, e);
//            }
//        }
//        protected virtual void OnCompleted(object[] e)
//        {

//            if (OnControlComplete != null)
//            {
//                OnControlComplete(e);
//            }
//        }
//    }
//}
