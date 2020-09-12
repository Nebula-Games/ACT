///-------------------------------------------------------------------------------------------------
// file:	Windows\Controls\Listbox.cs
//
// summary:	Implements the listbox class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Windows.Controls
{
#if DOTNETFRAMEWORK
    public static class Listbox
    {
        public static void MoveUp(System.Windows.Forms.ListBox listBox1) { MoveItem(listBox1, -1); }
        public static void MoveDown(System.Windows.Forms.ListBox listBox1) { MoveItem(listBox1, 1); }
        public static void MoveItem(System.Windows.Forms.ListBox listBox1, int direction)
        {
            // Checking selected item
            // No selected item - nothing to do 
            if (listBox1.SelectedItem == null || listBox1.SelectedIndex < 0) { return; }

            // Calculate new index using move direction
            int newIndex = listBox1.SelectedIndex + direction;

            // Checking bounds of the range
            // Index out of range - nothing to do
            if (newIndex < 0 || newIndex >= listBox1.Items.Count) { return; }

            object selected = listBox1.SelectedItem;
            // Removing removable element
            listBox1.Items.Remove(selected);
            // Insert it in new position
            listBox1.Items.Insert(newIndex, selected);
            // Restore selection
            listBox1.SetSelected(newIndex, true);
        }
    }
#endif
}
