using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using NDSUtils;
using TextTableEditor;

namespace MarioPartyEditor
{
    public partial class Form1 : Form
    {
        private EditorProject _projectEditing;
        public EditorProject ProjectEditing
        {
            get => _projectEditing;
            set
            {
                _projectEditing = value;
                saveProjectButton.Enabled = value != null;
                packToROMButton.Enabled = value != null;
                if (value != null)
                {
                    value.OnROMEditingChange += (_, newROM) => updateFilesystemView(newROM.Filesystem.RootDataDirectory);
                    value.OnHeadCommitChange += (_, __) => updateVCSControls();
                    value.HeadCommit.Changes.CollectionChanged += (_, __) => updateVCSControls();
                }
            }
        }

        public string DefaultDataFolderName => "MarioPartyEditor";
        public NDSFile SelectedFile { get => (NDSFile)filesystemView.SelectedNode.Tag; }

        public Form1()
        {
            InitializeComponent();

            filesystemView.AfterSelect += (_, nodeArgs) =>
            {
                // Update selected file labels
                bool isSelectingFile = SelectedFile != null;
                mainContainer.Panel2.Enabled = isSelectingFile;
                if (isSelectingFile)
                {
                    selectedFileFilenameLabel.Text = SelectedFile.Name;
                    selectedFileFullPathLabel.Text = SelectedFile.FullPath;
                    selectedFileSizeLabel.Text = $"{SelectedFile.LatestVersionSize} Bytes";
                    selectedFileEntryIDLabel.Text = $"0x{SelectedFile.EntryID:X4}";
                }
            };
        }

        #region Project Saving
        private void ShowSaveProjectDialog()
        {
            var pathToSaveToDialog = new SaveFileDialog() { AddExtension = true, DefaultExt = "ndsproj" };
            if (pathToSaveToDialog.ShowDialog() == DialogResult.OK)
            {
                SaveProjectTo(pathToSaveToDialog.FileName);
            }
        }

        private void SaveProjectTo(string path)
        {
            var serializer = new BinaryFormatter();
            using var fileToSaveTo = File.OpenWrite(path);
            serializer.Serialize(fileToSaveTo, _projectEditing);
        }

        private void ShowLoadProjectDialog()
        {
            var pathToLoadFromDialog = new OpenFileDialog() { Filter = "RaveNDS Project (*.ndsproj)|*.ndsproj", CheckFileExists = true };
            if (pathToLoadFromDialog.ShowDialog() == DialogResult.OK)
            {
                LoadProjectFrom(pathToLoadFromDialog.FileName);
            }
        }

        private void LoadProjectFrom(string path)
        {
            var serializer = new BinaryFormatter();
            using var fileToReadFrom = File.OpenRead(path);
            ProjectEditing = (EditorProject)serializer.Deserialize(fileToReadFrom);
            updateFilesystemView(ProjectEditing.ROMEditing.Filesystem.RootDataDirectory);
            updateVCSControls();
        }
        #endregion

        private async void updateFilesystemView(NDSDirectory directoryToView)
        {
            void recurseFiles(TreeNode parentNode, NDSDirectory dir)
            {
                foreach (var childDir in dir.ChildrenDirectories)
                {
                    var dirNode = parentNode.Nodes.Add(childDir.Name);
                    dirNode.ImageKey = "Folder";
                    recurseFiles(dirNode, childDir);
                }

                foreach (var childFile in dir.ChildrenFiles)
                {
                    var childData = childFile.RetrieveLatestVersionData();
                    // TODO: Cache file formats
                    var fileFormatName = (from type in Assembly.GetExecutingAssembly().GetTypes()
                                          let attributes = type.GetCustomAttributes(typeof(FileFormatCheckerAttribute))
                                          where attributes.Count() > 0 && (bool)type.GetMethods().First().Invoke(null, new object[] { childFile })
                                          select ((FileFormatCheckerAttribute)attributes.First()).FormatName).FirstOrDefault() ?? "Unknown";
                    bool isCompressed = LZ77.IsCompressed(childFile);
                    string compressionType = isCompressed ? "LZ77 compressed" : "Uncompressed";

                    var nodeName = $"{childFile.Name} [{compressionType} {fileFormatName}]";
                    TreeNode newNode = parentNode.Nodes.Add(nodeName);
                    if (isCompressed)
                        newNode.ImageKey = "LZ77";
                    else
                        newNode.ImageKey = fileFormatName;
                    newNode.Tag = childFile;
                    newNode.BackColor = isCompressed ? Color.FromArgb(50, 251, 146, 142) : newNode.BackColor;
                }
            }

            var newNodeTree = await Task.Run(() =>
            {
                var newNode = new TreeNode("root");
                newNode.ImageKey = "Root";
                newNode.Expand();
                recurseFiles(newNode, directoryToView);
                return newNode;
            });

            filesystemView.Nodes.Clear();
            filesystemView.Nodes.Add(newNodeTree);
        }

