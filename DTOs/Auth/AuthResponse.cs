namespace TripWise.Api.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        // We include the UserId in the response so that the client can easily associate the token with the user.
        public string UserId { get; set; }

        // error message feedback for existing user registration or invalid login attempts
        public string ErrorMessage { get; set; }
    }
}
 