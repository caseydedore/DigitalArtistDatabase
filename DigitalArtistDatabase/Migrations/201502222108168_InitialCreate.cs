namespace DigitalArtistDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artist",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Description = c.String(),
                        Reputation = c.Byte(nullable: false),
                        DateJoined = c.DateTime(nullable: false),
                        ThumbnailID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Thumbnail", t => t.ThumbnailID, cascadeDelete: true)
                .Index(t => t.ThumbnailID);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        DatePosted = c.DateTime(nullable: false),
                        ArtistID = c.Int(),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Artist", t => t.ArtistID)
                .ForeignKey("dbo.Post", t => t.PostID, cascadeDelete: true)
                .Index(t => t.ArtistID)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        DatePosted = c.DateTime(nullable: false),
                        ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Artist", t => t.ArtistID, cascadeDelete: true)
                .Index(t => t.ArtistID);
            
            CreateTable(
                "dbo.Picture",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageURL = c.String(),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Post", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Thumbnail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artist", "ThumbnailID", "dbo.Thumbnail");
            DropForeignKey("dbo.Picture", "PostID", "dbo.Post");
            DropForeignKey("dbo.Comment", "PostID", "dbo.Post");
            DropForeignKey("dbo.Post", "ArtistID", "dbo.Artist");
            DropForeignKey("dbo.Comment", "ArtistID", "dbo.Artist");
            DropIndex("dbo.Picture", new[] { "PostID" });
            DropIndex("dbo.Post", new[] { "ArtistID" });
            DropIndex("dbo.Comment", new[] { "PostID" });
            DropIndex("dbo.Comment", new[] { "ArtistID" });
            DropIndex("dbo.Artist", new[] { "ThumbnailID" });
            DropTable("dbo.Thumbnail");
            DropTable("dbo.Picture");
            DropTable("dbo.Post");
            DropTable("dbo.Comment");
            DropTable("dbo.Artist");
        }
    }
}
