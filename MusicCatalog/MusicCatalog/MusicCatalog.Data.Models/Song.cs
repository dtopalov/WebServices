namespace MusicCatalog.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Song
    {
        [Key]
        public int SongId { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime? ReleasedOn { get; set; }

        public string Genre { get; set; }

        public virtual Artist Artist { get; set; }

        public int? ArtistId { get; set; }
    }
}
