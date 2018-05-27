using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.Models.GitHub
{
    public class Commit
    {
        public User Owner { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Sha { get; set; }
    }
}
