using System.Threading.Tasks;
using Dorothy.Models;
using Dorothy.ViewModels.Guests;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;


namespace Dorothy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GuestsController : Controller
    {
        private readonly Db _db;


        public GuestsController(Db db)
        {
            _db = db;
        }


        public async Task<IActionResult> Index()
        {
            return View(await new IndexViewModel().Fill(_db));
        }


        public async Task<IActionResult> Create()
        {
            return View(await new CreateViewModel().Fill(_db));
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var guest = new Guest
                {
                    Names = model.Names,
                    AdultCount = model.AdultCount,
                    ChildCount = model.ChildCount,
                    HasInvitation = model.HasInvitation,
                    IsOptional = model.IsOptional,
                    Notes = model.Notes,
                    Group = model.Group
                };

                _db.Guests.Add(guest);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<IActionResult> Edit(int id)
        {
            var guest = await _db.Guests.FirstOrDefaultAsync(x => x.Id == id);
            if (guest == null)
            {
                return RedirectToAction("Index");
            }

            return View(await new EditViewModel().Fill(_db, guest));
        }


        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> PostEdit(int id, EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var guest = await _db.Guests.FirstOrDefaultAsync(x => x.Id == id);
            if (guest != null)
            {
                guest.Names = model.Names;
                guest.AdultCount = model.AdultCount;
                guest.ChildCount = model.ChildCount;
                guest.HasInvitation = model.HasInvitation;
                guest.IsOptional = model.IsOptional;
                guest.Notes = model.Notes;
                guest.Group = model.Group;

                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var guest = await _db.Guests.FirstOrDefaultAsync(x => x.Id == id);
            if (guest == null)
            {
                return RedirectToAction("Index");
            }

            return View(new DeleteViewModel().Fill(guest));
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> PostDelete(int id)
        {
            var guest = await _db.Guests.FirstOrDefaultAsync(x => x.Id == id);
            if (guest != null)
            {
                _db.Guests.Remove(guest);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
