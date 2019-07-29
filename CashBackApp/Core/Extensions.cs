using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CashBackApp.Api.Core
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems)
        {
            response.Headers.Add("CurrentPage", currentPage.ToString());
            response.Headers.Add("ItemsPerPage", itemsPerPage.ToString());
            response.Headers.Add("TotalRecords", totalItems.ToString());
            response.Headers.Add("TotalPages", (itemsPerPage > 0 ? (int)Math.Ceiling((double)totalItems / itemsPerPage) : 0).ToString());

            // CORS
            //response.Headers.Add("access-control-expose-headers", "X-Total-Count");
        }

        public static void AddApplicationError(this HttpResponse response, string message)
        {
            message = Regex.Replace(message, @"[^\u001F-\u007F]+", string.Empty);
            response.Headers.Add("Application-Error", message.GetASCIIString());
            // CORS
            response.Headers.Add("access-control-expose-headers", "Application-Error");
        }

        private static string GetASCIIString(this string input)
        {
            byte[] array = Encoding.ASCII.GetBytes(input);
            return Encoding.ASCII.GetString(array);
        }
    }
}
