using HwProj.Filters;
using HwProj.Models.Repositories;
using HwProj.Models.ViewModels;
using HwProj.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HwProj.Controllers.GitHub
{
    [GitHubAccess]
    public class PullRequestController : Controller
    {
        MainRepository db = MainRepository.Instance;

        public async Task<ActionResult> Index(string repName, int number)
        {
            var pullRequest = (await GitHubInstance.GetClientInstance().
                GetExistPullRequestManagerAsync(repName,
                number))
                .PullRequest;
            return View(pullRequest);
        }

        public async Task<ActionResult> Chose(long taskId)
        {
            ViewBag.Repos = await GitHubInstance.GetStorageInstance().GetRepositoriesAsync();
            return PartialView(new PullRequestChoseViewModel { TaskId = taskId });
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

        public async Task<ActionResult> Create(long taskId)
        {
            ViewBag.Repos = await GitHubInstance.GetStorageInstance().GetRepositoriesAsync();
            return PartialView(new PullRequestCreateViewModel { TaskId = taskId });
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


        #region Helper
        public void CreateHomework(long taskId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}