using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using TripWise.Api.Models;
using TripWise.Api.Models.Dto;
using TripWise.Api.Repositories;
using Asp.Versioning;

namespace TripWise.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/trips/{tripId}/activities")]
    [ApiVersion("1.0")]
    [Authorize]
    public class ActivitiesController : ControllerBase
    {
        private readonly ITripRepository _trips;

        public ActivitiesController(ITripRepository trips)
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
        // ADD ACTIVITY
        [HttpPost]
        public async Task<IActionResult> AddActivity(string tripId, CreateActivityDto dto)
        {
            var trip = await _trips.GetByIdAsync(tripId);
            if (trip == null)
                return NotFound("Trip not found or access denied.");

            var userId = GetUserId();
            if (trip.CreatorId != userId)
                return Unauthorized("You do not own this trip");

            var activity = new Activity
            {
                Id = Guid.NewGuid().ToString(),
                Type = dto.Type,
                Name = dto.Name,
                Description = dto.Description,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Location = dto.Location
            };

           
            await _trips.AddActivityAsync(tripId, activity);
            return Ok(activity);

        }

        // UPDATE ACTIVITY
        [HttpPut("{activityId}")]
        public async Task<IActionResult> UpdateActivity(string tripId, string activityId, UpdateActivityDto dto)
        {
            var trip = await _trips.GetByIdAsync(tripId);
            if (trip == null)
                return NotFound("Trip not found or access denied.");

            var userId = GetUserId();
            if (trip.CreatorId != userId)
                return Unauthorized("You do not own this trip");

            var existing = trip.Activities.FirstOrDefault(a => a.Id == activityId);
            if (existing == null)
                return NotFound("Activity not found.");

            existing.Type = dto.Type;
            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.StartTime = dto.StartTime;
            existing.EndTime = dto.EndTime;
            existing.Location = dto.Location;

            await _trips.UpdateActivityAsync(tripId, existing);
            return Ok(existing);
        }

        // DELETE ACTIVITY
        [HttpDelete("{activityId}")]
        public async Task<IActionResult> DeleteActivity(string tripId, string activityId)
        {
            var trip = await _trips.GetByIdAsync(tripId);
            if (trip == null)
                return NotFound("Trip not found or access denied.");
            var userId = GetUserId();
            if (trip.CreatorId != userId)
                return Unauthorized("You do not own this trip");
            var existing = trip.Activities.FirstOrDefault(a => a.Id == activityId);
            if (existing == null)
                return NotFound("Activity not found.");

            await _trips.DeleteActivityAsync(tripId, activityId);
            return Ok(new { message = "Activity deleted successfully." });
        }
    }
}
