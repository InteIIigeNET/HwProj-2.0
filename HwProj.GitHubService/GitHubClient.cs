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
        public PullRequestRepository PullRequest { get; private set; }

        public ReviewRepository ReviewRepository { get; private set; }

        public CommentRepository CommentRepository { get; private set; }

        public GitHubClient(string token)
        {
            var client = new Octokit.GitHubClient(new ProductHeaderValue("HwProj"))
            {
                Credentials = new Credentials(token)
            };
            var owner = client?.User.Current().Result.Login;
            PullRequest = new PullRequestRepository(client, owner);
            ReviewRepository = new ReviewRepository(client, owner);
            CommentRepository = new CommentRepository(client, owner);
        }       
    }
}
