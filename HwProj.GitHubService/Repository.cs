using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    public abstract class Repository
    {
        protected readonly Octokit.GitHubClient client;

        public Repository(Octokit.GitHubClient client)
        {
            this.client = client;
        }
    }
}
