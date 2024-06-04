using FilmZone.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmZone.Domain.Models
{
    [Table("film")]
    public class Film
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("path_to_image")]
        public string PathToImage { get; set; } = "~/img/layoutImage";
        [Column("film_or_serial")]
        public FilmOrSerial FilmOrSerial { get; set; } = FilmOrSerial.Film;
        [Column("type")]
        public TypeFilm Type { get; set; } = TypeFilm.Anime;
        [Column("release_film_date")]
        public int ReleaseFilmDate { get; set; } = 0;
        [Column("director")]
        public string Director { get; set; } = "";
        [Column("preview")]
        public string Preview { get; set; } = "";
        [Column("links")]
        public List<string> Links { get; set; } = new List<string>();
        [Column("price")]
        public List<string> Price { get; set; } = new List<string>();
        [Column("advertisement")]
        public List<string> Advertisement { get; set; } = new List<string>();
    }
}
