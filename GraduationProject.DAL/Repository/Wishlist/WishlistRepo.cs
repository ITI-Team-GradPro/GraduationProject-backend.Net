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

        var userplace = await _context.Users
              .Where(d => d.Id == userid)
             .Include(a => a.OwnedPlaces)
             .ThenInclude(c => c.Images)
             .ToListAsync();

        var userwishlist = userplace.SelectMany(a => a.OwnedPlaces).ToList();
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
