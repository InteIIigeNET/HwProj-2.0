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
    public class ReviewController : Controller
    {
        private MainRepository _repository = MainRepository.Instance;

        [HttpPost]
        public async Task<ActionResult> Create(ReviewCreateViewModel reviewCreateViewModel)
        {
            var prd = _repository.PullRequestsDataManager.Get(p => p.Id == reviewCreateViewModel.PullRequestDataId);
            var pullRequestManager = await GitHubInstance.GetClientInstance().
                GetExistPullRequestManagerAsync(prd.RepositoryName, prd.PullRequestNumber);

            await pullRequestManager.ReviewRepository.
                CreateReviewAsync(reviewCreateViewModel.Body, reviewCreateViewModel.ReviewComments, reviewCreateViewModel.ReviewEvent);

            return RedirectToAction("Index", "PullRequest", new
            {
                pullRequestDataId = reviewCreateViewModel.PullRequestDataId
            });
        }
    }
}