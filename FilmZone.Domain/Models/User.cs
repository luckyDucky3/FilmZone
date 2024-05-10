namespace FilmZone.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }
        //public string PhoneNumber { get; set; } = string.Empty;
        //public int Age { get; set; }
        public string LoginName { get; set; } = string.Empty;
        public string Token { get; set; }= string.Empty;
    }
}