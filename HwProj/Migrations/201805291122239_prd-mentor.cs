namespace HwProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prdmentor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PullRequestsData", "MentorId", c => c.Long(nullable: false));
            AddColumn("dbo.PullRequestsData", "Mentor_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.PullRequestsData", "Mentor_Id");
            AddForeignKey("dbo.PullRequestsData", "Mentor_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PullRequestsData", "Mentor_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PullRequestsData", new[] { "Mentor_Id" });
            DropColumn("dbo.PullRequestsData", "Mentor_Id");
            DropColumn("dbo.PullRequestsData", "MentorId");
        }
    }
}
