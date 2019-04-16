using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogApi.Domain
{
    public class CatalogEvent
    {
        public int ID { get; set; }
        public string Name { get; set; }
        //public string Month { get; set; }
        //public string Date { get; set; }
        //public string Day { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public string Time { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string EventDescription { get; set; }

        public int CatalogCategoryID { get; set; }
        public int CatalogTypeID { get; set; }
        public int CatalogCityID { get; set; }

        public virtual CatalogType CatalogType { get; set; }
        public virtual CatalogCategory CatalogCategory { get; set; }
        public virtual CatalogCity CatalogCity { get; set; }


    }
}
