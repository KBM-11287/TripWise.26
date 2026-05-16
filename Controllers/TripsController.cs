using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TripWise.Api.Models;
using TripWise.Api.Models.Dto;
using TripWise.Api.Repositories;
using Asp.Versioning;
using System.IdentityModel.Tokens.Jwt;

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
        private string? GetUserId()
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
        // Get Trip by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip(string id)
        {
            var trip = await _trips.GetByIdAsync(id);
            if (trip == null)
                return NotFound();

            var userId = GetUserId();
            if (trip.CreatorId != userId)
                return Unauthorized();

            return Ok(trip);
        }
        // Update Trip
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrip(string id, UpdateTripDto dto)
        {
            var trip = await _trips.GetByIdAsync(id);
            if (trip == null)
                return NotFound();

            var userId = GetUserId();
            if (trip.CreatorId != userId)
                return Unauthorized();

            trip.Title = dto.Title;
            trip.Destination = dto.Destination;
            trip.StartDate = dto.StartDate;
            trip.EndDate = dto.EndDate;

            await _trips.UpdateAsync(id, trip);
            return Ok(trip);
        }
        // Delete Trip
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(string id)
        {
            var trip = await _trips.GetByIdAsync(id);
            if (trip == null)
                return NotFound();

            var userId = GetUserId();
            if (trip.CreatorId != userId)
                return Unauthorized();

            await _trips.DeleteAsync(id);
            return Ok(new { message = "Trip deleted successfully." });
        }
    }
    

}
