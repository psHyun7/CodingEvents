using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
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
    }
}
