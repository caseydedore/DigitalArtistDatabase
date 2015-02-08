using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }

        public int? ArtistID { get; set; }
        public int PostID { get; set; }

        public Artist Artist { get; set; }
        public Post Post { get; set; }
    }
}