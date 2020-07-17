namespace MarioPartyEditor
{
    partial class CommitHistoryForm
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
            this.commitListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // commitListBox
            // 
            this.commitListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commitListBox.FormattingEnabled = true;
            this.commitListBox.Location = new System.Drawing.Point(0, 0);
            this.commitListBox.Name = "commitListBox";
            this.commitListBox.Size = new System.Drawing.Size(800, 450);
            this.commitListBox.TabIndex = 0;
            this.commitListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.commitListBox_MouseDoubleClick);
            // 
            // CommitHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.commitListBox);
            this.Name = "CommitHistoryForm";
            this.Text = "Commit History";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox commitListBox;
    }
}