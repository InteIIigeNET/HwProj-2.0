using HwProj.Models.Roles;

namespace HwProj.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HwProj.Models.Contexts.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HwProj.Models.Contexts.AppDbContext context)
        {
            //context.Roles.Add(RolesFactory.GetById("1"));
            //context.Roles.Add(RolesFactory.GetById("2"));

            base.Seed(context);
        }
    }
}