namespace HwProj.Migrations
{
	using HwProj.Models.Roles;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<HwProj.Models.Contexts.ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(HwProj.Models.Contexts.ApplicationDbContext context)
		{
			context.Roles.Add(RolesFactory.GetById("1"));
			context.Roles.Add(RolesFactory.GetById("2"));

			base.Seed(context);
		}
	}
}
