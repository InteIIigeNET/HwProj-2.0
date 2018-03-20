using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.Repositories
{
    public class HomeworksManager : IRepository<Homework>
    {
        public bool Add(Homework item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Homework item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Homework Get(Func<Homework, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}