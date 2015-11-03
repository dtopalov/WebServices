namespace MusicCatalog.ServiceApi.Models
{
    using System;

    public class ArtistServiceModel
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}