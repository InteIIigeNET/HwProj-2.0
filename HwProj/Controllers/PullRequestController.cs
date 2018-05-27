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
        // GET: PullRequest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chose()
        {
            return View();
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.Repos = await GitHubInstance.GetStorageInstance().GetRepositoriesAsync();
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PullRequestCreateViewModel pullRequestModel)
        {
            return PartialView("Index", (await GitHubInstance.GetClientInstance().
                GetNewPullRequestManagerAsync(pullRequestModel.Title, pullRequestModel.RepositoryName,
                pullRequestModel.HeadBranchName, pullRequestModel.BaseBranchName)).PullRequest);
        }
    }
}