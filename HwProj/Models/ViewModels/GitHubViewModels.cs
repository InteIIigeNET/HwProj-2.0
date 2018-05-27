using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.ViewModels
{
    public class PullRequestCreateViewModel
    {
        public string Title { get; set; }
        public string HeadBranchName { get; set; }
        public string RepositoryName { get; set; }
        public string BaseBranchName { get; set; }
    }
}