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

        public PullRequestRepository PullRequest { get; private set; }

        private Octokit.User user;

        public GitHubClient(string token)
        {
            client = new Octokit.GitHubClient(new ProductHeaderValue("HwProj"))
            {
                Credentials = new Credentials(token)
            };
            PullRequest = new PullRequestRepository(client);
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
    }
}
