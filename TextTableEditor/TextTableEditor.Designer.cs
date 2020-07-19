namespace TextTableEditor
{
    partial class TextTableEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextTableEditor));
            this.textListBox = new System.Windows.Forms.ListBox();
            this.fileSizeLabel = new System.Windows.Forms.Label();
            this.saveAndCloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textListBox
            // 
            this.textListBox.FormattingEnabled = true;
            this.textListBox.Location = new System.Drawing.Point(13, 13);
            this.textListBox.Name = "textListBox";
            this.textListBox.Size = new System.Drawing.Size(775, 407);
            this.textListBox.TabIndex = 0;
            this.textListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textListBox_MouseDoubleClick);
            // 
            // fileSizeLabel
            // 
            this.fileSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fileSizeLabel.AutoSize = true;
            this.fileSizeLabel.Location = new System.Drawing.Point(12, 423);
            this.fileSizeLabel.Name = "fileSizeLabel";
            this.fileSizeLabel.Size = new System.Drawing.Size(54, 13);
            this.fileSizeLabel.TabIndex = 2;
            this.fileSizeLabel.Text = "Loading...";
            // 
            // saveAndCloseButton
            // 
            this.saveAndCloseButton.Location = new System.Drawing.Point(688, 426);
            this.saveAndCloseButton.Name = "saveAndCloseButton";
            this.saveAndCloseButton.Size = new System.Drawing.Size(100, 23);
            this.saveAndCloseButton.TabIndex = 3;
            this.saveAndCloseButton.Text = "Save And Close";
            this.saveAndCloseButton.UseVisualStyleBackColor = true;
            this.saveAndCloseButton.Click += new System.EventHandler(this.saveAndCloseButton_Click);
            // 
            // TextTableEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.saveAndCloseButton);
            this.Controls.Add(this.fileSizeLabel);
            this.Controls.Add(this.textListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextTableEditor";
            this.Text = "TextEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox textListBox;
        private System.Windows.Forms.Label fileSizeLabel;
        private System.Windows.Forms.Button saveAndCloseButton;
    }
}