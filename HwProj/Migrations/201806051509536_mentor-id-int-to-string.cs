namespace HwProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mentoridinttostring : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PullRequestsData", new[] { "Mentor_Id" });
            DropColumn("dbo.PullRequestsData", "MentorId");
            RenameColumn(table: "dbo.PullRequestsData", name: "Mentor_Id", newName: "MentorId");
            AlterColumn("dbo.PullRequestsData", "MentorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.PullRequestsData", "MentorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PullRequestsData", new[] { "MentorId" });
            AlterColumn("dbo.PullRequestsData", "MentorId", c => c.Long(nullable: false));
            RenameColumn(table: "dbo.PullRequestsData", name: "MentorId", newName: "Mentor_Id");
            AddColumn("dbo.PullRequestsData", "MentorId", c => c.Long(nullable: false));
            CreateIndex("dbo.PullRequestsData", "Mentor_Id");
        }
    }
}
