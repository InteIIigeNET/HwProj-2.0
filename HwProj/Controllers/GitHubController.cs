using HwProj.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HwProj.Tools;
using HwProj.GitHubService;
using HwProj.Filters;
using HwProj.Models.ViewModels;
using System.Threading.Tasks;

namespace HwProj.Controllers
{
    [GitHubAccess]
    public class GitHubController : Controller
    {
        // GET: GitHub
        public async Task<ActionResult> Index()
        {
            var token = User.Identity.GetGitHubToken();
            github = new Repository(token);
            ViewBag.Repos = await github.GetRepositories();            
            return PartialView();
        }

        MainEduRepository Db = MainEduRepository.Instance;
        volatile Repository github;

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Index(PullRequestCreateViewModel pullRequestModel)
        {
            return PartialView("PullRequest", await github.CreatePullRequest(pullRequestModel.Title, pullRequestModel.BranchRef, pullRequestModel.RepositoryName));
        }

        public async Task<ActionResult> FillBranch(string repository)
        {
            var token = User.Identity.GetGitHubToken();
            github = new Repository(token);
            var branches = await github.GetBranches(repository);
            return Json(branches, JsonRequestBehavior.AllowGet);
        }
    }
}