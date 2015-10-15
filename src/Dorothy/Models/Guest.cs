using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dorothy.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Names { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public bool IsOptional { get; set; }
        public bool HasInvitation { get; set; }
        public string Notes { get; set; }
        public string Group { get; set; }
    }
}
