using System.ComponentModel.DataAnnotations.Schema;

namespace FilmZone.Domain.Models
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Column("lastname")]
        public string LastName { get; set; } = string.Empty;
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("password")]
        public string Password { get; set; }
        //public string PhoneNumber { get; set; } = string.Empty;
        //public int Age { get; set; }
        [Column("login_name")]
        public string Login { get; set; } = string.Empty;
        [Column("token")]
        public string Token { get; set; }= string.Empty;
        [Column("email_confirmation")]
        public bool EmailConfirmation { get; set; } = false;
    }
}