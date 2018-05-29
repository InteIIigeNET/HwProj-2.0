using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace HwProj.Models.Repositories
{
    internal class PullRequestsDataManager : BaseManager<PullRequestData>, IRepository<PullRequestData>
    {
        public bool Add(PullRequestData item)
        {
            return Execute
            (
                    context =>
                    {
                        if (Contains(pr => pr.Id == item.Id)) return false;
                        context.Add(item);
                        SaveChanges();
                        return true;
                    }
            );
        }

        public bool Contains(Func<PullRequestData, bool> predicate)
        {
            return Execute
            (
                context => Get(predicate) != null
            );
        }

        public bool Delete(PullRequestData item)
        {
            return Execute
            (
                context =>
                {
                    if (!Contains(p => p.Id == item.Id)) return false;
                    context.Remove(item);
                    SaveChanges();
                    return true;
                }
            );
        }

        public PullRequestData Get(Func<PullRequestData, bool> predicate)
        {
            return Execute
            (
                context =>
                {
                    //можно заинклудить hw и ментора, но они мне не нужны пока что.
                    return context.FirstOrDefault(predicate);
                }
            );
        }

        public IEnumerable<PullRequestData> GetAll()
        {
            return Execute
            (
                context =>
                {
                    return context.Include(p => p.Homework).ToList();
                }
            );
        }

        public IEnumerable<PullRequestData> GetAll(Func<PullRequestData, bool> predicate)
        {
            return Execute
            (
                context => context.Where(predicate).ToList()
            );
        }

        public PullRequestsDataManager(AppDbContext context) : base(context)
        {
        }
    }
}