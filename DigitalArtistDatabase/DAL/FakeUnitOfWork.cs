using DigitalArtistDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.DAL
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        private IGenericRepository<Thumbnail> thumbnailRepo;
        private IGenericRepository<Artist> artistRepo;
        private IGenericRepository<Post> postRepo;
        private IGenericRepository<Picture> pictureRepo;
        private IGenericRepository<Comment> commentRepo;

        private List<Thumbnail> thumbnailList;
        private List<Artist> artistList;
        private List<Post> postList;
        private List<Picture> pictureList;
        private List<Comment> commentList;

        public FakeUnitOfWork(List<Thumbnail> t = null, List<Artist> a = null, 
                              List<Post> p = null, List<Picture> pic = null, List<Comment> c = null)
        {
            if (t == null) thumbnailList = new List<Thumbnail>();
            else thumbnailList = t;

            if (a == null) artistList = new List<Artist>();
            else artistList = a;

            if (p == null) postList = new List<Post>();
            else postList = p;

            if (pic == null) pictureList = new List<Picture>();
            else pictureList = pic;

            if (c == null) commentList = new List<Comment>();
            else commentList = c;
        }

        public IGenericRepository<Thumbnail> ThumbnailRepository
        {
            get
            {
                if (this.thumbnailRepo == null)
                {
                    this.thumbnailRepo = new FakeRepository<Thumbnail>(thumbnailList);
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
                    this.artistRepo = new FakeRepository<Artist>(artistList);
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
                    this.postRepo = new FakeRepository<Post>(postList);
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
                    this.pictureRepo = new FakeRepository<Picture>(pictureList);
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
                    this.commentRepo = new FakeRepository<Comment>(commentList);
                }
                return commentRepo;
            }
        }

        public void Save()
        {
            //save: unneeded
        }

        public void Dispose()
        {
            //dispose: unneeded
        }
    }
}