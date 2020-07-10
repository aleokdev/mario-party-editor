using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NDSUtils;

namespace TextTableEditor
{
    public partial class TextTableEditor : Form
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

        public TextTableEditor(string fileToEdit)
        {
            InitializeComponent();
            filepathEditing = fileToEdit;
            this.Text = $"Text Editor [{fileToEdit}]";

            originalFileSize = newFileSize = new FileInfo(fileToEdit).Length;
            textListBox.Items.Clear();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            using var fstream = File.OpenRead(filepathEditing);
            var texts = readTexts(new BinaryReader(fstream));
            if (texts == null)
            {
                MessageBox.Show("This file does not seem to be a valid text file.");
                Close();
            }
            else
            {
                textListBox.DrawMode = DrawMode.OwnerDrawVariable;
                textListBox.MeasureItem += textListBox_MeasureItem;
                textListBox.DrawItem += textListBox_DrawItem;
                textListBox.Items.AddRange(texts);
            }
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
    }
}
