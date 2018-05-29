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
            CreateHomeworkViaPullRequest(pullRequestModel.TaskId, pullRequestModel.RepositoryName, pullRequestModel.Number);

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

            CreateHomeworkViaPullRequest(pullRequestModel.TaskId, pullRequestModel.RepositoryName, pullRequest.Number);

            return RedirectToAction("Index", new
            {
                repName = pullRequestModel.RepositoryName,
                number = pullRequest.Number
            });
        }


        #region Helper
        public async void CreateHomeworkViaPullRequest(long taskId, string repName, int pullRequestNumber)
        {
            var task = _repository.TaskManager.Get(t => t.Id == taskId);
            var student = _repository.UserManager.Get(u => u.Id == User.Identity.GetUserId());
            var homework = new Homework(new HomeworkCreateViewModel { TaskId = taskId }, task, student);

            if (!_repository.HomeworkManager.Add(homework) ||
                !_repository.PullRequestsDataManager.Add(new PullRequestData(_repository.HomeworkManager.GetLastAttempted(taskId, student.Id), repName, pullRequestNumber)))
            {
                //TODO : Сделать страницу ошибок
                AddViewBagError("Ошибка при обновлении базы данных!");
            }
            else
            {
                await(new NewPullRequestHomeworkNotification(task, student, repName, pullRequestNumber, Request)).Send();
                ViewBag.Message = "Решение было успешно добавлено!";
            }
        }

        private void AddViewBagError(string errorMessage)
        {
            ModelState.AddModelError("", errorMessage);
            ViewBag.Message = errorMessage;
            ViewBag.Color = "danger";
        }
        #endregion
    }
}