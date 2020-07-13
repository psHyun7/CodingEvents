using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        private EventDbContext context;

        public EventsController (EventDbContext dbContext)
        {
            context = dbContext;
        }
       
        // Get: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            //List<Event> events = new List<Event>(EventData.GetAll());
            List<Event> events = context.Events
                .Include(e => e.Category)
                .ToList();

            return View(events);
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<EventCategory> categories = context.EventCategories.ToList();
            AddEventViewModel addEventViewModel = new AddEventViewModel(categories);

            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory theCategory = context.EventCategories.Find(addEventViewModel.CategoryId);
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Location = addEventViewModel.Location,
                    NumAttendees = addEventViewModel.NumAttendees,
                    Description = addEventViewModel.Description,
                    //Type = addEventViewModel.Type,
                    Category = theCategory,
                    ContactEmail = addEventViewModel.ContactEmail
                };

                //EventData.Add(newEvent);
                context.Events.Add(newEvent);
                context.SaveChanges();
                return Redirect("/Events");
            }

            List<EventCategory> categories = context.EventCategories.ToList();
            addEventViewModel.Categories = new List<SelectListItem>();
            foreach (var category in categories)
            {
                addEventViewModel.Categories.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }
            return View(addEventViewModel);
        }

        public IActionResult Delete()
        {
            //List<Event> events = new List<Event>(EventData.GetAll());
            List<Event> events = context.Events
                .Include(e => e.Category)
                .ToList();
            return View(events);
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach(var id in eventIds)
            {
                //EventData.Remove(id);
                Event theEvent = context.Events.Find(id);
                context.Events.Remove(theEvent);
                context.SaveChanges();
            }

            return Redirect("/Events");
        }

        [HttpGet]
        [Route("/Events/Edit/{eventId?}")]
        public IActionResult Edit(int eventId)
        {
            if (eventId != 0)
            {
                //Event eventToEdit = EventData.GetById(eventId);
                Event eventToEdit = context.Events.Find(eventId);
                List<EventCategory> categories = context.EventCategories.ToList();
                EditEventViewModel editEventViewModel = new EditEventViewModel(categories);
                editEventViewModel.IdForEditEvent = eventId;
                editEventViewModel.Name = eventToEdit.Name;
                editEventViewModel.Location = eventToEdit.Location;
                editEventViewModel.NumAttendees = eventToEdit.NumAttendees;
                editEventViewModel.Description = eventToEdit.Description;
                editEventViewModel.ContactEmail = eventToEdit.ContactEmail;
                editEventViewModel.CategoryId = eventToEdit.CategoryId;
                ViewBag.title = $"Edit Event {eventToEdit.Name} (id={eventToEdit.Id})";
                return View(editEventViewModel);
            }
            else
            {
                return Redirect("/Events");
            }
        }

        [HttpPost]
        [Route("/Events/Edit/{eventId?}")]
        public IActionResult SubmitEditEventForm(EditEventViewModel editEventViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory theCategory = context.EventCategories.Find(editEventViewModel.CategoryId);
                Event eventToEdit = context.Events.Find(editEventViewModel.IdForEditEvent);
                eventToEdit.Name = editEventViewModel.Name;
                eventToEdit.Location = editEventViewModel.Location;
                eventToEdit.NumAttendees = editEventViewModel.NumAttendees;
                eventToEdit.Description = editEventViewModel.Description;
                //Type = addEventViewModel.Type,
                eventToEdit.Category = theCategory;
                eventToEdit.ContactEmail = editEventViewModel.ContactEmail;

                context.SaveChanges();
                return Redirect("/Events");
            }

            List<EventCategory> categories = context.EventCategories.ToList();
            editEventViewModel.Categories = new List<SelectListItem>();
            foreach (var category in categories)
            {
                editEventViewModel.Categories.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }
            return View("/Events/Edit", editEventViewModel);
        }

        public IActionResult Detail(int id)
        {
            Event theEvent = context.Events
                .Include(e => e.Category)
                .Single(e => e.Id == id);

            EventDetailViewModel viewModel = new EventDetailViewModel(theEvent);
            return View(viewModel);
        }
    }
}
