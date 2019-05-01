using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
        //private readonly IHttpContextAccessor _httpContextAccesor;
        public CatalogService(IHttpClient httpClient, IConfiguration configuration)
        {
            _client = httpClient;
            _remoteServiceBaseUri = $"{configuration["CatalogUrl"]}/api/event/";
            //_httpContextAccesor = httpContextAccesor;
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
            var categoryUri = ApiPaths.Catalog.GetAllCategories(_remoteServiceBaseUri);
            var dataString = await _client.GetStringAsync(categoryUri);

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

        public async Task<CatalogEvent> GetEventItemAsync (int eventid)
        {
            var getEventDescriptionUri = ApiPaths.Catalog.GetEvent(_remoteServiceBaseUri, eventid);

            var dataString = await _client.GetStringAsync(getEventDescriptionUri);

            var item = JsonConvert.DeserializeObject<CatalogEvent>(dataString);

            return item; 

        }

        //extra

        

        public async Task<Catalog> GetEventsWithNameAsync(string name, int page, int take)
        {
            var eventswithnameUri = ApiPaths.Catalog.GetEventsWithName(_remoteServiceBaseUri, name, page, take);
            var dataString = await _client.GetStringAsync(eventswithnameUri);
            var response = JsonConvert.DeserializeObject<Catalog>(dataString);
            return response;
        }              

        public async Task<Catalog> GetEventsInCityAsync(string city)
        {
            var allEventsCityUri = ApiPaths.Catalog.GetCatalogEventsInCity(_remoteServiceBaseUri, city);
            var dataString = await _client.GetStringAsync(allEventsCityUri);
            var response = JsonConvert.DeserializeObject<Catalog>(dataString);
            return response;
        }
        
        async Task<CatalogCa> ICatalogService.GetCatalogCategoriesWithImageAsync(int page, int take)
        {
           var getCatalogCategoriesUri = ApiPaths.Catalog.GetAllCatalogCategoriesImage(_remoteServiceBaseUri, page, take);
            var dataString = await _client.GetStringAsync(getCatalogCategoriesUri);
            var response = JsonConvert.DeserializeObject<CatalogCa>(dataString);

            return response;
        }

        public async Task<List<CatalogCategory>> GetCategoriesforsearchAsync()
        {
            var getCatalogCategoriesUri = ApiPaths.Catalog.GetAllCategories(_remoteServiceBaseUri);
            var dataString = await _client.GetStringAsync(getCatalogCategoriesUri);
            var response = JsonConvert.DeserializeObject<List<CatalogCategory>>(dataString);

            return response;
        }
        public async Task<List<CatalogType>> GetTypesforsearchAsync()
        {
            var getCatalogTypesUri = ApiPaths.Catalog.GetAllTypes(_remoteServiceBaseUri);
            var dataString = await _client.GetStringAsync(getCatalogTypesUri);
            var response = JsonConvert.DeserializeObject<List<CatalogType>>(dataString);

            return response;
        }

        public async Task<Catalog> GetEventsByAllFiltersAsync(int page, int take, int? category, int? type)
        {
            var alleventsUri = ApiPaths.Catalog.GetEventsByAllFilters(_remoteServiceBaseUri, page, take, category, type);
            var dataString = await _client.GetStringAsync(alleventsUri);
            var response = JsonConvert.DeserializeObject<Catalog>(dataString);
            return response;
        }

        //async Task<string> GetUserTokenAsync()
        //{
        //    var context = _httpContextAccesor.HttpContext;

        //    return await context.GetTokenAsync("access_token");
        //}
        public async Task<int> CreateEvent(EventForCreation eve)
        {
            // var token = await GetUserTokenAsync();

            var addNewEventUri = ApiPaths.Catalog.PostEvent(_remoteServiceBaseUri);
            //   _logger.LogDebug(" OrderUri " + addNewOrderUri);


            var response = await _client.PostAsync(addNewEventUri, eve);
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error creating Event, try later.");
            }

            // response.EnsureSuccessStatusCode();
            var jsonString = response.Content.ReadAsStringAsync();

            jsonString.Wait();
            //  _logger.LogDebug("response " + jsonString);
            dynamic data = JObject.Parse(jsonString.Result);
            string value = data.id;
            return Convert.ToInt32(value);
        }


    }
}
