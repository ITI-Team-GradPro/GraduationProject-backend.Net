using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GraduationProject.BL.Dtos.PlaceDtos;
using GraduationProject.Bl.Dtos.PlaceDtos;
using GraduationProject.BL;
namespace GraduationProject.API.Controllers.Place_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlacesManager _placesManager;

        public PlaceController(IPlacesManager placesManager)
        {
            _placesManager = placesManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetPlacesDtos>>> GetAllPlaces()
        {
            var allPlaces = _placesManager.GetAll().ToList();
            return Ok(allPlaces);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetPlacesDtos>> GetById(int id)
        {
            GetPlacesDtos? PlacesById = _placesManager.GetPlacesById(id);
            if(PlacesById == null)
            {
                return NotFound();
            }
            return Ok(PlacesById);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPlaceDto placedto)
        {
            if (!ModelState.IsValid)
                 return BadRequest();
            var newPlaceId = _placesManager.Add(placedto);

          return Ok(newPlaceId);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isFound =_placesManager.Delete(id);
            if(! isFound) return NotFound();
            return Ok("Place Remove Sucsses");

            //Place oldPlace =await context.Places.FirstOrDefaultAsync(a=>a.PlaceId == id);
            //if (oldPlace != null)
            //{
            //    try
            //    {
            //        context.Places.Remove(oldPlace);
            //        context.SaveChanges();
            //        return Ok("Place Remove Sucsses");
            //    }
            //    catch(Exception e) 
            //    { 
            //        return BadRequest(e.Message);
            //    }
            //}
            //return BadRequest("Id Not Found");

        }

    }
}
