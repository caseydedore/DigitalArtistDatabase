using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.Models
{
    public class ArtistPostViewModel
    {
        public string ArtistName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
        public uint ViewCount { get; set; }

        //This should be used to store comments belonging to the post
        public List<Comment> Comments { get; set; }
    }

    public class CommentViewModel
    {
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }
    }
}