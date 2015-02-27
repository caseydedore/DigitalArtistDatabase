using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.Models
{
    public static class ImageUtility
    {
        public static byte[] ImageToByte(HttpPostedFileBase i)
        {
            byte[] b = new byte[i.ContentLength];

            using (MemoryStream stream = new MemoryStream())
            {
                i.InputStream.CopyTo(stream);
                b = stream.ToArray();
            }
            return b;
        }
    }
}