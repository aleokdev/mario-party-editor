using System.Windows.Forms;
using NDSUtils;

namespace MarioPartyEditor
{
    public partial class CreateCommitForm : Form
    {
        public readonly VCSCommit CommitViewing;

        public string CommitName => commitNameTextBox.Text;

        public string CommitDescription => commitDescriptionTextBox.Text;

        public enum CommitFormMode
        {
            CreateCommit,
            EditCommit
        }

        public CreateCommitForm(VCSCommit commit, CommitFormMode mode)
        {
            CommitViewing = commit;

            InitializeComponent();

            foreach (var change in commit.Changes)
                changedFilesListBox.Items.Add(change);

            commitNameTextBox.Text = commit.Name;
            commitDescriptionTextBox.Text = commit.Description;

            createCommitButton.Text = mode switch
            {
                CommitFormMode.CreateCommit => "Accept And Create Commit",
                CommitFormMode.EditCommit => "Accept And Edit Commit",
                _ => throw new System.NotImplementedException()
            };
            this.Text = mode switch
            {
                CommitFormMode.CreateCommit => "Create New Commit...",
                CommitFormMode.EditCommit => "Edit Commit...",
                _ => throw new System.NotImplementedException()
            };
            AcceptButton = createCommitButton;

            commitTimeLabel.Text = commit.Time switch
            {
                null => "Not yet committed",
                _ => $"{commit.Time?.ToLongTimeString()} - {commit.Time?.ToLongDateString()}"
            };
        }

        private void createCommitButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
