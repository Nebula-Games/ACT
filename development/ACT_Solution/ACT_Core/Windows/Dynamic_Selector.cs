///-------------------------------------------------------------------------------------------------
// file:	Windows\Dynamic_Selector.cs
//
// summary:	Implements the dynamic selector class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class Dynamic_Selector : Form
    {
        public bool OneItemOnly = false;
        public Dynamic_Selector()
        {
            InitializeComponent();

            this.FormClosing += Dynamic_Selector_FormClosing;
        }

        private void Dynamic_Selector_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Show the Dialog Box to select from a list of items.
        /// </summary>
        /// <param name="Parent"></param>
        /// <param name="FormTitle"></param>
        /// <param name="TitleMSG"></param>
        /// <param name="ItemListLabel"></param>
        /// <param name="ItemsToAdd"></param>
        /// <param name="ButtonText"></param>
        /// <returns></returns>
        public static List<string> Show(Form Parent, string FormTitle, string TitleMSG, string ItemListLabel, List<string> ItemsToAdd, string ButtonText = "", bool AllowOnlyOne = false)
        {
            
            Dynamic_Selector _tmpForm = new Dynamic_Selector();
            _tmpForm.OneItemOnly = AllowOnlyOne;
            _tmpForm.SelectableLabel.Text = ItemListLabel;
            _tmpForm.TitleLabel.Text = TitleMSG;
            _tmpForm.Text = FormTitle;
            _tmpForm.SelectableItems.Items.AddRange(ItemsToAdd.ToArray<object>());
            if (ButtonText.NullOrEmpty()) { _tmpForm.ContinueButton.Text = ButtonText; }
            var _DResult = _tmpForm.ShowDialog(Parent);

            if (_DResult== DialogResult.Cancel) { return new List<string>(); }
            else
            {
                List<string> _tmpReturn = new List<string>();
                foreach(var sI in _tmpForm.SelectableItems.SelectedItems)
                {
                    _tmpReturn.Add(sI.ToString());
                }
                return _tmpReturn;
            }
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void SelectableItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (OneItemOnly)
            {
                if(SelectableItems.SelectedItems.Count > 0)
                {
                    e.NewValue = CheckState.Unchecked;
                }
            }
        }
    }
#endif
}
