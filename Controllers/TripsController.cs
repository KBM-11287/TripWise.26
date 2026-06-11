using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripWise.Api.DTOs.Activities;
using TripWise.Api.DTOs.Trips;
using TripWise.Api.Services;
using Asp.Versioning;
using System.Security.Claims;

namespace TripWise.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/trips")]
    [ApiVersion("1.0")]
    [Authorize]
    public class TripsController : ControllerBase
    {
        private readonly TripService _trips;
        private readonly ActivityService _activities;

        public TripsController(TripService trips, ActivityService activities)
        {
            _trips = trips;
            _activities = activities;
        }

        // -------------------- TRIPS --------------------

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if(string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _trips.GetAllAsync(userId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _trips.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTripDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _trips.CreateAsync(userId, dto);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateTripDto dto)
        {
            var result = await _trips.UpdateAsync(id, dto);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _trips.DeleteAsync(id);
            return NoContent();
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
