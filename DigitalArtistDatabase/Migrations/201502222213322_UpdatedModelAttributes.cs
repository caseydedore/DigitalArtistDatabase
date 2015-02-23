namespace DigitalArtistDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModelAttributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Artist", "UserName", c => c.String(maxLength: 25));
            AlterColumn("dbo.Artist", "Description", c => c.String(maxLength: 300));
            AlterColumn("dbo.Comment", "Text", c => c.String(maxLength: 200));
            AlterColumn("dbo.Post", "Title", c => c.String(maxLength: 35));
            AlterColumn("dbo.Post", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Post", "Description", c => c.String());
            AlterColumn("dbo.Post", "Title", c => c.String());
            AlterColumn("dbo.Comment", "Text", c => c.String());
            AlterColumn("dbo.Artist", "Description", c => c.String());
            AlterColumn("dbo.Artist", "UserName", c => c.String());
        }
    }
}
