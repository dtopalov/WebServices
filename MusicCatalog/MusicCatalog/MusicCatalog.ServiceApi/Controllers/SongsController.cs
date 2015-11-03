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

    public class SongsController : ApiController
    {
        private static readonly IMusicCatalogDbContext Data = new MusicCatalogDbContext();
        private static readonly IGenericRepository<Song> SongsRepository = new GenericRepository<Song>(Data);

        // GET api/Songs
        public IEnumerable<SongServiceModel> Get()
        {
            var result = SongsRepository.All().Select(x => new SongServiceModel
            {
                Title = x.Title,
                Genre = x.Genre,
                ReleasedOn = x.ReleasedOn,
                Artist = x.Artist.Name
            }).ToList();

            return result;
        }

        // GET api/Songs/2
        public SongServiceModel Get(int id)
        {
            var result = SongsRepository.All().Where(x => x.SongId == id).Select(x => new SongServiceModel
            {
                Title = x.Title,
                Genre = x.Genre,
                ReleasedOn = x.ReleasedOn,
                Artist = x.Artist.Name
            }).FirstOrDefault();
            return result;
        }

        // POST api/Songs
        public IHttpActionResult Post([FromBody]SongServiceModel value)
        {
            var artists = new GenericRepository<Artist>(Data);

            if (!artists.All().Any(x => x.Name == value.Artist))
            {
                var newArtist = new Artist { Name = value.Artist };
                artists.Add(newArtist);
                Data.SaveChanges();
            }

            var song = new Song
            {
                Title = value.Title,
                Genre = value.Genre,
                ReleasedOn = value.ReleasedOn,
                Artist = artists.All().FirstOrDefault(x => x.Name == value.Artist),
                ArtistId = artists.All().Where(x => x.Name == value.Artist).Select(x => x.ArtistId).FirstOrDefault()
            };

            SongsRepository.Add(song);
            Data.SaveChanges();
            return this.StatusCode(HttpStatusCode.Created);
        }

        // PUT api/Songs/5
        public IHttpActionResult Put(int id, [FromBody]SongServiceModel value)
        {
            var songToUpdate = SongsRepository.All().FirstOrDefault(x => x.SongId == id);

            if (songToUpdate == null)
            {
                return this.BadRequest("No such song");
            }

            if (value.Title != null)
            {
                songToUpdate.Title = value.Title;
            }

            if (value.Genre != null)
            {
                songToUpdate.Genre = value.Genre;
            }

            if (value.ReleasedOn != null)
            {
                songToUpdate.ReleasedOn = value.ReleasedOn;
            }

            if (value.Artist != null)
            {
                var artists = new GenericRepository<Artist>(Data);

                if (!artists.All().Any(x => x.Name == value.Artist))
                {
                    var newArtist = new Artist { Name = value.Artist };
                    artists.Add(newArtist);
                    Data.SaveChanges();
                }

                songToUpdate.Artist = artists.All().FirstOrDefault(x => x.Name == value.Artist);
                songToUpdate.ArtistId =
                    artists.All().Where(x => x.Name == value.Artist).Select(x => x.ArtistId).FirstOrDefault();
            }

            Data.SaveChanges();

            return this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.Accepted));
        }

        // DELETE api/Songs/5
        public void Delete(int id)
        {
            var songToRemove = SongsRepository.All().FirstOrDefault(x => x.SongId == id);
            SongsRepository.Delete(songToRemove);
            Data.SaveChanges();
        }
    }
}
