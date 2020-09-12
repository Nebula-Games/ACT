namespace ACT.Core.Windows
{
    #if DOTNETFRAMEWORK
    partial class Dynamic_Selector
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

#region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TitleLabel = new System.Windows.Forms.Label();
            this.SelectableLabel = new System.Windows.Forms.Label();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.SelectableItems = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(0, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(554, 40);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "label1";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SelectableLabel
            // 
            this.SelectableLabel.AutoSize = true;
            this.SelectableLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectableLabel.Location = new System.Drawing.Point(12, 52);
            this.SelectableLabel.Name = "SelectableLabel";
            this.SelectableLabel.Size = new System.Drawing.Size(220, 28);
            this.SelectableLabel.TabIndex = 1;
            this.SelectableLabel.Text = "Selectable Label";
            // 
            // ContinueButton
            // 
            this.ContinueButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContinueButton.Location = new System.Drawing.Point(17, 331);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(525, 51);
            this.ContinueButton.TabIndex = 3;
            this.ContinueButton.Text = "Select && Continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // SelectableItems
            // 
            this.SelectableItems.CheckOnClick = true;
            this.SelectableItems.FormattingEnabled = true;
            this.SelectableItems.Location = new System.Drawing.Point(17, 83);
            this.SelectableItems.Name = "SelectableItems";
            this.SelectableItems.Size = new System.Drawing.Size(525, 235);
            this.SelectableItems.TabIndex = 4;
            this.SelectableItems.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SelectableItems_ItemCheck);
            // 
            // Dynamic_Selector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 394);
            this.Controls.Add(this.SelectableItems);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.SelectableLabel);
            this.Controls.Add(this.TitleLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dynamic_Selector";
            this.Text = "Dynamic_Selector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

#endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label SelectableLabel;
        private System.Windows.Forms.Button ContinueButton;
        public System.Windows.Forms.CheckedListBox SelectableItems;
    }
#endif
}