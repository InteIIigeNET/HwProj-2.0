using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HwProj.Models.GitHub;
using Octokit;

namespace HwProj.GitHubService
{
    public class CommentRepository : Repository
    {
        public CommentRepository(Octokit.GitHubClient client, string owner) :
            base(client, owner){ }

        public async Task<ReviewComment> CreateReviewComment(string repName, int pullRequestNumber, string body, string commitSHA, string path, int position)
        {
            var comment = await client?.PullRequest.ReviewComment.Create(owner, repName, pullRequestNumber, 
                new PullRequestReviewCommentCreate(body, commitSHA, path, position));
            return comment.ToReviewComment();
        }

        public async Task<Comment> CreateReplyComment(string repName, int pullRequestNumber, string body, int replyTo)
        {
            var replyComment = await client?.PullRequest.ReviewComment.CreateReply(owner, repName, pullRequestNumber, 
                new PullRequestReviewCommentReplyCreate(body, replyTo));
            return replyComment.ToReviewComment();
        }
    }
}
