using DigitalArtistDatabase.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DigitalArtistDatabase.DAL
{
    public class DADContext : DbContext
    {
        public DADContext() : base("DADContext")
        {

        }

        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}