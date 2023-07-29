namespace Virgol_Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdb1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Coments", "User_UserId", "dbo.Users");
            DropIndex("dbo.Coments", new[] { "User_UserId" });
            RenameColumn(table: "dbo.Coments", name: "User_UserId", newName: "UserId");
            AlterColumn("dbo.Coments", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Coments", "UserId");
            AddForeignKey("dbo.Coments", "UserId", "dbo.Users", "UserId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coments", "UserId", "dbo.Users");
            DropIndex("dbo.Coments", new[] { "UserId" });
            AlterColumn("dbo.Coments", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Coments", name: "UserId", newName: "User_UserId");
            CreateIndex("dbo.Coments", "User_UserId");
            AddForeignKey("dbo.Coments", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
