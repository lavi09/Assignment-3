using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    //mirror EventCategory model/table in EventCatalog API
    public class CatalogCategory
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string PictureUrl { get; set; }
    }
}
