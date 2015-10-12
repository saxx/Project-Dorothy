using Dorothy.ViewModels.User;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;


namespace Dorothy.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View(new IndexViewModel());
        }
    }
}
