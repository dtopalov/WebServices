namespace MusicCatalog.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        private ICollection<Artist> artists;
        private ICollection<Song> songs;

        public Album()
        {
            this.artists = new List<Artist>();
            this.songs = new List<Song>();
        }

        [Key]
        public int AlbumId { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime? ReleasedOn { get; set; }

        public string Genre { get; set; }

        [Required]
        public virtual ICollection<Artist> Artists
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

        [Required]
        public virtual ICollection<Song> Songs
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
