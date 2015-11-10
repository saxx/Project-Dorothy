using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dorothy.Models;
using Microsoft.Data.Entity;

namespace Dorothy.ViewModels.RsvpAdmin
{
    public class IndexViewModel
    {
        public async Task<IndexViewModel> Fill(Db db)
        {
            Rsvps = await db.Rsvps.OrderBy(x => x.Name).ToListAsync();
            return this;
        }

        public IEnumerable<Models.Rsvp>  Rsvps { get; set; }

        public int AdultsYes
        {
            get { return Rsvps.Where(x => x.Type == RsvpType.Yes).Sum(x => x.AdultsCount); }
        }

        public int ChildsYes
        {
            get { return Rsvps.Where(x => x.Type == RsvpType.Yes).Sum(x => x.ChildCount); }
        }

        public int AdultsNo
        {
            get { return Rsvps.Where(x => x.Type == RsvpType.No).Sum(x => x.AdultsCount); }
        }

        public int ChildsNo
        {
            get { return Rsvps.Where(x => x.Type == RsvpType.No).Sum(x => x.ChildCount); }
        }

        public int AdultsMaybe
        {
            get { return Rsvps.Where(x => x.Type == RsvpType.Maybe).Sum(x => x.AdultsCount); }
        }

        public int ChildsMaybe
        {
            get { return Rsvps.Where(x => x.Type == RsvpType.Maybe).Sum(x => x.ChildCount); }
        }
    }
}
