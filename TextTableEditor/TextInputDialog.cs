using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextTableEditor
{
    public partial class TextInputDialog : Form
    {
        public string TextInputted => inputTextBox.Text;

        public TextInputDialog(string startingText)
        {
            InitializeComponent();
            inputTextBox.Text = startingText;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
