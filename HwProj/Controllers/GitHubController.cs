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
        private static GitHubRepositoryStorage storageInstance;
        private static GitHubClient clientInstance;

        public static GitHubRepositoryStorage GetStorageInstance()
        {
            if (storageInstance == null)
            {
                var token = HttpContext.Current.User.Identity.GetGitHubToken();
                storageInstance = new GitHubRepositoryStorage(token);
            }
            return storageInstance;
        }

        public static GitHubClient GetClientInstance(string repName)
        {
            if (clientInstance == null)
            {
                var token = HttpContext.Current.User.Identity.GetGitHubToken();
                clientInstance = new GitHubClient(token, repName);
            }
            return clientInstance;
        }
    }

    [GitHubAccess]
    public class GitHubController : Controller
    {
        // GET: GitHub 
        public async Task<ActionResult> Index()
        {
            ViewBag.Repos = await GitHubCreator.GetStorageInstance().GetRepositories();
            return PartialView();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Index(PullRequestCreateViewModel pullRequestModel)
        {
            return PartialView("PullRequest", await GitHubCreator.GetClientInstance(null).
                PullRequest.CreatePullRequest(pullRequestModel.Title, pullRequestModel.RepositoryName, pullRequestModel.BranchRef));
        }

        public async Task<ActionResult> FillBranch(string repository)
        {
            var branches = await GitHubCreator.GetStorageInstance().GetBranches(repository);
            return Json(branches, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SendComment(string content)
        {
            throw new NotImplementedException();
        }
    }
}