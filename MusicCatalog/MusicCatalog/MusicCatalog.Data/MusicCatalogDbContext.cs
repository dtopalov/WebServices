namespace MusicCatalog.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using MusicCatalog.Data.Migrations;
    using MusicCatalog.Data.Models;

    public class MusicCatalogDbContext : DbContext, IMusicCatalogDbContext
    {
        public MusicCatalogDbContext()
            : base("MusicCatalogConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MusicCatalogDbContext, Configuration>());
        }

        public IDbSet<Artist> Artists { get; set; }

        public IDbSet<Album> Albums { get; set; }

        public IDbSet<Song> Songs { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
