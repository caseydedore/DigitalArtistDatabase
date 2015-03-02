using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.Models
{
    public class PictureFeaturedViewModel
    {
        public byte[] Image { get; set; }
        //info from the post that is it's parent
        public DateTime DatePosted { get; set; }
        public uint ViewCount { get; set; }
    }

    public class PostCreateViewModel
    {
        public int ArtistID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public ICollection<Picture> Pictures{get; set;}
    }

    /*
    public class ArtistViewModel
    {
        public ArtistViewModel()
        {
            Posts = new List<Post>();
        }

        public string UserName { get; set; }
        public string Description { get; set; }
        public byte Reputation { get; set; }
        public DateTime DateJoined { get; set; }

        //Used to store posts belonging to the artist
        public ICollection<Post> Posts { get; set; }
        //the thumbnail that the artist links to. This is the entire reason for this view model
        public byte[] Thumbnail { get; set; }
    }

    public class PostViewModel
    {
        public PostViewModel()
        {
            Pictures = new List<PictureViewModel>();
            Comments = new List<CommentViewModel>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
        public uint ViewCount { get; set; }

        //pictures under posts and comments under posts
        public List<PictureViewModel> Pictures { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }

    public class CommentViewModel
    {
        public string ArtistName { get; set; }
        public string ArtistThumbnail { get; set; }
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }
    }

    public class PictureViewModel
    {
        public string ImageURL { get; set; }
    }
    */
}