namespace DigitalArtistDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelImagesChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Picture", "Image", c => c.Binary());
            AddColumn("dbo.Thumbnail", "Image", c => c.Binary());
            DropColumn("dbo.Picture", "ImageURL");
            DropColumn("dbo.Thumbnail", "ImageURL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Thumbnail", "ImageURL", c => c.String());
            AddColumn("dbo.Picture", "ImageURL", c => c.String());
            DropColumn("dbo.Thumbnail", "Image");
            DropColumn("dbo.Picture", "Image");
        }
    }
}
