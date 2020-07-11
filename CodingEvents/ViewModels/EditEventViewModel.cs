using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodingEvents.ViewModels
{
    public class EditEventViewModel : EventViewModel
    {
        public int IdForEditEvent { get; set; }

        public EditEventViewModel() : base() { }

        public EditEventViewModel(List<EventCategory> categories) : base(categories) { }
    }
}
