using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dorothy.Models;
using Microsoft.EntityFrameworkCore;

namespace Dorothy.ViewModels.Guests
{
    public class IndexViewModel
    {
        public async Task<IndexViewModel> Fill(IMapper mapper, Db db)
        {
            Guests = (await db.Guests.ToListAsync())
                .OrderBy(x => x.IsOptional)
                .ThenBy(x => x.Group)
                .ThenBy(x => x.Names)
                .Select(x => new Guest(mapper, x))
                .ToList();

            OverallCount = BuildGroupCount(Guests.ToList());

            var guestsWithoutGroup = Guests.Where(x => string.IsNullOrEmpty(x.Group)).ToList();
            WithoutGroupCount = BuildGroupCount(guestsWithoutGroup);

            PerGroupCount = new Dictionary<string, GroupCount>();
            foreach (var group in Guests.Select(x => x.Group).Where(x => !string.IsNullOrEmpty(x)).Distinct())
            {
                var guestsInGroup = Guests.Where(x => group == x.Group).ToList();
                PerGroupCount[group] = BuildGroupCount(guestsInGroup);
            }

            return this;
        }


        private GroupCount BuildGroupCount(IList<Guest> guests)
        {
            return new GroupCount
            {
                AdultCount = guests.Where(x => !x.IsOptional).Sum(x => x.AdultCount),
                AdultCountInvited = guests.Where(x => !x.IsOptional && x.HasInvitation).Sum(x => x.AdultCount),
                ChildCount = guests.Where(x => !x.IsOptional).Sum(x => x.ChildCount),
                ChildCountInvited = guests.Where(x => !x.IsOptional && x.HasInvitation).Sum(x => x.ChildCount),
                OptionalAdultCount = guests.Where(x => x.IsOptional).Sum(x => x.AdultCount),
                OptionalAdultCountInvited = guests.Where(x => x.IsOptional && x.HasInvitation).Sum(x => x.AdultCount),
                OptionalChildCount = guests.Where(x => x.IsOptional).Sum(x => x.ChildCount),
                OptionalChildCountInvited = guests.Where(x => x.IsOptional && x.HasInvitation).Sum(x => x.ChildCount)
            };
        }


        public IEnumerable<Guest> Guests { get; private set; }

        public GroupCount OverallCount { get; private set; }
        public GroupCount WithoutGroupCount { get; private set; }
        public IDictionary<string, GroupCount> PerGroupCount { get; private set; }

        public class GroupCount
        {
            public int AdultCount { get; set; }
            public int AdultCountInvited { get; set; }
            public int ChildCount { get; set; }
            public int ChildCountInvited { get; set; }
            public int OptionalAdultCount { get; set; }
            public int OptionalAdultCountInvited { get; set; }
            public int OptionalChildCount { get; set; }
            public int OptionalChildCountInvited { get; set; }

            public bool HasAny => AdultCount + ChildCount + OptionalAdultCount + OptionalChildCount > 0;
        }

        public class Guest : Models.Guest
        {
            public Guest(IMapper mapper, Models.Guest guest)
            {
                mapper.Map(guest, this);
            }

            public bool HasMissingName => Names == null || Names.IndexOf('?') >= 0;
            public bool HasMissingEmail => !ExtractEmailAdresses().Any();
        }
    }
}
