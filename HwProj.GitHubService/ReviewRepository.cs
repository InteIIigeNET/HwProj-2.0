using HwProj.Models.GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    public class ReviewRepository : Repository
    {
        public ReviewRepository(Octokit.GitHubClient client, string owner) :
            base(client, owner){ }

        public async Task<Review> CreateReview()
        {
            throw new NotImplementedException();
        }
    }
}
