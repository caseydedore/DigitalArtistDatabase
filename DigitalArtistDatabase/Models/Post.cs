using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.Models
{
    public class Post : IEntityModel
    {
        public int ID { get; set; }

        [StringLength(ModelFormatting.POST_TITLE_LENGTH)]
        public string Title { get; set; }

        [StringLength(ModelFormatting.POST_DESCRIPTION_LENGTH)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = ModelFormatting.DATE_FORMAT, ApplyFormatInEditMode = true)]
        public DateTime DatePosted { get; set; }

        public uint ViewCount { get; set; }

        public int ArtistID { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public Artist Artist { get; set; }
    }
}