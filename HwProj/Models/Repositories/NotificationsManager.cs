using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;

namespace HwProj.Models.Repositories
{
	internal class NotificationsManager : BaseManager<Notification>, IRepository<Notification>
	{
		public bool Add(Notification item)
		{
			return Execute
			(
				context =>
				{
					if (Contains(n => n.Id == item.Id)) return false;
					context.Add(item);
					
					/* Замыкание id */
					item.Text = item.Text.Replace(Notification.ContextId, item.Id.ToString());
					
					return true;
				}
			);
		}

		public bool Delete(Notification item)
		{
			return Execute
			(
				context =>
				{
					if (Contains(c => c.Id == item.Id)) return false;
					context.Remove(item);
					
					return true;
				}
			);
		}
		public bool Delete(long id)
		{
			return Execute
			(
				context =>
				{
					var item = Get(n => n.Id == id);
					if (item == null) return false;
					context.Remove(item);
					
					return true;
				}
			);
		}

		public Notification Get(Func<Notification, bool> predicate)
		{
			return Execute
			(
				context => context.FirstOrDefault(predicate)
			);
		}

		public IEnumerable<Notification> GetAll()
		{
			return Execute
			(
				context => context.Include(n => n.User).ToList()
			);
		}

		public IEnumerable<Notification> GetAll(Func<Notification, bool> predicate)
		{
			return Execute
			(
				context => context.Include(n => n.User).Where(predicate).ToList()
			);
		}

		public bool Contains(Func<Notification, bool> predicate)
		{
			return Execute
			(
				context => Get(predicate) != null
			);
		}

		public NotificationsManager(AppDbContext context) : base(context)
		{
		}
	}
}