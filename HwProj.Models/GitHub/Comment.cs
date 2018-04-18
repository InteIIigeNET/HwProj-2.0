using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.Models.GitHub
{
    class Comment
    {
        public User Owner { get; set; }

        public string Content { get; set; }
    }
}
