///-------------------------------------------------------------------------------------------------
// file:	Windows\ACT_Windows_Form_Manager.cs
//
// summary:	Implements the act windows form manager class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if DOTNETFRAMEWORK
using System.Windows.Forms;


namespace ACT.Core.Windows
{
    /// <summary>
    /// Form Manager
    /// </summary>
    public class ACT_Windows_Form_Manager
    {
        bool _ExitApplicationWhenNoVisibleForms = false;
        Action _ExitMethodRef;
        
        Dictionary<string, Form> ActiveForms = new Dictionary<string, Form>();
       
        /// <summary>
        /// 
        /// </summary>
        public ACT_Windows_Form_Manager(bool ExitApplicationWhenNoVisibleForms, ref Action ExitMethod)
        {
            _ExitApplicationWhenNoVisibleForms = ExitApplicationWhenNoVisibleForms;
            _ExitMethodRef = ExitMethod;
        }

        /// <summary>
        /// Show Form
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="frm"></param>
        public void ShowForm(string Name, Form frm)
        {
            if (ActiveForms.ContainsKey(Name))
            {
                ActiveForms[Name].Show();
                ActiveForms[Name].Visible = true;
                ActiveForms[Name].BringToFront();
            }
            else
            {
                ActiveForms.Add(Name, frm);
                ActiveForms[Name].Show();
                ActiveForms[Name].Visible = true;
                ActiveForms[Name].BringToFront();
            }
        }

        /// <summary>
        /// Hide the Form
        /// </summary>
        /// <param name="Name"></param>
        public void HideForm(string Name)
        {
            if (ActiveForms.ContainsKey(Name))
            {
                ActiveForms[Name].Visible = false;
                ActiveForms[Name].Hide();
            }
        }

        /// <summary>
        /// Hide and Remove Form
        /// </summary>
        /// <param name="Name"></param>
        public void HideAndRemove(string Name)
        {
            if (ActiveForms.ContainsKey(Name))
            {
                ActiveForms[Name].Visible = false;
                ActiveForms[Name].Hide();
                ActiveForms[Name].Dispose();

                if (ActiveForms.Count == 0)
                {
                    _ExitMethodRef();
                }
            }
        }

        /// <summary>
        /// Gets the Form and Returns It
        /// </summary>
        /// <param name="Name">Name Of The Form</param>
        /// <returns>Form that Matches Name</returns>
        public Form GetForm(string Name)
        {
            if (ActiveForms.ContainsKey(Name))
            {
                return ActiveForms[Name];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// REturns all the Names of the Forms
        /// </summary>
        /// <returns></returns>
        public List<string> ReturnAllFormNames()
        {
            return ActiveForms.Keys.ToList();
        }

        /// <summary>
        /// Returns all the current visible forms
        /// </summary>
        /// <returns></returns>
        public List<string> ReturnVisibleForms()
        {
            return ActiveForms.Where(x => x.Value.Visible == true).Select(x => x.Key).ToList();
        }
    }
}
#endif