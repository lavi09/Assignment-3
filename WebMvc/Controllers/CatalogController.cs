using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _service;
        public CatalogController(ICatalogService service) =>
            _service = service;

        public async Task<IActionResult> Index(int? categoryFilterApplied, int? typeFilterApplied, int? cityFilterApplied, int? page)
        {
            var eventsOnPage = 10;
            var catalog = await _service.GetCatalogEventsAsync(page ?? 0, eventsOnPage,
                categoryFilterApplied, typeFilterApplied , cityFilterApplied);

            var vm = new CatalogIndexViewModel
            {
                CatalogEvents = catalog.Data,
                Categories = await _service.GetCategoriesAsync(),
                Types = await _service.GetTypesAsync(),
                Cities=await _service.GetCitiesAsync(),
                CategoryFilterApplied = categoryFilterApplied ?? 0,
                TypesFilterApplied = typeFilterApplied ?? 0,
                CitiesFilterApplied = cityFilterApplied ?? 0,
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    EventsPerPage = eventsOnPage,
                    TotalEvents = catalog.Count,
                    TotalPages = (int)Math.Ceiling((decimal)catalog.Count / eventsOnPage)
                }
            };

            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";

            return View(vm);
        }

     
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";


            return View();
        }



    }
}