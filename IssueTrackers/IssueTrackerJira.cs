using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitChangelog
{
    public class IssueTrackerJira : IIssueTracker
    {
        private readonly string _hostname = "";
        private readonly string _username = "";
        private readonly string _password = "";
        private readonly string _project = "";

        private Jira _jira;

        public IssueTrackerJira()
        {
        }

        public IssueTrackerJira(string hostname, string username, string password, string project)
        {
            _hostname = hostname;
            _username = username;
            _password = password;
            _project = project;
        }

        private Issue Convert(Atlassian.Jira.Issue issue)
        {
            Issue obj = new Issue();

            obj.Key = issue.Key.Value;
            obj.Summary = issue.Summary;
            obj.Description = issue.Description;
            obj.ResolutionDateTime = issue.ResolutionDate;

            return obj;
        }

        public void Close()
        {
            if (_jira != null)
            {
                _jira = null;
            }
        }

        public void Open()
        {
            Close();

            _jira = Jira.CreateRestClient(_hostname, _username, _password);
            _jira.Issues.MaxIssuesPerRequest = 10000;
            GetProjects(); // Just to force a connection
        }

        public Issue GetIssue(string key)
        {
            var issues = (from i in _jira.Issues.Queryable
                          where i.Key == key
                          orderby i.Created
                          select i).ToList();

            if (issues.Count > 0)
            {
                return Convert(issues.First());
            }

            return null;
        }

        public List<Issue> GetIssues(string project, string[] keys)
        {
            if (project == null || project.Length == 0)
            {
                project = _project;
            }

            var issues = (from i in _jira.Issues.Queryable
                          where i.Project == project
                          orderby i.Created
                          select i).ToList();

            var issueList = new List<Issue>();

            foreach (var issue in issues)
            {
                var results = Array.FindAll(keys, s => s.Equals(issue.Key.Value));

                if (results != null && results.Length > 0)
                {
                    issueList.Add(Convert(issue));
                }
            }

            return issueList;
        }

        public List<Issue> GetIssues(string[] keys)
        {
            List<Issue> issueList = new List<Issue>();

            foreach (string key in keys)
            {
                var x = GetIssue(key);

                if (x != null)
                {
                    issueList.Add(GetIssue(key));
                }
            }

            if (issueList.Count == 0)
            {
                return null;
            }

            return issueList;
        }

        public List<string> GetProjects()
        {
            var projects = _jira.Projects.GetProjectsAsync();
            projects.Wait();

            var result = new List<string>();

            var projSorted = from i in projects.Result
                             orderby i.Name
                             select i;

            foreach (var project in projSorted)
            {
                result.Add(project.Name);
            }

            return result;
        }
    }
}
