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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.SplitContainer splitContainer1;
            System.Windows.Forms.GroupBox selectedFileInfoGroupBox;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.SplitContainer splitContainer2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
            System.Windows.Forms.ToolStripMenuItem loadNDSFileToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem loadNDSProjectToolStripMenuItem;
            System.Windows.Forms.GroupBox sourceControlGroupBox;
            System.Windows.Forms.GroupBox groupBox1;
            this.filesystemView = new System.Windows.Forms.TreeView();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.packToROMButton = new System.Windows.Forms.ToolStripButton();
            this.mainContainer = new System.Windows.Forms.SplitContainer();
            this.extractFileButton = new System.Windows.Forms.Button();
            this.openWithButton = new System.Windows.Forms.Button();
            this.loadTextEditorButton = new System.Windows.Forms.Button();
            this.saveProjectButton = new System.Windows.Forms.ToolStripButton();
            this.replaceFileButton = new System.Windows.Forms.Button();
            this.selectedFileFilenameLabel = new System.Windows.Forms.Label();
            this.selectedFileFullPathLabel = new System.Windows.Forms.Label();
            this.selectedFileSizeLabel = new System.Windows.Forms.Label();
            this.selectedFileEntryIDLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            selectedFileInfoGroupBox = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            loadNDSFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            loadNDSProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            sourceControlGroupBox = new System.Windows.Forms.GroupBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).BeginInit();
            this.mainContainer.Panel1.SuspendLayout();
            this.mainContainer.Panel2.SuspendLayout();
            this.mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            selectedFileInfoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(splitContainer2)).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.filesystemView.Size = new System.Drawing.Size(494, 411);
            this.filesystemView.TabIndex = 1;
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripDropDownButton1,
            this.saveProjectButton,
            this.packToROMButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // packToROMButton
            // 
            this.packToROMButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.packToROMButton.Image = ((System.Drawing.Image)(resources.GetObject("packToROMButton.Image")));
            this.packToROMButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.packToROMButton.Name = "packToROMButton";
            this.packToROMButton.Size = new System.Drawing.Size(121, 22);
            this.packToROMButton.Text = "Export Patched ROM";
            this.packToROMButton.Click += new System.EventHandler(this.packToROMButton_Click);
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
            this.mainContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.mainContainer.Panel2.Enabled = false;
            this.mainContainer.Size = new System.Drawing.Size(776, 417);
            this.mainContainer.SplitterDistance = 500;
            this.mainContainer.TabIndex = 3;
            // 
            // extractFileButton
            // 
            this.extractFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extractFileButton.Location = new System.Drawing.Point(0, 0);
            this.extractFileButton.Margin = new System.Windows.Forms.Padding(0);
            this.extractFileButton.Name = "extractFileButton";
            this.extractFileButton.Size = new System.Drawing.Size(125, 23);
            this.extractFileButton.TabIndex = 4;
            this.extractFileButton.Text = "Extract decompressed";
            this.extractFileButton.UseVisualStyleBackColor = true;
            this.extractFileButton.Click += new System.EventHandler(this.extractFileButton_Click);
            // 
            // openWithButton
            // 
            this.openWithButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openWithButton.Enabled = false;
            this.openWithButton.Location = new System.Drawing.Point(4, 64);
            this.openWithButton.Name = "openWithButton";
            this.openWithButton.Size = new System.Drawing.Size(250, 39);
            this.openWithButton.TabIndex = 3;
            this.openWithButton.Text = "Open With...";
            this.openWithButton.UseVisualStyleBackColor = true;
            this.openWithButton.Click += new System.EventHandler(this.openWithButton_Click);
            // 
            // loadTextEditorButton
            // 
            this.loadTextEditorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadTextEditorButton.Location = new System.Drawing.Point(4, 19);
            this.loadTextEditorButton.Name = "loadTextEditorButton";
            this.loadTextEditorButton.Size = new System.Drawing.Size(250, 39);
            this.loadTextEditorButton.TabIndex = 0;
            this.loadTextEditorButton.Text = "Load with Text Editor";
            this.loadTextEditorButton.UseVisualStyleBackColor = true;
            this.loadTextEditorButton.Click += new System.EventHandler(this.loadTextEditorButton_Click);
            // 
            // saveProjectButton
            // 
            this.saveProjectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveProjectButton.Image = ((System.Drawing.Image)(resources.GetObject("saveProjectButton.Image")));
            this.saveProjectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveProjectButton.Name = "saveProjectButton";
            this.saveProjectButton.Size = new System.Drawing.Size(75, 22);
            this.saveProjectButton.Text = "Save Project";
            this.saveProjectButton.Click += new System.EventHandler(this.saveProjectButton_Click);
            // 
            // replaceFileButton
            // 
            this.replaceFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceFileButton.Location = new System.Drawing.Point(0, 0);
            this.replaceFileButton.Margin = new System.Windows.Forms.Padding(0);
            this.replaceFileButton.Name = "replaceFileButton";
            this.replaceFileButton.Size = new System.Drawing.Size(121, 23);
            this.replaceFileButton.TabIndex = 5;
            this.replaceFileButton.Text = "Replace with...";
            this.replaceFileButton.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new System.Drawing.Point(4, 109);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(this.extractFileButton);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(this.replaceFileButton);
            splitContainer1.Size = new System.Drawing.Size(250, 23);
            splitContainer1.SplitterDistance = 125;
            splitContainer1.TabIndex = 6;
            // 
            // selectedFileInfoGroupBox
            // 
            selectedFileInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            selectedFileInfoGroupBox.Controls.Add(splitContainer2);
            selectedFileInfoGroupBox.Location = new System.Drawing.Point(3, 3);
            selectedFileInfoGroupBox.Name = "selectedFileInfoGroupBox";
            selectedFileInfoGroupBox.Size = new System.Drawing.Size(260, 113);
            selectedFileInfoGroupBox.TabIndex = 7;
            selectedFileInfoGroupBox.TabStop = false;
            selectedFileInfoGroupBox.Text = "Selected File Information";
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(4, 3);
            label2.Margin = new System.Windows.Forms.Padding(3);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(49, 13);
            label2.TabIndex = 0;
            label2.Text = "Filename";
            // 
            // splitContainer2
            // 
            splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new System.Drawing.Point(4, 19);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(label1);
            splitContainer2.Panel1.Controls.Add(label4);
            splitContainer2.Panel1.Controls.Add(label3);
            splitContainer2.Panel1.Controls.Add(label2);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(this.selectedFileEntryIDLabel);
            splitContainer2.Panel2.Controls.Add(this.selectedFileSizeLabel);
            splitContainer2.Panel2.Controls.Add(this.selectedFileFullPathLabel);
            splitContainer2.Panel2.Controls.Add(this.selectedFileFilenameLabel);
            splitContainer2.Size = new System.Drawing.Size(250, 88);
            splitContainer2.SplitterDistance = 56;
            splitContainer2.TabIndex = 1;
            // 
            // label3
            // 
            label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(5, 22);
            label3.Margin = new System.Windows.Forms.Padding(3);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(48, 13);
            label3.TabIndex = 1;
            label3.Text = "Full Path";
            // 
            // label4
            // 
            label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(26, 41);
            label4.Margin = new System.Windows.Forms.Padding(3);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(27, 13);
            label4.TabIndex = 2;
            label4.Text = "Size";
            // 
            // selectedFileFilenameLabel
            // 
            this.selectedFileFilenameLabel.AutoSize = true;
            this.selectedFileFilenameLabel.Location = new System.Drawing.Point(3, 3);
            this.selectedFileFilenameLabel.Margin = new System.Windows.Forms.Padding(3);
            this.selectedFileFilenameLabel.Name = "selectedFileFilenameLabel";
            this.selectedFileFilenameLabel.Size = new System.Drawing.Size(16, 13);
            this.selectedFileFilenameLabel.TabIndex = 0;
            this.selectedFileFilenameLabel.Text = "...";
            // 
            // selectedFileFullPathLabel
            // 
            this.selectedFileFullPathLabel.AutoSize = true;
            this.selectedFileFullPathLabel.Location = new System.Drawing.Point(3, 22);
            this.selectedFileFullPathLabel.Margin = new System.Windows.Forms.Padding(3);
            this.selectedFileFullPathLabel.Name = "selectedFileFullPathLabel";
            this.selectedFileFullPathLabel.Size = new System.Drawing.Size(16, 13);
            this.selectedFileFullPathLabel.TabIndex = 1;
            this.selectedFileFullPathLabel.Text = "...";
            // 
            // selectedFileSizeLabel
            // 
            this.selectedFileSizeLabel.AutoSize = true;
            this.selectedFileSizeLabel.Location = new System.Drawing.Point(3, 41);
            this.selectedFileSizeLabel.Margin = new System.Windows.Forms.Padding(3);
            this.selectedFileSizeLabel.Name = "selectedFileSizeLabel";
            this.selectedFileSizeLabel.Size = new System.Drawing.Size(16, 13);
            this.selectedFileSizeLabel.TabIndex = 2;
            this.selectedFileSizeLabel.Text = "...";
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 60);
            label1.Margin = new System.Windows.Forms.Padding(3);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(45, 13);
            label1.TabIndex = 3;
            label1.Text = "Entry ID";
            label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // selectedFileEntryIDLabel
            // 
            this.selectedFileEntryIDLabel.AutoSize = true;
            this.selectedFileEntryIDLabel.Location = new System.Drawing.Point(3, 60);
            this.selectedFileEntryIDLabel.Margin = new System.Windows.Forms.Padding(3);
            this.selectedFileEntryIDLabel.Name = "selectedFileEntryIDLabel";
            this.selectedFileEntryIDLabel.Size = new System.Drawing.Size(16, 13);
            this.selectedFileEntryIDLabel.TabIndex = 3;
            this.selectedFileEntryIDLabel.Text = "...";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            loadNDSFileToolStripMenuItem,
            loadNDSProjectToolStripMenuItem});
            toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new System.Drawing.Size(46, 22);
            toolStripDropDownButton1.Text = "Load";
            // 
            // loadNDSFileToolStripMenuItem
            // 
            loadNDSFileToolStripMenuItem.Name = "loadNDSFileToolStripMenuItem";
            loadNDSFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            loadNDSFileToolStripMenuItem.Text = "Open NDS File...";
            loadNDSFileToolStripMenuItem.Click += new System.EventHandler(this.loadNDSButton_Click);
            // 
            // loadNDSProjectToolStripMenuItem
            // 
            loadNDSProjectToolStripMenuItem.Name = "loadNDSProjectToolStripMenuItem";
            loadNDSProjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            loadNDSProjectToolStripMenuItem.Text = "Load NDS Project...";
            loadNDSProjectToolStripMenuItem.Click += new System.EventHandler(this.loadNDSProjectToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(groupBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(sourceControlGroupBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(selectedFileInfoGroupBox, 0, 0);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(266, 411);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // sourceControlGroupBox
            // 
            sourceControlGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            sourceControlGroupBox.Location = new System.Drawing.Point(3, 122);
            sourceControlGroupBox.Name = "sourceControlGroupBox";
            sourceControlGroupBox.Size = new System.Drawing.Size(260, 133);
            sourceControlGroupBox.TabIndex = 10;
            sourceControlGroupBox.TabStop = false;
            sourceControlGroupBox.Text = "Source Control";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Controls.Add(this.loadTextEditorButton);
            groupBox1.Controls.Add(splitContainer1);
            groupBox1.Controls.Add(this.openWithButton);
            groupBox1.Location = new System.Drawing.Point(3, 261);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(260, 140);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tools And Utils";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 457);
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainContainer.Panel1.ResumeLayout(false);
            this.mainContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).EndInit();
            this.mainContainer.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            selectedFileInfoGroupBox.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(splitContainer2)).EndInit();
            splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView filesystemView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer mainContainer;
        private System.Windows.Forms.Button loadTextEditorButton;
        private System.Windows.Forms.Button openWithButton;
        private System.Windows.Forms.Button extractFileButton;
        private System.Windows.Forms.ImageList iconList;
        private System.Windows.Forms.ToolStripButton packToROMButton;
        private System.Windows.Forms.ToolStripButton saveProjectButton;
        private System.Windows.Forms.Button replaceFileButton;
        private System.Windows.Forms.Label selectedFileSizeLabel;
        private System.Windows.Forms.Label selectedFileFullPathLabel;
        private System.Windows.Forms.Label selectedFileFilenameLabel;
        private System.Windows.Forms.Label selectedFileEntryIDLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