        private void updateVCSControls()
        {
            vcsChangeListBox.Items.Clear();
            foreach (var item in ProjectEditing.HeadCommit.Changes)
                vcsChangeListBox.Items.Add(item);
        }

        private void loadTextEditorButton_Click(object sender, EventArgs e)
        {
            var editor = new TextTableEditor.TextTableEditor(SelectedFile, ProjectEditing.HeadCommit);
            editor.ShowDialog(this);
        }

        private void openWithButton_Click(object sender, EventArgs e)
        {
            // TODO: Reimplement
            /*
            var processInfo = new ProcessStartInfo(SelectedFile);
            processInfo.Verb = "openas";
            Process.Start(processInfo);
            */
        }

        private void loadNDSButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog() { Filter = "Nintendo DS ROMs (*.nds)|*.nds", CheckFileExists = true };

            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                Console.WriteLine($"Loading file {dialog.FileName}...");
                NDSROM rom = new NDSROM();

                using (var romFile = File.OpenRead(dialog.FileName))
                {
                    rom.Data = new ByteSlice(new byte[romFile.Length]);
                    romFile.Read(rom.Data.Source, 0, (int)romFile.Length);
                }

                rom.Filesystem.Initialize();

                ProjectEditing = new EditorProject();

                ProjectEditing.ROMEditing = rom;
            }
        }

        private void packToROMButton_Click(object sender, EventArgs e)
        {
            var newROMFilepathDialog = new SaveFileDialog() { Title = "Save new ROM to...", DefaultExt = "nds", AddExtension = true };
            if (newROMFilepathDialog.ShowDialog() == DialogResult.OK)
            {
                ByteSlice newROMData = new ByteSlice(ProjectEditing.ROMEditing.Data.GetAsArrayCopy());

                // We pack everything back to the ROM with the new data, and we're done.
                ProjectEditing.ROMEditing.Filesystem.PackTo(newROMData);

                var newROMFilepath = newROMFilepathDialog.FileName;
                using (var romFile = File.OpenWrite(newROMFilepath))
                {
                    romFile.Write(newROMData.Source, 0, newROMData.Size);
                }
                Console.WriteLine("Done!!");
            }
        }

        private void saveProjectButton_Click(object sender, EventArgs e) => ShowSaveProjectDialog();

        private void loadNDSProjectToolStripMenuItem_Click(object sender, EventArgs e) => ShowLoadProjectDialog();

        private void extractFileButton_Click(object sender, EventArgs e)
        {
            if (SelectedFile == null) return;

            ByteSlice contents;
            if (LZ77.IsCompressed(SelectedFile))
            {
                contents = new ByteSlice(LZ77.Decompress(new ByteSlice(SelectedFile.RetrieveLatestVersionData())));
            }
            else
            {
                contents = new ByteSlice(SelectedFile.RetrieveLatestVersionData());
            }

            var saveFileDialog = new SaveFileDialog() { FileName = SelectedFile.Name };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using var fileToDecompressTo = File.OpenWrite(saveFileDialog.FileName);
                fileToDecompressTo.Write(contents.Source, 0, contents.Size);
            }
        }

        #region Keybindings
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                ShowSaveProjectDialog();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        private void replaceFileButton_Click(object sender, EventArgs e)
        {
            if (SelectedFile == null) return;

            OpenFileDialog dialog = new OpenFileDialog() { CheckFileExists = true };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var file = File.OpenRead(dialog.FileName);
                var rawContents = new byte[file.Length];
                file.Read(rawContents, 0, (int)file.Length);
                byte[] contents;

                if (LZ77.IsCompressed(SelectedFile))
                {
                    Console.WriteLine("Compressing...");
                    contents = LZ77.Compress(new ByteSlice(rawContents));
                }
                else
                {
                    contents = rawContents;
                }

                SelectedFile.AddNewVersion(ProjectEditing.HeadCommit, contents);
            }
        }

        private void vcsChangeListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void vcsCommitButton_Click(object sender, EventArgs e)
        {
            var createCommitDialog = new CreateCommitForm(ProjectEditing.HeadCommit, CreateCommitForm.CommitFormMode.CreateCommit);

            if (createCommitDialog.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine("Commit Done");
                ProjectEditing.HeadCommit.Time = DateTime.UtcNow;
                ProjectEditing.HeadCommit.Name = createCommitDialog.CommitName;
                ProjectEditing.HeadCommit.Description = createCommitDialog.CommitDescription;
                ProjectEditing.PastCommits.Push(ProjectEditing.HeadCommit);
                ProjectEditing.HeadCommit = new VCSCommit();
            }
        }

        private void vcsHistoryButton_Click(object sender, EventArgs e)
        {
            new CommitHistoryForm(ProjectEditing.PastCommits).ShowDialog();
        }
    }
}
