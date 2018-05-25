using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    public class GitHubRepositoryStorage
    {
        private Octokit.GitHubClient client;

        public GitHubRepositoryStorage(string token)
        {
            client = new Octokit.GitHubClient(new ProductHeaderValue("HwProj"))
            {
                Credentials = new Credentials(token)
            };
        }

        public async Task<IEnumerable<string>> GetRepositories()
        {
            return from rep in await client?.Repository.GetAllForCurrent()
                   select rep.Name;
        }

        public async Task<IEnumerable<string>> GetBranches(string repName)
        {
            var user = await client?.User.Current();
            return from b in await client?.Repository.Branch.GetAll(user.Login, repName)
                   select b.Name;
        }
    }
}
