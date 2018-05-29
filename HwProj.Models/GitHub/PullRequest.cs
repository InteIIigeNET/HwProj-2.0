using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.Models.GitHub
{
    public class PullRequest
    {
        public string Title { get; set; }

        public User Owner { get; set; }

        public IEnumerable<Review> Reviews { get; set; }

        public IEnumerable<Commit> Commits { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Number { get; set; }

        public string DiffUrl { get; set; }

        public IEnumerable<DiffFile> DiffFiles { get; set; }

        public long Id { get; set; }

        public string RepositoryName { get; set; }
    }
}
