﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.Models
{
    public class Event
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int NumAttendees { get; set; }
        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public EventCategory Category { get; set; }
        public int CategoryId { get; set; }

        public int Id { get; set;  }

        public Event()
        {
        }
        public Event(string name, string location, int numAttendees, string description, string contactEmail)
        {
            Name = name;
            Location = location;
            NumAttendees = numAttendees;
            Description = description;
            ContactEmail = contactEmail;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Event @event &&
                   Id == @event.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
