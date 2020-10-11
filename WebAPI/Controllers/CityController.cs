using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Data.Repo;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
          private readonly ICityRepository repo;

        public CityController(ICityRepository repo)
        {
            this.repo = repo;
       }

        //GET api/city
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            //var cities = await dc.Cities.ToListAsync();
            var cities = await repo.GetCitiesAsync();
            return Ok(cities);
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
        public async Task<IActionResult> AddCity(City city)
        {
            repo.AddCity(city);
            await repo.SaveAsync();
            return StatusCode(201);
        }

        //http://localhost:5000/api/city/delete/8
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            // var city = await dc.Cities.FindAsync(id);
            // dc.Cities.Remove(city);

            repo.DeleteCity(id);
            await repo.SaveAsync();
            return Ok(id);
        }
    }
}
