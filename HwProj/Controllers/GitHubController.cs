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
    public static class GitHubCreator
    {
        private static GitRepository _instance;

        public static GitRepository GetInstance()
        {
            if (_instance == null)
            {
                var token = HttpContext.Current.User.Identity.GetGitHubToken();
                _instance = new GitRepository(token);
            }
            return _instance;
        }
    }

    [GitHubAccess]
    public class GitHubController : Controller
    {
	    MainRepository Db = MainRepository.Instance;
		// GET: GitHub 
		public async Task<ActionResult> Index()
        {
            ViewBag.Repos = await GitHubCreator.GetInstance().GetRepositories();
            return PartialView();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Index(PullRequestCreateViewModel pullRequestModel)
        {
            return PartialView("PullRequest", await GitHubCreator.GetInstance().CreatePullRequest(pullRequestModel.Title, pullRequestModel.BranchRef, pullRequestModel.RepositoryName));
        }

        public async Task<ActionResult> FillBranch(string repository)
        {
            var branches = await GitHubCreator.GetInstance().GetBranches(repository);
            return Json(branches, JsonRequestBehavior.AllowGet);
        }
    }
}