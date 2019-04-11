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

        Task<CatalogEvent> GetEventItemAsync(int eventid);


    }
}
