using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.Repositories
{
    //public class HomeworksManager : BaseManager, IRepository<Homework>
    //{
    //    public HomeworksManager(ApplicationDbContext context) : base(context) { }

    //    public bool Add(Homework item)
    //    {
    //        if (Contains(h => h.Id == item.Id)) return false;
    //        Context.Homeworks.Add(item);
    //        Context.SaveChanges();
    //        return true;
    //    }

    //    public bool Contains(Func<Homework, bool> predicate)
    //    {
    //        return Get(predicate) != null;
    //    }

    //    public bool Delete(Homework item)
    //    {
    //        if (!Contains(h => h.Id == item.Id)) return false;
    //        Context.Homeworks.Remove(item);
    //        Context.SaveChanges();
    //        return true;            
    //    }

    //    public Homework Get(Func<Homework, bool> predicate)
    //    {
    //        return Context.Homeworks.FirstOrDefault(predicate);
    //    }
    //}
}