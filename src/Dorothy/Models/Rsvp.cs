﻿using System;

namespace Dorothy.Models
{
    public class Rsvp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AdultsCount { get; set; }
        public int ChildCount { get; set; }
        public string Note { get; set; }
        public RsvpType Type { get; set; }
        public DateTime DateTime { get; set; }
    }

    public enum RsvpType
    {
        Yes,
        No,
        Maybe
    }
}
