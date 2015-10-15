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
            Guests = (await db.Guests.ToListAsync()).OrderBy(x => x.IsOptional).ThenBy(x => x.Group).ThenBy(x => x.Names);

            OverallCount = new GroupCount
            {
                AdultCount = Guests.Where(x => !x.IsOptional).Sum(x => x.AdultCount),
                ChildCount = Guests.Where(x => !x.IsOptional).Sum(x => x.ChildCount),
                OptionalAdultCount = Guests.Where(x => x.IsOptional).Sum(x => x.AdultCount),
                OptionalChildCount = Guests.Where(x => x.IsOptional).Sum(x => x.ChildCount)
            };

            var guestsWithoutGroup = Guests.Where(x => string.IsNullOrEmpty(x.Group)).ToList();
            WithoutGroupCount = new GroupCount
            {
                AdultCount = guestsWithoutGroup.Where(x => !x.IsOptional).Sum(x => x.AdultCount),
                ChildCount = guestsWithoutGroup.Where(x => !x.IsOptional).Sum(x => x.ChildCount),
                OptionalAdultCount = guestsWithoutGroup.Where(x => x.IsOptional).Sum(x => x.AdultCount),
                OptionalChildCount = guestsWithoutGroup.Where(x => x.IsOptional).Sum(x => x.ChildCount)
            };

            PerGroupCount = new Dictionary<string, GroupCount>();
            foreach (var group in Guests.Select(x => x.Group).Where(x => !string.IsNullOrEmpty(x)).Distinct())
            {
                var guestsInGroup = Guests.Where(x => group == x.Group).ToList();
                PerGroupCount[group] = new GroupCount
                {
                    AdultCount = guestsInGroup.Where(x => !x.IsOptional).Sum(x => x.AdultCount),
                    ChildCount = guestsInGroup.Where(x => !x.IsOptional).Sum(x => x.ChildCount),
                    OptionalAdultCount = guestsInGroup.Where(x => x.IsOptional).Sum(x => x.AdultCount),
                    OptionalChildCount = guestsInGroup.Where(x => x.IsOptional).Sum(x => x.ChildCount)
                };
            }

            return this;
        }

        public IEnumerable<Guest> Guests { get; private set; }

        public GroupCount OverallCount { get; private set; }
        public GroupCount WithoutGroupCount { get; private set; }
        public IDictionary<string, GroupCount> PerGroupCount { get; private set; }

        public class GroupCount
        {
            public int AdultCount { get; set; }
            public int ChildCount { get; set; }
            public int OptionalAdultCount { get; set; }
            public int OptionalChildCount { get; set; }

            public bool HasAny => AdultCount + ChildCount + OptionalAdultCount + OptionalChildCount > 0;
        }
    }
}
