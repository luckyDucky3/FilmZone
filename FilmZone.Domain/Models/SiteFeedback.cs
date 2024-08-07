using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmZone.Domain.Models
{
    [Table("site_feedback")]
    public class SiteFeedback
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = "unknown";
        [Column("text")]
        public string Text { get; set; } = "unknown";
    }
}