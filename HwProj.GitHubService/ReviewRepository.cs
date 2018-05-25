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
        private readonly PullRequestData data;

        internal ReviewRepository(PullRequestData data)
        {
            this.data = data;
        }
    
        public async Task<Review> CreateReviewAsync(string body, IEnumerable<ReviewComment> comments, ReviewEvent reviewEvent)
        {
            var review = await data.client?.PullRequest.Review.Create(data.owner, data.repName, data.pullRequestNumber, new Octokit.PullRequestReviewCreate
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
