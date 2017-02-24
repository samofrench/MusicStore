namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumOwnership : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAlbum",
                c => new
                    {
                        UserAlbumId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.Guid(nullable: false),
                        AlbumId = c.Int(nullable: false),
                        Sound = c.String(),
                        Condition = c.String(),
                        Eq = c.String(),
                        Notes = c.String(),
                        Clean = c.Boolean(nullable: false),
                        Sell = c.Boolean(nullable: false),
                        Donate = c.Boolean(nullable: false),
                        Needs = c.Boolean(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserAlbumId)
                .ForeignKey("dbo.Album", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.AlbumId)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAlbum", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserAlbum", "AlbumId", "dbo.Album");
            DropIndex("dbo.UserAlbum", new[] { "Owner_Id" });
            DropIndex("dbo.UserAlbum", new[] { "AlbumId" });
            DropTable("dbo.UserAlbum");
        }
    }
}
