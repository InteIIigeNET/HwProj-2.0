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
    public class GitHubClient
    {
        private Octokit.GitHubClient client;

        public GitHubClient(string token)
        {
            client = new Octokit.GitHubClient(new ProductHeaderValue("HwProj"))
            {
                Credentials = new Credentials(token)
            };
        }       

        public async Task<PullRequestManager> GetNewPullRequestManagerAsync(string title, string repName, string branchRef)
        {
            var owner = (await client.User.Current()).Login;
            var pullRequest = await CreatePullRequestAsync(title, repName, branchRef, owner);
            return new PullRequestManager(pullRequest, new PullRequestData(client, owner, repName, pullRequest.Number));
        }

        public async Task<PullRequestManager> GetExistPullRequestManagerAsync(string repName, int pullRequestNumber)
        {
            var owner = (await client.User.Current()).Login;
            var pullRequest = await GetPullRequestAsync(repName, pullRequestNumber, owner);
            return new PullRequestManager(pullRequest, new PullRequestData(client, owner, repName, pullRequest.Number));
        }

        private async Task<Models.GitHub.PullRequest> GetPullRequestAsync(string repName, int pullRequestNumber, string owner)
        {
            var pullRequest = await client?.PullRequest.Get(owner, repName, pullRequestNumber);
            var commits = await client?.PullRequest.Commits(owner, repName, pullRequestNumber);
            return pullRequest.ToPullRequest(commits.ToCommits());
        }

        private async Task<Models.GitHub.PullRequest> CreatePullRequestAsync(string title, string repName, string branchRef, string owner)
        {
            var pullRequest = await client?.PullRequest.Create(owner, repName, new NewPullRequest(title, "master", branchRef));
            var commits = await client?.PullRequest.Commits(owner, repName, pullRequest.Number);
            return pullRequest.ToPullRequest(commits.ToCommits());
        }
    }
}
