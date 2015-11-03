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

    public class ArtistsController : ApiController
    {
        private static readonly IMusicCatalogDbContext Data = new MusicCatalogDbContext();
        private static readonly IGenericRepository<Artist> ArtistsRepository = new GenericRepository<Artist>(Data);

        // GET api/Artists
        public IEnumerable<ArtistServiceModel> Get()
        {
            var result = ArtistsRepository.All().Select(x => new ArtistServiceModel
            {
                Name = x.Name,
                Country = x.Country,
                DateOfBirth = x.DateOfBirth
            }).ToList();

            return result;
        }

        // GET api/Artists/2
        public ArtistServiceModel Get(int id)
        {
            var result = ArtistsRepository.All().Where(x => x.ArtistId == id).Select(x => new ArtistServiceModel
            {
                Name = x.Name,
                Country = x.Country,
                DateOfBirth = x.DateOfBirth
            }).FirstOrDefault();
            return result;
        }

        // POST api/Artists
        public IHttpActionResult Post([FromBody]ArtistServiceModel value)
        {
            var artist = new Artist
            {
                Name = value.Name,
                Country = value.Country,
                DateOfBirth = value.DateOfBirth
            };

            ArtistsRepository.Add(artist);
            Data.SaveChanges();
            return this.StatusCode(HttpStatusCode.Created);
        }

        // PUT api/Artists/5
        public IHttpActionResult Put(int id, [FromBody]ArtistServiceModel value)
        {
            var artistToUpdate = ArtistsRepository.All().FirstOrDefault(x => x.ArtistId == id);

            if (artistToUpdate == null)
            {
                return this.BadRequest("No such artist");
            }

            if (value.Name != null)
            {
                artistToUpdate.Name = value.Name;
            }

            if (value.Country != null)
            {
                artistToUpdate.Country = value.Country;
            }

            if (value.DateOfBirth != null)
            {
                artistToUpdate.DateOfBirth = value.DateOfBirth;
            }

            Data.SaveChanges();

            return this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.Accepted));
        }

        // DELETE api/Artists/5
        public void Delete(int id)
        {
            var artistToRemove = ArtistsRepository.All().FirstOrDefault(x => x.ArtistId == id);
            ArtistsRepository.Delete(artistToRemove);
            Data.SaveChanges();
        }
    }
}
