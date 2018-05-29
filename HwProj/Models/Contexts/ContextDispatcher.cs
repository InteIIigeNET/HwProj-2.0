using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Models.Repositories;
using Microsoft.Owin;
using Owin;

namespace HwProj.Models.Contexts
{
	internal static class ContextDispatcher
	{
		static readonly AppDbContext Context = AppDbContext.Create();
		public static AppDbContext GetContext(this MainRepository obj)
		{
			return Context;
		}
		public static AppDbContext GetContext(this IOwinContext obj)
		{
			return Context;
		}
	}
}