﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.ViewModels
{
    public class AddEventCategoryViewModel
    {
        [Required(ErrorMessage="Name is Required.")]
        [StringLength(20, MinimumLength =3, ErrorMessage ="Name must be between 3 and 20 characters.")]
        public string Name { get; set; }
    }
}
