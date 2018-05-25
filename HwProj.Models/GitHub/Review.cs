using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.Models.GitHub
{
    public class Review
    {
        public IEnumerable<ReviewComment> ReviewComments { get; set; }

        public Comment HeadComment { get; set; }

        public long Id { get; set; }
    }
}
