using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dorothy.Models;
using Microsoft.Data.Entity;


namespace Dorothy.ViewModels.Guests
{
    public class CreateViewModel
    {

        public async Task<CreateViewModel> Fill(Db db)
        {
            AvailableGroups = await db.Guests.Select(x => x.Group).Where(x => x != null && x != "").Distinct().ToListAsync();
            return this;
        }

        [Required]
        public string Names { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public bool IsOptional { get; set; }
        public bool HasInvitation { get; set; }
        public string Notes { get; set; }
        public string Group { get; set; }

        public IEnumerable<string> AvailableGroups { get; private set; }
    }
}
