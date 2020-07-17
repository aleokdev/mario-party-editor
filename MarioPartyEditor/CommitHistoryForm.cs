using NDSUtils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MarioPartyEditor
{
    public partial class CommitHistoryForm : Form
    {
        public CommitHistoryForm(IEnumerable<VCSCommit> commits)
        {
            InitializeComponent();

            foreach(var commit in commits)
            {
                commitListBox.Items.Add(commit);
            }
        }

        private void commitListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Show commit dialog if double clicked a commit
            if (commitListBox.SelectedItem != null)
            {
                var commit = commitListBox.SelectedItem as VCSCommit;
                var dialog = new CreateCommitForm(commit, CreateCommitForm.CommitFormMode.EditCommit);

                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    commit.Name = dialog.CommitName;
                    commit.Description = dialog.CommitDescription;
                }
            }
        }
    }
}
