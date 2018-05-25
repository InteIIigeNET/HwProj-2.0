using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HwProj.Models.GitHub;
using Octokit;

namespace HwProj.GitHubService
{
    public class CommentRepository
    {
        private readonly PullRequestData data;

        internal CommentRepository(PullRequestData data)
        {
            this.data = data;
        }

        public async Task<ReviewComment> CreateReviewCommentAsync(string body, string commitSHA, string path, int position)
        {
            var comment = await data.client?.PullRequest.ReviewComment.Create(data.owner, data.repName, data.pullRequestNumber,
                new PullRequestReviewCommentCreate(body, commitSHA, path, position));
            return comment.ToReviewComment();
        }

        public async Task<Comment> CreateReplyCommentAsync(string body, int replyTo)
        {
            var replyComment = await data.client?.PullRequest.ReviewComment.CreateReply(data.owner, data.repName, data.pullRequestNumber,
                new PullRequestReviewCommentReplyCreate(body, replyTo));
            return replyComment.ToReviewComment();
        }
    }
}
