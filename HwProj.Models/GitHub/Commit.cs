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

        public string Label { get; set; }

        public string Message { get; set; }

        public string Url { get; set; }
        
        public string Sha { get; set; }
    }
}
