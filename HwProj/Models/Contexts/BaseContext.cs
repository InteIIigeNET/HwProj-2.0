using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HwProj.Models.Contexts
{
    public class BaseContext : DbContext
    {
        public BaseContext() : base("Hw2") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasMany(c => c.Users)
                .WithMany(u => u.Courses)
                .Map(t => t.MapLeftKey("CourseId")
                .MapRightKey("UserId")
                .ToTable("CourseMates"));
        }
    }
}