using HwProj.Filters;
using HwProj.Models.ViewModels;
using HwProj.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HwProj.Controllers
{
    [GitHubAccess]
    public class PullRequestController : Controller
    {
        
        public async Task<ActionResult> Index(string repName, int number)
        {
            var pullRequest = (await GitHubInstance.GetClientInstance().
                GetExistPullRequestManagerAsync(repName,
                number))
                .PullRequest;
            return PartialView(pullRequest);
        }

        public async Task<ActionResult> Chose()
        {
            ViewBag.Repos = await GitHubInstance.GetStorageInstance().GetRepositoriesAsync();
            return PartialView();
        }

        [HttpPost]
        public ActionResult Chose(PullRequestChoseViewModel pullRequestModel)
        {
            return RedirectToAction("Index", new
            {
                repName = pullRequestModel.RepositoryName,
                number = pullRequestModel.Number
            });
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.Repos = await GitHubInstance.GetStorageInstance().GetRepositoriesAsync();
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PullRequestCreateViewModel pullRequestModel)
        {
            var pullRequest = (await GitHubInstance.GetClientInstance().
                GetNewPullRequestManagerAsync(pullRequestModel.Title, pullRequestModel.RepositoryName,
                pullRequestModel.HeadBranchName, pullRequestModel.BaseBranchName)).PullRequest;
            return RedirectToAction("Index", new
            {
                repName = pullRequestModel.RepositoryName,
                number = pullRequest.Number
            });
        }        
    }
}