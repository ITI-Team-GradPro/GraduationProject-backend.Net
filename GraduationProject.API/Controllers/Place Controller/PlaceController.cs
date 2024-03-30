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


            var result = await _placesManager.AddPlaceAsync(newPlaceDto, file);

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
        public async Task<ActionResult<GetPlacesWithUserDtos>> GetByIdWithUser(int id)
        {
            GetPlacesWithUserDtos? PlacesById = await _placesManager.GetByIdWithUser(id);

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
        [HttpGet("search")]
        [EnableQuery(PageSize = 20)]
        public ActionResult<IQueryable<FilterSearchPlaceDto>> SearchPlaces(string query)
        {
            var places = _placesManager.SearchPlaces().AsQueryable();
            return Ok(places);
        }
        //[HttpGet("category")]
        //[EnableQuery(PageSize = 20)]
        //public ActionResult<IQueryable<CategoryPlacesDto>> GetCategoryPlaces(string categoryName, bool order, string orderby)
        //{
        //    string orderAsString;
        //    var places = _placesManager.GetCategoryPlaces().AsQueryable();
        //    if (!order == false)
        //    {
        //        orderAsString = "asc";
        //    }
        //    else
        //    {
        //        orderAsString = "desc";
        //    }
        //    if (orderby is null)
        //    {
        //        orderby = "id";
        //    }
        //    string baseUrl = "localhost:44300/api/Place/category/";
        //    string query = $"?$filter=categoryname eq '{categoryName}'&$orderby={orderby} {orderAsString}";

        //    var uri = new Uri(baseUrl + query);
        //    ODataRouteOptions options = new ODataRouteOptions();
        //    return Ok();
        //}

        /*
         Api/Place/category?$filter=categoryname eq '{categoryName}'&$orderby={order} {AscOrDesc}
         */
        [HttpGet("category")]
        [EnableQuery(PageSize = 20)]
        public ActionResult<IQueryable<CategoryPlacesDto>> GetCategoryPlaces(string query)
        {
            var places = _placesManager.GetCategoryPlaces().AsQueryable();
            return Ok(places);
        }


    }
}


