namespace HwProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentidinprd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PullRequestsData", "StudentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.PullRequestsData", "StudentId");
            AddForeignKey("dbo.PullRequestsData", "StudentId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PullRequestsData", "StudentId", "dbo.AspNetUsers");
            DropIndex("dbo.PullRequestsData", new[] { "StudentId" });
            DropColumn("dbo.PullRequestsData", "StudentId");
        }
    }
}
