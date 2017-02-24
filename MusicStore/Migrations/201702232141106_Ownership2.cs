namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ownership2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserAlbum", new[] { "Owner_Id" });
            DropColumn("dbo.UserAlbum", "ApplicationUserId");
            RenameColumn(table: "dbo.UserAlbum", name: "Owner_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.UserAlbum", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserAlbum", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserAlbum", new[] { "ApplicationUserId" });
            AlterColumn("dbo.UserAlbum", "ApplicationUserId", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.UserAlbum", name: "ApplicationUserId", newName: "Owner_Id");
            AddColumn("dbo.UserAlbum", "ApplicationUserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.UserAlbum", "Owner_Id");
        }
    }
}
