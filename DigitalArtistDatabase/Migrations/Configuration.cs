namespace DigitalArtistDatabase.Migrations
{
    using DigitalArtistDatabase.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DigitalArtistDatabase.DAL.DADContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DigitalArtistDatabase.DAL.DADContext context)
        {
            //initialize default thumbnails here
            List<Thumbnail> thumbnails = new List<Thumbnail>
            {
                new Thumbnail{ImageURL = "~/Pictures/Thumbnails/Whale01.png"}
            };

            List<Artist> artists = new List<Artist>
            {
                new Artist{ID=1, UserName="CaseyYesac", Description="I am the main test user for this site", Reputation=245, DateJoined=DateTime.Parse("2-2-2015"), ThumbnailID=1},
            };
            List<Post> posts = new List<Post>
            {
                new Post{ID=1, ArtistID=1, Title="Steve - The test", 
                         Description="Steve is a character who is now part of site testing.", ViewCount=1344, DatePosted=DateTime.Parse("2-2-2015")},
            };
            List<Comment> comments = new List<Comment>
            {
                new Comment{ID=1, ArtistID=1, PostID=1, Text="Awesome!", DatePosted=DateTime.Parse("2011-10-10")}

            };
            List<Picture> pictures = new List<Picture>
            {
                //this image url does not follow site conventions
                new Picture{ID=1, ImageURL="~/Pictures/Uploads/OUTPUT_SteveHeadShot.png", PostID=1}
            };

            thumbnails.ForEach(s => context.Thumbnails.AddOrUpdate(s));
            context.SaveChanges();

            artists.ForEach(s => context.Artists.AddOrUpdate(s));
            context.SaveChanges();

            posts.ForEach(s => context.Posts.AddOrUpdate(s));
            context.SaveChanges();

            pictures.ForEach(s => context.Pictures.AddOrUpdate(s));
            context.SaveChanges();

            comments.ForEach(s => context.Comments.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}
