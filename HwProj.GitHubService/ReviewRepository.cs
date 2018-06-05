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
            var draftComments = (from c in comments
                                select new Octokit.DraftPullRequestReviewComment(c.Content, c.Path, (int)c.Position)).ToList();
            var review = await data.client?.PullRequest.Review.Create(data.owner, data.repName, data.pullRequestNumber, new Octokit.PullRequestReviewCreate
            {
                Body = body,
                Comments = draftComments,
                Event = (Octokit.PullRequestReviewEvent)reviewEvent
            });
            return review.ToReview(comments);
        }

        public async Task<IEnumerable<Review>> GetAllReviewAsync()
        {
            var reviews = await data.client.PullRequest.Review.GetAll(data.owner, data.repName,
                data.pullRequestNumber, Octokit.ApiOptions.None);
            var commentRep = new CommentRepository(data);
            var comments = await commentRep.GetAllReviewCommentsAsync();
            return from r in reviews
                   select r.ToReview(from c in comments
                                     where c.ReviewId == r.Id
                                     select c);
        }
    }
}
