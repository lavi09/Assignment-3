using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class EventCreationViewModel
    {
        public EventForCreation Event { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
       // public IEnumerable<SelectListItem> Cities { get; set; }

        public int? CategoryFilterApplied { get; set; }
        public int? TypesFilterApplied { get; set; }
        //public int? CityFilterApplied { get; set; }
        // public PaginationInfo PaginationInfo { get; set; }
    }
}
