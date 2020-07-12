using System;
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
        public string DefaultDataFolderName => "MarioPartyEditor";
        public NDSFile SelectedFile { get => (NDSFile)filesystemView.SelectedNode.Tag; }

        public Form1()
        {
            InitializeComponent();
            EditorData.OnROMEditingChange += (_, newROM) => updateFilesystemView(newROM.Filesystem.RootDataDirectory);
            selectedFileLinkText.LinkClicked += (_a, _b) => filesystemView.SelectedNode = (TreeNode)selectedFileLinkText.Tag;
            filesystemView.AfterSelect += (_, nodeArgs) =>
            {
                // Update selected file label
                mainContainer.Panel2.Enabled = filesystemView.SelectedNode != null;
                selectedFileLinkText.Text = nodeArgs.Node.Text;
                selectedFileLinkText.Tag = nodeArgs.Node;
            };
        }

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
                    var childData = childFile.RetrieveOriginalContents();
                    // TODO: Cache file formats
                    var fileFormatName = (from type in Assembly.GetExecutingAssembly().GetTypes()
                                          let attributes = type.GetCustomAttributes(typeof(FileFormatCheckerAttribute))
                                          where attributes.Count() > 0 && (bool)type.GetMethods().First().Invoke(null, new object[] { childFile })
                                          select ((FileFormatCheckerAttribute)attributes.First()).FormatName).FirstOrDefault() ?? "Unknown";
                    bool isCompressed = LZ77.IsCompressed(childFile);
                    string compressionType = isCompressed ? "LZ77 compressed" : "Uncompressed";
                    bool isPatched = childFile.Patches.Any();

                    var nodeName = $"{childFile.Name} [{compressionType} {fileFormatName}]";
                    TreeNode newNode = parentNode.Nodes.Add(nodeName);
                    if (isCompressed)
                        newNode.ImageKey = "LZ77";
                    else
                        newNode.ImageKey = fileFormatName;
                    newNode.Tag = childFile;
                    newNode.BackColor = (isPatched, isCompressed) switch
                    {
                        (false, false) => newNode.BackColor,
                        (true, false) => Color.FromArgb(50, 251, 245, 142),
                        (false, true) => Color.FromArgb(50, 251, 146, 142),
                        (true, true) => Color.FromArgb(50, 250, 197, 143)
                    };
                }
            }

            var newNodeTree = await Task.Run(() =>
            {
                var newNode = new TreeNode("root");
                newNode.Expand();
                recurseFiles(newNode, directoryToView);
                return newNode;
            });

            filesystemView.Nodes.Clear();
            filesystemView.Nodes.Add(newNodeTree);
        }

        private void loadTextEditorButton_Click(object sender, EventArgs e)
        {
            var editor = new TextTableEditor.TextTableEditor(SelectedFile);
            editor.Show(this);
            editor.FormClosed += (_, __) =>
            {
                // TODO: Patch
                /*
                var tempNewFilePath = Path.GetTempFileName();
                using (var tempNewFile = File.OpenWrite(tempNewFilePath))
                    tempNewFile.Write(new byte[] { 1, 2, 3, 4, 5, 6 }, 0, 6);
                string relativeEditingPath = PathHelpers.GetRelativePath(EditorData.GamePath, filepathEditing);
                var patchPath = Path.Combine(EditorData.GamePath, "patch", Path.ChangeExtension(relativeEditingPath, "xdelta"));
                Directory.CreateDirectory(Path.GetDirectoryName(patchPath));
                XDelta.CreatePatch(tempNewFilePath, filepathEditing, patchPath);
                updateFilesystemView(Path.Combine(EditorData.GamePath, "data"));
                */
            };
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

                EditorData.ROMEditing = rom;
            }
        }

        private void uncompressLZ77Button_Click(object sender, EventArgs e)
        {
            var contents = SelectedFile.RetrievePatchedContents();

            var saveDialog = new SaveFileDialog() { Title = "Uncompress file to..." };
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string pathToDecompressTo = saveDialog.FileName;
                using (var decompressedFile = File.OpenWrite(pathToDecompressTo))
                {
                    var decompressedData = LZ77.Decompress(contents);
                    decompressedFile.Write(decompressedData, 0, decompressedData.Length);
                }
            }
        }

        private void packToROMButton_Click(object sender, EventArgs e)
        {
            var newROMFilepathDialog = new SaveFileDialog() { Title = "Save new ROM to...", DefaultExt = "nds", AddExtension = true };
            if (newROMFilepathDialog.ShowDialog() == DialogResult.OK)
            {
                ByteSlice newROMData = new ByteSlice(EditorData.ROMEditing.Data.GetAsArrayCopy());

                // We pack everything back to the ROM with the new data, and we're done.
                EditorData.ROMEditing.Filesystem.PackTo(newROMData);

                var newROMFilepath = newROMFilepathDialog.FileName;
                using (var romFile = File.OpenWrite(newROMFilepath))
                {
                    romFile.Write(newROMData.Source, 0, newROMData.Size);
                }
                Console.WriteLine("Done!!");
            }
        }

        private void saveProjectButton_Click(object sender, EventArgs e)
        {
            var serializer = new BinaryFormatter();
            var pathToSaveToDialog = new SaveFileDialog() { AddExtension = true, DefaultExt = "ndsproj" };
            if (pathToSaveToDialog.ShowDialog() == DialogResult.OK)
            {
                var pathToSaveTo = pathToSaveToDialog.FileName;
                using var fileToSaveTo = File.OpenWrite(pathToSaveTo);
                serializer.Serialize(fileToSaveTo, EditorData.ROMEditing);
            }
        }
    }
}
