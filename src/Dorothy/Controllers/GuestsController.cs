using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult Create()
        {
            return View(new CreateViewModel());
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
                    Notes = model.Notes
                };

                _db.Guests.Add(guest);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
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
        public async Task<IActionResult> DeletePost(int id)
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
