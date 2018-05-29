using HwProj.GitHubService;
using HwProj.Models.GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.ViewModels
{
    public class PullRequestCreateViewModel
    {
        public long TaskId { get; set; }
        public string Title { get; set; }
        public string HeadBranchName { get; set; }
        public string RepositoryName { get; set; }
        public string BaseBranchName { get; set; }
    }

    public class PullRequestChoseViewModel
    {
        public long TaskId { get; set; }
        public int Number { get; set; }
        public string RepositoryName { get; set; }
    }

    public class ReviewCreateViewModel
    {
        public string RepositoryName { get; set; }
        public int PullRequestNumber { get; set; } 
        public IEnumerable<ReviewComment> ReviewComments { get; set; }
        public string Body { get; set; }
        public ReviewEvent ReviewEvent { get; set; }
    }
}