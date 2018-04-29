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
        private static Repository instance;

        public static Repository GetInstance()
        {
            if (instance == null)
            {
                var token = HttpContext.Current.User.Identity.GetGitHubToken();
                instance = new Repository(token);
            }
            return instance;
        }
    }

    [GitHubAccess]
    public class GitHubController : Controller
    {
        // GET: GitHub 
        public async Task<ActionResult> Index()
        {
            ViewBag.Repos = await GitHubCreator.GetInstance().GetRepositories();
            return PartialView();
        }

        MainEduRepository Db = MainEduRepository.Instance;

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