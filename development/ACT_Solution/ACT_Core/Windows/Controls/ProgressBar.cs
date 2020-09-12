///-------------------------------------------------------------------------------------------------
// file:	Windows\Controls\ProgressBar.cs
//
// summary:	Implements the progress bar class
///-------------------------------------------------------------------------------------------------

namespace ACT.Core.Windows.Controls
{
#if DOTNETFRAMEWORK
    /// <summary>
    /// ACT Progress Bar Control
    /// </summary>
    public class ProgressBar : System.Windows.Forms.UserControl
    {
        private System.Windows.Forms.Label CaptionLabel;
        System.Windows.Forms.ProgressBar _PB = new System.Windows.Forms.ProgressBar();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="r"></param>
        /// <param name="CenterInParent"></param>
        public ProgressBar(System.Windows.Forms.Form parent, System.Drawing.Rectangle? r = null, bool CenterInParent = true) : base()
        {
            if (r == null)
            {
                this.Width = 400;
                this.Height = 250;
            }
            else
            {
                this.Width = r.Value.Width;
                this.Height = r.Value.Height;
            }

            parent.Controls.Add(this);
            this.Left = (parent.Width) / 2 - (this.Width / 2);
            this.Top = (parent.Height) / 2 - (this.Height / 2);
            this.Visible = false;


            _PB.Width = (int)(this.Width * .75);
            _PB.Height = 50;
            _PB.Left = (this.Width) / 2 - (_PB.Width / 2);
            _PB.Top = (this.Height) / 2 - (_PB.Height / 2);
            this.Controls.Add(_PB);
        }

        /// <summary>
        /// Show Controls
        /// </summary>
        public void Show(int MaxProgressValue, string Caption = "Task Progress")
        {
            this.Visible = true;
            _PB.Maximum = MaxProgressValue;
            _PB.Minimum = 0;
            _PB.Value = 0;
            this.BringToFront();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amt"></param>
        public int Increment
        {
            set
            {
                _PB.Increment(value);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void InitializeComponent()
        {
            this.CaptionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.CaptionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CaptionLabel.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.CaptionLabel.Location = new System.Drawing.Point(0, 0);
            this.CaptionLabel.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.CaptionLabel.Name = "label1";
            this.CaptionLabel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.CaptionLabel.Size = new System.Drawing.Size(400, 45);
            this.CaptionLabel.TabIndex = 0;
            this.CaptionLabel.Text = "label1";
            this.CaptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressBar
            // 
            this.Controls.Add(this.CaptionLabel);
            this.Name = "ProgressBar";
            this.Size = new System.Drawing.Size(400, 250);

            this.ResumeLayout(false);

        }
    }
#endif
}
