using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitChangelog
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void SettingsDialog_Shown(object sender, EventArgs e)
        {
            comboBoxJiraProjects.Items.Clear();

            textBoxGitRepositoryPath.Text = Properties.Settings.Default.GitRepositoryPath;
            textBoxJiraHostname.Text = Properties.Settings.Default.JiraHost;
            textBoxJiraUsername.Text = Properties.Settings.Default.JiraUsername;
            textBoxJiraPassword.Text = Properties.Settings.Default.JiraPassword;

            if (Properties.Settings.Default.JiraProject != null)
            {
                comboBoxJiraProjects.Items.Add(Properties.Settings.Default.JiraProject);
            }

            textBoxJiraMatchPrefix.Text = Properties.Settings.Default.JiraMatchPrefix;
            comboBoxJiraProjects.SelectedIndex = 0;

            buttonOk.Select();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.GitRepositoryPath = textBoxGitRepositoryPath.Text;
            Properties.Settings.Default.JiraHost = textBoxJiraHostname.Text;
            Properties.Settings.Default.JiraUsername = textBoxJiraUsername.Text;
            Properties.Settings.Default.JiraPassword = textBoxJiraPassword.Text;
            Properties.Settings.Default.JiraProject = comboBoxJiraProjects.SelectedItem != null ? comboBoxJiraProjects.SelectedItem.ToString() : null;
            Properties.Settings.Default.JiraMatchPrefix = textBoxJiraMatchPrefix.Text;
            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }

        private void comboBoxJiraProjects_DropDown(object sender, EventArgs e)
        {
            string selected = comboBoxJiraProjects.SelectedItem != null ? comboBoxJiraProjects.SelectedItem.ToString() : null;
            comboBoxJiraProjects.Items.Clear();

            try
            {
                var issueTracker = new IssueTrackerJira(textBoxJiraHostname.Text, textBoxJiraUsername.Text, textBoxJiraPassword.Text, comboBoxJiraProjects.SelectedItem != null ? comboBoxJiraProjects.SelectedItem.ToString(): "");
                issueTracker.Open();
                var projects = issueTracker.GetProjects();

                if (projects != null)
                {
                    comboBoxJiraProjects.Items.AddRange(projects.ToArray());
                }
            }
            catch
            {
                // Silence errors
            }

            if (selected != null && selected.Length > 0)
            {
                foreach (var s in comboBoxJiraProjects.Items)
                {
                    if (s.ToString().Trim().ToLower().Equals(selected.Trim().ToLower()))
                    {
                        comboBoxJiraProjects.SelectedItem = s;
                        break;
                    }
                }
            }
        }

        private void buttonGitPathDialog_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = textBoxGitRepositoryPath.Text;

                DialogResult result = fbd.ShowDialog(this);

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBoxGitRepositoryPath.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
