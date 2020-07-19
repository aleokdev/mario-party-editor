using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using NDSUtils;

namespace TextTableEditor
{
    // TODO: Redo this entire class. Contains outdated code.
    public partial class TextTableEditor : Form
    {
        public NDSFile FileEditing { get; set; }
        private long _originalFileSize;
        private long OriginalFileSize
        {
            get => _originalFileSize;
            set
            {
                _originalFileSize = value;
                updateFileSizeLabel();
            }
        }
        private IEnumerable<string> Texts => from object s in textListBox.Items select s.ToString();
        private readonly bool isOriginalCompressed;
        private readonly VCSCommit masterCommit;

        public TextTableEditor(NDSFile fileToEdit, VCSCommit masterCommit)
        {
            // Initial setup
            InitializeComponent();
            this.Text = $"Text Editor [{fileToEdit.FullPath}]";
            FileEditing = fileToEdit;
            isOriginalCompressed = LZ77.IsCompressed(FileEditing);
            OriginalFileSize = fileToEdit.LatestVersionSize;
            this.masterCommit = masterCommit;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Read file contents
            var data = isOriginalCompressed switch
            {
                true => LZ77.Decompress(new ByteSlice(FileEditing.RetrieveLatestVersionData())),
                false => FileEditing.RetrieveLatestVersionData()
            };

            var texts = readTexts(new BinaryReader(new MemoryStream(data)));
            if (texts == null)
            {
                MessageBox.Show("This file does not seem to be a valid text file.");
                Close();
                return;
            }

            // Bind listbox draw events
            textListBox.DrawMode = DrawMode.OwnerDrawVariable;
            textListBox.MeasureItem += textListBox_MeasureItem;
            textListBox.DrawItem += textListBox_DrawItem;
            textListBox.Items.AddRange(texts);
        }

        void updateFileSizeLabel()
        {
            var compressedText = isOriginalCompressed ? "Compressed" : "";
            fileSizeLabel.Text = $"Original: {compressedText} {OriginalFileSize}B";
        }

        #region Format Helpers
        string[] readTexts(BinaryReader reader)
        {
            try
            {
                uint textCount = reader.ReadUInt32();
                if (textCount > 1000) // I doubt there are any text tables out there that have more than 1000 texts...
                    return null;
                var textAddresses = new uint[textCount];
                for (int textIndex = 0; textIndex < textCount; textIndex++)
                {
                    textAddresses[textIndex] = reader.ReadUInt32();
                }
                var texts = new string[textCount];

                for (int textIndex = 0; textIndex < textCount; textIndex++)
                {
                    const int bytes_per_char = 2;
                    reader.BaseStream.Seek(textAddresses[textIndex], SeekOrigin.Begin);
                    var chars = new List<byte>();
                    byte lastChar = 0xFF;
                    while (lastChar != 0)
                    {
                        chars.AddRange(reader.ReadBytes(bytes_per_char));
                        lastChar = chars[chars.Count - 2];
                    }

                    texts[textIndex] = Encoding.GetEncoding("UTF-16").GetString(chars.ToArray());
                }

                return texts;
            }
            catch (Exception _)
            {
                return null;
            }
        }

        byte[] serializeTexts()
        {
            var writer = new BinaryWriter(new MemoryStream());

            // First, write the number of texts there are.
            var textCount = Texts.Count();
            writer.Write((uint)textCount);

            // Then write the estimated addresses of those texts.
            int addressesPartSize = sizeof(uint) * textCount;
            int currentAddress = sizeof(uint) + addressesPartSize;
            foreach(var text in Texts)
            {
                writer.Write(currentAddress);
                // Null-terminated UTF-16
                currentAddress += (text.Length + 1) * 2;
            }

            // And finally write the texts themselves in UTF-16, which should be in the addresses
            // estimated earlier.
            foreach(var text in Texts)
            {
                writer.Write(Encoding.GetEncoding("UTF-16").GetBytes(text));
                // GetBytes doesn't encode string terminator
                writer.Write(new byte[] { 0, 0 });
            }

            return ((MemoryStream)writer.BaseStream).ToArray();
        }
        #endregion

        private void textListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            const int spacing = 4;
            var lineCount = (from chr in textListBox.Items[e.Index].ToString() where chr == '\n' select chr).Count() + 1;
            e.ItemHeight = lineCount * e.ItemHeight + spacing;
        }

        private void textListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            var brush = new SolidBrush(e.ForeColor);
            var lineY = e.Bounds.Bottom - 2;
            e.Graphics.DrawString(textListBox.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
            e.Graphics.DrawLine(new Pen(brush), new Point(e.Bounds.Left, lineY), new Point(e.Bounds.Right, lineY));
        }

        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            var newData = isOriginalCompressed switch
            {
                true => LZ77.Compress(new ByteSlice(serializeTexts())),
                false => serializeTexts()
            };
            FileEditing.AddNewVersion(masterCommit, newData);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void textListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (textListBox.SelectedItem == null) return;

            var editItemDialog = new TextInputDialog(textListBox.SelectedItem.ToString());

            if(editItemDialog.ShowDialog() == DialogResult.OK)
            {
                textListBox.Items[textListBox.SelectedIndex] = editItemDialog.TextInputted;
            }
        }
    }
}
