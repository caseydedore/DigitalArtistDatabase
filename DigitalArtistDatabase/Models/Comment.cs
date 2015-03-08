using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.Models
{
    public class Comment : IEntityModel
    {
        public int ID { get; set; }

        [StringLength(ModelFormatting.COMMENT_TEXT_LENGTH)]
        public string Text { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = ModelFormatting.DATE_FORMAT, ApplyFormatInEditMode = true)]
        public DateTime DatePosted { get; set; }

        public int? ArtistID { get; set; }
        public int PostID { get; set; }

        public Artist Artist { get; set; }
        public Post Post { get; set; }
    }
}