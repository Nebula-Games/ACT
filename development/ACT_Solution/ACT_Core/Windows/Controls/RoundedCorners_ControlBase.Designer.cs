namespace ACT.Core.Windows.Controls
{
#if DOTNETFRAMEWORK
    partial class RoundedCorners_ControlBase
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CloseButton_Panel = new System.Windows.Forms.Panel();
            this.MainDrawingPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // CloseButton_Panel
            // 
            this.CloseButton_Panel.Location = new System.Drawing.Point(449, 3);
            this.CloseButton_Panel.Name = "CloseButton_Panel";
            this.CloseButton_Panel.Size = new System.Drawing.Size(25, 25);
            this.CloseButton_Panel.TabIndex = 0;
            this.CloseButton_Panel.Click += new System.EventHandler(this.CloseButton_Panel_Click);
            this.CloseButton_Panel.MouseEnter += new System.EventHandler(this.CloseButton_Panel_MouseEnter);
            this.CloseButton_Panel.MouseLeave += new System.EventHandler(this.CloseButton_Panel_MouseLeave);
            this.CloseButton_Panel.MouseHover += new System.EventHandler(this.CloseButton_Panel_MouseHover);
            // 
            // MainDrawingPanel
            // 
            this.MainDrawingPanel.Location = new System.Drawing.Point(199, 126);
            this.MainDrawingPanel.Name = "MainDrawingPanel";
            this.MainDrawingPanel.Padding = new System.Windows.Forms.Padding(0, 25, 0, 25);
            this.MainDrawingPanel.Size = new System.Drawing.Size(200, 100);
            this.MainDrawingPanel.TabIndex = 1;
            // 
            // RoundedCorners_ControlBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainDrawingPanel);
            this.Controls.Add(this.CloseButton_Panel);
            this.Name = "RoundedCorners_ControlBase";
            this.Size = new System.Drawing.Size(500, 300);


            this.ResumeLayout(false);

        }

    #endregion

        private System.Windows.Forms.Panel CloseButton_Panel;
        private System.Windows.Forms.Panel MainDrawingPanel;
    }
#endif
}
