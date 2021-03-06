﻿using System.Threading.Tasks;
using AutoMapper;
using Dorothy.Models;
using Dorothy.ViewModels.Guests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dorothy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GuestsController : Controller
    {
        private readonly Db _db;
        private readonly IMapper _mapper;


        public GuestsController(Db db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            return View(await new IndexViewModel().Fill(_mapper, _db));
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
                var guest = new Guest();
                _mapper.Map(model, guest);
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

            return View(await new EditViewModel().Fill(_mapper, _db, guest));
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
                _mapper.Map(model, guest);
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
