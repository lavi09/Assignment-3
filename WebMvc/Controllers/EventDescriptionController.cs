using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventDescriptionController : Controller
    {
        private ICatalogService _eventservice;

        public EventDescriptionController(ICatalogService eventservice) =>

            _eventservice = eventservice;

        public async Task<IActionResult> EventDescription(int id)
        {
           
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
                    Date = eventDescription.Date,   
                    Day= eventDescription.Day,
                    Month= eventDescription.Month,
                    CatalogTypeID = eventDescription.CatalogTypeID,
                    CatalogCategoryID= eventDescription.CatalogCategoryID,
                    CatalogType = eventDescription.CatalogType,
                    CatalogCategory = eventDescription.CatalogCategory,
                    Time = eventDescription.Time
                    
                    
                 
    };

                return View(vm);
            }
            
    }
}