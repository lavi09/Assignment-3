using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogApi.Data;
using EventCatalogApi.Domain;
using EventCatalogApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventCatalogApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Event")]
    public class CatalogController : Controller
    {
        private readonly CatalogContext _context;
        private readonly IConfiguration _config;

        public CatalogController(CatalogContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet]
        [Route("[action]")]

        public async Task<IActionResult> CatalogTypes()
        {
            var events = await _context.CatalogTypes.ToListAsync();
            return Ok(events);
        }
        [HttpGet]
        [Route("[action]")]

        public async Task<IActionResult> CatalogCities()
        {
            var events = await _context.CatalogCities.ToListAsync();
            return Ok(events);
        }



        [HttpGet]
        [Route("[action]")]

        public async Task<IActionResult> CatalogCategories()
        {
            var events = await _context.CatalogCategories.ToListAsync();
            return Ok(events);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CatalogCategoriesImage([FromQuery] int pageSize = 6,
                                                [FromQuery] int pageIndex = 0)
        {
            var eventsCount = await _context.CatalogCategories
                                    .LongCountAsync();
            var events = await _context.CatalogCategories
                                        .OrderBy(c => c.Category)
                                        .Skip(pageSize * pageIndex)
                                        .Take(pageSize)
                                        .ToListAsync();

            events = ChangeCatalogPictureUrl(events);
            var model = new PaginatedEventsViewModel<CatalogCategory>
                   (pageIndex, pageSize, eventsCount, events);

            return Ok(model);
        }
      

        [HttpGet]
        [Route("Events/{id:int}")]
        public async Task<IActionResult> GetEventsById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Incorrect Id!");
            }

            var Event = await _context.CatalogEvents.SingleOrDefaultAsync(c => c.ID == id);

            if (Event != null)
            {
                Event.PictureUrl =Event.PictureUrl
                    .Replace("http://externalcatalogbaseurltobereplaced"
                    , _config["ExternalCatalogBaseUrl"]);
                return Ok(Event);
            }

            return NotFound();
        }
        
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Events([FromQuery] int pageSize = 6,[FromQuery] int pageIndex = 0)
        {
            var eventsCount =
                await _context.CatalogEvents.LongCountAsync();

            var events = await _context.CatalogEvents

                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            events = ChangePictureUrl(events);

            var model = new PaginatedEventsViewModel<CatalogEvent>(pageIndex, pageSize, eventsCount, events);            
            
            return Ok(model);

        }

        private List<CatalogEvent>
            ChangePictureUrl(List<CatalogEvent> events)
        {
            events.ForEach(
                c => c.PictureUrl = c.PictureUrl
                    .Replace("http://externalcatalogbaseurltobereplaced"
                    , _config["ExternalCatalogBaseUrl"])
                );
            return events;
        }
        private List<CatalogCategory> ChangeCatalogPictureUrl(List<CatalogCategory> events)
        {
            events.ForEach(
                 x => x.PictureUrl =
                 x.PictureUrl
                 .Replace("http://externalcatalogbaseurltobereplaced",
                 _config["ExternalCatalogBaseUrl"])
                 );

            return events;
        }

        //extra
        //GET api/Events/withname/daring Women?pageSize=2&pageIndex=0
        [HttpGet]
        [Route("[action]/withname/{name:minlength(1)}")]
        public async Task<IActionResult> Events(string name,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var eventsCount = await _context.CatalogEvents
                                    .Where(c => c.Name.StartsWith(name))
                                    .LongCountAsync();
            var events = await _context.CatalogEvents
                                    .Where(c => c.Name.StartsWith(name))
                                    .OrderBy(c => c.Name)
                                    .Skip(pageSize * pageIndex)
                                    .Take(pageSize)
                                    .ToListAsync();
            events = ChangePictureUrl(events);
            var model = new PaginatedEventsViewModel<CatalogEvent>
                    (pageIndex, pageSize, eventsCount, events);

            return Ok(model);
        }


        [HttpGet]
        [Route("Events/date/{date}")]
        public async Task<IActionResult> EventsWithDate(DateTime date,
         [FromQuery] int pageSize = 6,
         [FromQuery] int pageIndex = 0)
        {
            var eventsCount = await _context.CatalogEvents
                                    .Where(c => c.StartDate == date)
                                    .LongCountAsync();
            var events = await _context.CatalogEvents
                                    .Where(c => c.StartDate == date)
                                    .OrderBy(c => c.Name)
                                    .Skip(pageSize * pageIndex)
                                    .Take(pageSize)
                                    .ToListAsync();
            events = ChangePictureUrl(events);
            var model = new PaginatedEventsViewModel<CatalogEvent>
                    (pageIndex, pageSize, eventsCount, events);

            return Ok(model);
        }

        [HttpGet]
        [Route("[action]/catalogtype/{catalogtypeId}/catalogcategory/{catalogcategoryId}")]

        public async Task<IActionResult> Events(int? catalogtypeId, int? catalogcategoryId, [FromQuery] int pageSize = 6,
                                                                              [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<CatalogEvent>)_context.CatalogEvents;
            if (catalogtypeId.HasValue)
            {
                root = root.Where(c => c.CatalogTypeID == catalogtypeId);
            }
            if (catalogcategoryId.HasValue)
            {
                root = root.Where(c => c.CatalogCategoryID == catalogcategoryId);
            }
            var eventsCount = await root
                                .LongCountAsync();

            var events = await root
                                .OrderBy(c => c.Name)
                                .Skip(pageSize * pageIndex)
                                .Take(pageSize)
                                .ToListAsync();
            events = ChangePictureUrl(events);
            var model = new PaginatedEventsViewModel<CatalogEvent>(pageIndex, pageSize, eventsCount, events);
            return Ok(model);
        }

        [HttpGet]
        [Route("Events/withcity/{city:minlength(1)}")]
        public async Task<IActionResult> EventsWithCity(string city,
          [FromQuery] int pageSize = 6,
          [FromQuery] int pageIndex = 0)
        {

            //need to address city and state
            string State = null;
            if (city.Contains(","))
            {
                var cityState = city.Split(',');
                city = cityState[0];
                State = cityState[1];
            }

            var root = (IQueryable<CatalogEvent>)_context.CatalogEvents;

            if (State != null)
            {
                root = root.Where(c => c.State.StartsWith(State));
            }

            var eventsCount = await root
                                    .Where(c => c.City.StartsWith(city))
                                    .LongCountAsync();
            var events = await root
                                    .Where(c => c.City.StartsWith(city))
                                    .OrderBy(c => c.Name)
                                    .Skip(pageSize * pageIndex)
                                    .Take(pageSize)
                                    .ToListAsync();
            events = ChangePictureUrl(events);
            var model = new PaginatedEventsViewModel<CatalogEvent>
                    (pageIndex, pageSize, eventsCount, events);

            return Ok(model);

        }
        [HttpGet]
        [Route("[action]/withcityname/{city:minlength(1)}")]
        public async Task<IActionResult> City(string city,
                     [FromQuery] int pageSize = 6,
                      [FromQuery] int pageIndex = 0)
        {
            var eventsCount = await _context.CatalogCities
                                 .Where(c => c.City.StartsWith(city))
                                 .LongCountAsync();
            var events = await _context.CatalogCities
                                 .Where(c => c.City.StartsWith(city))
                                 .OrderBy(c => c.City)
                                 .Skip(pageSize * pageIndex)
                                 .Take(pageSize)
                                 .ToListAsync();
         
            var model = new PaginatedEventsViewModel<CatalogCity>
                 (pageIndex, pageSize, eventsCount, events);

            return Ok(model);
        }


        [HttpGet]
        [Route("events/title/{title}/city/{city}/date/{date}")]

        public async Task<IActionResult> EventsWithTitleCityDate(string title, string city, string date, [FromQuery] int pageSize = 6,
                                                                         [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<CatalogEvent>)_context.CatalogEvents;

            if (title != "notitle")
            {
                root = root.Where(c => c.Name.StartsWith(title));
            }
            if (city != "nocity")
            {
                root = root.Where(c => c.City.StartsWith(city));
            }
            if (date != "nodate")
            {
                root = root.Where(c => c.StartDate.ToShortDateString() == date.ToString());
            }
            var eventsCount = await root
                                .LongCountAsync();
            var events = await root
                                .OrderBy(c => c.Name)
                                .Skip(pageSize * pageIndex)
                                .Take(pageSize)
                                .ToListAsync();
            events = ChangePictureUrl(events);
            var model = new PaginatedEventsViewModel<CatalogEvent>(pageIndex, pageSize, eventsCount, events);
            return Ok(model);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> AllEventsCities()
        {
            //  IList<String> totalItems;
            List<String> cities = new List<string>();
            var totalItems = await _context.CatalogEvents.ToListAsync();
            foreach (var item in totalItems)
            {
                if (!cities.Contains(item.City + "," + item.State))
                    cities.Add(item.City + "," + item.State);
            }

            return Ok(cities);
        }

        [HttpGet]
        [Route("[action]/type/{eventTypeId}/category/{eventCategoryId}/date/{eventDate}/city/{eventCity}")]

        public async Task<IActionResult> EventsByFilters(int? eventTypeId, int? eventCategoryId, String eventDate, String eventCity, [FromQuery] int pageSize = 6, [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<CatalogEvent>)_context.CatalogEvents;
            if (eventTypeId.HasValue)
            {
                root = root.Where(c => c.CatalogTypeID == eventTypeId);
            }
            if (eventCategoryId.HasValue)
            {
                root = root.Where(c => c.CatalogCategoryID == eventCategoryId);
            }
            if (eventCity != "null" && eventCity != "All")
            {
              
                root = root.Where(c => c.City.ToLower() == eventCity.ToLower());
              
            }     

            if (eventDate != "null" && eventDate != "All Days")

            {
                root = FindingEventsByDate(root, eventDate); 
            }

            var totalItems = await root
                                .LongCountAsync();
            var itemsOnPage = await root
                                .OrderBy(c => c.Name)
                                .Skip(pageSize * pageIndex)
                                .Take(pageSize)
                                .ToListAsync();
            itemsOnPage = ChangePictureUrl(itemsOnPage);
            var model = new PaginatedEventsViewModel<CatalogEvent>(pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
        }

        private IQueryable<CatalogEvent> FindingEventsByDate(IQueryable<CatalogEvent> root, String date)
        {
            DateTime dateTime = DateTime.Now.Date;

            if (date != null && date != "All Days")
            {
                switch (date)
                {
                    case "Today":
                        dateTime = DateTime.Now;
                        root = root.Where(c => DateTime.Compare(c.StartDate.Date, dateTime.Date) == 0);
                        break;
                    case "Tomorrow":
                        var tomorrow = dateTime.AddDays(1);
                        root = root.Where(c => DateTime.Compare(c.StartDate.Date, tomorrow) == 0);
                        break;
                    case "This week":
                        var thisWeekdaySun = dateTime.AddDays(-(7 - (int)dateTime.DayOfWeek));
                        var comingSun = dateTime.AddDays(7 - (int)dateTime.DayOfWeek);

                        root = root.Where(c => (c.StartDate.Date >= thisWeekdaySun && c.StartDate.Date <= comingSun));

                        break;
                    case "This weekend":
                        var weekendFri = dateTime.AddDays(5 - (int)dateTime.DayOfWeek);
                        var weekendSun = dateTime.AddDays(7 - (int)dateTime.DayOfWeek);
                        root = root.Where(c => (c.StartDate.Date >= weekendFri && c.StartDate.Date <= weekendSun));
                        break;
                    case "Next week":
                        var nextWeekSunday = dateTime.AddDays((7 - (int)dateTime.DayOfWeek) + 7);
                        var comingSunday = dateTime.AddDays(7 - (int)dateTime.DayOfWeek);

                        root = root.Where(c => (c.StartDate.Date >= comingSunday && c.StartDate.Date <= nextWeekSunday));
                        break;
                    case "Next weekend":
                        var nextWeekFri = dateTime.AddDays(5 - (int)dateTime.DayOfWeek + 7);
                        var nextWeekSun = dateTime.AddDays(7 - (int)dateTime.DayOfWeek + 7);

                        root = root.Where(c => (c.StartDate.Date >= nextWeekFri && c.StartDate.Date <= nextWeekSun));

                        break;
                    case "This month":

                        var thisMonth = dateTime.Month;
                        root = root.Where(c => c.StartDate.Date.Month == thisMonth);
                        break;

                    case "Next month":
                        var nextMonth = dateTime.AddMonths(1).Month;
                        root = root.Where(c => c.StartDate.Date.Month == nextMonth);
                        break;
                    default:
                        break;

                }
            }

            return root;
        }
    }
}