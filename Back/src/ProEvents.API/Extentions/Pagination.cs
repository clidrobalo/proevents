using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProEvents.API.Models;

namespace ProEvents.API.Extentions
{
    public static class Pagination
    {
        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPages, int totalItems, int totalPages)
        {
            var paginationHeaders = new PaginationHeaders(currentPage, totalItems, itemsPerPages, totalPages);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeaders, options));

            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

    }
}