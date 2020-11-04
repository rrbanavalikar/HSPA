using AutoMapper;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
      public AutoMapperProfiles()
      {
        //comment
          CreateMap<City, CityDto>().ReverseMap();
      }
    }
}
