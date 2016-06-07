using Dorothy.Models;
using Dorothy.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Dorothy.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly Db _db;

        public UserController(Db db)
        {
            _db = db;
        }

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
