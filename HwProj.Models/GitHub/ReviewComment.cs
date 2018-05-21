using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.Models.GitHub
{
    public class ReviewComment : Comment
    {
        public User Owner { get; set; }

        public string DiffHunk { get; set; }
    }
}
