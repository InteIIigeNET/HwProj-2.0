using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.Contexts
{
	public class ApplicationContext : BaseContext
	{
		public static ApplicationContext Create()
		{
			return new ApplicationContext();
		}
	}
}