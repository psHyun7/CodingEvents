using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
       
        // Get: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = new List<Event>(EventData.GetAll());

            return View(events);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel();

            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Location = addEventViewModel.Location,
                    NumAttendees = addEventViewModel.NumAttendees,
                    Description = addEventViewModel.Description,
                    Type = addEventViewModel.Type,
                    ContactEmail = addEventViewModel.ContactEmail
                };

                EventData.Add(newEvent);
                return Redirect("/Events");
            }

            return View(addEventViewModel);
        }

        public IActionResult Delete()
        {
            List<Event> events = new List<Event>(EventData.GetAll());
            return View(events);
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach(var id in eventIds)
            {
                EventData.Remove(id);
            }

            return Redirect("/Events");
        }

        [Route("/Events/Edit/{eventId?}")]
        public IActionResult Edit(int eventId)
        {
            Event eventToEdit = EventData.GetById(eventId);
            ViewBag.title = $"Edit Event {eventToEdit.Name} (id={eventToEdit.Id})";
            ViewBag.eventToEdit = eventToEdit;
            return View();
        }

        [HttpPost]
        [Route("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string location, int numAttendees, string description, EventType eventType, string email)
        {
            Event eventToEdit = EventData.GetById(eventId);
            eventToEdit.Name = name;
            eventToEdit.Location = location;
            eventToEdit.NumAttendees = numAttendees;
            eventToEdit.Description = description;
            eventToEdit.Type = eventType;
            eventToEdit.ContactEmail = email;
            return Redirect("/Events");
        }
    }
}
