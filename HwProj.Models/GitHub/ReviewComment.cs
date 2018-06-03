using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.Models.GitHub
{
    public class ReviewComment : Comment
    {

        public string DiffHunk { get; set; }

        public int? Position { get; set; }

        public string Path { get; set; }

        public int Id { get; set; }

        public int? ReplyToId { get; set; }

        public int? ReviewId { get; set; }
    }
}
