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
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ReviewCreateViewModel reviewCreateViewModel)
        {
            var pullRequestManager = await GitHubInstance.GetClientInstance().
                GetExistPullRequestManagerAsync(reviewCreateViewModel.RepositoryName, reviewCreateViewModel.PullRequestNumber);
            var review = await pullRequestManager.ReviewRepository.
                CreateReviewAsync(reviewCreateViewModel.Body, reviewCreateViewModel.ReviewComments, reviewCreateViewModel.ReviewEvent);
            return View();
        }
    }
}