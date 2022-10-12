using System.Text.Json.Serialization;

namespace ProEvents.Application.Dtos
{
    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string lastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string function { get; set; }
        public string description { get; set; }
        public string ImageURL { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}