namespace TripWise.Mvc.Services
{
    public class ApiException : Exception
    {
        public string ApiErrorMessage { get; }

        public ApiException(string message) : base(message)
        {
            ApiErrorMessage = message;
        }
    }
}
