using System.Windows.Forms;
using NDSUtils;

namespace MarioPartyEditor
{
    public partial class CreateCommitForm : Form
    {
        public readonly VCSCommit CommitViewing;

        public string CommitName => commitNameTextBox.Text;

        public string CommitDescription => commitDescriptionTextBox.Text;

        public CreateCommitForm(VCSCommit commit, bool canAccept)
        {
            CommitViewing = commit;

            InitializeComponent();

            foreach (var change in commit.Changes)
                changedFilesListBox.Items.Add(change);

            commitNameTextBox.Text = commit.Name;
            commitDescriptionTextBox.Text = commit.Description;

            createCommitButton.Visible = canAccept;
            AcceptButton = createCommitButton;
        }

        private void createCommitButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
