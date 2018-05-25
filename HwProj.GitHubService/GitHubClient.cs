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

        public GitHubClient(string token, string repName)
        {
            client = new Octokit.GitHubClient(new ProductHeaderValue("HwProj"))
            {
                Credentials = new Credentials(token)
            };
        }       

        public async Task<PullRequestManager> GetNewPullRequestManager(string title, string repName, string branchRef)
        {
            var owner = (await client.User.Current()).Login;
            var pullRequest = await CreatePullRequest(title, repName, branchRef, owner);
            return new PullRequestManager(pullRequest, new Repository(client, owner, repName, pullRequest.Number));
        }

        public async Task<PullRequestManager> GetExistPullRequestManager(string repName, int pullRequestNumber)
        {
            var owner = (await client.User.Current()).Login;
            var pullRequest = await GetPullRequest(repName, pullRequestNumber, owner);
            return new PullRequestManager(pullRequest, new Repository(client, owner, repName, pullRequest.Number));
        }

        private async Task<Models.GitHub.PullRequest> GetPullRequest(string repName, int pullRequestNumber, string owner)
        {
            var pullRequest = await client?.PullRequest.Get(owner, repName, pullRequestNumber);
            var commits = await client?.PullRequest.Commits(owner, repName, pullRequestNumber);
            return pullRequest.ToPullRequest(commits.ToCommits());
        }

        private async Task<Models.GitHub.PullRequest> CreatePullRequest(string title, string repName, string branchRef, string owner)
        {
            var pullRequest = await client?.PullRequest.Create(owner, repName, new NewPullRequest(title, "master", branchRef));
            var commits = await client?.PullRequest.Commits(owner, repName, pullRequest.Number);
            return pullRequest.ToPullRequest(commits.ToCommits());
        }
    }
}
