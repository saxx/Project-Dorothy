using Dorothy.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Dorothy.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View(new IndexViewModel());
        }

        public IActionResult Route()
        {
            return View();
        }

        public IActionResult Agenda()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View(new IndexViewModel());
        }

        public IActionResult LogOff()
        {
            return Redirect("~/Home/Logoff");
        }
    }
}
