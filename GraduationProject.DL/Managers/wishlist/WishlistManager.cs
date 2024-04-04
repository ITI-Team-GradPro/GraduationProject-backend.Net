using GraduationProject.BL.Dtos;
using GraduationProject.BL.Dtos.PlaceDtos;
using GraduationProject.DAL.Data;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Managers;

public class WishlistManager : IWishlistManager
{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly ApplicationDbContext _context;

    public WishlistManager(IUnitOfWork unitOfWork , ApplicationDbContext context)
    {
        _UnitOfWork = unitOfWork;
        _context = context;
    }
    async Task<int> IWishlistManager.Add(AddWishlistDto addWishlistDto)
    {
        WishList wishList = new WishList
        {
            UserId = addWishlistDto.UserId,
            PlaceId = addWishlistDto.PlaceId
        };

        await _UnitOfWork.Wishlistrepo.AddAsync(wishList);
        await _UnitOfWork.SaveChangesAsync();
        return wishList.PlaceId;

    }

    //async Task<IEnumerable<GetPlaceWishlistDto>> IWishlistManager.GetAll(string userid)
    //{
    //    IEnumerable<Place> wishLists = await _UnitOfWork.Placesrepo.GetAll();

    //    var placeDtos = wishLists.Select(p => new GetPlaceWishlistDto
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

    [HttpGet("{userid}")]
    public async Task<ActionResult<IEnumerable<GetPlaceWishlistDto>>> UserplaceList(string userid)
    {
        var userPlaces = await _context.Users
            .Where(u => u.Id == userid)
            .Include(u => u.OwnedPlaces)
            .ThenInclude(p => p.Images)
            .SelectMany(u => u.OwnedPlaces)
            .ToListAsync();

        var placeDtos = userPlaces.Select(p => new GetPlaceWishlistDto
        {
            PlaceId = p.PlaceId,
            Name = p.Name,
            Price = p.Price,
            OverAllRating = p.OverAllRating,
            Description = p.Description,
            ImgsPlaces = p.Images.Select(i => new GetImagePlaceWishlistDto
            {
              
                ImageUrl = i.ImageUrl
              
            }).ToList()
        }).ToList();

        return placeDtos;
    }


    //async Task<bool> IWishlistManager.Delete(string userid, int placeid)
    //{
    //    IEnumerable<WishList> wishlist = await _UnitOfWork.Wishlistrepo.GetAll();
    //    IEnumerable<Place> userplaces = await _UnitOfWork.Wishlistrepo.UserplaceList(userid);
    //    WishList? placetoberemoved = wishlist.FirstOrDefault(d => d.PlaceId == placeid);

    //    if (placetoberemoved is null)
    //    {
    //        return false;
    //    }
    //    await _UnitOfWork.Wishlistrepo.Delete(placetoberemoved);
    //    await _UnitOfWork.SaveChangesAsync();
    //    return true;


    //}

    public async Task<WishList> DeletePlaceFromWishlist(string userid, int placeid)
    {
        var user = await _context.Users
            .Include(u => u.OwnedPlaces)
            .ThenInclude(d => d.WishListPlaceUsers)
            .FirstOrDefaultAsync(u => u.Id == userid);

        if (user != null)
        {
            var placeToDelete = user.WishListUserPlaces.FirstOrDefault(p => p.PlaceId == placeid);

            if (placeToDelete != null)
            {
                user.WishListUserPlaces.Remove(placeToDelete);
                await _context.SaveChangesAsync();
                return placeToDelete;
            }
        }

        return null;
    }

    public async Task<WishList> GetByUserIdAndPlaceId(string userid , int placeid)
    {
        return await _UnitOfWork.Wishlistrepo.Wishlistbyuseridandplaceid(userid, placeid);
    }
}




