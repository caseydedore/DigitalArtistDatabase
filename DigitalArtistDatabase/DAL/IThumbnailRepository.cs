using DigitalArtistDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.DAL
{
    public interface IThumbnailRepository : IDisposable
    {
        IEnumerable<Thumbnail> GetThumbnails();
        Thumbnail GetThumbnail(int id);
        void AddThumbnail(Thumbnail t);
        void UpdateThumbnail(Thumbnail t);
        void DeleteThumbnail(int id);
        void SaveChanges();
    }
}