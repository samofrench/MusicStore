namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RecordLabelId = c.Int(nullable: false),
                        CatNo = c.String(),
                        ArtworkUrl = c.String(),
                        Notes = c.String(),
                        Country = c.Int(nullable: false),
                        Year = c.Int(),
                        Discs = c.Int(nullable: false),
                        Audio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RecordLabel", t => t.RecordLabelId, cascadeDelete: true)
                .Index(t => t.RecordLabelId);
            
            CreateTable(
                "dbo.Recording",
                c => new
                    {
                        RecordingId = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(nullable: false),
                        PieceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecordingId)
                .ForeignKey("dbo.Album", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Piece", t => t.PieceId, cascadeDelete: true)
                .Index(t => t.AlbumId)
                .Index(t => t.PieceId);
            
            CreateTable(
                "dbo.Credit",
                c => new
                    {
                        CreditId = c.Int(nullable: false, identity: true),
                        PerformerId = c.Int(nullable: false),
                        RecordingId = c.Int(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.CreditId)
                .ForeignKey("dbo.Performer", t => t.PerformerId, cascadeDelete: true)
                .ForeignKey("dbo.Recording", t => t.RecordingId, cascadeDelete: true)
                .Index(t => t.PerformerId)
                .Index(t => t.RecordingId);
            
            CreateTable(
                "dbo.Performer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Piece",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComposerId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Composer", t => t.ComposerId, cascadeDelete: true)
                .Index(t => t.ComposerId);
            
            CreateTable(
                "dbo.Composer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthYear = c.Int(),
                        DeathYear = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecordLabel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Album", "RecordLabelId", "dbo.RecordLabel");
            DropForeignKey("dbo.Recording", "PieceId", "dbo.Piece");
            DropForeignKey("dbo.Piece", "ComposerId", "dbo.Composer");
            DropForeignKey("dbo.Credit", "RecordingId", "dbo.Recording");
            DropForeignKey("dbo.Credit", "PerformerId", "dbo.Performer");
            DropForeignKey("dbo.Recording", "AlbumId", "dbo.Album");
            DropIndex("dbo.Piece", new[] { "ComposerId" });
            DropIndex("dbo.Credit", new[] { "RecordingId" });
            DropIndex("dbo.Credit", new[] { "PerformerId" });
            DropIndex("dbo.Recording", new[] { "PieceId" });
            DropIndex("dbo.Recording", new[] { "AlbumId" });
            DropIndex("dbo.Album", new[] { "RecordLabelId" });
            DropTable("dbo.RecordLabel");
            DropTable("dbo.Composer");
            DropTable("dbo.Piece");
            DropTable("dbo.Performer");
            DropTable("dbo.Credit");
            DropTable("dbo.Recording");
            DropTable("dbo.Album");
        }
    }
}
