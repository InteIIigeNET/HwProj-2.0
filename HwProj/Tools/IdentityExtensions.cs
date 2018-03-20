using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace HwProj.Tools
{
	public static class IdentityExtensions
	{
		public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			var ci = identity as ClaimsIdentity;
			if (ci != null)
			{
				var id = ci.FindFirst(ClaimTypes.NameIdentifier);
				if (id != null)
				{
					return (T)Convert.ChangeType(id.Value, typeof(T), CultureInfo.InvariantCulture);
				}
			}
			return default(T);
		}
		public static string GetUserRole(this IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			var ci = identity as ClaimsIdentity;
			string role = "";
			if (ci != null)
			{
				var id = ci.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
				if (id != null)
					role = id.Value;
			}
			return role;
		}
		public static string GetUserSurname(this IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			var ci = identity as ClaimsIdentity;
			string surname = "";
			if (ci != null)
			{
				var id = ci.FindFirst(ClaimTypes.Surname);
				if (id != null)
					surname = id.Value;
			}
			return surname;
		}
	}
}