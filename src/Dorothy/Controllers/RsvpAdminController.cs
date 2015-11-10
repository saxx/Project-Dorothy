using System.Threading.Tasks;
using Dorothy.Models;
using Dorothy.ViewModels.RsvpAdmin;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;


namespace Dorothy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RsvpAdminController : Controller
    {
        private readonly Db _db;


        public RsvpAdminController(Db db)
        {
            _db = db;
        }


        public async Task<IActionResult> Index()
        {
            return View(await new IndexViewModel().Fill(_db));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var rsvp = await _db.Rsvps.FirstOrDefaultAsync(x => x.Id == id);
            if (rsvp == null)
            {
                return RedirectToAction("Index");
            }

            return View(new DeleteViewModel().Fill(rsvp));
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> PostDelete(int id)
        {
            var rsvp = await _db.Rsvps.FirstOrDefaultAsync(x => x.Id == id);
            if (rsvp != null)
            {
                _db.Rsvps.Remove(rsvp);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
