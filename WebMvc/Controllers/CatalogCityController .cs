using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMvc.Models;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class CatalogCityController : Controller
    {
       
        private ICatalogService _service;
        public CatalogCityController(ICatalogService service) =>
                     _service = service;

        
        public async Task<IActionResult> Index( string city)
         {
     
            var eventsCatalog = await _service.GetEventsInCityAsync(city);
            //var citycatalog = await _service.GetCityInfo(city);
            var vm = new CatalogCityIndexViewModel()
             {
                //CityItems = citycatalog.Data,
                Events = eventsCatalog.Data,
                 CityFilterName = city,
                 Cities = await _service.GetCitiesAsync(),
                 PaginationInfo = new PaginationInfo()
                 {
                     ActualPage = 0,
                     EventsPerPage = 6, 
                     TotalEvents = eventsCatalog.Count,
                     TotalPages = (int)Math.Ceiling(((decimal)eventsCatalog.Count /6)),
                 }
             };
             if (vm.PaginationInfo.TotalEvents < vm.PaginationInfo.EventsPerPage)
                 vm.PaginationInfo.EventsPerPage = vm.PaginationInfo.TotalEvents;

             vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";

             vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

             return View(vm);
         }

      

    }
}
