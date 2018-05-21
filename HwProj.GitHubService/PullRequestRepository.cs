using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    public class PullRequestRepository : Repository
    {
        private readonly string userLogin;

        public PullRequestRepository(Octokit.GitHubClient client) : base(client)
        {
            userLogin = client?.User.Current().Result.Login;
        }

        public async Task<Models.GitHub.PullRequest> CreatePullRequest(string title, string branchRef, string repName)
        {
            var pullRequest = await client?.PullRequest.Create(userLogin, repName, new NewPullRequest(title, "master", branchRef));
            return await CreatePullRequestModel(repName, pullRequest.Number, pullRequest);
        }

        public async Task<Models.GitHub.PullRequest> GetPullRequest(string repName, int pullRequestNumber)
        {
            var pullRequest = await client?.PullRequest.Get(userLogin, repName, pullRequestNumber);
            return await CreatePullRequestModel(repName, pullRequestNumber, pullRequest);
        }

        public async Task<Models.GitHub.PullRequest> ClosePullRequest(string repName, int pullRequestNumber)
        {
            var pullRequest = await client?.PullRequest.Update(userLogin, repName, pullRequestNumber,
                new PullRequestUpdate { State = ItemState.Closed });
            return await CreatePullRequestModel(repName, pullRequestNumber, pullRequest);
        }

        #region Helpers
        private async Task<Models.GitHub.PullRequest> CreatePullRequestModel(string repName, int pullRequestNumber, Octokit.PullRequest pullRequest)
        {
            var commits = await client?.PullRequest.Commits(userLogin, repName, pullRequestNumber);
            return pullRequest.ConvertToGitHubModel(commits.ConvertToGitHubModel());
        }
        #endregion
    }
}
