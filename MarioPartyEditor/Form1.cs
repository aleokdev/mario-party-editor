﻿using MarioPartyEditor.ROM;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarioPartyEditor
{
    public partial class Form1 : Form
    {
        public string DefaultDataFolderName => "MarioPartyEditor";
        public string SelectedFile { get => (string)filesystemView.SelectedNode.Tag; }

        public Form1()
        {
            InitializeComponent();
            EditorData.OnGamePathChange += (_, pathToLoad) => updateFilesystemView(pathToLoad);
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
                    string compressionType = Util.LZ77.IsCompressed(file) ? "LZ77 compressed" : "Uncompressed";
                    // TODO: Move to Util (IsPatched, GetPatchPath, etc)
                    string relativeFilePath =
                Uri.UnescapeDataString(
                    new Uri(EditorData.GamePath).MakeRelativeUri(new Uri(file))
                        .ToString()
                        .Replace('/', Path.DirectorySeparatorChar)
                    );
                    var patchPath = Path.Combine(EditorData.GamePath, "patch", Path.ChangeExtension(relativeFilePath, "xdelta"));
                    bool isPatched = File.Exists(patchPath);

                    var nodeName = $"{Path.GetFileName(file)} [{compressionType} {fileFormatName}]";
                    TreeNode newNode = parentNode.Nodes.Add(nodeName);
                    newNode.Tag = file;
                    if (isPatched)
                        newNode.BackColor = Color.PaleVioletRed;
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
            var editor = new TextEditor(SelectedFile);
            editor.Show(this);
            editor.FormClosed += (_, __) => updateFilesystemView(EditorData.GamePath);
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
                NDSROM rom = new NDSROM();

                using (var romFile = File.OpenRead(dialog.FileName))
                {
                    rom.Data = new Util.ByteSlice(new byte[romFile.Length], rom.Data.SliceStart, rom.Data.SliceEnd);
                    romFile.Read(rom.Data.Source, 0, (int)romFile.Length);
                }

                string dataFolderPath = Path.Combine(Path.GetDirectoryName(dialog.FileName), DefaultDataFolderName);
                string ndsFolderPath = Path.Combine(dataFolderPath, rom.Header.GameCode);

                // Only extract contents of NDS if not extracted before
                if(!Directory.Exists(ndsFolderPath))
                {
                    Directory.CreateDirectory(ndsFolderPath);

                    rom.Filesystem.Initialize();
                    Console.WriteLine("Loading done.");
                    void extractDirectory(NDSDirectory dir)
                    {
                        foreach (var childDirectory in dir.ChildrenDirectories)
                        {
                            extractDirectory(childDirectory);
                        }
                        foreach (var childFile in dir.ChildrenFiles)
                        {
                            var filePath = Path.Combine(ndsFolderPath, childFile.FullPath.TrimStart('/'));
                            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                            using (var file = File.OpenWrite(filePath))
                            {
                                var contents = childFile.RetrieveContents();
                                file.Write(contents.GetAsArrayCopy(), 0, contents.Size);
                            }
                        }
                    }

                    extractDirectory(rom.Filesystem.RootDirectory);
                }
                
                EditorData.GamePath = ndsFolderPath;
            }
        }

        private void uncompressLZ77Button_Click(object sender, EventArgs e)
        {
            using(var file = File.OpenRead(SelectedFile))
            {
                var contents = new byte[file.Length];
                file.Read(contents, 0, (int)file.Length);

                using (var decompressedFile = File.OpenWrite(SelectedFile + ".decompressed"))
                {
                    var decompressedData = Util.LZ77.Decompress(new Util.ByteSlice(contents));
                    decompressedFile.Write(decompressedData, 0, decompressedData.Length);
                }
            }
        }
    }
}
