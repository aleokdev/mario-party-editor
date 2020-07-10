using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MarioPartyEditor.ROM;

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
                    var fileFormatName = (from type in Assembly.GetExecutingAssembly().GetTypes()
                                          let attributes = type.GetCustomAttributes(typeof(FileFormatCheckerAttribute))
                                          where attributes.Count() > 0 && (bool)type.GetMethods().First().Invoke(null, new object[] { file })
                                          select ((FileFormatCheckerAttribute)attributes.First()).FormatName).FirstOrDefault() ?? "Unknown";

                    var nodeName = $"{Path.GetFileName(file)} [{fileFormatName}]";
                    parentNode.Nodes.Add(nodeName).Tag = file;
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
            => new TextEditor(SelectedFile).Show(this);

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
                    rom.Data = new ByteSlice(new byte[romFile.Length], rom.Data.SliceStart, rom.Data.SliceEnd);
                    romFile.Read(rom.Data.Source, 0, (int)romFile.Length);
                }

                string dataFolderPath = Path.Combine(Path.GetDirectoryName(dialog.FileName), DefaultDataFolderName);
                string ndsFolderPath = Path.Combine(dataFolderPath, rom.Header.GameCode);
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
                        using(var file = File.OpenWrite(filePath))
                        {
                            var contents = childFile.RetrieveContents();
                            file.Write(contents.GetAsArrayCopy(), 0, contents.Size);
                        }
                    }
                }

                extractDirectory(rom.Filesystem.RootDirectory);
                EditorData.GamePath = ndsFolderPath;
            }
        }
    }
}
