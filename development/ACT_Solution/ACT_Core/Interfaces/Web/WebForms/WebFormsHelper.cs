///-------------------------------------------------------------------------------------------------
// file:	Web\WebForms\WebFormsHelper.cs
//
// summary:	Implements the web forms helper class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if DOTNETFRAMEWORK
using System.Web.UI;
#endif

namespace ACT.Core.Web.WebForms
{
    public static class WebFormsHelper
    {

        //public static T FindControl<T>(string ID, Page PageToCheck)
        //{
        //    object _TmpReturn = null;

        //    if (PageToCheck.Master == null)
        //    {

        //    }
        //    else if (PageToCheck.Master.Master == null)
        //    {

        //    }
        //    else if (PageToCheck.Master.Master.Master == null)
        //    {

        //    }
        //    return (T)_TmpReturn;
        //}
                
        ///// <summary>
        ///// Finds all controls of type T stores them in FoundControls
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        //private class ControlFinder<T> where T : Control
        //{
        //    private readonly List<T> _foundControls = new List<T>();
        //    public IEnumerable<T> FoundControls
        //    {
        //        get { return _foundControls; }
        //    }

        //    public void FindChildControlsRecursive(Control control)
        //    {
        //        foreach (Control childControl in control.Controls)
        //        {
        //            if (childControl.GetType() == typeof(T))
        //            {
        //                _foundControls.Add((T)childControl);
        //            }
        //            else
        //            {
        //                FindChildControlsRecursive(childControl);
        //            }
        //        }
        //    }
        //}
        //public static MasterPage GetTopLevelMasterPage()
    }
}
