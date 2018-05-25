using HwProj.Models.GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    public class ReviewRepository
    {
        private readonly Repository repository;

        internal ReviewRepository(Repository repository)
        {
            this.repository = repository;
        }
    
        public async Task<Review> CreateReview(string body, IEnumerable<ReviewComment> comments, ReviewEvent reviewEvent)
        {
            var review = await repository.client?.PullRequest.Review.Create(repository.owner, repository.repName, repository.pullRequestNumber, new Octokit.PullRequestReviewCreate
            {
                Body = body,
                Comments = (from c in comments
                            select new Octokit.DraftPullRequestReviewComment(c.Content, c.Path, c.Position)).ToList(),
                Event = (Octokit.PullRequestReviewEvent)reviewEvent
            });
            return review.ToReview(comments);
        }
    }
}
