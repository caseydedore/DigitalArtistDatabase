using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.Models
{
    public class Artist
    {
        public int ID { get; set; }

        [StringLength(ModelFormatting.ARTIST_NAME_LENGTH)]
        public string UserName { get; set; }

        [StringLength(ModelFormatting.ARTIST_DESCRIPTION_LENGTH)]
        public string Description { get; set; }

        public byte Reputation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = ModelFormatting.DATE_FORMAT, ApplyFormatInEditMode = true)]
        public DateTime DateJoined { get; set; }

        public int ThumbnailID { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Thumbnail Thumbnail { get; set; }
    }
}