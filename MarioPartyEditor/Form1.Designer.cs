namespace MarioPartyEditor
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.filesystemView = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.loadGameButton = new System.Windows.Forms.ToolStripButton();
            this.loadNDSButton = new System.Windows.Forms.ToolStripButton();
            this.mainContainer = new System.Windows.Forms.SplitContainer();
            this.openWithButton = new System.Windows.Forms.Button();
            this.selectedFileLinkText = new System.Windows.Forms.LinkLabel();
            this.loadTextEditorButton = new System.Windows.Forms.Button();
            this.uncompressLZ77Button = new System.Windows.Forms.Button();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).BeginInit();
            this.mainContainer.Panel1.SuspendLayout();
            this.mainContainer.Panel2.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(4, 4);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 13);
            label1.TabIndex = 1;
            label1.Text = "Selected:";
            // 
            // filesystemView
            // 
            this.filesystemView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesystemView.ImageKey = "Unknown";
            this.filesystemView.ImageList = this.iconList;
            this.filesystemView.Location = new System.Drawing.Point(3, 3);
            this.filesystemView.Name = "filesystemView";
            this.filesystemView.SelectedImageIndex = 0;
            this.filesystemView.Size = new System.Drawing.Size(494, 404);
            this.filesystemView.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadGameButton,
            this.loadNDSButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // loadGameButton
            // 
            this.loadGameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadGameButton.Image = ((System.Drawing.Image)(resources.GetObject("loadGameButton.Image")));
            this.loadGameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadGameButton.Name = "loadGameButton";
            this.loadGameButton.Size = new System.Drawing.Size(95, 22);
            this.loadGameButton.Text = "Load Filesystem";
            this.loadGameButton.Click += new System.EventHandler(this.loadGameButton_Click);
            // 
            // loadNDSButton
            // 
            this.loadNDSButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadNDSButton.Image = ((System.Drawing.Image)(resources.GetObject("loadNDSButton.Image")));
            this.loadNDSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadNDSButton.Name = "loadNDSButton";
            this.loadNDSButton.Size = new System.Drawing.Size(84, 22);
            this.loadNDSButton.Text = "Load NDS File";
            this.loadNDSButton.Click += new System.EventHandler(this.loadNDSButton_Click);
            // 
            // mainContainer
            // 
            this.mainContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainContainer.Location = new System.Drawing.Point(12, 28);
            this.mainContainer.Name = "mainContainer";
            // 
            // mainContainer.Panel1
            // 
            this.mainContainer.Panel1.Controls.Add(this.filesystemView);
            // 
            // mainContainer.Panel2
            // 
            this.mainContainer.Panel2.Controls.Add(this.uncompressLZ77Button);
            this.mainContainer.Panel2.Controls.Add(this.openWithButton);
            this.mainContainer.Panel2.Controls.Add(this.selectedFileLinkText);
            this.mainContainer.Panel2.Controls.Add(label1);
            this.mainContainer.Panel2.Controls.Add(this.loadTextEditorButton);
            this.mainContainer.Panel2.Enabled = false;
            this.mainContainer.Size = new System.Drawing.Size(776, 410);
            this.mainContainer.SplitterDistance = 500;
            this.mainContainer.TabIndex = 3;
            // 
            // openWithButton
            // 
            this.openWithButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openWithButton.Location = new System.Drawing.Point(3, 94);
            this.openWithButton.Name = "openWithButton";
            this.openWithButton.Size = new System.Drawing.Size(266, 39);
            this.openWithButton.TabIndex = 3;
            this.openWithButton.Text = "Open With...";
            this.openWithButton.UseVisualStyleBackColor = true;
            this.openWithButton.Click += new System.EventHandler(this.openWithButton_Click);
            // 
            // selectedFileLinkText
            // 
            this.selectedFileLinkText.AutoSize = true;
            this.selectedFileLinkText.LinkColor = System.Drawing.Color.Gray;
            this.selectedFileLinkText.Location = new System.Drawing.Point(63, 4);
            this.selectedFileLinkText.Name = "selectedFileLinkText";
            this.selectedFileLinkText.Size = new System.Drawing.Size(33, 13);
            this.selectedFileLinkText.TabIndex = 2;
            this.selectedFileLinkText.TabStop = true;
            this.selectedFileLinkText.Text = "None";
            // 
            // loadTextEditorButton
            // 
            this.loadTextEditorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadTextEditorButton.Location = new System.Drawing.Point(3, 49);
            this.loadTextEditorButton.Name = "loadTextEditorButton";
            this.loadTextEditorButton.Size = new System.Drawing.Size(266, 39);
            this.loadTextEditorButton.TabIndex = 0;
            this.loadTextEditorButton.Text = "Load with Text Editor";
            this.loadTextEditorButton.UseVisualStyleBackColor = true;
            this.loadTextEditorButton.Click += new System.EventHandler(this.loadTextEditorButton_Click);
            // 
            // uncompressLZ77Button
            // 
            this.uncompressLZ77Button.Location = new System.Drawing.Point(3, 140);
            this.uncompressLZ77Button.Name = "uncompressLZ77Button";
            this.uncompressLZ77Button.Size = new System.Drawing.Size(266, 23);
            this.uncompressLZ77Button.TabIndex = 4;
            this.uncompressLZ77Button.Text = "Uncompress LZ77";
            this.uncompressLZ77Button.UseVisualStyleBackColor = true;
            this.uncompressLZ77Button.Click += new System.EventHandler(this.uncompressLZ77Button_Click);
            // 
            // iconList
            // 
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "Text");
            this.iconList.Images.SetKeyName(1, "Folder");
            this.iconList.Images.SetKeyName(2, "Unknown");
            this.iconList.Images.SetKeyName(3, "LZ77");
            this.iconList.Images.SetKeyName(4, "Text Database");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainContainer.Panel1.ResumeLayout(false);
            this.mainContainer.Panel2.ResumeLayout(false);
            this.mainContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).EndInit();
            this.mainContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView filesystemView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton loadGameButton;
        private System.Windows.Forms.SplitContainer mainContainer;
        private System.Windows.Forms.Button loadTextEditorButton;
        private System.Windows.Forms.LinkLabel selectedFileLinkText;
        private System.Windows.Forms.Button openWithButton;
        private System.Windows.Forms.ToolStripButton loadNDSButton;
        private System.Windows.Forms.Button uncompressLZ77Button;
        private System.Windows.Forms.ImageList iconList;
    }
}

