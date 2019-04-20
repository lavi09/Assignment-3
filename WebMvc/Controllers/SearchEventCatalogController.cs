using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Services;
using WebMvc.ViewModels;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class SearchEventCatalogController: Controller
    {
        private ICatalogService _service;
        public SearchEventCatalogController(ICatalogService service) =>

            _service = service;

        public async Task<IActionResult> Index(string name, string city, string date, int? page)
        {
            int eventsPage = 10;
            
            var catalog = await

                _service.GetEventsWithNameCityDateAsync(name, city, date, page ?? 0, eventsPage);


            //get eventcategories from service, then from apipath who gets it from EventCatalog api to get  categories from EventCategoryDB
            var ecategories = await _service.GetCatalogCategoriesWithImageAsync(page ?? 0, eventsPage);

            //pass events into view model to return back to httpclient
            var vm = new CatalogIndexViewModel()
            {

                CatalogEvents = catalog.Data,

                Categories = await _service.GetCategoriesAsync(),

                CatalogCategoriesWithImage = ecategories.Data,

                Types = await _service.GetTypesAsync(),

                CategoryFilterApplied = 0,

                TypesFilterApplied = 0,

                PaginationInfo = new PaginationInfo()

                {

                    ActualPage = page ?? 0,

                    TotalEvents = catalog.Count,
                    EventsPerPage = catalog.Count < eventsPage ? catalog.Count : eventsPage, //catalog.Data.Count,

                    TotalPages = (int)Math.Ceiling(((decimal)catalog.Count / eventsPage))

                }

            };

            //update the categoryname of allevents in vm
            foreach (var category in vm.CatalogCategoriesWithImage)
            {
                foreach (var eventitem in vm.CatalogEvents.Where(w => w.CatalogCategoryID == category.ID))
                {
                    eventitem.CatalogCategory = category.Category;
                }
            }

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";

            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";


            //heading to specify search
            if (name == "No Name")
            {
                name = "";
            }
            if (city == "No city")
            {
                city = "";
            }
            if (date == "no Date")
            {
                date = "";
            }
            ViewData["Message"] = $"Search results for {name} {city} {date}";

            return View(vm);

        }

    }
}
