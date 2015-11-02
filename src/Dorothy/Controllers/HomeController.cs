using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Security.Claims;
using Dorothy.Models;
using Dorothy.ViewModels.Home;

namespace Dorothy.Controllers
{
    public class HomeController : Controller
    {
        private readonly Configuration _config;

        public HomeController(Configuration config)
        {
            _config = config;
        }


        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Login");
        }


        [Route("~/Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User");
            }
            return View(new LoginViewModel());
        }


        [HttpPost]
        [Route("~/Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == _config.UserPassword)
                {
                    var claims = new[] { new Claim("name", "user"), new Claim(ClaimTypes.Role, "User") };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await
                        HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(identity));
                    return RedirectToAction("Index", "User");
                }

                if (model.Password == _config.AdminPassword)
                {
                    var claims = new[] { new Claim("name", "user"), new Claim(ClaimTypes.Role, "Admin") };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await
                        HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(identity));
                    return RedirectToAction("Index", "User");
                }

                ModelState.AddModelError("Password", "Passwort ungültig.");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }


        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
