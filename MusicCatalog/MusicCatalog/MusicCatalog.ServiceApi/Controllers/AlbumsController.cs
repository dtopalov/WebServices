namespace MusicCatalog.ServiceApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using MusicCatalog.Data;
    using MusicCatalog.Data.Models;
    using MusicCatalog.Data.Repositories;
    using MusicCatalog.ServiceApi.Models;

    public class AlbumsController : ApiController
    {
        private static readonly IMusicCatalogDbContext Data = new MusicCatalogDbContext();
        private static readonly IGenericRepository<Album> AlbumsRepository = new GenericRepository<Album>(Data);

        // GET api/Albums
        public IEnumerable<AlbumServiceModel> Get()
        {
            var result = AlbumsRepository.All().Select(x => new AlbumServiceModel
            {
                Title = x.Title,
                Genre = x.Genre,
                ReleasedOn = x.ReleasedOn,
                Songs = x.Songs.Select(s => s.Title).ToList(),
                Artists = x.Artists.Select(a => a.Name).ToList()
            }).ToList();

            return result;
        }

        // GET api/Albums/2
        public AlbumServiceModel Get(int id)
        {
            var result = AlbumsRepository.All().Where(x => x.AlbumId == id).Select(x => new AlbumServiceModel
            {
                Title = x.Title,
                Genre = x.Genre,
                ReleasedOn = x.ReleasedOn,
                Songs = x.Songs.Select(s => s.Title).ToList(),
                Artists = x.Artists.Select(a => a.Name).ToList()
            }).FirstOrDefault();
            return result;
        }

        // POST api/Albums
        public IHttpActionResult Post([FromBody]AlbumServiceModel value)
        {
            var artists = new GenericRepository<Artist>(Data);
            var songs = new GenericRepository<Song>(Data);

            if (value.Artists != null)
            {
                foreach (var artist in value.Artists)
                {
                    if (!artists.All().Any(x => x.Name == artist))
                    {
                        var newArtist = new Artist { Name = artist };
                        artists.Add(newArtist);
                    }

                    Data.SaveChanges();
                }
            }

            if (value.Songs != null)
            {
                foreach (var song in value.Songs)
                {
                    if (!songs.All().Any(x => x.Title == song))
                    {
                        var newSong = new Song { Title = song };
                        songs.Add(newSong);
                    }

                    Data.SaveChanges();
                }
            }

            var album = new Album
            {
                Title = value.Title,
                Genre = value.Genre,
                ReleasedOn = value.ReleasedOn,
                Artists = artists.All().Where(x => value.Artists.Contains(x.Name)).ToList(),
                Songs = songs.All().Where(x => value.Songs.Contains(x.Title)).ToList()
            };

            AlbumsRepository.Add(album);
            Data.SaveChanges();
            return this.StatusCode(HttpStatusCode.Created);
        }

        // PUT api/Albums/5
        public IHttpActionResult Put(int id, [FromBody]AlbumServiceModel value)
        {
            var albumToUpdate = AlbumsRepository.All().FirstOrDefault(x => x.AlbumId == id);

            if (albumToUpdate == null)
            {
                return this.BadRequest("No such album");
            }

            var artists = new GenericRepository<Artist>(Data);
            var songs = new GenericRepository<Song>(Data);

            if (value.Artists != null)
            {
                foreach (var artist in value.Artists)
                {
                    if (!artists.All().Any(x => x.Name == artist))
                    {
                        var newArtist = new Artist { Name = artist };
                        artists.Add(newArtist);
                    }

                    Data.SaveChanges();
                }

                albumToUpdate.Artists = artists.All().Where(x => value.Artists.Contains(x.Name)).ToList();
            }

            if (value.Songs != null)
            {
                foreach (var song in value.Songs)
                {
                    if (!songs.All().Any(x => x.Title == song))
                    {
                        var newSong = new Song { Title = song };
                        songs.Add(newSong);
                    }

                    Data.SaveChanges();
                }

                albumToUpdate.Songs = songs.All().Where(x => value.Songs.Contains(x.Title)).ToList();
            }

            if (value.Title != null)
            {
                albumToUpdate.Title = value.Title;
            }

            if (value.Genre != null)
            {
                albumToUpdate.Genre = value.Genre;
            }

            if (value.ReleasedOn != null)
            {
                albumToUpdate.ReleasedOn = value.ReleasedOn;
            }

            Data.SaveChanges();

            return this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.Accepted));
        }

        // DELETE api/Album/5
        public void Delete(int id)
        {
            var albumToRemove = AlbumsRepository.All().FirstOrDefault(x => x.AlbumId == id);
            AlbumsRepository.Delete(albumToRemove);
            Data.SaveChanges();
        }
    }
}
