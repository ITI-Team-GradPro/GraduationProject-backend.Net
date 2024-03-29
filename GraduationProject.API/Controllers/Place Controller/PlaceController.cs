using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GraduationProject.BL.Dtos.PlaceDtos;
using GraduationProject.Bl.Dtos.PlaceDtos;
using GraduationProject.BL;
using System;
using CloudinaryDotNet;
using GraduationProject.BL.Managers.Places;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.HttpResults;
namespace GraduationProject.API.Controllers.Place_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlacesManager _placesManager;
        private readonly ApplicationDbContext _context;



        public PlaceController(IPlacesManager placesManager, ApplicationDbContext context)
        {
            _placesManager = placesManager;
            _context = context;


        }

        [HttpGet]
        public async Task<ActionResult<List<GetPlacesDtos>>> GetAll()
        {
            var allPlaces =await _placesManager.GetAll();
            return Ok(allPlaces);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetPlacesDtos>> GetById(int id)
        {
            GetPlacesDtos? PlacesById =await _placesManager.GetById(id);
            if(PlacesById == null)
            {
                return NotFound();
            }
            return Ok(PlacesById);
        }

      

        //Delete Place with Photo
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isFound =await _placesManager.Delete(id);
            if (!isFound) return NotFound();
            return Ok("Place Remove Sucsses");

        }




        [HttpPost("Add Place with Photo")]

        public async Task<IActionResult> AddNewPlace([FromForm] AddPlaceDto NewPLace, IFormFile file)
        {
            Place placedb = new Place
            {
                Name = NewPLace.Name,
                Price = NewPLace.Price,
                Location = NewPLace.Location,
                Description = NewPLace.Description,
                PeopleCapacity = NewPLace.PeopleCapacity,
                OwnerId = NewPLace.OwnerId,
                //CategoryId = NewPLace.CategoryId,

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

     

            return Ok("Place Added ");

        }


        [HttpPut("Update Place without Photo")]

        public async Task<IActionResult> Update([FromForm] UpdatePlaceDto NewPLace)
        {
           
           await _placesManager.Update(NewPLace);

            return Ok("Place Updated ");

        }




       //Update Image Only

        [HttpPut("update image/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] UpdateImageDto updateImageDto)
        {
            // Retrieve the image entity from the database
            var image =  _context.ImagesPlaces.Find(imageId);
            if (image == null)
            {
                return NotFound();
            }

            // Check if a file was provided
            if (updateImageDto.ImageFile == null || updateImageDto.ImageFile.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            // Upload the new image to Cloudinary
            var uploadResult = await _placesManager.UpdateImageAsync(updateImageDto.ImageFile);
            if (uploadResult.Error != null)
            {
                return BadRequest(uploadResult.Error.Message);
            }

            // Update the image URL in the database entity
            image.ImageUrl = uploadResult.SecureUrl.AbsoluteUri;
            //image.PublicId = uploadResult.PublicId;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok("Image updated successfully");
        }






        [HttpPost("Add Place with Photo and Categegory Name")]
        public async Task<IActionResult> AddPlacewithImage([FromForm] AddPlaceDto newPlaceDto, IFormFile file)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName.ToLower() == newPlaceDto.CategoryName.ToLower());

            if (category == null)
            {
                return BadRequest($"Category Name '{newPlaceDto.CategoryName}' not found.");
            }

            Place place = new Place
            {
                Name = newPlaceDto.Name,
                Price = newPlaceDto.Price,
                Location = newPlaceDto.Location,
                Description = newPlaceDto.Description,
                PeopleCapacity = newPlaceDto.PeopleCapacity,
                OwnerId = newPlaceDto.OwnerId,
                CategoryId = category.CategoryId 
            };



            var result = await _placesManager.AddPhotoAsync(newPlaceDto, file);
            if (result.Error != null) return BadRequest(result.Error.Message);

            var ImgsPlace = new ImgsPlace
            {
                ImageUrl = result.SecureUrl.AbsoluteUri,
                publicId = result.PublicId,
                PlaceId = place.PlaceId,

            };
            _context.Places.Add(place);

            place.Images.Add(ImgsPlace);

            await _context.SaveChangesAsync();



            return Ok("Place Added ");


        }

    }
}
