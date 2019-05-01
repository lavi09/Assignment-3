using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _service;
        public CatalogController(ICatalogService service) =>
            _service = service;

        public async Task<IActionResult> Index(int? categoryFilterApplied, int? typeFilterApplied, int? cityFilterApplied, int? page,String EventDateFilterApplied)
        {
            var eventsOnPage = 10;
            var catalog = await _service.GetCatalogEventsAsync(page ?? 0, eventsOnPage,
                categoryFilterApplied, typeFilterApplied , cityFilterApplied);
            var ecategories = await _service.GetCatalogCategoriesWithImageAsync(page ?? 0, eventsOnPage);

            var vm = new CatalogIndexViewModel
            {
                CatalogEvents = catalog.Data,
                Categories = await _service.GetCategoriesAsync(),
                CatalogCategoriesWithImage = ecategories.Data,
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


        //extra

        //public IActionResult Search(string SearchEventCategory, string SearchEventCity, string SearchEventDate)
        //{
        //    if (SearchEventCategory == null && SearchEventDate == null && SearchEventCity != null)
        //    {
        //        return RedirectToAction("Index", "CatalogCity", new { city = SearchEventCity });
        //    }
        //    else if (SearchEventCategory != null && SearchEventCity == null && SearchEventDate == null)
        //    {
        //        return RedirectToAction("EventSearchByCategory", "Catalog", new {categoryFilterApplied=SearchEventCategory});
        //    }
        //    else if (SearchEventCategory == null && SearchEventCity != null && SearchEventDate != null)
        //    {
        //        return RedirectToAction("EventSearchByCategory", "Catalog", new { EventDateFilterApplied = SearchEventDate });
        //    }
        //    else if (SearchEventCategory == null && SearchEventDate == null && SearchEventCity == null)
        //    {
        //        //uer did not provide anything
        //        ViewData["Message"] = $"PLEASE ENTER EVENT CATEGORY OR CITY OR DATE";
        //    }
           
        //    //else
        //    //{
                
        //    //    if (SearchEventCategory == null)
        //    //    {
        //    //        SearchEventCategory = "No Name";
        //    //    }
        //    //    if (SearchEventCity == null)
        //    //    {
        //    //        SearchEventCity = "No City";
        //    //    }
        //    //    if (SearchEventDate == null || SearchEventDate == "mm-dd-yyyy")
        //    //    {
        //    //        SearchEventDate = "No Date";
        //    //    }
        //    //    return RedirectToAction("Index", "SearchEventCatalog", new { name = SearchEventCategory
        //    //        , city = SearchEventCity, date = SearchEventDate });

        //    //}


        //    return View();
        //}

        public async Task<IActionResult> EventSearchByCategory(int? categoryFilterApplied, int? typeFilterApplied, int? page)
        {

            int eventsOnPage = 10;

            var catalog = await _service.GetEventsByAllFiltersAsync(page ?? 0, eventsOnPage, categoryFilterApplied, typeFilterApplied);

           
            List<CatalogCategory> s_categories = await _service.GetCategoriesforsearchAsync();
            List<CatalogType> s_types = await _service.GetTypesforsearchAsync();

            var vm = new EventFiltersCatalogViewModel()
            {

                CatalogEvents = catalog.Data,
                Categories = await _service.GetCategoriesAsync(),
                Types = await _service.GetTypesAsync(),
                Cities = await _service.GetCitiesAsync(),
               // Dates = _service.GetDates(),
               // DatesFilterApplied = EventDateFilterApplied ?? "All Days",
                CategoryFilterApplied = categoryFilterApplied?? 0,
                TypesFilterApplied = typeFilterApplied ?? 0,
                //CitiesFilterApplied = cityFilterApplied ?? "All",
                PaginationInfo = new PaginationInfo()
                {

                    ActualPage = page ?? 0,

                    TotalEvents = catalog.Count,
                    EventsPerPage = catalog.Count < eventsOnPage ? catalog.Count : eventsOnPage, 

                    TotalPages = (int)Math.Ceiling(((decimal)catalog.Count / eventsOnPage))

                }

            };
            foreach (var c in s_categories)
            {
                foreach (var eventitem in vm.CatalogEvents.Where(w => w.CatalogCategoryID== c.ID))
                {
                    eventitem.CatalogCategory = c.Category;
                    vm.CatalogEvents.Where(w => w.CatalogCategoryID == c.ID).First().CatalogCategory = c.Category;
                }
            }
            foreach (var t in s_types)
            {
                foreach (var eventitem in vm.CatalogEvents.Where(w => w.CatalogTypeID == t.ID))
                {
                    eventitem.CatalogType = t.Type;
                    vm.CatalogEvents.Where(w => w.CatalogTypeID == t.ID).First().CatalogType = t.Type;
                }
            }

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";

            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
     
            return View(vm);
        }


    }
}