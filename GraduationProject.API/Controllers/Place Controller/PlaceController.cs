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
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using GraduationProject.BL.Dtos.SignDtos;

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
            try
            {
                var isFound = await _placesManager.Delete(id);
                if (!isFound) return NotFound();
                //return Ok("Place Remove Sucsses");
                return StatusCode(200, "Place Removed successfully");
            }

            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the place: " + ex.Message);
            }
        }


        [HttpPut("Update Place without Photo")]

        public async Task<IActionResult> Update([FromForm] UpdatePlaceDto NewPLace)
        {
            await _placesManager.Update(NewPLace);
            //return Ok("Place Updated ");
            return StatusCode(200, "Place Updated successfully");
        }



        //Update Image Only

        [HttpPut("update image/{imageId}")]

        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] UpdateImageDto updateImageDto)
        {
            var image = _context.ImagesPlaces.Find(imageId);

            if (image == null)
            {
                return NotFound();
            }

            if (updateImageDto.ImageFile == null || updateImageDto.ImageFile.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var uploadResult = await _placesManager.UpdateImageAsync(updateImageDto.ImageFile);

            if (uploadResult.Error != null)
            {
                return BadRequest(uploadResult.Error.Message);
            }

            image.ImageUrl = uploadResult.SecureUrl.AbsoluteUri;
            await _context.SaveChangesAsync();
            //return Ok("Image updated successfully");
            return StatusCode(200, " Image updated successfully");
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

                //return Ok("Place Added Successfully.");
                return StatusCode(200, "Place Added Successfully.");

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

        /*Query Examples:
         api/Place/filter?$filter=location eq 'Alexandria'
         api/Place/filter/?$orderby=Name&skip=5
         api/Place/filter?$filter=location eq 'Alexandria'&orderby=Name desc
         api/Place/filter?$filter=location eq 'Alexandria' and price le 220
         api/Place/filter/?$select=Name
         */

        /**
        Static Guide Url:
        if (categoryName == null)
        categoryCheck = "ne"
        if (location == null)
        locationCheck = "ne"
         Api/Place/filter?$filter=CategoryName {categoryCheck} '{categoryName}' and price le {price} and rating ge {rating} and PeopleCapacity ge {capacity} and location {locationCheck} '{location}'&$orderby={order} {ascORdesc}&skip={skippedItems}
         */
        [HttpGet("filter")]
        [EnableQuery(PageSize = 20)]
        public ActionResult<IQueryable<FilterSearchPlaceDto>> FilterPlaces()
        {
            var places = _placesManager.FilterPlaces().AsQueryable();
            return Ok(places);
        }

        /*
         api/Place/search?$filter=contains(location, 'airo')
         api/Place/search?$filter=contains(name, 'Luxury Villa')&top=3
         */

        /*
         Static Guide Url:
            Api/Place/search?$filter=contains(location, '{query}') or contains(name, '{query}')&$orderby={order} {AscOrDesc}
         */
        [HttpGet("search")]
        [EnableQuery(PageSize = 20)]
        public ActionResult<IQueryable<FilterSearchPlaceDto>> SearchPlaces(string query)
        {
            var places = _placesManager.SearchPlaces().AsQueryable();
            return Ok(places);
        }

        /*
         Static Guide Url:
            Api/Place/category?$filter=categoryname eq '{categoryName}'&$orderby={order} {AscOrDesc}
         */
        [HttpGet("category")]
        [EnableQuery(PageSize = 20)]
        public ActionResult<IQueryable<CategoryPlacesDto>> GetCategoryPlaces(string query)
        {
            var places = _placesManager.GetCategoryPlaces().AsQueryable();
            return Ok(places);
        }
        [HttpGet("Owner")]
        public async Task<ActionResult<IEnumerable<GetOwnerPlacesDto>>> GetOwnerPlacesAsync(string ownerId)
        {
            //check if owner is logged in or not
            if (ownerId == null)
            {
                return BadRequest("Owner is not logged in");
            }
            var places = await _placesManager.GetOwnerPlacesAsync(ownerId);
            if (places == null)
            {
                return NotFound();
            }
            return Ok(places);
        }






        [HttpPost("{placeId}/Reviews and OverallRating/{userId}")]
        public async Task<IActionResult> AddReviewAndCalculateOverallRating(int placeId, string userId, ReviewDto reviewDto)
        {
            var isSuccess = await _placesManager.AddReviewAndCalculateOverallRating(placeId, userId, reviewDto);
            if (isSuccess)
            {
                return StatusCode(200, "Review added successfully, and updated overall rating");
            }
            else
            {
                return StatusCode(404, "Place not found");
            }
        }

        [HttpPost("{placeId}/post comment /{userId}")]
        public async Task<IActionResult> AddComment(int placeId, string userId, CommentDto commentDto)
        {
            var comment = await _placesManager.AddComment(placeId, userId, commentDto);
            if (comment)
            {
                return Ok(new Response { Status = "Success", Message = "Comment added successfully" });
            }
            else
            {
                return BadRequest(new Response { Status = "Error", Message = "Place not found" });
            }
        }
    }
}



