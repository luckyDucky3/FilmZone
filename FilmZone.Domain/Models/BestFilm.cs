using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace FilmZone.Domain.Models
{
    public class BestFilm
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("film_name")]
        public string FilmName { get; set; } = string.Empty;
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;
    }
}