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
    public class Repository : IDisposable
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

        public async Task<Models.GitHub.PullRequest> CreatePullRequest(string title, string branchRef, string repName)
        {
            var owner = user.Login;
            var pullRequest = await client?.PullRequest.Create(owner, repName, new NewPullRequest(title, "master", branchRef));
            var commits = await client?.PullRequest.Commits(owner, repName, pullRequest.Number);
            return new Models.GitHub.PullRequest
            {
                DiffUrl = pullRequest.DiffUrl,
                RepositoryName = repName,
                Number = pullRequest.Number,
                Owner = new Models.GitHub.User
                {
                    Login = owner,
                    Url = user.Url
                },
                CreatedAt = pullRequest.CreatedAt.DateTime,
                Commits = from c in commits
                          select new Models.GitHub.Commit
                          {
                              Sha = c.Commit.Sha,
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

        public async Task<ReviewComment> CreateReviewComment(string content, Models.GitHub.PullRequest pullRequest)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
