using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TripWise.Api.DTOs.Activities;
using TripWise.Api.DTOs.Trips;
using TripWise.Api.Models;
using TripWise.Api.Repositories;
using TripWise.Api.Services;

namespace TripWise.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/trips")]
    [ApiVersion("1.0")]
    [Authorize]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _trips;
        private readonly ActivityService _activities;
        

        public TripsController(ITripRepository trips, ActivityService activities)
        {
            _trips = trips;
            _activities = activities;
        }

        // -------------------- TRIPS --------------------

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

        // -------------------- ACTIVITIES (NESTED) --------------------
        [HttpPost("{tripId}/activities")]
        public async Task<IActionResult> AddActivity(string tripId, CreateActivityDto dto)
        {
            var result = await _activities.AddAsync(tripId, dto);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{tripId}/activities/{activityId}")]
        public async Task<IActionResult> UpdateActivity(string tripId, string activityId, UpdateActivityDto dto)
        {
            var result = await _activities.UpdateAsync(tripId, activityId, dto);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{tripId}/activities/{activityId}")]
        public async Task<IActionResult> DeleteActivity(string tripId, string activityId)
        {
            var success = await _activities.DeleteAsync(tripId, activityId);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }


}
