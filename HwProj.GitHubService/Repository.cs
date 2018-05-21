using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HwProj.Models.GitHub;

namespace HwProj.GitHubService
{
    public class Repository : IDisposable
    {
        private GitHubClient client;
        private Octokit.User user;

        public Repository(string token)
        {
            client = new GitHubClient(new ProductHeaderValue("HwProj"))
            {
                Credentials = new Credentials(token)
            };
            user = client.User.Current().Result;
        }

        public async Task<IEnumerable<string>> GetRepositories()
        {
            return from rep in await client?.Repository.GetAllForCurrent()
                   select rep.Name;
        }

        public async Task<IEnumerable<string>> GetBranches(string repName)
        {
            return from b in await client?.Repository.Branch.GetAll(user.Login, repName)
                   select b.Name;                        
        }

        #region PullRequest's methods
        public async Task<Models.GitHub.PullRequest> CreatePullRequest(string title, string branchRef, string repName)
        {
            var owner = user.Login;
            var pullRequest = await client?.PullRequest.Create(owner, repName, new NewPullRequest(title, "master", branchRef));
            return await CreatePullRequestModel(repName, pullRequest.Number, pullRequest);
        }

        public async Task<Models.GitHub.PullRequest> GetPullRequest(string repName, int pullRequestNumber)
        {
            var pullRequest = await client?.PullRequest.Get(user.Login, repName, pullRequestNumber);
            return await CreatePullRequestModel(repName, pullRequestNumber, pullRequest);
        }

        public async Task<Models.GitHub.PullRequest> ClosePullRequest(string repName, int pullRequestNumber)
        {
            var pullRequest = await client?.PullRequest.Update(user.Login, repName, pullRequestNumber,
                new PullRequestUpdate { State = ItemState.Closed });
            return await CreatePullRequestModel(repName, pullRequestNumber, pullRequest);
        } 
        #endregion

        public async Task<Review> CreateReview(string content, Models.GitHub.PullRequest pullRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<ReviewComment> CreateReviewComment(string content, Models.GitHub.PullRequest pullRequest)
        {
            throw new NotImplementedException();
        }

        #region Helpers
        private async Task<Models.GitHub.PullRequest> CreatePullRequestModel(string repName, int pullRequestNumber, Octokit.PullRequest pullRequest)
        {
            var commits = await client?.PullRequest.Commits(user.Login, repName, pullRequestNumber);
            return pullRequest.ConvertToGitHubModel(commits.ConvertToGitHubModel());
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
