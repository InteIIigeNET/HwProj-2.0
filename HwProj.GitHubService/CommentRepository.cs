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
        private readonly Repository repository;

        internal CommentRepository(Repository repository)
        {
            this.repository = repository;
        }

        public async Task<ReviewComment> CreateReviewComment(string repName, int pullRequestNumber, string body, string commitSHA, string path, int position)
        {
            var comment = await repository.client?.PullRequest.ReviewComment.Create(repository.owner, repName, pullRequestNumber, 
                new PullRequestReviewCommentCreate(body, commitSHA, path, position));
            return comment.ToReviewComment();
        }

        public async Task<Comment> CreateReplyComment(string repName, int pullRequestNumber, string body, int replyTo)
        {
            var replyComment = await repository.client?.PullRequest.ReviewComment.CreateReply(repository.owner, repName, pullRequestNumber, 
                new PullRequestReviewCommentReplyCreate(body, replyTo));
            return replyComment.ToReviewComment();
        }

        public async Task<ReviewComment> CreateReviewComment(string body, string commitSHA, string path, int position)
        {
            if (repository.pullRequestNumber == null)
                throw new ArgumentNullException("First create pull request or use overload");
            var comment = await repository.client?.PullRequest.ReviewComment.Create(repository.owner, repository.repName, (int)repository.pullRequestNumber,
                new PullRequestReviewCommentCreate(body, commitSHA, path, position));
            return comment.ToReviewComment();
        }

        public async Task<Comment> CreateReplyComment(string body, int replyTo)
        {
            if (repository.pullRequestNumber == null)
                throw new ArgumentNullException("First create pull request or use overload");
            var replyComment = await repository.client?.PullRequest.ReviewComment.CreateReply(repository.owner, repository.repName, (int)repository.pullRequestNumber,
                new PullRequestReviewCommentReplyCreate(body, replyTo));
            return replyComment.ToReviewComment();
        }
    }
}
