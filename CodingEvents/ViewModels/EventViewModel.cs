using CodingEvents.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.ViewModels
{
    public abstract class EventViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please enter the number of attendees")]
        [Range(0, 100000, ErrorMessage = "Number of Attendees must be between 0 and 100,000")]
        public int NumAttendees { get; set; }

        [Required(ErrorMessage = "please enter a description for your event.")]
        [StringLength(500, ErrorMessage = "Description is too long!")]
        public string Description { get; set; }

        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public EventViewModel() { }

        public EventViewModel(List<EventCategory> categories)
        {
            Categories = new List<SelectListItem>();

            foreach (var category in categories)
            {
                Categories.Add(
                    new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name
                    }
                );
            }
        }

        // Changed EventType to EventCategory
        //public List<SelectListItem> EventTypes { get; set; } = new List<SelectListItem>
        //{
        //    new SelectListItem(EventType.Conference.ToString(), ((int)EventType.Conference).ToString()),
        //    new SelectListItem(EventType.Meetup.ToString(), ((int)EventType.Meetup).ToString()),
        //    new SelectListItem(EventType.Workshop.ToString(), ((int)EventType.Workshop).ToString()),
        //    new SelectListItem(EventType.Social.ToString(), ((int)EventType.Social).ToString())
        //};
    }
}
