using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.Models.GitHub
{
    class PullRequest
    {
        public User Owner { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Commit> Commits { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
