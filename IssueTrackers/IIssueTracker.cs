using System;
using System.Collections.Generic;

namespace GitChangelog
{
    public class Issue
    {
        public string Key { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public DateTime? ResolutionDateTime { get; set; }
    }

    public interface IIssueTracker
    {
        void Close();
        void Open();
        Issue GetIssue(string key);
        List<Issue> GetIssues(string[] keys);
        List<Issue> GetIssues(string project, string[] keys);
    }
}
