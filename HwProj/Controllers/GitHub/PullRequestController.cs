using HwProj.Filters;
using HwProj.Models;
using HwProj.Models.Repositories;
using HwProj.Models.ViewModels;
using HwProj.Services.NotificationPatterns;
using HwProj.Tools;
using Microsoft.AspNet.Identity;
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
        private MainRepository _repository = MainRepository.Instance;
        private IAsyncManager _asyncManager = new AsyncManager();

        public async Task<ActionResult> Index(long pullRequestDataId)
        {
            var prd = _repository.PullRequestsDataManager.Get(p => p.Id == pullRequestDataId);
            var pullRequest = (await GitHubInstance.GetClientInstance().
                GetExistPullRequestManagerAsync(prd.RepositoryName,
                prd.PullRequestNumber))
                .PullRequest;
            
            return View(new PullRequestViewModel
            {
                PullRequest = pullRequest,
                MentorId = prd.MentorId,
                PullRequestDataId = pullRequestDataId
            });
        }

        public async Task<ActionResult> Chose(long taskId)
        {
            ViewBag.Repos = await GitHubInstance.GetStorageInstance().GetRepositoriesAsync();
            return PartialView(new PullRequestChoseViewModel { TaskId = taskId });
        }
        
        [HttpPost]
        public async Task<ActionResult> Chose(PullRequestChoseViewModel pullRequestModel)
        {
            if (ModelState.IsValid)
            {
				var id = await CreateHomeworkViaPullRequest(pullRequestModel.TaskId, pullRequestModel.RepositoryName, pullRequestModel.Number);
	            return id.HasValue ? RedirectToAction("Index", new { pullRequestDataId = id })
								   : RedirectToAction("Index", "Home", new { errorMessage = "Ошибка при обновлении базы данных" });
			}
            else
            {
                ViewBag.Repos = await GitHubInstance.GetStorageInstance().GetRepositoriesAsync();
                return PartialView(pullRequestModel);
            }  
        }
        
        public async Task<ActionResult> Create(long taskId)
        {
            ViewBag.Repos = await GitHubInstance.GetStorageInstance().GetRepositoriesAsync();
            return PartialView(new PullRequestCreateViewModel { TaskId = taskId });
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(PullRequestCreateViewModel pullRequestModel)
        {
            if (ModelState.IsValid)
            {
                var pullRequest = (await GitHubInstance.GetClientInstance().
                        GetNewPullRequestManagerAsync(pullRequestModel.Title, pullRequestModel.RepositoryName,
                        pullRequestModel.HeadBranchName, pullRequestModel.BaseBranchName)).PullRequest;

				var id = await CreateHomeworkViaPullRequest(pullRequestModel.TaskId, pullRequestModel.RepositoryName, pullRequestModel.Number);
	            return id.HasValue ? RedirectToAction("Index", new { pullRequestDataId = id })
								   : RedirectToAction("Index", "Home", new { errorMessage = "Ошибка при обновлении базы данных" });
			}
            else
            {
                ModelState.AddModelError("", @"Заполните все поля!");
                ViewBag.Repos = await GitHubInstance.GetStorageInstance().GetRepositoriesAsync();
                return PartialView(pullRequestModel);
            }
        }


        #region Helper
        private async Task<long?> CreateHomeworkViaPullRequest(long taskId, string repName, int pullRequestNumber)
        {
            var task = _repository.TaskManager.Get(t => t.Id == taskId);
            var student = _repository.UserManager.Get(u => u.Id == User.Identity.GetUserId());
            var homework = new Homework(new HomeworkCreateViewModel { TaskId = taskId }, task, student);

            if (_repository.HomeworkManager.Add(homework))
            {
                var prd = new PullRequestData(homework, repName, pullRequestNumber);

                if (_repository.PullRequestsDataManager.Add(prd))
                {
                    await (new NewPullRequestHomeworkNotification(task, student, repName, pullRequestNumber, Request)).Send();
                    return prd.Id;
                }
            }
	        return null;
        }
        #endregion
    }
}