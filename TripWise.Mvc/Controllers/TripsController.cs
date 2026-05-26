using Microsoft.AspNetCore.Mvc;
using TripWise.Mvc.Services;
using TripWise.Mvc.Models;
using Microsoft.AspNetCore.Authorization;

namespace TripWise.Mvc.Controllers
{
    [Authorize]
    public class TripsController : Controller
    {
        private readonly TripService _trips;

        public TripsController(TripService trips)
        {
            _trips = trips;
        }

        // List ALL trips
        public async Task<IActionResult> Index()
        {
            var list = await _trips.GetTripsAsync();
            return View(list);
        }
        // Trip details
        public async Task<IActionResult> Details(string id)
        {
            var trip = await _trips.GetTripByIdAsync(id);
            return View(trip);
        }
    }
}
