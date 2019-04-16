using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{ //mirror Eventtype model/table in EventCatalog API
    public class CatalogType
    {

        public int ID { get; set; }
        public string Type { get; set; }
    }
}
