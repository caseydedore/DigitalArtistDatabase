using DigitalArtistDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.DAL
{
    public class ThumbnailRepository: IThumbnailRepository, IDisposable
    {
        private DADContext dc;


        public ThumbnailRepository(DADContext d)
        {
            dc = d;
        }

        public IEnumerable<Thumbnail> GetThumbnails()
        {
            return dc.Thumbnails.ToList();
        }

        public Thumbnail GetThumbnail(int id)
        {
            return dc.Thumbnails.Find(id);
        }

        public void AddThumbnail(Thumbnail t)
        {
            dc.Thumbnails.Add(t);
        }

        public void UpdateThumbnail(Thumbnail t)
        {
            dc.Entry(t).State = EntityState.Modified;
        }

        public void DeleteThumbnail(int id)
        {
            Thumbnail t = dc.Thumbnails.Find(id);
            dc.Thumbnails.Remove(t);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dc.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            dc.SaveChanges();
        }

        public void SaveChanges()
        {
            dc.SaveChanges();
        }
    }
}