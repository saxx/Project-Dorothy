using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

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
        public bool HasEmail { get; set; }
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
            var splitNames = Names.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            if (splitNames.Length == 1)
            {
                var firstName = splitNames[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                return "Hallo " + firstName + ",";
            }

            for (var i = 0; i < splitNames.Length; i++)
            {
                var firstName = splitNames[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                if (i == 0)
                {
                    result = "Liebe " + firstName + ",";
                }
                else
                {
                    result += " lieber " + firstName + ",";
                }
            }

            return result;
        }


        public string GetSaveTheDateLink(HttpRequest request)
        {
            return request.Scheme + "://" + request.Host + "/SaveTheDate/" + GetUniqueToken();
        }

        public string GetUniqueToken()
        {
            return Math.Abs(Id.ToString().GetHashCode()).ToString();
        }
    }
}
