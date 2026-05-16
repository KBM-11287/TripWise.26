using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TripWise.Api.Models;
using TripWise.Api.Models.Dto;
using TripWise.Api.Repositories;
using Asp.Versioning;


namespace TripWise.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/trips")]
    [ApiVersion("1.0")]
    [Authorize]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _trips;

        public TripsController(ITripRepository trips)
        {
            _trips = trips;
        }

        // Helper to get logged-in user ID from JWT
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(ClaimTypes.Name)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        }

        // CREATE TRIP
        [HttpPost]
        public async Task<IActionResult> CreateTrip(CreateTripDto dto)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");
            var trip = new Trip
            {
                CreatorId = userId,
                Title = dto.Title,
                Destination = dto.Destination,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Activities = new List<Activity>()
            };
            await _trips.CreateAsync(trip);
            return Ok(trip);
        }

        // GET ALL TRIPS FOR USER
        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");
            var trips = await _trips.GetTripsForUserAsync(userId);
            return Ok(trips);
        }
    }
}
