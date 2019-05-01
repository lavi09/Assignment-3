using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [Route("[action]/catalogtype/{catalogtypeID}/catalogcategory/{catalogcategoryID}")]

        public async Task<IActionResult> Events(int? catalogtypeID, int? catalogcategoryID, [FromQuery] int pageSize = 6,
                                                                              [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<CatalogEvent>)_context.CatalogEvents;
            if (catalogtypeID.HasValue)
            {
                root = root.Where(c => c.CatalogTypeID == catalogtypeID);
            }
            if (catalogcategoryID.HasValue)
            {
                root = root.Where(c => c.CatalogCategoryID == catalogcategoryID);
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
        [Route("[action]/type/{eventTypeId}/category/{eventCategoryId}")]

        public async Task<IActionResult> EventsByFilters(int? eventTypeId, int? eventCategoryId, [FromQuery] int pageSize = 6, [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<CatalogEvent>)_context.CatalogEvents;
            if (eventTypeId.HasValue)
            {
                root = root.Where(c => c.CatalogTypeID == eventTypeId);
            }
            if (eventCategoryId.HasValue)
            {
                root = root.Where(c => c.CatalogCategoryID== eventCategoryId);
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



        [HttpPost]
        [Route("new")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateEvent([FromBody]CatalogEvent newEvent)
        {

            var item = new CatalogEvent
            {

                Name = newEvent.Name,
                Address = newEvent.Address,
                City = newEvent.City,
                State = newEvent.State,
                Zipcode = newEvent.Zipcode,
                PictureUrl = newEvent.PictureUrl,
                Price = newEvent.Price,
                StartDate = newEvent.StartDate,
                EndDate = newEvent.EndDate,            
                CatalogCategoryID = newEvent.CatalogCategoryID,
                CatalogTypeID = newEvent.CatalogTypeID,
                //CatalogCityID = newEvent.CatalogCityID,
                EventDescription = newEvent.EventDescription

            };
        
            _context.CatalogEvents.Add(item);
            await _context.SaveChangesAsync();
            var result = GetEventsById(item.ID);
            return Ok(result);
            // return CreatedAtAction(nameof(GetEventById), new { id = item.Id });
        }

        [HttpPut]
        [Route("events")]
        public async Task<IActionResult> UpdateEvent(
            [FromBody] CatalogEvent eventToUpdate)
        {
            var catalogEvent = await _context.CatalogEvents
                              .SingleOrDefaultAsync
                              (i => i.ID == eventToUpdate.ID);
            if (catalogEvent == null)
            {
                return NotFound(new { Message = $"Item with id {eventToUpdate.ID} not found." });
            }
            catalogEvent = eventToUpdate;
            _context.CatalogEvents.Update(catalogEvent);
            await _context.SaveChangesAsync();
            var result = GetEventsById(eventToUpdate.ID);
            return Ok(result);

            // return CreatedAtAction(nameof(GetEventById), new { id = eventToUpdate.Id });
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var catalogEvent = await _context.CatalogEvents
                .SingleOrDefaultAsync(p => p.ID == id);
            if (catalogEvent == null)
            {
                return NotFound();

            }
            _context.CatalogEvents.Remove(catalogEvent);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        

    }
}