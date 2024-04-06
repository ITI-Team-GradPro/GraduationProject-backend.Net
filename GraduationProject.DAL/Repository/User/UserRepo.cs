using GraduationProject.DAL.Repository.Generics;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL;

public class UserRepo : GenericRepo<User>, IUserRepo
{
    private readonly ApplicationDbContext _context;
    public UserRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    //async Task<User> GetUserById(string id);
    public async Task<User> GetUserProfile(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        //await _context.Users.Entry(user).Collection(i => i.OwnedPlaces).LoadAsync();
       await _context.Users
                            .Include(user => user.OwnedPlaces)
                            .ThenInclude(place => place.Images)
                            .Where(user => user.Id == id)
                            .LoadAsync();
        return user;
    }

    //public async Task<Place> GetHostBookingDetails(string hostid, int placeid)
    //{
    ////    //    var Booking = await _context.Bookings.Where(d => d.BookingId == bookingid)
    ////    //        .Include(a => a.User)
    ////    //        .Where(d => d.UserId == userid)
    ////    //        .Include(e => e.Place);

    ////            var userBooking = await _context.Users
    ////         .Where(u => u.Id == hostid)
    ////         .Include(u => u.OwnedPlaces)
    ////         .Select(u => new UserBookingResult
    ////         {
    ////             User = u,
    ////             Booking = _context.Places
    ////                 .Where(p => p.PlaceId == placeid)
    ////                 .Include(p => p.Bookings)
    ////                 .FirstOrDefault()
    ////         })
    ////         .FirstOrDefaultAsync();


    ////    return userBooking ;

    //        }
}
