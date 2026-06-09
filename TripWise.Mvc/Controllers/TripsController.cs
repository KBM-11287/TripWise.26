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
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }
        // -------------------------------------------------------------------------
        // CREATE: Trip Form (GET)
        // -------------------------------------------------------------------------
        public IActionResult Create()
        {
            return View();
        }
        // Create a new trip submit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTripViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var success = await _trips.CreateTripAsync(model);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Failed to create trip. Please try again.");
            return View(model);
        }
        // -------------------------------------------------------------------------
        // UPDATE: Edit Form (GET)
        // -------------------------------------------------------------------------
        public async Task<IActionResult> Edit(string id)
        {
            var trip = await _trips.GetTripByIdAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            var editModel = new EditTripViewModel
            {
                Id = trip.Id,
                Title = trip.Title,
                Destination = trip.Destination,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate
            };
            return View(editModel);
        }
        // a trip Edit submit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTripViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var success = await _trips.UpdateTripAsync(model);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Failed to update trip. Please try again.");
            return View(model);
        }
        // -------------------------------------------------------------------------
        // DELETE: Confirmation/Action
        // -------------------------------------------------------------------------
        public async Task<IActionResult> Delete(string id)
        {
            var trip = await _trips.GetTripByIdAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var success = await _trips.DeleteTripAsync(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Failed to delete trip. Please try again.");
            return RedirectToAction(nameof(Delete), new { id });
        }
    }

}
