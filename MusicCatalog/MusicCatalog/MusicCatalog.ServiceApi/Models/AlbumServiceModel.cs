namespace MusicCatalog.ServiceApi.Models
{
    using System;
    using System.Collections.Generic;

    public class AlbumServiceModel
    {
        private ICollection<string> artists;
        private ICollection<string> songs;

        public AlbumServiceModel()
        {
            this.artists = new List<string>();
            this.songs = new List<string>();
        }

        public string Title { get; set; }

        public DateTime? ReleasedOn { get; set; }

        public string Genre { get; set; }

        public ICollection<string> Artists
        {
            get
            {
                return this.artists;
            }
            set
            {
                this.artists = value;
            }
        }

        public ICollection<string> Songs
        {
            get
            {
                return this.songs;
            }
            set
            {
                this.songs = value;
            }
        }
    }
}