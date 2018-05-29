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
        private readonly PullRequestData data;

        public Models.GitHub.PullRequest PullRequest { get; private set; }

        public ReviewRepository ReviewRepository { get; private set; }

        public CommentRepository CommentRepository { get; private set; }

        internal PullRequestManager(Models.GitHub.PullRequest pullRequest, PullRequestData data)
        {
            PullRequest = pullRequest;
            this.data = data;
            ReviewRepository = new ReviewRepository(data);
            CommentRepository = new CommentRepository(data);
        }

        public async Task<Models.GitHub.PullRequest> ClosePullRequestAsync()
        {
            var pullRequest = await data.client?.PullRequest.Update(data.owner, data.repName, data.pullRequestNumber,
                new PullRequestUpdate { State = ItemState.Closed });
            var commits = await data.client?.PullRequest.Commits(data.owner, data.repName, data.pullRequestNumber);
            var reviews = await ReviewRepository.GetAllReviewAsync();
            return pullRequest.ToPullRequest(commits.ToCommits(), reviews, data.repName);
        }
    }
}
