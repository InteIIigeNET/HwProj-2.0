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
		private static T GetClaim<T>(IIdentity identity, Func<ClaimsIdentity, T> Getter)
		{
			if (identity == null)
			{
				throw new ArgumentNullException(nameof(identity));
			}
			if (!(identity is ClaimsIdentity ci))
				throw new ArgumentException("Not claims identity");
			var result = Getter(ci);
			return result;
		}
		public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
		{
			return GetClaim(identity,
				ci =>
				{
					var id = ci.FindFirst(ClaimTypes.NameIdentifier);
					if (id != null)
					{
						return (T)Convert.ChangeType(id.Value, typeof(T), CultureInfo.InvariantCulture);
					}
					return default(T);
				});
		}

		public static string GetUserRole(this IIdentity identity)
		{
			return GetClaim(identity,
				ci =>
				{
					var id = ci.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
					return id?.Value;
				});
		}
		public static string GetUserSurname(this IIdentity identity)
		{
			return GetClaim(identity,
				ci =>
				{
					var id = ci.FindFirst(ClaimTypes.Surname);
					return id?.Value;
				});
		}

        public static string GetUserFirstName(this IIdentity identity)
        {
	        return GetClaim(identity,
		        ci =>
		        {
				    var id = ci.FindFirst(ClaimTypes.Name);
				    return id?.Value;
		        });
        }

		public static string GetUserFullName(this IIdentity identity)
		{
			return GetClaim(identity,
				ci =>
				{
					var id = ci.FindFirst(ClaimTypes.GivenName);
					return id?.Value;
				});
	}

        public static string GetUserEmail(this IIdentity identity)
        {
			return GetClaim(identity,
				ci =>
				{
					var id = ci.FindFirst(ClaimTypes.Email);
					return id?.Value;
				});
        }

		public static string GetGitHubToken(this IIdentity identity)
		{
			return GetClaim(identity,
				ci =>
				{
					var id = ci.FindFirst(c => c.Type.Equals("GitHubAccessToken"));
					return id?.Value;
				});
		}
	}
}