using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DatingApp.API.Helpers
{
  public static class Extensions
  {
    public static void AddApplicationError(this HttpResponse response, string message)
    {
      response.Headers.Add("Application-Error", message);
      response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
      response.Headers.Add("Access-Control-Allow-Origin", "*");
    }

    public static void AddPagination(this HttpResponse response, int currentPage, 
        int itemsPerPage, int totalItems, int totalPages)
    {
      var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, 
              totalPages);
      response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader));
      response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }

    public static List<T> PageResults<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
      int numofRecords = pageSize > 1000 ? 1000 : pageSize;
      int skippedRecords = (pageNumber - 1) * numofRecords;
      return query.Skip(skippedRecords).Take(pageSize).ToList();
    }

    public static List<T> PageResults<T>(this IEnumerable<T> list, int pageNumber, int pageSize)
    {
      int numofRecords = pageSize > 4000 ? 4000 : pageSize;
      int skippedRecords = (pageNumber - 1) * numofRecords;
      return list.Skip(skippedRecords).Take(pageSize).ToList();
    }

    public static async Task<List<T>> PageResultsAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
      int numofRecords = pageSize > 1000 ? 1000 : pageSize;
      int skippedRecords = (pageNumber - 1) * numofRecords;
      return await query.Skip(skippedRecords).Take(pageSize).ToListAsync<T>();
    }

    public static Int32 GetAge(this DateTime dateOfBirth)
    {
        var today = DateTime.UtcNow.Date;

        var a = (today.Year * 100 + today.Month) * 100 + today.Day;
        var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

        return (a - b) / 10000;
    }
  }
}