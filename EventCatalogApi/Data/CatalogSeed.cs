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
                new CatalogCategory() { Category= "Arts"},
                new CatalogCategory() { Category="Business"},
                new CatalogCategory() { Category="Community"},
                new CatalogCategory() { Category="Food & Drink"},
                new CatalogCategory() { Category= "Music"},
                new CatalogCategory() { Category= "Sports & Fitness"},
                new CatalogCategory() { Category= "Travel & Outdoor"},

            };            
        }

        static IEnumerable<CatalogEvent> GetPreconfiguredEvents()
        {
            return new List<CatalogEvent>()
            {
                new CatalogEvent() { CatalogTypeID=4,CatalogCategoryID=5,CatalogCityID=1,Name = "BollyWood & Bhangra Showcase at NW Folklife Festival",Month="May",Date="24",Day="Friday",Time="5.30 p.m",Address="Seattle Center Exhibition Hall",City="Seattle",State="WA", Price = 30, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },

                new CatalogEvent() { CatalogTypeID=5,CatalogCategoryID=3,CatalogCityID=4,Name = "Holi:Festival of Colors !",Month="March",Date="30",Day="Saturday",Time="9.00 p.m",Address="The Crocodile ",City="SanFrancisco",State="CA",Price= 15, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },

                new CatalogEvent() { CatalogTypeID=2,CatalogCategoryID=3,CatalogCityID=1,Name="Blossom in your smile !",Month="April",Date="6",Day="Saturday",Time="3.00 p.m",Address="The Quad-University of Washington",City="Seattle",State="WA", Price =20, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },

                new CatalogEvent() { CatalogTypeID=5,CatalogCategoryID=5,CatalogCityID=5,Name="Live Music with Brian James",Month="June",Date="20",Day="Sunday",Time="6.00 p.m",Address="Village Vines",City="LosAngeles",State="CA", Price = 80, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },

                new CatalogEvent() { CatalogTypeID=6,CatalogCategoryID=2, CatalogCityID=6,Name="Daring Women",Month="May",Date="21",Day="Tuesday",Time="1.00 p.m", Address="Block 41",City="Brooklyn",State="NY", Price = 95, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },

                new CatalogEvent() { CatalogTypeID=6,CatalogCategoryID=4,CatalogCityID=2,Name="Downtown Issaquah Wine & Art Walk",Month="April", Date="6",Day="Saturday",Time="6.00 p.m",Address="Historic Shell Station",City="Issaquah",State="WA", Price = 25, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },

                new CatalogEvent() { CatalogTypeID=2,CatalogCategoryID=7,CatalogCityID=1,Name="Skagit Valley Tulip Festival",Month="April",Date="10",Day="Friday",Time="12.00 a.m",Address="Skagit Valley",City="Seattle",State="WA", Price = 70, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7"  },

                new CatalogEvent() { CatalogTypeID=5,CatalogCategoryID=6,CatalogCityID=7,Name="Zumba Pink Party",Month="October",Date="13",Day="Friday",Time="5.00 p.m",Address="Union Park",City="Chicago",State="IL", Price = 90, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },

                new CatalogEvent() { CatalogTypeID=4,CatalogCategoryID=6,CatalogCityID=1,Name="Bubble Run",Month="January",Date="1",Day="Wednesday",Time="9.00 a.m",Address="Elm Coffee Roasters",City="Seattle",State="WA", Price = 50, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },

                new CatalogEvent() { CatalogTypeID=2,CatalogCategoryID=7,CatalogCityID=8,Name="Disney Cruise 2019 ",Month="July",Date="7",Day="Friday",Time="4.00 p.m",Address="Orlando" ,City="Orlando",State="FL",Price = 1000, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },

                new CatalogEvent() { CatalogTypeID=2,CatalogCategoryID=2,CatalogCityID=1,Name="Washington State Spring Fair",Month="April", Date="14",Day="Sunday",Time="12.00 a.m",Address="Washington state Fair Events Center",City="Puyallup",State="WA", Price = 10, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },

                new CatalogEvent() { CatalogTypeID=3,CatalogCategoryID=1,CatalogCityID=3,Name="Bellevue Art Museum Fair", Month="July",Date="26",Day="Friday",Time="9.30 a.m",Address="Bellevue Arts Museum Fair",City="Bellevue",State="WA",Price = 40, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },

                new CatalogEvent() { CatalogTypeID=2,CatalogCategoryID=7,CatalogCityID=1,Name="Seattle 101 ",Month="March",Date="28",Day="Thursday",Time="12.00 P.M",Address="Seattle Free Walking Hours",City="Seattle",State="WA",Price=10,PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/13" },

                new CatalogEvent() { CatalogTypeID=1,CatalogCategoryID=2,CatalogCityID=1,Name="Innoventures",Month="March",Date="28",Day="Thursday",Time="10.00 a.m",Address="Columbia city Theatre ",City="Seattle",State="WA",Price = 20, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/14" },

                new CatalogEvent() { CatalogTypeID=5,CatalogCategoryID=5,CatalogCityID=1,Name="Capitol Hill Block Party 2019",Month="July",Date="19",Day="Friday",Time="3.00 p.m",Address="Capitol hill block  Party",City="Seattle",State="WA", Price = 100, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/15" }

            };
        }


    }}
