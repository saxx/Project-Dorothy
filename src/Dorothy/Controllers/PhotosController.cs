using Dorothy.ViewModels.Photos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Dorothy.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        public IActionResult Index()
        {
            return View(new IndexViewModel());
        }
    }
}
