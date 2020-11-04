using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Data.Repo;
using WebAPI.Interfaces;
using WebAPI.DTOs;
using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
          private readonly ICityRepository repo;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        //GET api/city
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            //var cities = await dc.Cities.ToListAsync();
            var cities = await uow.CityRepository.GetCitiesAsync();
            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);

            // var citiesDto = from c in cities
            //     select new CityDto()
            //     {
            //       Id = c.Id,
            //       Name = c.Name
            //     };
            return Ok(citiesDto);
        }

        // //Post api/city/add?cityname=Miami
        // //Post api/city/add/Los Angeles
        // [HttpPost("add")]
        // [HttpPost("add/{cityname}")]
        // public async Task<IActionResult> AddCity(string cityName)
        // {
        //     City city = new City();
        //     city.Name = cityName;
        //     await dc.Cities.AddAsync(city);
        //     await dc.SaveChangesAsync();
        //     return Ok(city);
        // }

        //Post api/city/post --Post the data in Json Format
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = mapper.Map<City>(cityDto);
            city.LastUpdatedBy = 1;
            city.LastUpdatedOn = DateTime.Now;

            // var city = new City{
            //   Name = cityDto.Name,
            //   LastUpdatedBy = 1,
            //   LastUpdatedOn = DateTime.Now
            // };

            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        //http://localhost:5000/api/city/delete/8
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            // var city = await dc.Cities.FindAsync(id);
            // dc.Cities.Remove(city);

            uow.CityRepository.DeleteCity(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}
