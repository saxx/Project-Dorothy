using System.Linq;
using Dorothy.Models;
using Dorothy.ViewModels.User;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;


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
           var x = _db.Guests.ToList();
            return View(new IndexViewModel());
        }
    }
}
