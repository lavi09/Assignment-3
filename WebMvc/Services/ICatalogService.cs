using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.Services
{
   public interface ICatalogService
    {
        Task<Catalog> GetCatalogEventsAsync(int page, int take, int? category, int? type, int? city);

        Task<IEnumerable<SelectListItem>> GetCategoriesAsync();
        Task<IEnumerable<SelectListItem>> GetTypesAsync();
        Task<IEnumerable<SelectListItem>> GetCitiesAsync();
        Task<Catalog> GetEventsByAllFiltersAsync(int page, int take, int? category, int? type);

        Task<CatalogEvent> GetEventItemAsync(int eventid);

        //extra
        Task<Catalog> GetEventsWithNameAsync(string name, int page, int take);

        

        Task<CatalogCa> GetCatalogCategoriesWithImageAsync(int page, int take);

     
        Task<Catalog> GetEventsInCityAsync(string city);
        Task<List<CatalogCategory>> GetCategoriesforsearchAsync();
        Task<List<CatalogType>> GetTypesforsearchAsync();


        Task<int> CreateEvent(EventForCreation newEvent);
    }
}
