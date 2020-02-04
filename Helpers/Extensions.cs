using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
  }
}