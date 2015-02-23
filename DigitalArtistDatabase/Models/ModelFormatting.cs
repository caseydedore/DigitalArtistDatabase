using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.Models
{
    public static class ModelFormatting
    {
        public const int POST_DESCRIPTION_LENGTH = 500,
                         POST_TITLE_LENGTH = 35,
                         COMMENT_TEXT_LENGTH = 200,
                         ARTIST_DESCRIPTION_LENGTH = 300,
                         ARTIST_NAME_LENGTH = 25;

        public const string DATE_FORMAT = "{0:dd-MMMM-yyyy}";
    }
}