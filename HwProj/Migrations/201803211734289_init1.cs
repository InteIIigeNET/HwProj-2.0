namespace HwProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Teachers", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.Teachers", new[] { "Id" });
            DropTable("dbo.Students");
            DropTable("dbo.Teachers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Teachers", "Id");
            CreateIndex("dbo.Students", "Id");
            AddForeignKey("dbo.Teachers", "Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Students", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
