namespace Virgol_Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleId = c.Int(nullable: false, identity: true),
                        CtegoryId = c.Int(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        ImageName = c.String(),
                        DateRegester = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        Like = c.Int(nullable: false),
                        ComentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ArticleId)
                .ForeignKey("dbo.Ctegories", t => t.CtegoryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CtegoryId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Ctegories",
                c => new
                    {
                        CtegoryId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ImageName = c.String(),
                    })
                .PrimaryKey(t => t.CtegoryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RegesterDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Family = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleTitle = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Coments",
                c => new
                    {
                        ComentId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CreatDate = c.DateTime(nullable: false),
                        Phone = c.String(),
                        Password = c.String(),
                        ArticleId = c.Int(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ComentId)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.ArticleId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coments", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Coments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Articles", "CtegoryId", "dbo.Ctegories");
            DropIndex("dbo.Coments", new[] { "User_UserId" });
            DropIndex("dbo.Coments", new[] { "ArticleId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropIndex("dbo.Articles", new[] { "CtegoryId" });
            DropTable("dbo.Coments");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Ctegories");
            DropTable("dbo.Articles");
        }
    }
}
