﻿using FilmZone.Domain.Enum;

namespace FilmZone.Domain.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TypeFilm Type { get; set; }
        public int ReleaseFilmDate { get; set; } = 0;
        public string Director { get; set; } = "";
        public string Preview { get; set; } = "";
        public string LinkF { get; set; } = "";
        public string LinkS { get; set; } = "";
    }
}
