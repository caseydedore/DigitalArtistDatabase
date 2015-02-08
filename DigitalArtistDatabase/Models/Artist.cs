using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.Models
{
    public class Artist
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public byte Reputation { get; set; }
        public DateTime DateJoined { get; set; }

        public int ThumbnailID { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Thumbnail Thumbnail { get; set; }
    }
}