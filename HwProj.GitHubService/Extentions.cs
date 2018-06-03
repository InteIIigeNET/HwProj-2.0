using HwProj.Models.GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    internal static class Extentions
    {

        public static IEnumerable<Commit> ToCommits(this IReadOnlyList<Octokit.PullRequestCommit> commits)
        {
            return from c in commits
                   select new Commit
                   {
                       Sha = c.Sha,
                       Owner = new Models.GitHub.User
                       {
                           Login = c.Author.Name,
                           Url = c.Commit.User?.HtmlUrl
                       },
                       Url = c.HtmlUrl,
                       Message = c.Commit.Message
                   };
        }


        public static PullRequest ToPullRequest(this Octokit.PullRequest pullRequest, IEnumerable<Commit> commits, IEnumerable<Review> reviews, string reposName)
        {
            var user = pullRequest.User;
            var diffParser = new DiffParser(pullRequest.DiffUrl);
            return new PullRequest
            {
                RepositoryName = reposName,
                Title = pullRequest.Title,
                Id = pullRequest.Id,
                DiffUrl = pullRequest.DiffUrl,
                Number = pullRequest.Number,
                Owner = new User
                {
                    Login = user.Login,
                    Url = user.HtmlUrl
                },
                CreatedAt = pullRequest.CreatedAt.DateTime,
                Commits = commits,
                DiffFiles = diffParser.DiffFiles,
                Reviews = reviews
            };
        }

        public static ReviewComment ToReviewComment(this Octokit.PullRequestReviewComment comment) => new ReviewComment
        {
            Id = comment.Id,
            ReplyToId = comment.InReplyToId,
            Content = comment.Body,
            DiffHunk = comment.DiffHunk,
            Owner = new User
            {
                Login = comment.User.Login,
                Url = comment.User.HtmlUrl
            },
            Path = comment.Path,
            Position = comment.Position,
            ReviewId = comment.PullRequestReviewId
        };

        public static Review ToReview(this Octokit.PullRequestReview review, IEnumerable<ReviewComment> comments)
        {
            return new Review
            {
                HeadComment = new Comment
                {
                    Content = review.Body,
                    Owner = new User
                    {
                        Login = review.User.Login,
                        Url = review.User.HtmlUrl
                    }
                },
                ReviewComments = comments,
                Id = review.Id
            };
        }
    }
}
