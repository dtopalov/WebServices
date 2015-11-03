namespace MusicCatalog.ServiceApi.Models
{
    using System;

    using MusicCatalog.Data.Models;

    public class SongServiceModel
    {
        public string Title { get; set; }

        public DateTime? ReleasedOn { get; set; }

        public string Genre { get; set; }

        public string Artist { get; set; }
    }
}