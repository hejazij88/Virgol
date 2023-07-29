namespace Virgol_Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteComent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Coments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Coments", "UserId", "dbo.Users");
            DropIndex("dbo.Coments", new[] { "UserId" });
            DropIndex("dbo.Coments", new[] { "ArticleId" });
            DropTable("dbo.Coments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Coments",
                c => new
                    {
                        ComentId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CreatDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ComentId);
            
            CreateIndex("dbo.Coments", "ArticleId");
            CreateIndex("dbo.Coments", "UserId");
            AddForeignKey("dbo.Coments", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Coments", "ArticleId", "dbo.Articles", "ArticleId", cascadeDelete: true);
        }
    }
}
