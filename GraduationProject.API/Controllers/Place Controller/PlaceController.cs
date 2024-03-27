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
        private readonly ApplicationDbContext _context;



        public PlaceController(IPlacesManager placesManager , ApplicationDbContext context)
        {
            _placesManager = placesManager;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetPlacesDtos>>> GetAll()
        {
            var allPlaces = _placesManager.GetAll().ToList();
            return Ok(allPlaces);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetPlacesDtos>> GetById(int id)
        {
            GetPlacesDtos? PlacesById = _placesManager.GetById(id);
            if(PlacesById == null)
            {
                return NotFound();
            }
            return Ok(PlacesById);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddPlaceDto placedto, IFormFile file)
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
        [HttpPost("ADD-PHOTO")]
        public async Task<ActionResult<AddPlaceDto>> AddPhoto([FromForm] AddPlaceDto NewPLace, IFormFile file)
        {
            Place placedb = new Place
            {
                Name = NewPLace.Name,
                Price = NewPLace.Price,
                Location = NewPLace.Location,
                Description = NewPLace.Description,
                PeopleCapacity = NewPLace.PeopleCapacity,
                OwnerId = NewPLace.OwnerId,
                CategoryId = NewPLace.CategoryId,

            };
            var result = await _placesManager.AddPhotoAsync(NewPLace, file);
            if (result.Error != null) return BadRequest(result.Error.Message);

            var ImgsPlace = new ImgsPlace
            {
                ImageUrl = result.SecureUrl.AbsoluteUri,
                publicId = result.PublicId,
                PlaceId = placedb.PlaceId,

            };
            _context.Places.Add(placedb);

            placedb.Images.Add(ImgsPlace);

            await _context.SaveChangesAsync();

            return Ok();

        }

    }
}
