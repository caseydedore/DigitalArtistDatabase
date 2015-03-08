using DigitalArtistDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalArtistDatabase.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Thumbnail> ThumbnailRepository { get; }
        IGenericRepository<Artist> ArtistRepository { get; }
        IGenericRepository<Post> PostRepository { get; }
        IGenericRepository<Picture> PictureReposiroty { get; }
        IGenericRepository<Comment> CommentRepository { get; }
        void Save();
    }
}
