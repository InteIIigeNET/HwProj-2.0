﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;

namespace HwProj.Models.Repositories
{
	public class NotificationsManager : BaseManager, IRepository<Notification>
	{
		public NotificationsManager(ApplicationDbContext context) : base(context)
		{
		}
		public bool Add(Notification item)
		{
			if (Contains(n => n.Id == item.Id)) return false;
			Context.Notifications.Add(item);
			Context.SaveChanges();
			/* Замыкание id */
			item.Text = item.Text.Replace(Notification.ContextId, item.Id.ToString());
			Context.SaveChanges();
			return true;
		}

		public bool Delete(Notification item)
		{
			if (Contains(c => c.Id == item.Id)) return false;
			Context.Notifications.Remove(item);
			Context.SaveChanges();
			return true;
		}
		public bool Delete(long id)
		{
			var item = Get(n => n.Id == id);
			if (item == null) return false;
			Context.Notifications.Remove(item);
			Context.SaveChanges();
			return true;
		}

		public Notification Get(Func<Notification, bool> predicate)
		{
			return Context.Notifications.FirstOrDefault(predicate);
		}

		public IEnumerable<Notification> GetAll()
		{
			return Context.Notifications.Include(n => n.User).AsEnumerable();
		}

		public IEnumerable<Notification> GetAll(Func<Notification, bool> predicate)
		{
			return Context.Notifications.Include(n => n.User).Where(predicate).AsEnumerable();
		} 

		public bool Contains(Func<Notification, bool> predicate)
		{
			return Get(predicate) != null;
		}
	}
}