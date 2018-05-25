using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace HwProj.GitHubService
{
    public abstract class Repository
    {
        protected readonly Octokit.GitHubClient client;
        protected readonly string owner;

        public Repository(Octokit.GitHubClient client, string owner)
        {
            this.client = client;
            this.owner = owner;
        }
    }
}
