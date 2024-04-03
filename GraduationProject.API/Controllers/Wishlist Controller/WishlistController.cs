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

        var newwishlist = await _wishlistManager.Add(addWishlistDto);
        return Ok("Wishlist Added Successfully");
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
    //    IEnumerable<Place> wishlist = await _UnitOfWork.Placesrepo.GetAll();

    //    var userPlaces = await _context.Users
    //          .Where(u => u.Id == userid)
    //        .Include(u => u.OwnedPlaces)
    //        .ThenInclude(p => p.Images)
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


    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////
  
    //[HttpGet("{userid}")]
    //public async Task<ActionResult<IEnumerable<GetPlaceWishlistDto>>> UserplaceList(string userid)
    //{
    //    IEnumerable<WishList> wishLists1 = await _UnitOfWork.Wishlistrepo.GetAll();
    //    IEnumerable<Place> wishLists = await _UnitOfWork.Wishlistrepo.UserplaceList(userid);
    //    // Retrieve the user with their owned places and associated images
    //    var user = await _context.Users
    //        .Where(u => u.Id == userid)
    //        .Include(u => u.OwnedPlaces)
    //            .ThenInclude(p => p.Images)
    //        .FirstOrDefaultAsync();

    //    if (user == null)
    //    {
    //        return NotFound(); // User not found
    //    }

    //    // Extract the user's owned places
    //    var userPlaces = user.OwnedPlaces;

    //    // Map the user's owned places to DTOs
    //    var placeDtos = userPlaces.Select(p => new GetPlaceWishlistDto
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


    [HttpDelete("{userid}/wishlist/{placeid}")]
    public async Task<ActionResult> DeletePlaceFromWishlist(string userid, int placeid)
    {
        IEnumerable<WishList> wishlist = await _UnitOfWork.Wishlistrepo.GetAll();
        try
        {
            var deletedPlace = await _wishlistManager.DeletePlaceFromWishlist(userid, placeid);

            if (deletedPlace != null)
            {
                return Ok("place deleted from wishlist successfully"); 
            }
            else
            {
                return NotFound("no place found"); 
            }
        }
        catch (Exception ex)
        {
           
            return StatusCode(500, "An error occurred while deleting the place from the wishlist.");
        }
    }






}


