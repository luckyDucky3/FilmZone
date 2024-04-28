using FilmZone.Domain.Enum;

namespace FilmZone.Domain.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //public string PathToImage { get; set; } = "~/img/layoutImage";
        public TypeFilm Type { get; set; }
        public int ReleaseFilmDate { get; set; } = 0;
        public string Director { get; set; } = "";
        public string Preview { get; set; } = "";
        public List<string> Links { get; set; } = new List<string>();
        public List<string> Price { get; set; } = new List<string>();
        public List<string> Advertisement { get; set; } = new List<string>();
    }
}
