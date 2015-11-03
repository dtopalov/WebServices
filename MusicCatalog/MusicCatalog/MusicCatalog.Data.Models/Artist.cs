namespace MusicCatalog.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
