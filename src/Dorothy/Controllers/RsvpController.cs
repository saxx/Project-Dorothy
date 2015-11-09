using System;
using System.Text;
using Dorothy.Models;
using Dorothy.ViewModels.Rsvp;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Dorothy.Controllers
{
    [Authorize]
    public class RsvpController : Controller
    {
        private readonly Db _db;
        private const string CookieKey = "rsvp";

        public RsvpController(Db db)
        {
            _db = db;
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

                AutoMapper.Mapper.Map(model, rsvp);
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
