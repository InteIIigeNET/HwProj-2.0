using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    public class Class1
    {
        private GitHubClient client;
        private string userName;

        public Class1(string token)
        {
            client = new GitHubClient(new ProductHeaderValue("HwProj"))
            {
                Credentials = new Credentials(token)
            };
            userName = client.User.Current().Result.Login;
        }

        public async Task<IEnumerable<string>> GetBranches(string repName)
        {
            return from b in await client?.Repository.Branch.GetAll(userName, repName)
                   select b.Name;                        
        }

        public async void CreatePullRequest(string title, string branchRef, string repName, string owner)
        {
            var pullRequest = await client?.PullRequest.Create(owner, repName, new NewPullRequest(title, "master", branchRef));
            var comments = await client?.PullRequest.ReviewComment.GetAll(owner, repName, pullRequest.Number);
            var commits = await client?.PullRequest.Commits(owner, repName, pullRequest.Number);
            

        }

    }
}
