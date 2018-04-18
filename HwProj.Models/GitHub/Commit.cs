using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.Models.GitHub
{
    class Commit
    {
        public User Owner { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }
}
