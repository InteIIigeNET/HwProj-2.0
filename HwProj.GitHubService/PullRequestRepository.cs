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
        public PullRequestRepository(Octokit.GitHubClient client, string owner) : 
            base(client, owner){ }

        public async Task<Models.GitHub.PullRequest> CreatePullRequest(string title, string repName, string branchRef)
        {
            var pullRequest = await client?.PullRequest.Create(owner, repName, new NewPullRequest(title, "master", branchRef));
            return await CreatePullRequestModel(pullRequest, repName, pullRequest.Number);
        }

        public async Task<Models.GitHub.PullRequest> GetPullRequest(string repName, int pullRequestNumber)
        {
            var pullRequest = await client?.PullRequest.Get(owner, repName, pullRequestNumber);
            return await CreatePullRequestModel(pullRequest, repName, pullRequestNumber);
        }

        public async Task<Models.GitHub.PullRequest> ClosePullRequest(string repName, int pullRequestNumber)
        {
            var pullRequest = await client?.PullRequest.Update(owner, repName, pullRequestNumber,
                new PullRequestUpdate { State = ItemState.Closed });
            return await CreatePullRequestModel(pullRequest, repName, pullRequestNumber);
        }

        #region Helpers
        private async Task<Models.GitHub.PullRequest> CreatePullRequestModel(Octokit.PullRequest pullRequest, string repName, int pullRequestNumber)
        {
            var commits = await client?.PullRequest.Commits(owner, repName, pullRequestNumber);
            return pullRequest.ToPullRequest(commits.ToCommits());
        }
        #endregion
    }
}
