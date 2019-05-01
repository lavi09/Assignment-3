
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    [Authorize]
    public class EventCreationController : Controller
    {
        // GET: /<controller>/

        private ICatalogService _ecatalogSvc;
        // private IBus _bus;

        public EventCreationController(ICatalogService ecatalogSvc)
        {
            _ecatalogSvc = ecatalogSvc;


        }

        public async Task<IActionResult> Index(int? EventCategoryFilterApplied, int? EventTypeFilterApplied)
        {
            var vm = new EventCreationViewModel()
            {
                Event = new EventForCreation(),
                // Event = new Event(),
                TypesFilterApplied = EventTypeFilterApplied ?? 0,
                Types = await _ecatalogSvc.GetTypesAsync(),

                CategoryFilterApplied = EventCategoryFilterApplied ?? 0,
                Categories = await _ecatalogSvc.GetCategoriesAsync(),

                //CityFilterApplied = EventCityFilterApplied ?? 0,
                //Cities = await _ecatalogSvc.GetCitiesAsync()
            };
            return View(vm);
        }
        //Direct Called to EventApi
        [HttpPost]
        public async Task<IActionResult> Create(EventCreationViewModel frmEvent)
        {
            //if (ModelState.IsValid)
            {
                frmEvent.Event.CatalogTypeID = frmEvent.TypesFilterApplied ?? 0;                
                frmEvent.Event.CatalogCategoryID = frmEvent.CategoryFilterApplied ?? 0;
                //frmEvent.Event.CatalogCityID = frmEvent.CityFilterApplied ?? 0;
                var eventId = await _ecatalogSvc.CreateEvent(frmEvent.Event);

                frmEvent.Event.ID = eventId;
                var vm = new EventCreationViewModel()

                {
                    Event = frmEvent.Event,
                   TypesFilterApplied = frmEvent.TypesFilterApplied,
                    Types = await _ecatalogSvc.GetTypesAsync()
                };

                //  var eventType = frmEvent.EventTypes.GetType();
                //return View(vm);
                // return RedirectToAction("EventSaved", new { id = eventId, userName = "UserName" });

                return RedirectToAction("EventSaved", frmEvent.Event);

            }
           // return View(frmEvent);

        }






        public IActionResult EventCreate()
        {
            return View();

        }

        public IActionResult EventSaved(EventForCreation eventForCreation)
        {
            return View(eventForCreation);

        }


    }
}
