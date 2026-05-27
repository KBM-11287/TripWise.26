using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TripWise.Api.Repositories;
using TripWise.Api.Models;
using Asp.Versioning;
using TripWise.Api.DTOs.Destinations;

namespace TripWise.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class DestinationController : ControllerBase
    {
        private readonly IDestinationRepository _destinations;

        public DestinationController(IDestinationRepository destinations)
        {
            _destinations = destinations;
        }

        // GET DESTINATION BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var dest = await _destinations.GetByIdAsync(id);
            if (dest == null)
                return NotFound();

            return Ok(dest);
        }

        // GET ALL DESTINATIONS
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var list = await _destinations.GetAllAsync();
            return Ok(list);
        }

        // CREATE DESTINATION
        [HttpPost]
        public async Task<IActionResult> Create(CreateDestinationDto dto)
        {
            var dest = new Destination
            {
                DestinationName = dto.DestinationName,
                Coordinates = new GeoJsonPoint
                {
                    Coordinates = new double[] { dto.Longitude, dto.Latitude }
                }
            };

            await _destinations.CreateAsync(dest);
            return Ok(dest);
        }

        // UPDATE DESTINATION
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateDestinationDto dto)
        {
            var existingDest = await _destinations.GetByIdAsync(id);
            if (existingDest == null) return NotFound();

            existingDest.DestinationName = dto.DestinationName;
            existingDest.Coordinates = new GeoJsonPoint
            {
                Coordinates = new double[] { dto.Longitude, dto.Latitude }
            };

            await _destinations.UpdateAsync(id, existingDest);
            return Ok(existingDest);
        }

        // DELETE DESTINATION
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingDestination = await _destinations.GetByIdAsync(id);
            if (existingDestination == null) return NotFound();

            await _destinations.DeleteAsync(id);
            return Ok(new { message = "Destination deleted successfully" });
        }
    }
}
