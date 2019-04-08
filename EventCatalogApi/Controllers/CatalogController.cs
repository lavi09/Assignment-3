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
    [Route("api/Catalog")]
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
        [Route("events/{id:int}")]
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

            var model = new PaginatedEventsViewModel<CatalogEvent>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Count = eventsCount,
                Data = events
            };
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

    }
}