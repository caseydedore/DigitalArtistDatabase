﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.Models
{
    public class Thumbnail
    {
        public int ID { get; set; }
        public byte[] Image { get; set; }

        public ICollection<Artist> artists { get; set; }
    }
}