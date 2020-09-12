///-------------------------------------------------------------------------------------------------
// file:	Windows\FormManager.cs
//
// summary:	Implements the form manager class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if DOTNETFRAMEWORK
using System.Windows.Forms;
#endif
using ACT.Core.Extensions;

namespace ACT.Core.Windows
{
    #if DOTNETFRAMEWORK
    /// <summary>
    /// Form Manager
    /// </summary>
    public class FormManager
    {
        private bool _Formless = false;

        private Form _MainForm = null;

        /// <summary>
        /// Form Manager
        /// </summary>
        /// <param name="Formless">Formless Means ApplicationContext</param>
        public FormManager(bool Formless)
        {
            _Formless = Formless;
        }

        /// <summary>
        /// Main Form Constructor
        /// </summary>
        /// <param name="MainForm"></param>
        public FormManager(Form MainForm)
        {
            _MainForm = MainForm;
        }

        /// <summary>
        /// Exists the applications
        /// </summary>
        public void CloseApplication()
        {
            if (_MainForm != null)
            {
                _MainForm.Close();
                _MainForm.Dispose();
                Application.Exit();
            }
        }

        /// <summary>
        /// Loaded Forms
        /// </summary>
        public Dictionary<string, (Form frm, bool vis)> LoadedForms = new Dictionary<string, (Form,bool)>();


        /// <summary>
        /// Show a Form and Add it to Loaded Forms
        /// </summary>
        /// <param name="FormName"></param>
        /// <param name="FormToShow"></param>
        /// <param name="AddToCache"></param>
        public bool ShowForm(string FormName, Form FormToShow = null, bool AddToCache = true)
        {
        
            if (LoadedForms.ContainsKey(FormName) == true)
            {
                if (LoadedForms[FormName].vis) { LoadedForms[FormName].frm.BringToFront(); return true; }
                else
                {
                    LoadedForms[FormName].frm.Show();
                    LoadedForms[FormName].frm.BringToFront();
                    return true;
                }
            }
            else
            {
                if (FormToShow == null) { return false; }
                LoadedForms.Add(FormName, (FormToShow, true));
                FormToShow.Show();
                FormToShow.BringToFront();

                return true;
            }
        }

        /// <summary>
        /// Closes a Form and Removed From Cache
        /// </summary>
        /// <param name="FormName"></param>
        /// <returns></returns>
        public bool CloseForm(string FormName)
        {
            if (LoadedForms.ContainsKey(FormName) == false) { return false; }
            else
            {
                LoadedForms[FormName].frm.Hide();
                LoadedForms[FormName].frm.Close();
                LoadedForms[FormName].frm.Dispose();
                LoadedForms.Remove(FormName);
                return true;
            }
        }

        /// <summary>
        /// Hides a Form 
        /// </summary>
        /// <param name="FormName"></param>
        /// <returns></returns>
        public bool HideForm(string FormName)
        {
            if (LoadedForms.ContainsKey(FormName) == false) { return false; }
            else
            {
                LoadedForms[FormName].frm.Hide();
                LoadedForms[FormName] = (LoadedForms[FormName].frm, false);
                return true;
            }
        }
    }
#endif
}
