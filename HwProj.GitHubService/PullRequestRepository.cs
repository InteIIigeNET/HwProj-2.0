using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    public class PullRequestRepository
    {
        private readonly Repository repository;

        internal PullRequestRepository(Repository repository)
        {
            this.repository = repository;
        }

        public async Task<Models.GitHub.PullRequest> CreatePullRequest(string title, string repName, string branchRef)
        {
            var pullRequest = await repository.client?.PullRequest.Create(repository.owner, repName, new NewPullRequest(title, "master", branchRef));
            repository.pullRequestNumber = pullRequest.Number;
            return await CreatePullRequestModel(pullRequest, repName, pullRequest.Number);
        }

        public async Task<Models.GitHub.PullRequest> GetPullRequest(string repName, int pullRequestNumber)
        {
            var pullRequest = await repository.client?.PullRequest.Get(repository.owner, repName, pullRequestNumber);
            return await CreatePullRequestModel(pullRequest, repName, pullRequestNumber);
        }

        public async Task<Models.GitHub.PullRequest> ClosePullRequest(string repName, int pullRequestNumber)
        {
            var pullRequest = await repository.client?.PullRequest.Update(repository.owner, repName, pullRequestNumber,
                new PullRequestUpdate { State = ItemState.Closed });
            return await CreatePullRequestModel(pullRequest, repName, pullRequestNumber);
        }

        public async Task<Models.GitHub.PullRequest> CreatePullRequest(string title, string branchRef)
        {
            var pullRequest = await repository.client?.PullRequest.Create(repository.owner, repository.repName, new NewPullRequest(title, "master", branchRef));
            repository.pullRequestNumber = pullRequest.Number;
            return await CreatePullRequestModel(pullRequest, repository.repName, pullRequest.Number);
        }

        public async Task<Models.GitHub.PullRequest> GetPullRequest()
        {
            if (repository.pullRequestNumber == null)
                throw new ArgumentNullException("First create pull request or use overload");
            var pullRequest = await repository.client?.PullRequest.Get(repository.owner, repository.repName, (int)repository.pullRequestNumber);
            return await CreatePullRequestModel(pullRequest, repository.repName, (int)repository.pullRequestNumber);
        }

        public async Task<Models.GitHub.PullRequest> ClosePullRequest()
        {
            if (repository.pullRequestNumber == null)
                throw new ArgumentNullException("First create pull request or use overload");
            var pullRequest = await repository.client?.PullRequest.Update(repository.owner, repository.repName, (int)repository.pullRequestNumber,
                new PullRequestUpdate { State = ItemState.Closed });
            return await CreatePullRequestModel(pullRequest, repository.repName, (int)repository.pullRequestNumber);
        }

        #region Helpers
        private async Task<Models.GitHub.PullRequest> CreatePullRequestModel(Octokit.PullRequest pullRequest, string repName, int pullRequestNumber)
        {
            var commits = await repository.client?.PullRequest.Commits(repository.owner, repName, pullRequestNumber);
            return pullRequest.ToPullRequest(commits.ToCommits());
        }
        #endregion
    }
}
