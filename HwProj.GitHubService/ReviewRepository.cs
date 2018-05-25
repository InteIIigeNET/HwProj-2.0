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

        public async Task<Review> CreateReview()
        {
            throw new NotImplementedException();
        }
    }
}
