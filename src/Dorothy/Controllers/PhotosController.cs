using Dorothy.Models;
using Dorothy.ViewModels.Photos;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;


namespace Dorothy.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        private readonly Db _db;

        public PhotosController(Db db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel());
        }
    }
}
