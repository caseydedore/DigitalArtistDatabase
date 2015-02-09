using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DigitalArtistDatabase.Models;

namespace DigitalArtistDatabase.DAL
{
    public class DADInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DADContext>
                                  //System.Data.Entity.DropCreateDatabaseAlways<DADContext>
    {
        protected override void Seed(DADContext context)
        {
            List<Thumbnail> thumbnails = new List<Thumbnail>
            {
                new Thumbnail{Image = new byte[]{0x20,0x20,0x20,0x20,0x20,0x20,0x20}},
                new Thumbnail{Image = new byte[]{0x20,0x20,0x20,0x20,0x20,0x20,0x20}}
            };

            List<Artist> artists = new List<Artist>
            {
                new Artist{UserName = "MrArtist", Description="I like art and pineapple", Reputation=245, DateJoined=DateTime.Parse("1776-1-1"), ThumbnailID=1},
                new Artist{UserName = "MsArtist", Description="Consult my lawyer", Reputation=120, DateJoined=DateTime.Parse("2007-2-5"), ThumbnailID=1},
                new Artist{UserName = "H4><0rz", Description="I am l33t", Reputation=15, DateJoined=DateTime.Parse("1995-7-6"), ThumbnailID=2},
                new Artist{UserName = "Ludwig Kampfrugen", Description="I am 3D sculptor living in Hamberg", Reputation=144, DateJoined=DateTime.Parse("2013-6-11"), ThumbnailID=1}
            };
            List<Post> posts = new List<Post>
            {
                new Post{ArtistID=1, Title="Pineapple Express - Mr Pineapple model", Description="This is a model I created for my animation 'Pineapple Express'.", ViewCount=1340204, DatePosted=DateTime.Parse("1915-2-12")},
                new Post{ArtistID=1, Title="Pineapple Express - Ms. Pine d'Happle", Description="This is Mr Pineapple's love interest in 'Pineapple Express'", ViewCount=157304, DatePosted=DateTime.Parse("1915-3-12")},
                new Post{ArtistID=2, Title="Maple Tree", Description="A low-poly maple tree with diffuse, normal, spec maps and tesselation", ViewCount=154, DatePosted=DateTime.Parse("2014-9-2")}
            };
            List<Comment> comments = new List<Comment>
            {
                new Comment{ArtistID=1, PostID=1, Text="Awesome!", DatePosted=DateTime.Parse("2011-10-10")},
                new Comment{ArtistID=3, PostID=1, Text="Very Nice", DatePosted=DateTime.Parse("2011-12-11")},
                new Comment{ArtistID=4, PostID=3, Text="I like maple syrup", DatePosted=DateTime.Parse("2011-12-12")}

            };
            List<Picture> pictures = new List<Picture>
            {
                new Picture{Image = new byte[]{0x20,0x20,0x20,0x20,0x20,0x20,0x20}, PostID=1},
                new Picture{Image = new byte[]{0x20,0x20,0x20,0x20,0x20,0x20,0x20}, PostID=2},
                new Picture{Image = new byte[]{0x20,0x20,0x20,0x20,0x20,0x20,0x20}, PostID=2},
                new Picture{Image = new byte[]{0x20,0x20,0x20,0x20,0x20,0x20,0x20}, PostID=2},
                new Picture{Image = new byte[]{0x20,0x20,0x20,0x20,0x20,0x20,0x20}, PostID=3}
            };

            thumbnails.ForEach(s => context.Thumbnails.Add(s));
            context.SaveChanges();

            artists.ForEach(s => context.Artists.Add(s));
            context.SaveChanges();

            posts.ForEach(s => context.Posts.Add(s));
            context.SaveChanges();

            pictures.ForEach(s => context.Pictures.Add(s));
            context.SaveChanges();

            comments.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();


        }

    }
}