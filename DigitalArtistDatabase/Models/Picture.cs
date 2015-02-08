using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.Models
{
    public class Picture
    {
        public int ID { get; set; }
        public byte[] Image { get; set; }

        public int PostID { get; set; }

        public Post Post { get; set; }
    }


    //http://stackoverflow.com/questions/26347705/saving-images-to-database-with-asp-net-mvc-4-entity-framework
}