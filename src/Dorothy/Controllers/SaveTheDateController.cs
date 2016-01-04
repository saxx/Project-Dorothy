using System.Linq;
using System.Threading.Tasks;
using Dorothy.Models;
using Dorothy.ViewModels.SaveTheDate;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace Dorothy.Controllers
{
    [AllowAnonymous]
    [Route("~/SaveTheDate")]
    public class SaveTheDateController : Controller
    {
        private readonly Db _db;


        public SaveTheDateController(Db db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("Index");
            }

            var allGuests = await _db.Guests.ToListAsync();
            var guest = allGuests.FirstOrDefault(x => id == x.GetUniqueToken());
            if (guest == null)
            {
                return RedirectToAction("Index");
            }

            return View(new IndexViewModel
            {
                Salutation = guest.GetSalutation()
            });
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View(new IndexViewModel());
        }
    }
}
