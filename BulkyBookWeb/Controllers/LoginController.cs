using BulkyBookWeb.Data;
using BulkyBookWeb.IRepositories;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace BulkyBookWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            var objUserList = _userRepository.GetAllusers(user);
            if (objUserList != null)
            {
                // User found, sign in and navigate to home
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                TempData["success"] = "Login successfully";
                return RedirectToAction("Index", "Home");
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Clear TempData
            TempData.Clear();

            // User not found, display a message to register
            ViewData["ErrorMessage"] = "User not found. Please register.";
            return View(objUserList);
         }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Clear TempData
            TempData.Clear();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.AddUser(user);
                // User found, Navigate to login page.
                TempData["success"] = "Registered successfully";
                return RedirectToAction("Index", "Login"); // Redirect to home page or login page
            }
            return View("Index");
        }

    }
}