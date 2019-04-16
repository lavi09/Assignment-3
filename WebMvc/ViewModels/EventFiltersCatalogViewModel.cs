using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class EventFiltersCatalogViewModel
    {
        public IEnumerable<CatalogEvent> CatalogEvents { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> Dates { get; set; }   

       
        public int? CategoryFilterApplied { get; set; }
        public int? TypesFilterApplied { get; set; }
        public String CitiesFilterApplied { get; set; }
        public String DatesFilterApplied { get; set; }

        public PaginationInfo PaginationInfo { get; set; }
    }
}
