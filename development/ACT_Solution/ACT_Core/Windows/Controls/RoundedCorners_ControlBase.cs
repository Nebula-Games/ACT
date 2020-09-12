///-------------------------------------------------------------------------------------------------
// file:	Windows\Controls\RoundedCorners_ControlBase.cs
//
// summary:	Implements the rounded corners control base class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if DOTNETFRAMEWORK
using System.Windows.Forms;
#endif
using System.Drawing.Drawing2D;

namespace ACT.Core.Windows.Controls
{
    #if DOTNETFRAMEWORK
    [ToolboxItem(true)]
    public partial class RoundedCorners_ControlBase : UserControl
    {
        private int radius = 20;
        private Cursor _OriginalCursor = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public RoundedCorners_ControlBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Radius
        /// </summary>
        [DefaultValue(20)]
        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                this.RecreateRegion();
            }
        }

        private GraphicsPath GetRoundRectagle(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius,
                        radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }
        private void RecreateRegion()
        {
            var bounds = new Rectangle(this.ClientRectangle.Location, this.ClientRectangle.Size);
            bounds.Inflate(-1, -1);
            using (var path = GetRoundRectagle(bounds, this.Radius))
                this.Region = new Region(path);
            this.Invalidate();
        }

        /// <summary>
        /// Draw the Round Corners
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.RecreateRegion();
            MainDrawingPanel.Height = this.Height - 52;
            MainDrawingPanel.Width = this.Width - 10;
            MainDrawingPanel.Left = 5;
            MainDrawingPanel.Top = 26;

        }
        
        private void CloseButton_Panel_MouseEnter(object sender, EventArgs e)
        {
            if (_OriginalCursor == null) { _OriginalCursor= Cursor.Current; }
            Cursor = Cursors.Hand;
        }

        private void CloseButton_Panel_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void CloseButton_Panel_MouseLeave(object sender, EventArgs e)
        {
            if (_OriginalCursor != null) { Cursor = _OriginalCursor; }
            else { Cursor = Cursors.Default; }
        }

        private void CloseButton_Panel_Click(object sender, EventArgs e)
        {

        }
    }
#endif
}
