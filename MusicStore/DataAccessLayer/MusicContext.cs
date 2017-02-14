using MusicStore.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MusicStore.DataAccessLayer
{
    public class MusicContext : DbContext
    {
        public MusicContext() : base("MusicContext")
        {
        }
        
        public DbSet<Album> Albums { get; set; }
        public DbSet<Composer> Composers { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Piece> Pieces { get; set; } 
        public DbSet<Recording> Recordings { get; set; } 
        public DbSet<RecordLabel> Labels { get; set; } 
        public DbSet<Credit> Credits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}