using EventCatalogApi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogApi.Data
{
    public class CatalogSeed
    {
        public static void Seed(CatalogContext context)
        {

            context.Database.Migrate();
            if (!context.CatalogCategories.Any())
            {
                context.CatalogCategories.AddRange(GetPreconfiguredCatalogCategories());
                context.SaveChanges();
            }

            if (!context.CatalogTypes.Any())
            {                
                context.CatalogTypes.AddRange(GetPreconfiguredCatalogTypes());
                context.SaveChanges();
            }
            if (!context.CatalogCities.Any())
            {
                context.CatalogCities.AddRange(GetPreconfiguredCities());
                context.SaveChanges();
            }
            if (!context.CatalogEvents.Any())
            {
                context.CatalogEvents.AddRange(GetPreconfiguredEvents());
                context.SaveChanges();
            }
        }

        private static IEnumerable<CatalogCity> GetPreconfiguredCities()
        {
            return new List<CatalogCity>()
            {
                new CatalogCity() { City= "Seattle"},
                new CatalogCity() { City= "Issaquah"},
                new CatalogCity() { City= "Bellevue"},
                new CatalogCity() { City= "SanFrancisco"},
                new CatalogCity() { City= "LosAngeles"},
                new CatalogCity() { City= "Brooklyn"},
                new CatalogCity() { City= "Chicago"},
                new CatalogCity() { City= "Orlando"},

            };
        }

        private static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType() { Type= "Conference"},
                new CatalogType() { Type= "Attraction"},
                new CatalogType() { Type= "class"},
                new CatalogType() { Type= "Gala"},
                new CatalogType() { Type= "Party"},
                new CatalogType() { Type= "Networking"},

            };
        }

        private static IEnumerable<CatalogCategory> GetPreconfiguredCatalogCategories()
        {
            return new List<CatalogCategory>()
            {
                new CatalogCategory() { Category= "Arts",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/100" },
                new CatalogCategory() { Category="Business",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/101" },
                new CatalogCategory() { Category="Community",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/102" },
                new CatalogCategory() { Category="Food & Drink",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/103" },
                new CatalogCategory() { Category= "Music",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/104" },
                new CatalogCategory() { Category= "Sports & Fitness",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/105" },
                new CatalogCategory() { Category= "Travel & Outdoor",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/106" },

            };            
        }

        static IEnumerable<CatalogEvent> GetPreconfiguredEvents()
        {
            return new List<CatalogEvent>()
            {
                new CatalogEvent() { CatalogTypeID=4,CatalogCategoryID=5,CatalogCityID=1,Name = "BollyWood & Bhangra Festival",StartDate = new DateTime(2019, 4, 09, 7, 10, 0), EndDate = new DateTime(2019, 04, 10, 9, 15, 0),Address="Seattle Center Exhibition Hall",City="Seattle",State="WA",Zipcode="98156", Price = 30, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },

                new CatalogEvent() { CatalogTypeID=5,CatalogCategoryID=3,CatalogCityID=4,Name = "Holi:Festival of Colors !", StartDate = new DateTime(2019, 5, 27, 20, 30, 0), EndDate = new DateTime(2019, 5, 29, 23, 0, 0),Address="The Crocodile ",City="SanFrancisco",State="CA",Zipcode="90001",Price= 15, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },

                new CatalogEvent() { CatalogTypeID=2,CatalogCategoryID=3,CatalogCityID=1,Name="Blossom in your smile !",StartDate = new DateTime(2019, 6, 10, 12, 0, 0), EndDate = new DateTime(2019, 6, 8, 12, 0, 0),Address="The Quad-University",City="Seattle",State="WA",Zipcode="98156", Price =20, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },

                new CatalogEvent() { CatalogTypeID=5,CatalogCategoryID=5,CatalogCityID=5,Name="Live Music with Brian James",StartDate = new DateTime(2019, 6, 27, 9, 0, 0), EndDate = new DateTime(2019, 6, 27, 17, 30, 0),Address="Village Vines",City="LosAngeles",State="CA",Zipcode="90002", Price = 80, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },

                new CatalogEvent() { CatalogTypeID=6,CatalogCategoryID=2, CatalogCityID=6,Name="Daring Women",StartDate = new DateTime(2019, 4, 23, 20, 0, 0), EndDate = new DateTime(2019, 4, 23, 22, 0, 0), Address="Block 41",City="Brooklyn",State="NY",Zipcode="10101", Price = 95, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },

                new CatalogEvent() { CatalogTypeID=6,CatalogCategoryID=4,CatalogCityID=2,Name="Downtown Issaquah Wine & Art Walk", StartDate = new DateTime(2019, 5, 10, 0, 0, 0), EndDate = new DateTime(2019, 5, 13, 0, 0, 0),Address="Historic Shell Station",City="Issaquah",State="WA",Zipcode="97458", Price = 25, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },

                new CatalogEvent() { CatalogTypeID=2,CatalogCategoryID=7,CatalogCityID=1,Name="Skagit Valley Tulip Festival",StartDate = new DateTime(2019, 5, 23, 12, 0, 0), EndDate = new DateTime(2019, 5, 24, 14, 0, 0),Address="Skagit Valley",City="Seattle",State="WA", Zipcode="97880",Price = 70, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7"  },

                new CatalogEvent() { CatalogTypeID=5,CatalogCategoryID=6,CatalogCityID=7,Name="Zumba Pink Party",StartDate = new DateTime(2019, 4, 29, 11, 0, 0), EndDate = new DateTime(2019, 4, 30, 12, 0, 0),Address="Union Park",City="Chicago",State="IL",Zipcode="62462", Price = 90, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },

                new CatalogEvent() { CatalogTypeID=4,CatalogCategoryID=6,CatalogCityID=1,Name="Bubble Run",StartDate = new DateTime(2019, 4, 10, 7, 10, 0), EndDate = new DateTime(2019, 4, 10, 9, 15, 0),Address="Elm Coffee Roasters",City="Seattle",State="WA", Zipcode="91008",Price = 50, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },

                new CatalogEvent() { CatalogTypeID=2,CatalogCategoryID=7,CatalogCityID=8,Name="Disney Cruise 2019 ",StartDate = new DateTime(2019, 4, 16, 20, 30, 0), EndDate = new DateTime(2019, 4, 16, 23, 0, 0),Address="Orlando" ,City="Orlando",State="FL",Zipcode="32703",Price = 1000, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },

                new CatalogEvent() { CatalogTypeID=2,CatalogCategoryID=2,CatalogCityID=1,Name="Washington State Spring Fair",StartDate = new DateTime(2019, 7, 23, 20, 0, 0), EndDate = new DateTime(2019, 7, 23, 22, 0, 0),Address="Washington state Fair Events Center",City="Puyallup",State="WA",Zipcode="96320", Price = 10, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },

                new CatalogEvent() { CatalogTypeID=3,CatalogCategoryID=1,CatalogCityID=3,Name="Bellevue Art Museum Fair", StartDate = new DateTime(2019, 6, 5, 0, 0, 0), EndDate = new DateTime(2019, 6, 9, 0, 0, 0),Address="Bellevue Arts Museum Fair",City="Bellevue",State="WA",Zipcode="98007",Price = 40, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },

                new CatalogEvent() { CatalogTypeID=2,CatalogCategoryID=7,CatalogCityID=1,Name="Seattle 101 ",StartDate = new DateTime(2019, 8, 10, 12, 0, 0), EndDate = new DateTime(2019, 8, 12, 14, 0, 0),Address="Seattle Free Walking Hours",City="Seattle",State="WA",Zipcode="97138",Price=10,PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/13" },

                new CatalogEvent() { CatalogTypeID=1,CatalogCategoryID=2,CatalogCityID=1,Name="Innoventures",StartDate = new DateTime(2019, 6, 10, 11, 0, 0), EndDate = new DateTime(2019, 6, 10, 12, 0, 0),Address="Columbia city Theatre ",City="Seattle",State="WA",Zipcode="98236",Price = 20, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/14" },

                new CatalogEvent() { CatalogTypeID=5,CatalogCategoryID=5,CatalogCityID=1,Name="Capitol Hill Block Party 2019",StartDate = new DateTime(2019, 6, 25, 12, 0, 0), EndDate = new DateTime(2019, 6, 26, 12, 0, 0),Address="Capitol hill block  Party",City="Seattle",State="WA",Zipcode="98424", Price = 100, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/15" }

            };
        }


    }}
