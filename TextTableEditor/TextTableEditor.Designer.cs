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
            System.Windows.Forms.Label label1;
            this.textListBox = new System.Windows.Forms.ListBox();
            this.fileSizeLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textListBox
            // 
            this.textListBox.FormattingEnabled = true;
            this.textListBox.Location = new System.Drawing.Point(13, 13);
            this.textListBox.Name = "textListBox";
            this.textListBox.Size = new System.Drawing.Size(775, 407);
            this.textListBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(647, 428);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(30, 13);
            label1.TabIndex = 1;
            label1.Text = "Size:";
            // 
            // fileSizeLabel
            // 
            this.fileSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fileSizeLabel.AutoSize = true;
            this.fileSizeLabel.Location = new System.Drawing.Point(683, 428);
            this.fileSizeLabel.Name = "fileSizeLabel";
            this.fileSizeLabel.Size = new System.Drawing.Size(54, 13);
            this.fileSizeLabel.TabIndex = 2;
            this.fileSizeLabel.Text = "Loading...";
            // 
            // TextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.fileSizeLabel);
            this.Controls.Add(label1);
            this.Controls.Add(this.textListBox);
            this.Name = "TextEditor";
            this.Text = "TextEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox textListBox;
        private System.Windows.Forms.Label fileSizeLabel;
    }
}