using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GraduationProject.BL.Dtos.PlaceDtos;
using GraduationProject.Bl.Dtos.PlaceDtos;
using GraduationProject.BL;
using System.Runtime.InteropServices;
using CloudinaryDotNet;
using GraduationProject.DAL.Data;
using GraduationProject.BL.Managers.Places;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;

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

        // Delete Place With Image

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)

        {

            var isFound = await _placesManager.Delete(id);

            if (!isFound) return NotFound();

            return Ok("Place Remove Sucsses");

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

            var image = _context.ImagesPlaces.Find(imageId);

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





        //Add Place With Collection Of Image and Category name
        [HttpPost("AddPlaceWithImages")]
        public async Task<IActionResult> AddPlaceWithImages([FromForm] AddPlaceDto newPlaceDto, List<IFormFile> files)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName.ToLower() == newPlaceDto.CategoryName.ToLower());
            if (category == null)
            {
                return BadRequest($"Category Name '{newPlaceDto.CategoryName}' not found.");
            }

            try
            {
                var imageUrls = new List<string>();
                var publicIds = new List<string>();

                foreach (var file in files)
                {
                    var result = await _placesManager.AddPlaceAsync(newPlaceDto, file);

                    if (result.Error != null)
                    {
                        return BadRequest(result.Error.Message);
                    }

                    imageUrls.Add(result.SecureUrl.AbsoluteUri);
                    publicIds.Add(result.PublicId);
                }

                var imgsPlaces = imageUrls.Select((url, index) => new ImgsPlace
                {
                    ImageUrl = url,
                    publicId = publicIds[index]
                }).ToList();

                var place = new Place
                {
                    Name = newPlaceDto.Name,
                    Price = newPlaceDto.Price,
                    Location = newPlaceDto.Location,
                    Description = newPlaceDto.Description,
                    PeopleCapacity = newPlaceDto.PeopleCapacity,
                    OwnerId = newPlaceDto.OwnerId,
                    CategoryId = category.CategoryId,
                    Images = imgsPlaces
                };

                _context.Places.Add(place);
                await _context.SaveChangesAsync();

                return Ok("Place Added Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding place: {ex.Message}");
                return StatusCode(500, "An error occurred while adding the place.");
            }
        }



        // Get Place with Image By Id

        [HttpGet("Get Place By Id/{id:int}")]
        public async Task<ActionResult<GetPlacesDtos>> GetById(int id)

        {
            GetPlacesDtos? PlacesById = await _placesManager.GetById(id);

            if (PlacesById == null)

            {

                return NotFound();

            }

            return Ok(PlacesById);

        }


        // Get Place with Image and User By ID 


        [HttpGet("Get Place By Id With User/{id}")]
        public async Task<ActionResult<PlaceDetailsDto>> GetByIdWithUser(int id)
        {
            PlaceDetailsDto? PlacesById = await _placesManager.GetByIdWithUser(id);

            if (PlacesById == null)

            {

                return NotFound();

            }

            return Ok(PlacesById);
        }

     
    }
}


