using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class EventDetail
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogCategoryId { get; set; }
        public string CatalogType { get; set; }
        public string CatalogCategory { get; set; }       
        public string EventDescription { get; set; }
        
    }
}
