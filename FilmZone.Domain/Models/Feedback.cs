using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilmZone.Domain.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Name { get; set; } = "unknown";
        public string Email { get; set; } = "unknown";
        public string Text { get; set; } = "unknown";
    }
}