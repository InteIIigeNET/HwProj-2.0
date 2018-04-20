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
    public class Repository
    {
        private GitHubClient client;
        private Octokit.User user;

        public Repository(string token)
        {
            client = new GitHubClient(new ProductHeaderValue("HwProj"))
            {
                Credentials = new Credentials(token)
            };
            user = client.User.Current().Result;
        }

        public async Task<IEnumerable<string>> GetBranches(string repName)
        {
            return from b in await client?.Repository.Branch.GetAll(user.Login, repName)
                   select b.Name;                        
        }

        public async Task<Models.GitHub.PullRequest> CreatePullRequest(string title, string branchRef, string repName)
        {
            var owner = user.Login;
            var pullRequest = await client?.PullRequest.Create(owner, repName, new NewPullRequest(title, "master", branchRef));
            var comments = await client?.PullRequest.ReviewComment.GetAll(owner, repName, pullRequest.Number);
            var commits = await client?.PullRequest.Commits(owner, repName, pullRequest.Number);

            return new Models.GitHub.PullRequest
            {
                Owner = new Models.GitHub.User
                {
                    Login = owner,
                    Url = user.Url
                },
                CreatedAt = pullRequest.CreatedAt.DateTime,
                Comments = from c in comments
                           select new Models.GitHub.Comment
                           {
                               Content = c.Body,
                               Owner = new Models.GitHub.User
                               {
                                   Login = c.User.Login,
                                   Url = c.User.Url
                               }
                           },
                Commits = from c in commits
                          select new Models.GitHub.Commit
                          {
                              Owner = new Models.GitHub.User
                              {
                                  Login = c.Commit.User.Login,
                                  Url = c.Commit.User.Url
                              },
                              CreatedAt = c.Author.Date.DateTime,
                              Title = c.Commit.Message,
                              Url = c.Commit.Url
                          }
            };
        }

    }
}
