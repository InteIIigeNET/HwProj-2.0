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

        public GitHubClient(string login, string password)
        {
            client = new Octokit.GitHubClient(new ProductHeaderValue("HwProj"))
            {
                Credentials = new Credentials(login, password)
            };
        }

        public async Task<PullRequestManager> GetNewPullRequestManagerAsync(string title, string repName, string headBranchName, string baseBranchName)
        {
            var owner = (await client.User.Current()).Login;
            var pullRequest = await CreatePullRequestAsync(title, repName, headBranchName, owner, baseBranchName);
            return new PullRequestManager(pullRequest, new PullRequestData(client, owner, repName, pullRequest.Number));
        }

        public async Task<PullRequestManager> GetExistPullRequestManagerAsync(string repName, int pullRequestNumber)
        {
            var owner = (await client.User.Current()).Login;
            var data = new PullRequestData(client, owner, repName, pullRequestNumber);
            var pullRequest = await GetPullRequestAsync(data);
            return new PullRequestManager(pullRequest, data);
        }

        private async Task<Models.GitHub.PullRequest> GetPullRequestAsync(PullRequestData data)
        {
            var pullRequest = await client?.PullRequest.Get(data.owner, data.repName, data.pullRequestNumber);
            var commits = await client?.PullRequest.Commits(data.owner, data.repName, data.pullRequestNumber);
            var reviewRep = new ReviewRepository(data);
            var reviews = await reviewRep.GetAllReviewAsync();
            return pullRequest.ToPullRequest(commits.ToCommits(), reviews);
        }

        private async Task<Models.GitHub.PullRequest> CreatePullRequestAsync(string title, string repName, string headBranchName, string owner, string baseBranchName)
        {
            try
            {
                var pullRequest = await client?.PullRequest.Create(owner, repName, new NewPullRequest(title, headBranchName, baseBranchName));
            
            var commits = await client?.PullRequest.Commits(owner, repName, pullRequest.Number);
            return pullRequest.ToPullRequest(commits.ToCommits(), null);
            }
            catch (Octokit.ApiValidationException)
            {
                throw new ArgumentException($"PR from {headBranchName} to {baseBranchName} is already exist.");
            }
        }
    }
}
