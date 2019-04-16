using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public class ApiPaths
    {
        public static class Basket
        {
            public static string GetBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }

            public static string UpdateBasket(string baseUri)
            {
                return baseUri;
            }

            public static string CleanBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }
        }
        public static class Catalog
        {
            public static string GetAllCatalogEvents(
                string baseUri, int page, int take,
                int? category, int? type ,int? city)
            {
                var filterQs = string.Empty;

                if (category.HasValue || type.HasValue || city.HasValue)
                {
                    var categoryQs = (category.HasValue) ? category.Value.ToString() : "null";
                    var typeQs = (type.HasValue) ? type.Value.ToString() : "null";
                    var cityQs = (city.HasValue) ? city.Value.ToString() : "null";
                    filterQs = $"/type/{typeQs}/category/{categoryQs}/city/{cityQs}";
                }

                return $"{baseUri}events{filterQs}?pageIndex={page}&pageSize={take}";
            }
            public static string GetEvent(string baseUri, int id)

            {
                return $"{baseUri}events/{id}";

            } 
            public static string GetAllCategories(string baseUri)
            {
                return $"{baseUri}catalogcategories";
            }

            public static string GetAllCatalogCategoriesImage(string baseUri, int page, int take)
            {
                return $"{baseUri}catalogcategoriesimage?pageSize={take}&pageIndex={page}";
            }

            public static string GetAllTypes(string baseUri)
            {
                return $"{baseUri}catalogtypes";
            }

            public static string GetAllCities(string baseUri)
            {
                return $"{baseUri}catalogcities";
            }
            //extra
            public static string GetEventsWithName(string baseUri, string name, int page, int take)
            {
                return $"{baseUri}events/withname/{name}?pageSize={take}&pageIndex={page}";
            }

            public static string GetEventsWithNameCityDate(string baseUri, string name, string city, string date, int page, int take)
            {
                return $"{baseUri}events/name/{name}/city/{city}/date/{date}?pageSize={take}&pageIndex={page}";
            }

            public static string GetCatalogEventsInCity(string baseUri, string city)
            {
                return $"{baseUri}Events/withcity/{city}";
            }

            public static string GetEventsByAllFilters(string baseUri,int page, int take, int? eventcategory, int? eventtype, String date, String city)
            {
                var filterQs = string.Empty;

                if (eventcategory.HasValue || eventtype.HasValue || city != null || date != null)
                {

                    var eventcategoryQs = (eventcategory.HasValue) ? eventcategory.Value.ToString() : "null";
                    var eventtypeQs = (eventtype.HasValue) ? eventtype.Value.ToString() : "null";
                    var eventdateQs = date ?? "All Days";
                    var eventcityQs = city ?? "All";
                    filterQs = $"/type/{eventtypeQs}/category/{eventcategoryQs}/date/{eventdateQs}/city/{eventcityQs}";
                }
              
                return $"{baseUri}eventsByFilters{filterQs}?pageIndex={page}&pageSize={take}";
            }


        }

        public static class Order
        {
            public static string GetOrder(string baseUri, string orderId)
            {
                return $"{baseUri}/{orderId}";
            }

            //public static string GetOrdersByUser(string baseUri, string userName)
            //{
            //    return $"{baseUri}/userOrders?userName={userName}";
            //}
            public static string GetOrders(string baseUri)
            {
                return baseUri;
            }
            public static string AddNewOrder(string baseUri)
            {
                return $"{baseUri}/new";
            }
        }
    }
}
