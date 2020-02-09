using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Dtos
{
  public class PhotoForReturnDTO
  {
    public int Id { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public bool IsMain { get; set; }
    public string PublicId { get; set; }
  }
}