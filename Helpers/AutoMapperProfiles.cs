using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<User, UserForListDTO>()
        .ForMember(dest => dest.PhotoURL, opt => opt
          .MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
        .ForMember(dest => dest.Age, opt => opt
          .MapFrom(src => src.DateOfBirth.GetAge()));
      CreateMap<User, UserDTO>()
        .ForMember(dest => dest.PhotoURL, opt => opt
          .MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
        .ForMember(dest => dest.Age, opt => opt
          .MapFrom(src => src.DateOfBirth.GetAge()));
      CreateMap<UserForUpdateDTO, User>();
      CreateMap<Photo, PhotosForDetailedDTO>();      
      CreateMap<Photo, PhotoForReturnDTO>();
      CreateMap<PhotoForCreationDTO, Photo>();
    }
  }
}