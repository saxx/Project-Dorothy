using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Security.Claims;
using Dorothy.Models;

namespace Dorothy.Controllers
{
    public class HomeController : Controller
    {
        private readonly Configuration _config;

        public HomeController(Configuration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> Index(string password)
        {
            if (password == _config.UserPassword)
            {
                var claims = new[] { new Claim("name", "user"), new Claim(ClaimTypes.Role, "User") };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return RedirectToAction("Index", "User");
            }
            else if (password == _config.AdminPassword)
            {
                var claims = new[] { new Claim("name", "user"), new Claim(ClaimTypes.Role, "Admin") };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return RedirectToAction("Index", "User");
            }
            else
            {
                ModelState.AddModelError("", "Passwort ungültig.");
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
