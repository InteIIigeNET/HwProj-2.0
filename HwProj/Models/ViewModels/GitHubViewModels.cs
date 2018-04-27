using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.ViewModels
{
    public class PullRequestCreateViewModel
    {
        public string Title { get; set; }
        public string BranchRef { get; set; }
        public string RepositoryName { get; set; }
    }
}