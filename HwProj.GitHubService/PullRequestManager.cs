using HwProj.Models.GitHub;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    public class PullRequestManager
    {
        private readonly Repository repository;

        public Models.GitHub.PullRequest PullRequest { get; private set; }

        public ReviewRepository ReviewRepository { get; private set; }

        public CommentRepository CommentRepository { get; private set; }

        internal PullRequestManager(Models.GitHub.PullRequest pullRequest, Repository repository)
        {
            PullRequest = pullRequest;
            this.repository = repository;
            ReviewRepository = new ReviewRepository(repository);
            CommentRepository = new CommentRepository(repository);
        }

        public async Task<Models.GitHub.PullRequest> ClosePullRequest()
        {
            var pullRequest = await repository.client?.PullRequest.Update(repository.owner, repository.repName, repository.pullRequestNumber,
                new PullRequestUpdate { State = ItemState.Closed });
            var commits = await repository.client?.PullRequest.Commits(repository.owner, repository.repName, repository.pullRequestNumber);
            return pullRequest.ToPullRequest(commits.ToCommits());
        }
    }
}
