using AutoMapper;
using GraduationProject.BL.Dtos;
using GraduationProject.BL.Managers;
using GraduationProject.DAL.Data;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace GraduationProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishlistController : ControllerBase
{
    private readonly IWishlistManager _wishlistManager;

    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _UnitOfWork;

    public WishlistController(IWishlistManager wishlistManager, ApplicationDbContext context , IUnitOfWork unitOfWork)
    {
        _wishlistManager = wishlistManager;
        _context = context;
        _UnitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<ActionResult> Add(AddWishlistDto addWishlistDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var wishlist = await _UnitOfWork.Wishlistrepo.GetAll();
        var wishlistadded = await _UnitOfWork.Wishlistrepo.Wishlistbyuseridandplaceid(addWishlistDto.UserId, addWishlistDto.PlaceId);

        if(wishlistadded is not null && wishlist.Any(w => w.UserId == addWishlistDto.UserId && w.PlaceId == addWishlistDto.PlaceId))
        {
            return Conflict(new GeneralResponse { StatusCode="Error" , Message="Place already exists in your wishlist"});
        }

        var newwishlist = await _wishlistManager.Add(addWishlistDto);
        return Ok(new GeneralResponse { StatusCode="Success" , Message= "Place added to your Wishlist Successfully" });
    }



    //[HttpGet("{userid}")]
    //public async Task<ActionResult<IEnumerable<GetPlaceWishlistDto>>> UserplaceList(string userid)
    //{
    //    IEnumerable<Place> wishlist = await _UnitOfWork.Placesrepo.GetAll();

    //    try
    //    {
    //        var userPlaces = await _context.Users
    //            .Where(u => u.Id == userid)
    //            .Include(u => u.OwnedPlaces)
    //            .ThenInclude(p => p.Images)
    //            .SelectMany(u => u.OwnedPlaces)
    //            .Select(d => d.Images)
    //            .ToListAsync();

    //        var placeDtos = wishlist.Select(p => new GetPlaceWishlistDto
    //        {
    //            PlaceId = p.PlaceId,
    //            Name = p.Name,
    //            Price = p.Price,
    //            OverAllRating = p.OverAllRating,
    //            Description = p.Description,
    //            ImgsPlaces = p.Images.Select(i => new GetImagePlaceWishlistDto
    //            {
    //                PlaceId = p.PlaceId,
    //                ImgsPlaceId = i.ImgsPlaceId,
    //                ImageUrl = i.ImageUrl,
    //                publicId = i.publicId
    //            }).ToList()
    //        }).ToList();

    //        return placeDtos;
    //    }
    //    catch (Exception ex)
    //    {

    //        return StatusCode(500, "Can't get user's wishlist.");
    //    }
    //}

    //[HttpGet("{userid}")]
    //public async Task<ActionResult<IEnumerable<GetPlaceWishlistDto>>> UserplaceList(string userid)
    //{
    //    //IEnumerable<WishList> wishLists = await _UnitOfWork.Wishlistrepo.GetAll();
    //    IEnumerable<Place> wishlist = await _UnitOfWork.Wishlistrepo.UserplaceList(userid);

    //    var userPlaces = await _context.Users
    //          .Where(u => u.Id == userid)
    //        .Include(u => u.OwnedPlaces)
    //        .ThenInclude(d => d.Images)
    //        .SelectMany(u => u.OwnedPlaces)
    //        .ToListAsync();




    //    var placeDtos = wishlist.Select(p => new GetPlaceWishlistDto
    //    {
    //        PlaceId = p.PlaceId,
    //        Name = p.Name,
    //        Price = p.Price,
    //        OverAllRating = p.OverAllRating,
    //        Description = p.Description,
    //        ImgsPlaces = p.Images.Select(i => new GetImagePlaceWishlistDto
    //        {

    //            ImageUrl = i.ImageUrl

    //        }).ToList()
    //    }).ToList();

    //    return placeDtos;
    //}
    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<GetPlaceWishlistDto>>> userwishlist(string userId)
    {
        var wish = await _wishlistManager.GetAll(userId);
        return Ok(wish);
        //try
        //{
        //    var wishlist = await _context.WishList
        //       .Include(a => a.User)
        //       .Where(d => d.UserId == userId)
        //       .Include(s => s.Places)
        //       .ThenInclude(e => e.Images)
        //       .ToListAsync();


            var wishlistDtoList = wishlist
                .Select(item => item.Places)
                .Select(place => new GetPlaceWishlistDto
                {
                    PlaceId = place.PlaceId,
                    Name = place.Name,
                    Price = place.Price,
                    OverAllRating = place.OverAllRating,
                    Description = place.Description,
                    ImageUrls = place.Images.Select(x => x.ImageUrl).ToArray()
                })
                .ToList();

    [HttpDelete("{userid}/wishlist/{placeid}")]
    public async Task<ActionResult> DeletePlaceFromWishlist(string userid, int placeid)
    {
        IEnumerable<WishList> wishlist = await _UnitOfWork.Wishlistrepo.GetAll();
        try
        {
            var deletedPlace = await _wishlistManager.DeletePlaceFromWishlist(userid, placeid);

            if (deletedPlace != null)
            {
                return Ok(new GeneralResponse { StatusCode = "Success", Message = "place deleted from your wishlist successfully" });
            }
            else
            {
                return NotFound(new GeneralResponse { StatusCode = "Error", Message = "Place not found" });
            }
        }
        catch (Exception ex)
        {

            return NotFound(new GeneralResponse { StatusCode = "Error", Message = "An error occurred while deleting the place from your wishlist" });
        }
    }
}







    //[HttpDelete("{userId}/wishlist/{placeId}")]
    //public async Task<ActionResult> DeletePlaceFromWishlist(string userId, int placeId)
    //{

    //    IEnumerable<WishList> wishlist = await _UnitOfWork.Wishlistrepo.GetAll();

    //    var userplace = await _UnitOfWork.Placesrepo.GetById(placeId);

    //    var deleted = await _UnitOfWork.Wishlistrepo.placedeleted(userId, placeId);



    //    if (deleted == userplace)
    //    {
    //        await _UnitOfWork.Wishlistrepo.Delete(deleted);
    //        return Ok("Place deleted from wishlist successfully.");
    //    }
    //    else
    //    {
    //        return NotFound("Place not found in user's wishlist.");
    //    }
    //}


 









