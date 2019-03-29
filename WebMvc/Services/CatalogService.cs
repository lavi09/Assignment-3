using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMvc.Infrastructure;
using WebMvc.Models;

namespace WebMvc.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly string _remoteServiceBaseUri;
        private readonly IHttpClient _client;
        public CatalogService(IHttpClient httpClient, IConfiguration configuration)
        {
            _client = httpClient;
            _remoteServiceBaseUri = $"{configuration["CatalogUrl"]}/api/catalog/";
        }

        public async Task<Catalog> GetCatalogEventsAsync(int page, int take, int? category, int? type, int? city)
        {
            var catalogEventsUri = ApiPaths.Catalog.GetAllCatalogEvents(_remoteServiceBaseUri,
                page, take,  category, type, city);

            var dataString = await _client.GetStringAsync(catalogEventsUri);
            var response = JsonConvert.DeserializeObject<Catalog>(dataString);

            return response;
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            var brandUri = ApiPaths.Catalog.GetAllCategories(_remoteServiceBaseUri);
            var dataString = await _client.GetStringAsync(brandUri);

            var events = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value= null,
                    Text = "All",
                    Selected=true
                }
            };

            var categories = JArray.Parse(dataString);
            foreach (var category in categories)
            {
                events.Add(
                    new SelectListItem
                    {
                        Value = category.Value<string>("id"),
                        Text = category.Value<string>("category")
                    });
            }

            return events;
        }        

        public async Task<IEnumerable<SelectListItem>> GetTypesAsync()
        {
            var typeUri = ApiPaths.Catalog.GetAllTypes(_remoteServiceBaseUri);
            var dataString = await _client.GetStringAsync(typeUri);

            var events = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value= null,
                    Text = "All",
                    Selected=true
                }
            };

            var types = JArray.Parse(dataString);
            foreach (var type in types)
            {
                events.Add(
                    new SelectListItem
                    {
                        Value = type.Value<string>("id"),
                        Text = type.Value<string>("type")
                    });
            }

            return events;
        }

        public async Task<IEnumerable<SelectListItem>> GetCitiesAsync()
        {
            var cityUri = ApiPaths.Catalog.GetAllCities(_remoteServiceBaseUri);
            var dataString = await _client.GetStringAsync(cityUri);

            var events = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value= null,
                    Text = "All",
                    Selected=true
                }
            };

            var cities = JArray.Parse(dataString);
            foreach (var city in cities)
            {
                events.Add(
                    new SelectListItem
                    {
                        Value = city.Value<string>("id"),
                        Text = city.Value<string>("city")
                    });
            }

            return events;
        }


    }
}
