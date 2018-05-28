using Octokit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    public class GitHubRepositoryStorage
    {
        private Octokit.GitHubClient client;
        private string owner;

        public GitHubRepositoryStorage(string token)
        {
            client = new Octokit.GitHubClient(new ProductHeaderValue("HwProj"))
            {
                Credentials = new Credentials(token)
            };
            owner = client?.User.Current().Result.Login;
        }

        public async Task<IEnumerable<string>> GetRepositoriesAsync()
        {
            return from rep in await client?.Repository.GetAllForCurrent()
                   select rep.Name;
        }

        public async Task<IEnumerable<string>> GetBranchesAsync(string repName)
        {
            return from b in await client?.Repository.Branch.GetAll(owner, repName)
                   select b.Name;
        }

        public async Task<IEnumerable<string>> GetPullRequests(string repName)
        {
            return from pr in await client?.PullRequest.GetAllForRepository(owner, repName)
                   select pr.Number + " " + pr.Title;
        }
    }
}
