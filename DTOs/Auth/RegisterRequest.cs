namespace TripWise.Api.DTOs.Auth
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        required
        public string Email { get; set; }
        required
        public string Password { get; set; }
    }
}
