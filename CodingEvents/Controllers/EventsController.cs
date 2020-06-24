using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
       
        // Get: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.events = EventData.GetAll();

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("/Events/Add")]
        public IActionResult NewEvent(Event newEvent)
        {
            EventData.Add(newEvent);
            return Redirect("/Events");
        }

        public IActionResult Delete()
        {
            ViewBag.events = EventData.GetAll();
            return View();
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
        public IActionResult SubmitEditEventForm(int eventId, string name, string description)
        {
            Event eventToEdit = EventData.GetById(eventId);
            eventToEdit.Name = name;
            eventToEdit.Description = description;
            return Redirect("/Events");
        }
    }
}
