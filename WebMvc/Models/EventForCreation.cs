using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class EventForCreation
    {

        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public Decimal Price { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int CatalogTypeID { get; set; }
        [Required]
        public int CatalogCategoryID { get; set; }
        [Required]
        //public int CatalogCityID { get; set; }
        //[Required]
        //public int OrganizerId { get; internal set; }
        //[Required]
        //public string OrganizerName { get; set; }
        //[Required]
        public string EventDescription { get; set; }
        [Required]
        //public string OrganizerDescription { get; set; }

        public string CatalogType { get; set; }
        public string CatalogCategory { get; set; }
        //public string CatalogCity { get; set; }
    }
}
