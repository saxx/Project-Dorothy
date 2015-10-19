using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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


        public IEnumerable<string> ExtractEmailAdresses()
        {
            if (string.IsNullOrWhiteSpace(Notes))
            {
                return Enumerable.Empty<string>();
            }
            return (from Match m in Regex.Matches(Notes, @"([A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,})", RegexOptions.IgnoreCase) select m.Groups[1].Value).ToList();
        }


        public string GetSalutation()
        {
            if (string.IsNullOrWhiteSpace(Names))
            {
                return "";
            }

            var result = "";
            var splitNames = Names.Split(new[] {'&'}, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < splitNames.Length; i++)
            {
                var name = splitNames[i].Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                if (i == 0)
                {
                    result = "Hallo " + name + ",";
                }
                else
                {
                    result += " hallo " + name + ",";
                }
            }

            return result;
        }
    }
}
