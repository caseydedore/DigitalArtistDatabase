using DigitalArtistDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private DADContext context = new DADContext();
        private IGenericRepository<Thumbnail> thumbnailRepo;
        private IGenericRepository<Artist> artistRepo;
        private IGenericRepository<Post> postRepo;
        private IGenericRepository<Picture> pictureRepo;
        private IGenericRepository<Comment> commentRepo;

        public IGenericRepository<Thumbnail> ThumbnailRepository
        {
            get
            {
                if (this.thumbnailRepo == null)
                {
                    this.thumbnailRepo = new GenericRepository<Thumbnail>(context);
                }
                return thumbnailRepo;
            }
        }

        public IGenericRepository<Artist> ArtistRepository
        {
            get
            {
                if (this.artistRepo == null)
                {
                    this.artistRepo = new GenericRepository<Artist>(context);
                }
                return artistRepo;
            }
        }

        public IGenericRepository<Post> PostRepository
        {
            get
            {
                if (this.postRepo == null)
                {
                    this.postRepo = new GenericRepository<Post>(context);
                }
                return postRepo;
            }
        }

        public IGenericRepository<Picture> PictureReposiroty
        {
            get
            {
                if (this.pictureRepo == null)
                {
                    this.pictureRepo = new GenericRepository<Picture>(context);
                }
                return pictureRepo;
            }
        }

        public IGenericRepository<Comment> CommentRepository
        {
            get
            {
                if (this.commentRepo == null)
                {
                    this.commentRepo = new GenericRepository<Comment>(context);
                }
                return commentRepo;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}