﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;
using HwProj.Models.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace HwProj.Models.Roles
{
	class ApplicationRoleManager : RoleManager<ApplicationRole>
	{
		public ApplicationRoleManager(RoleStore<ApplicationRole> store)
			: base(store)
		{ }
		public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
			IOwinContext context)
		{
			return new ApplicationRoleManager(new
				RoleStore<ApplicationRole>(context.Get<ApplicationDbContext>()));
		}
	}

	public static class RolesFactory
	{
		public static ApplicationRole GetById(string id)
		{
			switch (id)
			{
				case "1":
					return new ApplicationRole() {Id = "1", Name = RoleType.Преподаватель.ToString()};
				case "2":
				default:
					return new ApplicationRole() { Id = "2", Name = RoleType.Студент.ToString() };
			}
		}

		public static string GetName(this string roleId)
		{
			switch (roleId)
			{
				case "1":
					return RoleType.Преподаватель.ToString();
				case "2":
				default:
					return RoleType.Студент.ToString();
			}
		}
	}
}