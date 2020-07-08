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
    public class EventCategoryController : Controller
    {
        private EventDbContext context;

        public EventCategoryController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        // Get: /EventCategory/Index
        public IActionResult Index()
        {
            List<EventCategory> eventCategories = context.EventCategories.ToList();

            return View(eventCategories);
        }

        // Get: /EventCategory/Create
        public IActionResult Create()
        {
            AddEventCategoryViewModel addEventCategoryViewModel = new AddEventCategoryViewModel();
            return View(addEventCategoryViewModel);
        }

        // Post: /EventCategory
        [HttpPost]
        [Route("/EventCategory/Create")]
        public IActionResult ProcessCreateEventCategoryForm(AddEventCategoryViewModel addEventCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory newEventCatogory = new EventCategory(addEventCategoryViewModel.Name);
                context.EventCategories.Add(newEventCatogory);
                context.SaveChanges();
                return Redirect("Index");
            }
            return View("create", addEventCategoryViewModel);
        }
    }
}
