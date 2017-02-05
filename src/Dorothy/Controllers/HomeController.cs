using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Dorothy.Models;
using Dorothy.ViewModels.Home;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

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
                var properties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = new DateTime(2017, 12, 31)
                };

                if (model.Password == _config.NormalUserPassword)
                {
                    var claims = new[] { new Claim("name", "user"), new Claim(ClaimTypes.Role, "User") };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await
                        HttpContext.Authentication.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(identity), 
                            properties);
                    return RedirectToAction("Index", "User");
                }

                if (model.Password == _config.InsiderUserPassword)
                {
                    var claims = new[] { new Claim("name", "user"), new Claim(ClaimTypes.Role, "Insider") };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await
                        HttpContext.Authentication.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(identity),
                            properties);
                    return RedirectToAction("Index", "User");
                }

                if (model.Password == _config.AdminPassword)
                {
                    var claims = new[] { new Claim("name", "user"), new Claim(ClaimTypes.Role, "Admin") };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await
                        HttpContext.Authentication.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(identity),
                            properties);
                    return RedirectToAction("Index", "User");
                }

                ModelState.AddModelError("Password", "Hmm, das Passwort scheint ungültig zu sein. Bitte versuche es mit dem richtigen Passwort noch einmal.");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logoff()
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
