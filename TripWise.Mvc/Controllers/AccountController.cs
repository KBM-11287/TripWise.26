using Microsoft.AspNetCore.Mvc;
using TripWise.Mvc.Services;
using TripWise.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TripWise.Mvc.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ApiClientService _api;

        public AccountController(ApiClientService api)
        {
            _api = api;
        }

        // ---------------------------
        // LOGIN (GET)
        // ---------------------------
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // ---------------------------
        // LOGIN (POST)
        // ---------------------------
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var result = await _api.PostAsync<LoginResponse>(
                    "/api/v1/auth/login",
                    new { email = model.Email, password = model.Password }
                );

                // Store JWT in cookie
                Response.Cookies.Append("TripWiseToken", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

                // Create MVC auth cookie
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Email)
                };

                var identity = new ClaimsIdentity(claims, "TripWiseCookie");
                await HttpContext.SignInAsync("TripWiseCookie", new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "Trips");
            }
            catch
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }
        }

        // ---------------------------
        // REGISTER (GET)
        // ---------------------------
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // ---------------------------
        // REGISTER (POST)
        // ---------------------------
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            Console.WriteLine("REGISTER POST HIT");
            if(!ModelState.IsValid)
                return View(model);

            try
            {
                //Call API to register user
                var result = await _api.PostAsync<LoginResponse>(
                    "/api/v1/auth/register",
                    new { name = model.Name, email = model.Email, password = model.Password }
                );
                // Store JWT in cookie
                Response.Cookies.Append("TripWiseToken", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                });
                // Create MVC auth cookie
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Email)
                };
                var identity = new ClaimsIdentity(claims, "TripWiseCookie");
                await HttpContext.SignInAsync("TripWiseCookie", new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "Trips");
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError("", ex.ApiErrorMessage);
                return View(model);
            }
            catch
            {
               ModelState.AddModelError("", "An error occurred while registering. Please try again.");
                return View(model);
            }
        }

        // ---------------------------
        // LOGOUT
        // ---------------------------
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("TripWiseToken");
            await HttpContext.SignOutAsync("TripWiseCookie");

            return RedirectToAction("Login");
        }
    }
}
