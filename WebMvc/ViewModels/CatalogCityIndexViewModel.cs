﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class CatalogCityIndexViewModel
    {
        public IEnumerable<CatalogEvent> Events { get; set; }
        public IEnumerable<CatalogCity> CityItems { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public int? CityFilterApplied { get; set; }
        public string CityFilterName { get; set; }
      //  public string? SelectedCityFilter { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
