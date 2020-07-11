using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using NDSUtils;
using TextTableEditor;

namespace MarioPartyEditor
{
    public partial class Form1 : Form
    {
        public string DefaultDataFolderName => "MarioPartyEditor";
        public string SelectedFile { get => (string)filesystemView.SelectedNode.Tag; }

        public Form1()
        {
            InitializeComponent();
            EditorData.OnGamePathChange += (_, pathToLoad) => updateFilesystemView(Path.Combine(pathToLoad, "data"));
            selectedFileLinkText.LinkClicked += (_a, _b) => filesystemView.SelectedNode = (TreeNode)selectedFileLinkText.Tag;
            filesystemView.AfterSelect += (_, nodeArgs) =>
            {
                // Update selected file label
                mainContainer.Panel2.Enabled = filesystemView.SelectedNode != null;
                selectedFileLinkText.Text = nodeArgs.Node.Text;
                selectedFileLinkText.Tag = nodeArgs.Node;
            };
        }

        private void loadGameButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.CheckFileExists = false;
            dialog.Filter = "";

            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                EditorData.GamePath = Path.GetDirectoryName(dialog.FileName);
            }
        }

        private async void updateFilesystemView(string pathToLoad)
        {
            void recurseFiles(TreeNode parentNode, string path)
            {
                foreach (var dir in Directory.EnumerateDirectories(path))
                {
                    var dirNode = parentNode.Nodes.Add(Path.GetFileName(dir));
                    dirNode.ImageKey = "Folder";
                    recurseFiles(dirNode, dir);
                }

                foreach (var file in Directory.EnumerateFiles(path))
                {
                    using var fstream = File.OpenRead(file);
                    // TODO: Cache file formats
                    var fileFormatName = (from type in Assembly.GetExecutingAssembly().GetTypes()
                                          let attributes = type.GetCustomAttributes(typeof(FileFormatCheckerAttribute))
                                          where attributes.Count() > 0 && (bool)type.GetMethods().First().Invoke(null, new object[] { file })
                                          select ((FileFormatCheckerAttribute)attributes.First()).FormatName).FirstOrDefault() ?? "Unknown";
                    bool isCompressed = LZ77.IsCompressed(file);
                    string compressionType = isCompressed ? "LZ77 compressed" : "Uncompressed";
                    // TODO: Move to Util (IsPatched, GetPatchPath, etc)
                    string relativeFilePath = PathHelpers.GetRelativePath(EditorData.GamePath, file);
                    var patchPath = Path.Combine(EditorData.GamePath, "patch", Path.ChangeExtension(relativeFilePath, "xdelta"));
                    bool isPatched = File.Exists(patchPath);

                    var nodeName = $"{Path.GetFileName(file)} [{compressionType} {fileFormatName}]";
                    TreeNode newNode = parentNode.Nodes.Add(nodeName);
                    if (isCompressed)
                        newNode.ImageKey = "LZ77";
                    else
                        newNode.ImageKey = fileFormatName;
                    newNode.Tag = file;
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
                recurseFiles(newNode, pathToLoad);
                return newNode;
            });

            filesystemView.Nodes.Clear();
            filesystemView.Nodes.Add(newNodeTree);
        }

        private void loadTextEditorButton_Click(object sender, EventArgs e)
        {
            string filepathEditing = SelectedFile;
            var editor = new TextTableEditor.TextTableEditor(filepathEditing);
            editor.Show(this);
            editor.FormClosed += (_, __) =>
            {
                var tempNewFilePath = Path.GetTempFileName();
                using (var tempNewFile = File.OpenWrite(tempNewFilePath))
                    tempNewFile.Write(new byte[] { 1, 2, 3, 4, 5, 6 }, 0, 6);
                string relativeEditingPath = PathHelpers.GetRelativePath(EditorData.GamePath, filepathEditing);
                var patchPath = Path.Combine(EditorData.GamePath, "patch", Path.ChangeExtension(relativeEditingPath, "xdelta"));
                Directory.CreateDirectory(Path.GetDirectoryName(patchPath));
                XDelta.CreatePatch(tempNewFilePath, filepathEditing, patchPath);
                updateFilesystemView(Path.Combine(EditorData.GamePath, "data"));
            };
        }

        private void openWithButton_Click(object sender, EventArgs e)
        {
            var processInfo = new ProcessStartInfo(SelectedFile);
            processInfo.Verb = "openas";
            Process.Start(processInfo);
        }

        private void loadNDSButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will create a new folder in the same directory as the NDS file.");
            var dialog = new OpenFileDialog() { Filter = "Nintendo DS ROMs (*.nds)|*.nds" };

            dialog.CheckFileExists = false;
            dialog.Filter = "";

            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                Console.WriteLine($"Loading file {dialog.FileName}...");
                NDSUtils.NDSROM rom = new NDSUtils.NDSROM();

                using (var romFile = File.OpenRead(dialog.FileName))
                {
                    rom.Data = new ByteSlice(new byte[romFile.Length]);
                    romFile.Read(rom.Data.Source, 0, (int)romFile.Length);
                }

                string dataFolderPath = Path.Combine(Path.GetDirectoryName(dialog.FileName), DefaultDataFolderName);
                string ndsFolderPath = Path.Combine(dataFolderPath, rom.Header.GameCode);

                rom.Filesystem.Initialize();

                // Only extract contents of NDS if not extracted before
                if (!Directory.Exists(Path.Combine(ndsFolderPath, "data")))
                {
                    Directory.CreateDirectory(ndsFolderPath);
                    File.Copy(dialog.FileName, Path.Combine(ndsFolderPath, "rom.nds"), true);

                    Console.Write("Loading done. Extracting ");
                    void extractDirectory(NDSDirectory dir)
                    {
                        foreach (var childDirectory in dir.ChildrenDirectories)
                        {
                            extractDirectory(childDirectory);
                        }
                        foreach (var childFile in dir.ChildrenFiles)
                        {
                            var contents = childFile.RetrieveContents();
                            Console.Write($"\r{childFile.FullPath}".PadRight(50) + $"[{contents.Size}B]");
                            var filePath = Path.Combine(ndsFolderPath, childFile.FullPath.TrimStart('/'));
                            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                            using (var file = File.OpenWrite(filePath))
                            {
                                file.Write(contents.GetAsArrayCopy(), 0, contents.Size);
                            }
                        }
                    }

                    extractDirectory(rom.Filesystem.RootDataDirectory);
                    extractDirectory(rom.Filesystem.RootOverlayDirectory);

                    Console.WriteLine("\nDone!");
                }

                EditorData.GamePath = ndsFolderPath;
            }
        }

        private void uncompressLZ77Button_Click(object sender, EventArgs e)
        {
            using (var file = File.OpenRead(SelectedFile))
            {
                var contents = new byte[file.Length];
                file.Read(contents, 0, (int)file.Length);

                string relativeFilePath = PathHelpers.GetRelativePath(EditorData.GamePath, SelectedFile);
                string decompressedPath = Path.Combine(EditorData.GamePath, "decompressed", relativeFilePath);
                Directory.CreateDirectory(Path.GetDirectoryName(decompressedPath));
                using (var decompressedFile = File.OpenWrite(decompressedPath))
                {
                    var decompressedData = LZ77.Decompress(new ByteSlice(contents));
                    decompressedFile.Write(decompressedData, 0, decompressedData.Length);
                }
            }

            updateFilesystemView(Path.Combine(EditorData.GamePath, "data"));
        }

        private void packToROMButton_Click(object sender, EventArgs e)
        {
            // Load the original ROM.
            NDSROM rom = new NDSROM();
            using (var romFile = File.OpenRead(Path.Combine(EditorData.GamePath, "rom.nds")))
            {
                rom.Data = new ByteSlice(new byte[romFile.Length]);
                romFile.Read(rom.Data.Source, 0, (int)romFile.Length);
            }
            rom.Filesystem.Initialize();
            // Replace the files in the original ROM with external ones pointing to the extracted directories.
            // TODO: Patch the files before using them.
            void ReplaceDirectory(NDSDirectory dir)
            {
                foreach (var childDir in dir.ChildrenDirectories)
                    ReplaceDirectory(childDir);

                // We use an index for here instead of a foreach because we are going to modify the children collection with the ReplaceWith calls, which
                // will throw an exception if using a foreach. We aren't modifying the number of files or indices so doing this is OK.
                for (int childFileIndex = 0; childFileIndex < dir.ChildrenFiles.Count; childFileIndex++)
                {
                    var childFile = dir.ChildrenFiles[childFileIndex];
                    string correspondingExternalFile = Path.Combine(EditorData.GamePath, childFile.FullPath.TrimStart('/'));
                    if (!File.Exists(correspondingExternalFile))
                        throw new Exception($"External file {correspondingExternalFile} is missing! Corrupted {DefaultDataFolderName} folder?");

                    childFile.ReplaceWith(new NDSExternalFile(rom.Filesystem, childFile.EntryID, childFile.Name, correspondingExternalFile) { Parent = dir });
                }
            }
            ReplaceDirectory(rom.Filesystem.RootDataDirectory);
            ReplaceDirectory(rom.Filesystem.RootOverlayDirectory);
            Console.WriteLine("Replaced everything! Repacking...");
            // Next, we pack everything back to the ROM with the new external data, and we're done.
            rom.Filesystem.PackToROM();
            using (var romFile = File.OpenWrite(Path.Combine(EditorData.GamePath, "mod.nds")))
            {
                romFile.Write(rom.Data.Source, 0, rom.Data.Size);
            }
            Console.WriteLine("Done!!");
        }
    }
}
