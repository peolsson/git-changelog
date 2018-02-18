using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitChangelog
{
    public class GitRepository
    {
        private Repository _repo;

        public Branch Head
        {
            get { return _repo.Head; }
        }

        public void Close()
        {
            if (_repo != null)
            {
                _repo.Dispose();
                _repo = null;
            }
        }

        public bool Open(string path)
        {
            Close();

            try
            {
                _repo = new Repository(path);
            }
            catch
            {
                _repo = null;
            }

            return _repo != null;
        }

        public Commit GetTail()
        {
            try
            {
                var filter = new CommitFilter
                {
                    FirstParentOnly = true
                };

                var commits = _repo.Commits.QueryBy(filter).Reverse().ToList();

                if (commits != null && commits.Count > 0)
                {
                    return commits.First();
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<Commit> GetCommits(Branch branch, object since, object until)
        {
            try
            {
                if (branch == null)
                {
                    var filter = new CommitFilter
                    {
                        ExcludeReachableFrom = since,
                        IncludeReachableFrom = until
                    };

                    return _repo.Commits.QueryBy(filter).Reverse().ToList();
                }
                else
                {
                    // If "HEAD" is selected, make sure to connect it to the selected branch
                    if (until is Branch)
                    {
                        Branch obj = (Branch)until;

                        if (obj.IsCurrentRepositoryHead)
                        {
                            until = branch;
                        }
                    }

                    List<Commit> result = new List<Commit>();
                    string sinceref = GetShaFromObject(since);
                    string untilref = GetShaFromObject(until);

                    bool found = sinceref == null ? true : false;
                    bool foundend = untilref == null || untilref.Length == 0 ? true : false;

                    foreach (Commit c in branch.Commits.Reverse())
                    {
                        if (found)
                        {
                            result.Add(c);

                            if (untilref != null && untilref.Length > 0 && c.Sha.Equals(untilref))
                            {
                                foundend = true;
                                break;
                            }
                        }

                        if (c.Sha.Equals(sinceref))
                        {
                            found = true;
                        }
                    }

                    if (!foundend)
                    {
                        result.Clear();
                    }

                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Commit> GetCommits(string branch, object since, object until)
        {
            if (branch != null && branch.Length > 0)
            {
                Branch b = _repo.Branches[branch];

                if (b != null)
                {
                    return GetCommits(b, since, until);
                }
            }

            return null;
        }

        public List<Branch> GetBranches()
        {
            return _repo.Branches.ToList();
        }

        public List<Tag> GetTags()
        {
            return _repo.Tags.ToList();
        }

        private string GetShaFromObject(object obj)
        {
            string data = null;

            try
            {
                if (obj is string)
                {
                    data = (string)obj;
                }
                else if (obj is Branch)
                {
                    data = ((Branch)obj).CanonicalName;
                }
                else if (obj is Reference)
                {
                    data = ((Reference)obj).CanonicalName;
                }
                else if (obj is Commit)
                {
                    data = ((Commit)obj).Sha;
                }
                else if (obj is TagAnnotation)
                {
                    data = ((TagAnnotation)obj).Target.Sha;
                }
                else if (obj is Tag)
                {
                    data = ((Tag)obj).Target.Sha;
                }
                else if (obj is ObjectId)
                {
                    data = ((ObjectId)obj).Sha;
                }

                if (data != null && data.Length > 0)
                {
                    var reference = _repo.Lookup<LibGit2Sharp.Commit>(data);

                    if (reference != null)
                    {
                        return reference.Sha;
                    }
                }
            }
            catch
            {
                // Catch all strange errors and just return null
            }

            return null;
        }
    }
}
