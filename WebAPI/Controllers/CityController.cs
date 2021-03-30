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
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Authorize]
    public class CityController : BaseController
    {
        //private readonly ICityRepository repo;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        //GET api/city
       [HttpGet ("cities")]

       [AllowAnonymous]
        public async Task<IActionResult> GetCities()
        {
          //throw new UnauthorizedAccessException();
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

        [HttpPatch("update/{id}")]
         public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<City> cityToPatch)
        {
            var cityFromDb = await uow.CityRepository.FindCity(id);
            cityFromDb.LastUpdatedBy = 1;
            cityFromDb.LastUpdatedOn = DateTime.Now;

            cityToPatch.ApplyTo(cityFromDb, ModelState);
            await uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpPut("update/{id}")]
         public async Task<IActionResult> UpdateCity(int id,CityDto cityDto)
        {
            if(id != cityDto.Id)
            return BadRequest("Update not allowed");

            var cityFromDb = await uow.CityRepository.FindCity(id);

            if(cityFromDb == null)
            return BadRequest("Update not allowed");

            cityFromDb.LastUpdatedBy = 1;
            cityFromDb.LastUpdatedOn = DateTime.Now;
            mapper.Map(cityDto, cityFromDb);

            //throw new Exception("Some unknown error occured");
            await uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpPut("updateCityName/{id}")]
         public async Task<IActionResult> UpdateCity(int id,CityUpdateDto cityDto)
        {
            var cityFromDb = await uow.CityRepository.FindCity(id);
            cityFromDb.LastUpdatedBy = 1;
            cityFromDb.LastUpdatedOn = DateTime.Now;
            mapper.Map(cityDto, cityFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
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
