namespace MusicCatalog.Data
{
    using System;
    using System.Collections.Generic;

    using Models;
    using Repositories;

    public class MusicCatalogData : IMusicCatalogData
    {
        private readonly IMusicCatalogDbContext context;
        private readonly IDictionary<Type, object> repositories;

        public MusicCatalogData()
            : this(new MusicCatalogDbContext())
        {
        }

        public IGenericRepository<Artist> Artists
        {
            get
            {
                return this.GetRepository<Artist>();
            }
        }

        public IGenericRepository<Song> Songs
        {
            get
            {
                return this.GetRepository<Song>();
            }
        }

        public IGenericRepository<Album> Albums
        {
            get
            {
                return this.GetRepository<Album>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public MusicCatalogData(IMusicCatalogDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
