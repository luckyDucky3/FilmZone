using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmZone.Domain.Models
{
    [Table("film_feedback")]
    public class FilmFeedback
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("film_id")]
        public int FilmId { get; set; }
        [Column("heading")]
        public string Heading { get; set; } = "";

        [Column("name")]
        public string Name { get; set; } = "";
        [Column("text")]
        public string Text { get; set; } = "";
        [Column("value")]
        public float Value { get; set; } = 0;
    }
}
