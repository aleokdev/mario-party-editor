using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarioPartyEditor
{
    public partial class TextEditor : Form
    {
        string filepathEditing;
        long _originalFileSize;
        long originalFileSize
        {
            get => _originalFileSize;
            set
            {
                _originalFileSize = value;
                updateFileSizeLabel();
            }
        }
        long newFileSize;

        public TextEditor(string fileToEdit)
        {
            InitializeComponent();
            filepathEditing = fileToEdit;

            originalFileSize = newFileSize = new FileInfo(fileToEdit).Length;
            textListBox.Items.Clear();
            textListBox.Items.AddRange(readTexts(new BinaryReader(File.OpenRead(fileToEdit))));
            textListBox.DrawMode = DrawMode.OwnerDrawVariable;
            textListBox.MeasureItem += textListBox_MeasureItem;
            textListBox.DrawItem += textListBox_DrawItem;
        }

        void updateFileSizeLabel()
        {
            fileSizeLabel.Text = $"{newFileSize} / {originalFileSize}";
            if (newFileSize > originalFileSize)
                fileSizeLabel.ForeColor = Color.Red;
            else if (newFileSize < originalFileSize)
            {
                fileSizeLabel.ForeColor = Color.Blue;
                fileSizeLabel.Text += " (Adding filler)";
            }
            else // newFileSize == originalFileSize
                fileSizeLabel.ForeColor = SystemColors.ControlText;
        }

        string[] readTexts(BinaryReader reader)
        {
            uint textCount = reader.ReadUInt32();
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

        private void textListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = ((from chr in textListBox.Items[e.Index].ToString() where chr == '\n' select chr).Count() + 1) * e.ItemHeight;
        }

        private void textListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(textListBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }
    }
}
