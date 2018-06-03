using HwProj.GitHubService;
using HwProj.Models.GitHub;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Validators;

namespace HwProj.Models.ViewModels
{
    public class PullRequestCreateViewModel
    {
        public long TaskId { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Откуда")]
        [Required]
        public string HeadBranchName { get; set; }

        [Display(Name = "Репозиторий")]
        [Required]
        public string RepositoryName { get; set; }

        [Display(Name = "Куда")]
        [Inequality("HeadBranchName", ErrorMessage = "Нельзя создать pull request из тождественных веток")]
        [Required]
        public string BaseBranchName { get; set; }
    }

    public class PullRequestChoseViewModel
    {
        public long TaskId { get; set; }

        [Display(Name = "Название")]
        [Range(1, int.MaxValue, ErrorMessage = "Выберите pull request")]
        public int Number { get; set; }

        [Display(Name = "Репозиторий")]
        [Required]
        public string RepositoryName { get; set; }
    }

    public class ReviewCreateViewModel
    { 
        public IEnumerable<ReviewComment> ReviewComments { get; set; }
        public string Body { get; set; }
        public ReviewEvent ReviewEvent { get; set; }
        public long PullRequestDataId { get; set; }
    }

    public class PullRequestViewModel
    {
        public PullRequest PullRequest { get; set; }
        public long PullRequestDataId { get; set; }
        public long MentorId { get; set; }
    }
}