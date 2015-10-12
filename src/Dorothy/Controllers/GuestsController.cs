using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dorothy.Models;
using Dorothy.ViewModels.Guests;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;


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
    }
}
