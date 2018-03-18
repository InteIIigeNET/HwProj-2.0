using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HwProj.Models.Roles
{
	public class ApplicationRole : IdentityRole
	{
		public ApplicationRole() { }
		public string Description { get; set; }
	}
}