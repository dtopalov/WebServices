namespace MusicCatalog.Data
{
    using Repositories;
    using Models;

    public interface IMusicCatalogData
    {
        IGenericRepository<Artist> Artists { get; }

        IGenericRepository<Song> Songs { get; }

        IGenericRepository<Album> Albums { get; }
    }
}
