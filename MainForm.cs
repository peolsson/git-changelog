using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitChangelog
{
    public partial class MainForm : Form
    {
        private SettingsDialog _settings = new SettingsDialog();
        private AboutBox _about = new AboutBox();

        private GitRepository _repo = new GitRepository();
        private IIssueTracker _issues = new IssueTrackerJira();

        private string _matchPrefix = "";

        private struct GitMessage
        {
            public string subject;
            public string body;
        }

        private const int WM_USER = 0x0400;
        private const int EM_SETEVENTMASK = (WM_USER + 69);
        private const int WM_SETREDRAW = 0x0b;
        private IntPtr OldEventMask;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private void BeginUpdate()
        {
            SendMessage(textChangelog.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
            OldEventMask = (IntPtr)SendMessage(textChangelog.Handle, EM_SETEVENTMASK, IntPtr.Zero, IntPtr.Zero);
        }

        private void EndUpdate()
        {
            SendMessage(textChangelog.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
            SendMessage(textChangelog.Handle, EM_SETEVENTMASK, IntPtr.Zero, OldEventMask);
        }

        public MainForm()
        {
            InitializeComponent();

            RefreshFromSettings();
        }

        private List<string> GetMatches(string text)
        {
            if (_matchPrefix.Length > 0 && text != null && text.Length > 0)
            {
                var result = new List<string>();
                Regex expression = new Regex(_matchPrefix);
                var results = expression.Matches(text);

                foreach (Match match in results)
                {
                    result.Add(match.Groups["Identifier"].Value);
                }

                if (result.Count == 0)
                {
                    return null;
                }

                return result;
            }

            return null;
        }

        private Dictionary<string, Issue> GetRelatedIssues(List<Commit> commits)
        {
            if (_matchPrefix == null || _matchPrefix.Length == 0)
            {
                return null;
            }

            HashSet<string> uniqueIssues = new HashSet<string>();
            Dictionary<string, Issue> relatedIssues = new Dictionary<string, Issue>();

            foreach (Commit c in commits)
            {
                var matches = GetMatches(c.Message);

                if (matches != null)
                {
                    foreach (string match in matches)
                    {
                        if (!uniqueIssues.Contains(match))
                        {
                            uniqueIssues.Add(match);
                        }
                    }
                }
            }

            var issues = _issues.GetIssues(null, uniqueIssues.ToArray());

            if (issues != null && issues.Count > 0)
            {
                foreach (Issue issue in issues)
                {
                    relatedIssues[issue.Key] = issue;
                }
            }

            return relatedIssues;
        }

        private GitMessage FilterMessage(string message)
        {
            var result = new GitMessage();

            // Fix linebreaks
            message = message.Replace("\n", Environment.NewLine);

            // Remove Change-Id, and separate into subject/body parameters
            var lines = message.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            int idx = 0;
            foreach (var line in lines)
            {
                if (idx > 1)
                {
                    if (line.IndexOf("Change-Id") == -1)
                    {
                        result.body += line + Environment.NewLine;
                    }
                }
                else if (idx == 0)
                {
                    result.subject = line;
                }

                idx++;
            }

            result.subject = result.subject.Trim();
            result.body = !string.IsNullOrWhiteSpace(result.body) ? result.body.Trim() : string.Empty;

            return result;
        }

        private void AppendStyledText(Color color, Color backColor, FontStyle fontStyle, Font font, bool bulleted, string text)
        {
            if (font == null)
            {
                font = new Font(textChangelog.Font, fontStyle);
            }

            // Change properties
            textChangelog.SelectionStart = textChangelog.TextLength;
            textChangelog.SelectionLength = 0;
            textChangelog.SelectionColor = color;
            textChangelog.SelectionBackColor = backColor;
            textChangelog.SelectionFont = font;
            textChangelog.SelectionBullet = bulleted;
            textChangelog.BulletIndent = 30;
            textChangelog.SelectionIndent = bulleted ? 15 : 0;

            // Print text
            textChangelog.AppendText(text);

            // Restore properties
            textChangelog.SelectionColor = textChangelog.ForeColor;
            textChangelog.SelectionBackColor = textChangelog.BackColor;
            textChangelog.SelectionFont = textChangelog.Font;
            textChangelog.SelectionBullet = false;
            textChangelog.SelectionIndent = 0;
        }

        private DateTime RoundToNextDay(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, 0, 0, 0).AddDays(1);
        }

        private void btnGenerateChangelog_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            BeginUpdate();

            try
            {
                textChangelog.Clear();

                object since = comboReferenceSince.SelectedItem != null ? ((ComboBoxItem)comboReferenceSince.SelectedItem).Value : null;
                object until = comboReferenceUntil.SelectedItem != null ? ((ComboBoxItem)comboReferenceUntil.SelectedItem).Value : null;

                if (since == null && comboReferenceSince.Text != null && comboReferenceSince.Text.Length > 0)
                {
                    if (!comboReferenceSince.Text.Equals("TAIL"))
                    {
                        since = comboReferenceSince.Text;
                    }
                }
                else if (since == null)
                {
                    MessageBox.Show("Invalid Git reference starting point");
                    return;
                }

                if (until == null && comboReferenceUntil.Text != null && comboReferenceUntil.Text.Length > 0)
                {
                    until = comboReferenceSince.Text;
                }
                else if (until == null)
                {
                    MessageBox.Show("Invalid Git reference end point");
                    return;
                }

                object branch = comboBranches.SelectedItem != null ? ((ComboBoxItem)comboBranches.SelectedItem).Value : null;
                var commits = _repo.GetCommits((Branch)branch, since, until);

                if (commits != null && commits.Any())
                {
                    var from = commits.First().Committer.When.DateTime;
                    var to = commits.Last().Committer.When.DateTime;
                    var relatedIssues = GetRelatedIssues(commits);

                    if (relatedIssues != null && relatedIssues.Count > 0)
                    {
                        var roundedTo = RoundToNextDay(to);
                        string resolved = "";
                        string unresolved = "";

                        foreach (var issue in relatedIssues)
                        {
                            if (issue.Value.ResolutionDateTime != null && roundedTo > issue.Value.ResolutionDateTime)
                            {
                                resolved += issue.Key + ": " + issue.Value.Summary + Environment.NewLine;
                            }
                            else
                            {
                                unresolved += issue.Key + ": " + issue.Value.Summary + Environment.NewLine;
                            }
                        }

                        if (resolved.Length > 0)
                        {
                            var font = new Font(textChangelog.Font.FontFamily, 14.0f);
                            AppendStyledText(textChangelog.ForeColor, textChangelog.BackColor, FontStyle.Regular, font, false, "Resolved issues:" + Environment.NewLine);
                            AppendStyledText(textChangelog.ForeColor, textChangelog.BackColor, FontStyle.Italic, null, true, resolved);
                            textChangelog.AppendText(Environment.NewLine);
                        }

                        if (unresolved.Length > 0)
                        {
                            var font = new Font(textChangelog.Font.FontFamily, 14.0f);
                            AppendStyledText(textChangelog.ForeColor, textChangelog.BackColor, FontStyle.Regular, font, false, "Issues in progress, not fully resolved:" + Environment.NewLine);
                            AppendStyledText(textChangelog.ForeColor, textChangelog.BackColor, FontStyle.Italic, null, true, unresolved);
                            textChangelog.AppendText(Environment.NewLine);
                        }
                    }

                    var f = new Font(textChangelog.Font.FontFamily, 14.0f, FontStyle.Underline);
                    AppendStyledText(textChangelog.ForeColor, textChangelog.BackColor, FontStyle.Regular, f, false, "Change details:" + Environment.NewLine + Environment.NewLine);

                    foreach (Commit c in commits)
                    {
                        var message = FilterMessage(c.Message);

                        AppendStyledText(textChangelog.ForeColor, textChangelog.BackColor, FontStyle.Bold | FontStyle.Italic, null, false, "[" + c.Author.When.ToString("yyyy-MM-dd HH:mm:ss") + "] " + message.subject);
                        textChangelog.AppendText(Environment.NewLine);

                        if (message.body != null && message.body.Length > 0)
                        {
                            textChangelog.AppendText(Environment.NewLine);
                            textChangelog.AppendText(message.body);
                            textChangelog.AppendText(Environment.NewLine);
                        }
#if false
                        var matches = GetMatches(c.Message);

                        if (matches != null)
                        {
                            textChangelog.AppendText(Environment.NewLine);

                            foreach (string match in matches)
                            {
                                if (relatedIssues.ContainsKey(match))
                                {
                                    AppendStyledText(textChangelog.ForeColor, textChangelog.BackColor, FontStyle.Italic, null, true, match + ": " + relatedIssues[match].Summary + Environment.NewLine);
                                }
                            }
                        }
#endif

                        AppendStyledText(textChangelog.ForeColor, textChangelog.BackColor, FontStyle.Regular, null, false, Environment.NewLine + "----------------------------" + Environment.NewLine + Environment.NewLine);
                    }

                    AppendStyledText(textChangelog.ForeColor, textChangelog.BackColor, FontStyle.Italic, null, false, "FROM: " + from + ", TO: " + to);
                }
            }
            finally
            {
                Cursor.Current = Cursors.Arrow;
                EndUpdate();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _repo.Close();
            comboBranches.Items.Clear();
            comboReferenceSince.Items.Clear();
        }

        private void InitializeComboBoxes()
        {
            // Add all branches, and select current branch
            comboBranches.Items.Clear();
            var branches = _repo.GetBranches();

            if (branches != null)
            {
                foreach (Branch b in branches)
                {
                    string suffix = "";

                    if (b.IsCurrentRepositoryHead)
                    {
                        suffix = " (current)";
                    }

                    var obj = new ComboBoxItem(b.FriendlyName + suffix, b);

                    comboBranches.Items.Add(obj);

                    if (b.IsCurrentRepositoryHead)
                    {
                        comboBranches.SelectedItem = obj;
                    }
                }
            }

            comboReferenceSince.Items.Clear();
            comboReferenceSince.Text = "";
            comboReferenceUntil.Items.Clear();
            comboReferenceUntil.Text = "";

            // Add HEAD/TAIL references
            comboReferenceUntil.Items.Add(new ComboBoxItem("HEAD", _repo.Head));
            comboReferenceUntil.SelectedItem = comboReferenceUntil.Items[comboReferenceUntil.Items.Count - 1];
            comboReferenceSince.Items.Add(new ComboBoxItem("TAIL", null));
            comboReferenceSince.SelectedItem = comboReferenceSince.Items[comboReferenceSince.Items.Count - 1];

            // Add all tags as references
            var tags = _repo.GetTags();
            if (tags != null)
            {
                foreach (Tag t in tags)
                {
                    var obj = new ComboBoxItem("Tag: " + t.FriendlyName, t);

                    comboReferenceSince.Items.Add(obj);
                    comboReferenceUntil.Items.Add(obj);
                }
            }

            // Add all branches as references
            if (branches != null)
            {
                foreach (Branch b in branches)
                {
                    if (b.IsRemote)
                    {
                        var obj = new ComboBoxItem("Branch: " + b.FriendlyName, b);

                        comboReferenceSince.Items.Add(obj);
                        comboReferenceUntil.Items.Add(obj);
                    }
                }
            }
        }

        private void RefreshFromSettings()
        {
            if (!Directory.Exists(Properties.Settings.Default.GitRepositoryPath + @"\.git"))
            {
                MessageBox.Show("Invalid Git repository path: " + Properties.Settings.Default.GitRepositoryPath);
                return;
            }

            try
            {
                var issues = new IssueTrackerJira(Properties.Settings.Default.JiraHost, Properties.Settings.Default.JiraUsername, Properties.Settings.Default.JiraPassword, Properties.Settings.Default.JiraProject);
                issues.Open();
            }
            catch(Exception e)
            {
                MessageBox.Show("Invalid Jira settings :: " + e.Message);
                return;
            }

            // Reinitialize the repository
            _repo.Close();
            _repo.Open(Properties.Settings.Default.GitRepositoryPath);

            // Reinit Jira stuff
            _issues = new IssueTrackerJira(Properties.Settings.Default.JiraHost, Properties.Settings.Default.JiraUsername, Properties.Settings.Default.JiraPassword, Properties.Settings.Default.JiraProject);
            _issues.Open();

            InitializeComboBoxes();

            if (Properties.Settings.Default.JiraMatchPrefix != null && Properties.Settings.Default.JiraMatchPrefix.Length > 0)
            {
                _matchPrefix = @"(?<Identifier>" + Properties.Settings.Default.JiraMatchPrefix + ")";
            }
            else
            {
                _matchPrefix = "";
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_settings.ShowDialog() == DialogResult.OK)
            {
                RefreshFromSettings();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _about.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    internal class ComboBoxItem
    {
        public string FriendlyText { get; set; }
        public object Value { get; set; }

        public ComboBoxItem(string friendlyText, object value)
        {
            FriendlyText = friendlyText;
            Value = value;
        }

        public override string ToString()
        {
            return FriendlyText;
        }
    }
}
