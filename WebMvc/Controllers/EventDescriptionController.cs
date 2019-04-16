using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventDescriptionController : Controller
    {
        private ICatalogService _eventservice;

        public EventDescriptionController(ICatalogService eventservice) =>

            _eventservice = eventservice;

        public async Task<IActionResult> EventDescription(CatalogEvent eventDescriptiontemp)
        {
            int id = 0;
            Int32.TryParse(eventDescriptiontemp.ID, out id);
            var eventDescription = await _eventservice.GetEventItemAsync(id);           

            var vm = new EventDescriptionViewModel
                {
                    ID = eventDescription.ID,
                    Name= eventDescription.Name,
                    Address = eventDescription.Address,
                    City = eventDescription.City,
                    State = eventDescription.State,                    
                    PictureUrl = eventDescription.PictureUrl,
                    Price = eventDescription.Price,
                    StartDate = eventDescription.StartDate.ToString("D"),
                    EndDate = eventDescription.EndDate.ToString("D"),
                    StartTime = eventDescription.StartDate.ToString("h:mm tt"),
                    EndTime = eventDescription.EndDate.ToString("h:mm tt"),
                    CatalogTypeID = eventDescription.CatalogTypeID,
                    CatalogCategoryID= eventDescription.CatalogCategoryID,
                    CatalogType = eventDescription.CatalogType,
                    CatalogCategory = eventDescription.CatalogCategory,
                    EventDescription = eventDescription.EventDescription,



            };

                return View(vm);
            }
            
    }
}