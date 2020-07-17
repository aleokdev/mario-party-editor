namespace MarioPartyEditor
{
    partial class CreateCommitForm
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
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label label2;
            this.changedFilesListBox = new System.Windows.Forms.ListBox();
            this.commitNameTextBox = new System.Windows.Forms.TextBox();
            this.commitTimeLabel = new System.Windows.Forms.Label();
            this.commitDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.createCommitButton = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.changedFilesListBox);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(312, 431);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Changed Files";
            // 
            // changedFilesListBox
            // 
            this.changedFilesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changedFilesListBox.FormattingEnabled = true;
            this.changedFilesListBox.Location = new System.Drawing.Point(3, 16);
            this.changedFilesListBox.Name = "changedFilesListBox";
            this.changedFilesListBox.Size = new System.Drawing.Size(306, 412);
            this.changedFilesListBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            groupBox2.Controls.Add(this.commitNameTextBox);
            groupBox2.Location = new System.Drawing.Point(318, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(419, 48);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Commit Name";
            // 
            // commitNameTextBox
            // 
            this.commitNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commitNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commitNameTextBox.Location = new System.Drawing.Point(3, 16);
            this.commitNameTextBox.Name = "commitNameTextBox";
            this.commitNameTextBox.Size = new System.Drawing.Size(413, 26);
            this.commitNameTextBox.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(318, 72);
            label2.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(97, 13);
            label2.TabIndex = 3;
            label2.Text = "Commit Description";
            // 
            // commitTimeLabel
            // 
            this.commitTimeLabel.AutoSize = true;
            this.commitTimeLabel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.commitTimeLabel.Location = new System.Drawing.Point(318, 51);
            this.commitTimeLabel.Name = "commitTimeLabel";
            this.commitTimeLabel.Size = new System.Drawing.Size(92, 13);
            this.commitTimeLabel.TabIndex = 2;
            this.commitTimeLabel.Text = "Not yet committed";
            // 
            // commitDescriptionTextBox
            // 
            this.commitDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commitDescriptionTextBox.Location = new System.Drawing.Point(321, 91);
            this.commitDescriptionTextBox.Multiline = true;
            this.commitDescriptionTextBox.Name = "commitDescriptionTextBox";
            this.commitDescriptionTextBox.Size = new System.Drawing.Size(416, 308);
            this.commitDescriptionTextBox.TabIndex = 4;
            // 
            // createCommitButton
            // 
            this.createCommitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createCommitButton.Location = new System.Drawing.Point(569, 405);
            this.createCommitButton.Name = "createCommitButton";
            this.createCommitButton.Size = new System.Drawing.Size(168, 23);
            this.createCommitButton.TabIndex = 5;
            this.createCommitButton.Text = "Accept And Commit";
            this.createCommitButton.UseVisualStyleBackColor = true;
            this.createCommitButton.Click += new System.EventHandler(this.createCommitButton_Click);
            // 
            // CreateCommitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 431);
            this.Controls.Add(this.createCommitButton);
            this.Controls.Add(this.commitDescriptionTextBox);
            this.Controls.Add(label2);
            this.Controls.Add(this.commitTimeLabel);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.Name = "CreateCommitForm";
            this.Text = "Create New Commit...";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox changedFilesListBox;
        private System.Windows.Forms.TextBox commitNameTextBox;
        private System.Windows.Forms.Label commitTimeLabel;
        private System.Windows.Forms.TextBox commitDescriptionTextBox;
        private System.Windows.Forms.Button createCommitButton;
    }
}