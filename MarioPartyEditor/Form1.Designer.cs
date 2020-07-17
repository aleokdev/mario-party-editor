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
            System.Windows.Forms.SplitContainer splitContainer1;
            System.Windows.Forms.GroupBox selectedFileInfoGroupBox;
            System.Windows.Forms.SplitContainer splitContainer2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.ToolStripMenuItem loadNDSFileToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem loadNDSProjectToolStripMenuItem;
            System.Windows.Forms.GroupBox sourceControlGroupBox;
            System.Windows.Forms.SplitContainer splitContainer3;
            System.Windows.Forms.GroupBox groupBox1;
            this.extractFileButton = new System.Windows.Forms.Button();
            this.replaceFileButton = new System.Windows.Forms.Button();
            this.selectedFileViewHistoryButton = new System.Windows.Forms.Button();
            this.selectedFileEntryIDLabel = new System.Windows.Forms.Label();
            this.selectedFileSizeLabel = new System.Windows.Forms.Label();
            this.selectedFileFullPathLabel = new System.Windows.Forms.Label();
            this.selectedFileFilenameLabel = new System.Windows.Forms.Label();
            this.vcsChangeListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.vcsCommitButton = new System.Windows.Forms.Button();
            this.vcsUndoButton = new System.Windows.Forms.Button();
            this.vcsHistoryButton = new System.Windows.Forms.Button();
            this.loadTextEditorButton = new System.Windows.Forms.Button();
            this.openWithButton = new System.Windows.Forms.Button();
            this.filesystemView = new System.Windows.Forms.TreeView();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveProjectButton = new System.Windows.Forms.ToolStripButton();
            this.packToROMButton = new System.Windows.Forms.ToolStripButton();
            this.mainContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            selectedFileInfoGroupBox = new System.Windows.Forms.GroupBox();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            loadNDSFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            loadNDSProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            sourceControlGroupBox = new System.Windows.Forms.GroupBox();
            splitContainer3 = new System.Windows.Forms.SplitContainer();
            groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            selectedFileInfoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(splitContainer2)).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            sourceControlGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(splitContainer3)).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).BeginInit();
            this.mainContainer.Panel1.SuspendLayout();
            this.mainContainer.Panel2.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new System.Drawing.Point(5, 134);
            splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(this.extractFileButton);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(this.replaceFileButton);
            splitContainer1.Size = new System.Drawing.Size(312, 28);
            splitContainer1.SplitterDistance = 153;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 6;
            // 
            // extractFileButton
            // 
            this.extractFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extractFileButton.Location = new System.Drawing.Point(0, 0);
            this.extractFileButton.Margin = new System.Windows.Forms.Padding(0);
            this.extractFileButton.Name = "extractFileButton";
            this.extractFileButton.Size = new System.Drawing.Size(153, 28);
            this.extractFileButton.TabIndex = 4;
            this.extractFileButton.Text = "Extract decompressed";
            this.extractFileButton.UseVisualStyleBackColor = true;
            this.extractFileButton.Click += new System.EventHandler(this.extractFileButton_Click);
            // 
            // replaceFileButton
            // 
            this.replaceFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceFileButton.Location = new System.Drawing.Point(0, 0);
            this.replaceFileButton.Margin = new System.Windows.Forms.Padding(0);
            this.replaceFileButton.Name = "replaceFileButton";
            this.replaceFileButton.Size = new System.Drawing.Size(150, 28);
            this.replaceFileButton.TabIndex = 5;
            this.replaceFileButton.Text = "Replace with...";
            this.replaceFileButton.UseVisualStyleBackColor = true;
            this.replaceFileButton.Click += new System.EventHandler(this.replaceFileButton_Click);
            // 
            // selectedFileInfoGroupBox
            // 
            selectedFileInfoGroupBox.Controls.Add(splitContainer2);
            selectedFileInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            selectedFileInfoGroupBox.Location = new System.Drawing.Point(3, 4);
            selectedFileInfoGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            selectedFileInfoGroupBox.Name = "selectedFileInfoGroupBox";
            selectedFileInfoGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            selectedFileInfoGroupBox.Size = new System.Drawing.Size(323, 140);
            selectedFileInfoGroupBox.TabIndex = 7;
            selectedFileInfoGroupBox.TabStop = false;
            selectedFileInfoGroupBox.Text = "Selected File Information";
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new System.Drawing.Point(3, 19);
            splitContainer2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            splitContainer2.Panel2.Controls.Add(this.selectedFileViewHistoryButton);
            splitContainer2.Panel2.Controls.Add(this.selectedFileEntryIDLabel);
            splitContainer2.Panel2.Controls.Add(this.selectedFileSizeLabel);
            splitContainer2.Panel2.Controls.Add(this.selectedFileFullPathLabel);
            splitContainer2.Panel2.Controls.Add(this.selectedFileFilenameLabel);
            splitContainer2.Size = new System.Drawing.Size(317, 117);
            splitContainer2.SplitterDistance = 67;
            splitContainer2.SplitterWidth = 5;
            splitContainer2.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(11, 74);
            label1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(49, 16);
            label1.TabIndex = 3;
            label1.Text = "Entry ID";
            label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(32, 51);
            label4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(28, 16);
            label4.TabIndex = 2;
            label4.Text = "Size";
            // 
            // label3
            // 
            label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(8, 28);
            label3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(54, 16);
            label3.TabIndex = 1;
            label3.Text = "Full Path";
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(7, 4);
            label2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(55, 16);
            label2.TabIndex = 0;
            label2.Text = "Filename";
            // 
            // selectedFileViewHistoryButton
            // 
            this.selectedFileViewHistoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedFileViewHistoryButton.Enabled = false;
            this.selectedFileViewHistoryButton.Location = new System.Drawing.Point(4, 91);
            this.selectedFileViewHistoryButton.Name = "selectedFileViewHistoryButton";
            this.selectedFileViewHistoryButton.Size = new System.Drawing.Size(234, 23);
            this.selectedFileViewHistoryButton.TabIndex = 4;
            this.selectedFileViewHistoryButton.Text = "View History";
            this.selectedFileViewHistoryButton.UseVisualStyleBackColor = true;
            // 
            // selectedFileEntryIDLabel
            // 
            this.selectedFileEntryIDLabel.AutoSize = true;
            this.selectedFileEntryIDLabel.Location = new System.Drawing.Point(3, 74);
            this.selectedFileEntryIDLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.selectedFileEntryIDLabel.Name = "selectedFileEntryIDLabel";
            this.selectedFileEntryIDLabel.Size = new System.Drawing.Size(17, 16);
            this.selectedFileEntryIDLabel.TabIndex = 3;
            this.selectedFileEntryIDLabel.Text = "...";
            // 
            // selectedFileSizeLabel
            // 
            this.selectedFileSizeLabel.AutoSize = true;
            this.selectedFileSizeLabel.Location = new System.Drawing.Point(3, 51);
            this.selectedFileSizeLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.selectedFileSizeLabel.Name = "selectedFileSizeLabel";
            this.selectedFileSizeLabel.Size = new System.Drawing.Size(17, 16);
            this.selectedFileSizeLabel.TabIndex = 2;
            this.selectedFileSizeLabel.Text = "...";
            // 
            // selectedFileFullPathLabel
            // 
            this.selectedFileFullPathLabel.AutoSize = true;
            this.selectedFileFullPathLabel.Location = new System.Drawing.Point(3, 28);
            this.selectedFileFullPathLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.selectedFileFullPathLabel.Name = "selectedFileFullPathLabel";
            this.selectedFileFullPathLabel.Size = new System.Drawing.Size(17, 16);
            this.selectedFileFullPathLabel.TabIndex = 1;
            this.selectedFileFullPathLabel.Text = "...";
            // 
            // selectedFileFilenameLabel
            // 
            this.selectedFileFilenameLabel.AutoSize = true;
            this.selectedFileFilenameLabel.Location = new System.Drawing.Point(3, 4);
            this.selectedFileFilenameLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.selectedFileFilenameLabel.Name = "selectedFileFilenameLabel";
            this.selectedFileFilenameLabel.Size = new System.Drawing.Size(17, 16);
            this.selectedFileFilenameLabel.TabIndex = 0;
            this.selectedFileFilenameLabel.Text = "...";
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
            loadNDSFileToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            loadNDSFileToolStripMenuItem.Text = "Open NDS File...";
            loadNDSFileToolStripMenuItem.Click += new System.EventHandler(this.loadNDSButton_Click);
            // 
            // loadNDSProjectToolStripMenuItem
            // 
            loadNDSProjectToolStripMenuItem.Name = "loadNDSProjectToolStripMenuItem";
            loadNDSProjectToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            loadNDSProjectToolStripMenuItem.Text = "Load NDS Project...";
            loadNDSProjectToolStripMenuItem.Click += new System.EventHandler(this.loadNDSProjectToolStripMenuItem_Click);
            // 
            // sourceControlGroupBox
            // 
            sourceControlGroupBox.Controls.Add(splitContainer3);
            sourceControlGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            sourceControlGroupBox.Location = new System.Drawing.Point(3, 152);
            sourceControlGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            sourceControlGroupBox.Name = "sourceControlGroupBox";
            sourceControlGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            sourceControlGroupBox.Size = new System.Drawing.Size(323, 164);
            sourceControlGroupBox.TabIndex = 10;
            sourceControlGroupBox.TabStop = false;
            sourceControlGroupBox.Text = "Source Control";
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer3.Location = new System.Drawing.Point(3, 19);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(this.vcsChangeListBox);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(this.tableLayoutPanel2);
            splitContainer3.Size = new System.Drawing.Size(317, 141);
            splitContainer3.SplitterDistance = 103;
            splitContainer3.TabIndex = 0;
            // 
            // vcsChangeListBox
            // 
            this.vcsChangeListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vcsChangeListBox.FormattingEnabled = true;
            this.vcsChangeListBox.ItemHeight = 16;
            this.vcsChangeListBox.Location = new System.Drawing.Point(0, 0);
            this.vcsChangeListBox.Name = "vcsChangeListBox";
            this.vcsChangeListBox.Size = new System.Drawing.Size(103, 141);
            this.vcsChangeListBox.TabIndex = 0;
            this.vcsChangeListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.vcsChangeListBox_MouseDoubleClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.vcsCommitButton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.vcsUndoButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.vcsHistoryButton, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(210, 141);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // vcsCommitButton
            // 
            this.vcsCommitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vcsCommitButton.Location = new System.Drawing.Point(3, 97);
            this.vcsCommitButton.Name = "vcsCommitButton";
            this.vcsCommitButton.Size = new System.Drawing.Size(204, 41);
            this.vcsCommitButton.TabIndex = 0;
            this.vcsCommitButton.Text = "Commit";
            this.vcsCommitButton.UseVisualStyleBackColor = true;
            this.vcsCommitButton.Click += new System.EventHandler(this.vcsCommitButton_Click);
            // 
            // vcsUndoButton
            // 
            this.vcsUndoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vcsUndoButton.Enabled = false;
            this.vcsUndoButton.Location = new System.Drawing.Point(3, 50);
            this.vcsUndoButton.Name = "vcsUndoButton";
            this.vcsUndoButton.Size = new System.Drawing.Size(204, 41);
            this.vcsUndoButton.TabIndex = 1;
            this.vcsUndoButton.Text = "Undo All";
            this.vcsUndoButton.UseVisualStyleBackColor = true;
            // 
            // vcsHistoryButton
            // 
            this.vcsHistoryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vcsHistoryButton.Location = new System.Drawing.Point(3, 3);
            this.vcsHistoryButton.Name = "vcsHistoryButton";
            this.vcsHistoryButton.Size = new System.Drawing.Size(204, 41);
            this.vcsHistoryButton.TabIndex = 2;
            this.vcsHistoryButton.Text = "History";
            this.vcsHistoryButton.UseVisualStyleBackColor = true;
            this.vcsHistoryButton.Click += new System.EventHandler(this.vcsHistoryButton_Click);
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.loadTextEditorButton);
            groupBox1.Controls.Add(splitContainer1);
            groupBox1.Controls.Add(this.openWithButton);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(3, 324);
            groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            groupBox1.Size = new System.Drawing.Size(323, 259);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tools And Utils";
            // 
            // loadTextEditorButton
            // 
            this.loadTextEditorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadTextEditorButton.Location = new System.Drawing.Point(5, 23);
            this.loadTextEditorButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loadTextEditorButton.Name = "loadTextEditorButton";
            this.loadTextEditorButton.Size = new System.Drawing.Size(312, 48);
            this.loadTextEditorButton.TabIndex = 0;
            this.loadTextEditorButton.Text = "Load with Text Table Editor";
            this.loadTextEditorButton.UseVisualStyleBackColor = true;
            this.loadTextEditorButton.Click += new System.EventHandler(this.loadTextEditorButton_Click);
            // 
            // openWithButton
            // 
            this.openWithButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openWithButton.Enabled = false;
            this.openWithButton.Location = new System.Drawing.Point(5, 79);
            this.openWithButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.openWithButton.Name = "openWithButton";
            this.openWithButton.Size = new System.Drawing.Size(312, 48);
            this.openWithButton.TabIndex = 3;
            this.openWithButton.Text = "Open With...";
            this.openWithButton.UseVisualStyleBackColor = true;
            this.openWithButton.Click += new System.EventHandler(this.openWithButton_Click);
            // 
            // filesystemView
            // 
            this.filesystemView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesystemView.ImageKey = "Unknown";
            this.filesystemView.ImageList = this.iconList;
            this.filesystemView.Location = new System.Drawing.Point(3, 4);
            this.filesystemView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.filesystemView.Name = "filesystemView";
            this.filesystemView.SelectedImageIndex = 0;
            this.filesystemView.Size = new System.Drawing.Size(615, 585);
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
            this.iconList.Images.SetKeyName(4, "Text Table");
            this.iconList.Images.SetKeyName(5, "Root");
            this.iconList.Images.SetKeyName(6, "Sound Data");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripDropDownButton1,
            this.saveProjectButton,
            this.packToROMButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(995, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // saveProjectButton
            // 
            this.saveProjectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveProjectButton.Enabled = false;
            this.saveProjectButton.Image = ((System.Drawing.Image)(resources.GetObject("saveProjectButton.Image")));
            this.saveProjectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveProjectButton.Name = "saveProjectButton";
            this.saveProjectButton.Size = new System.Drawing.Size(75, 22);
            this.saveProjectButton.Text = "Save Project";
            this.saveProjectButton.Click += new System.EventHandler(this.saveProjectButton_Click);
            // 
            // packToROMButton
            // 
            this.packToROMButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.packToROMButton.Enabled = false;
            this.packToROMButton.Image = ((System.Drawing.Image)(resources.GetObject("packToROMButton.Image")));
            this.packToROMButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.packToROMButton.Name = "packToROMButton";
            this.packToROMButton.Size = new System.Drawing.Size(120, 22);
            this.packToROMButton.Text = "Export Patched ROM";
            this.packToROMButton.Click += new System.EventHandler(this.packToROMButton_Click);
            // 
            // mainContainer
            // 
            this.mainContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainContainer.Location = new System.Drawing.Point(14, 35);
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.mainContainer.Size = new System.Drawing.Size(967, 594);
            this.mainContainer.SplitterDistance = 622;
            this.mainContainer.SplitterWidth = 5;
            this.mainContainer.TabIndex = 3;
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(329, 587);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 644);
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "RaveNDS";
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
            sourceControlGroupBox.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer3)).EndInit();
            splitContainer3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainContainer.Panel1.ResumeLayout(false);
            this.mainContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).EndInit();
            this.mainContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.ListBox vcsChangeListBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button vcsCommitButton;
        private System.Windows.Forms.Button vcsUndoButton;
        private System.Windows.Forms.Button vcsHistoryButton;
        private System.Windows.Forms.Button selectedFileViewHistoryButton;
    }
}

