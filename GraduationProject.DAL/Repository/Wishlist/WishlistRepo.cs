using GraduationProject.DAL.Repository.Generics;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL.Repository;

public class WishlistRepo : GenericRepo<WishList> , IWishlistRepo
{
    private readonly ApplicationDbContext _context;
    public WishlistRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Place>> UserplaceList(string userid)
    {

        //var userplace = await _context.WishList
        //      .Where(d => d.UserId == userid)
        //      .Include(d => d.User)
        //     .ThenInclude(a => a.OwnedPlaces)
        //     .ThenInclude(d => d.Images)
        //     .ToListAsync();

        //var userwishlist = userplace.Select(a => a.User).SelectMany(d => d.OwnedPlaces).ToList();
        //return userwishlist;

        //var userPlaces = await _context.Users
        //  .Where(u => u.Id == userid)
        //.Include(u => u.OwnedPlaces)
        //.ThenInclude(d => d.Images)
        //.SelectMany(u => u.OwnedPlaces)
        //.ToListAsync();

        var wishList = await _context.WishList
            .Where(w => w.UserId == userid)
            .Include(w => w.Places)
            .ToListAsync();

        var userwishlist = wishList.Select(a => a.User).SelectMany(d => d.OwnedPlaces).ToList();
        return userwishlist;
    }



    public async Task<WishList> placedeleted(string userid, int placeid)
    {
       
        var user = await _context.Users
            .Include(u => u.OwnedPlaces)
            .ThenInclude(d=>d.WishListPlaceUsers)
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


    //public async Task<bool> placedeleted(string userid, int placeid)
    //{
    //    var user = await _context.Users
    //        .Include(u => u.OwnedPlaces)
    //        .FirstOrDefaultAsync(u => u.Id == userid);

    //    if (user != null)
    //    {
    //        var placeToRemove = user.OwnedPlaces.FirstOrDefault(p => p.PlaceId == placeid);

    //        if (placeToRemove != null)
    //        {
    //            user.OwnedPlaces.Remove(placeToRemove); 
    //            await _context.SaveChangesAsync(); 

    //            return true;
    //        }
    //    }

    //    return false; 
    //}
 

}
