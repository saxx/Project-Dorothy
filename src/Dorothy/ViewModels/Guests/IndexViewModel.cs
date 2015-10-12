using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dorothy.Models;
using Microsoft.Data.Entity;

namespace Dorothy.ViewModels.Guests
{
    public class IndexViewModel
    {
        public async Task<IndexViewModel> Fill(Db db)
        {
            Guests = await db.Guests.OrderBy(x => x.Names).ToListAsync();
            return this;
        }
        
        public IEnumerable<Guest> Guests { get; set; }
    }
}
