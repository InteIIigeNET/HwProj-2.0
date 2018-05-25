using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace HwProj.GitHubService
{
    internal class PullRequestData
    {
        public readonly Octokit.GitHubClient client;
        public readonly string owner;
        public readonly string repName;
        public readonly int pullRequestNumber;

        public PullRequestData(Octokit.GitHubClient client, string owner, string repName, int pullRequestNumber)
        {
            this.client = client;
            this.owner = owner;
            this.repName = repName;
            this.pullRequestNumber = pullRequestNumber;
        }
    }
}
