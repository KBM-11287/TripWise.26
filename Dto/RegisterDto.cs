namespace TripWise.Api.Models.Dto
{
    public class RegisterDto
    {
        public string Name { get; set; }
        required
        public string Email { get; set; }
        required
        public string Password { get; set; }
    }
}
