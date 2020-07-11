using CodingEvents.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.ViewModels
{
    public class AddEventViewModel : EventViewModel
    {
        public AddEventViewModel() : base() { }

        public AddEventViewModel(List<EventCategory> categories) : base(categories) { }
    }
       
}
