namespace ACT.Core.Windows.Controls
{
#if DOTNETFRAMEWORK
    partial class GenericLoadingControl
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
            this.MainProgressBar = new System.Windows.Forms.ProgressBar();
            this.FinalizeLoadingScreenButton = new System.Windows.Forms.Button();
            this.MainLabel = new System.Windows.Forms.Label();
            this.DebugTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MainProgressBar
            // 
            this.MainProgressBar.Location = new System.Drawing.Point(12, 50);
            this.MainProgressBar.Name = "MainProgressBar";
            this.MainProgressBar.Size = new System.Drawing.Size(392, 29);
            this.MainProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.MainProgressBar.TabIndex = 0;
            // 
            // FinalizeLoadingScreenButton
            // 
            this.FinalizeLoadingScreenButton.CausesValidation = false;
            this.FinalizeLoadingScreenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FinalizeLoadingScreenButton.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalizeLoadingScreenButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.FinalizeLoadingScreenButton.Location = new System.Drawing.Point(12, 234);
            this.FinalizeLoadingScreenButton.Name = "FinalizeLoadingScreenButton";
            this.FinalizeLoadingScreenButton.Size = new System.Drawing.Size(392, 37);
            this.FinalizeLoadingScreenButton.TabIndex = 1;
            this.FinalizeLoadingScreenButton.Text = "Loading Completed";
            this.FinalizeLoadingScreenButton.UseVisualStyleBackColor = false;
            // 
            // MainLabel
            // 
            this.MainLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MainLabel.Location = new System.Drawing.Point(8, 5);
            this.MainLabel.Name = "MainLabel";
            this.MainLabel.Size = new System.Drawing.Size(396, 32);
            this.MainLabel.TabIndex = 2;
            this.MainLabel.Text = "Loading Screen Label";
            this.MainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DebugTextBox
            // 
            this.DebugTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DebugTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.DebugTextBox.Location = new System.Drawing.Point(12, 85);
            this.DebugTextBox.Multiline = true;
            this.DebugTextBox.Name = "DebugTextBox";
            this.DebugTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DebugTextBox.Size = new System.Drawing.Size(392, 143);
            this.DebugTextBox.TabIndex = 3;
            this.DebugTextBox.Text = "Loading Messages";
            // 
            // GenericLoadingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.DebugTextBox);
            this.Controls.Add(this.MainLabel);
            this.Controls.Add(this.FinalizeLoadingScreenButton);
            this.Controls.Add(this.MainProgressBar);
            this.DoubleBuffered = true;
            this.Name = "GenericLoadingControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(412, 279);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    #endregion

        private System.Windows.Forms.ProgressBar MainProgressBar;
        private System.Windows.Forms.Button FinalizeLoadingScreenButton;
        private System.Windows.Forms.Label MainLabel;
        private System.Windows.Forms.TextBox DebugTextBox;
    }
#endif
}
