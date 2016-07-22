using System;
using System.Text;
using Dorothy.Models;
using Dorothy.ViewModels.Rsvp;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dorothy.Controllers
{
    [Authorize]
    public class RsvpController : Controller
    {
        private readonly Db _db;
        private readonly IMapper _mapper;
        private const string CookieKey = "rsvp";

        public RsvpController(Db db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();

            if (Request.Cookies.ContainsKey(CookieKey))
            {
                try
                {
                    model = Newtonsoft.Json.JsonConvert.DeserializeObject<IndexViewModel>(Encoding.Unicode.GetString(Convert.FromBase64String(Request.Cookies[CookieKey])));
                }
                catch
                {
                    // do nothing here
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel model, bool overwrite)
        {
            if (ModelState.IsValid)
            {
                var rsvp = await _db.Rsvps.FirstOrDefaultAsync(x => string.Equals(x.Name, model.Name, StringComparison.CurrentCultureIgnoreCase));

                if (rsvp == null)
                {
                    rsvp = new Rsvp();
                    _db.Rsvps.Add(rsvp);
                }
                else
                {
                    if (!overwrite)
                    {
                        model.DisplayOverwriteMessage = true;
                        return View(model);
                    }
                }

                _mapper.Map(model, rsvp);
                rsvp.DateTime = DateTime.Now.ToUniversalTime();
                await _db.SaveChangesAsync();

                Response.Cookies.Append(CookieKey, Convert.ToBase64String(Encoding.Unicode.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(model))), new CookieOptions
                {
                    Expires = new DateTime(2016, 9, 4)
                });

                model.DisplaySuccessMessage = true;
            }
            return View(model);
        }
    }
}
