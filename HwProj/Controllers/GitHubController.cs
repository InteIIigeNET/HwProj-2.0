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
        public ActionResult Index()
        {
            var token = User.Identity.GetGitHubToken();
            github = new Repository(token);

            return PartialView(github);
        }

        MainEduRepository Db = MainEduRepository.Instance;
        Repository github;

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PullRequest(PullRequestCreateViewModel pullRequestModel)
        {
            return PartialView(await github.CreatePullRequest(pullRequestModel.Title, pullRequestModel.BranchRef, pullRequestModel.RepositoryName));
        }
    }
}