﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class CatalogEvent
    {
        public string ID { get; set; }
        public string Name { get; set; }
        //public string Month { get; set; }
        //public string Date { get; set; }
        //public string Day { get; set; }
        //public string Time { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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

        public string CatalogType { get; set; }
        public string CatalogCategory { get; set; }
        public string CatalogCity { get; set; }
    }
}
