using GraduationProject.DAL.Repository.Generics;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static GraduationProject.Data.Models.Booking;

namespace GraduationProject.DAL
{
    public class BookingRepo : GenericRepo<Booking>, IBookingRepo
    {
        private readonly ApplicationDbContext _context;

        public BookingRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllById(string Id, Expression<Func<Booking, bool>> predicate)
        {
            var query = _context.Set<Booking>(); // Get DbSet

            // Apply filtering with Where (builds IQueryable)
            var filteredQuery = query.Where(predicate);

            // Execute the query asynchronously and materialize results
            return await filteredQuery.ToListAsync();
        }

        public async Task<IEnumerable<DateOnly>> GetUnavailableDates(int placeId, string period)
        {
            var dates = await _context.Bookings
                .Where(b => b.PlaceId == placeId && b.Period == (BookingPeriod)Enum.Parse(typeof(BookingPeriod), period))
                .Select(b => DateOnly.FromDateTime(b.EventDate))
                .ToListAsync();
            return dates;
        }

        //public async Task<IEnumerable<Booking>> GetBookingsAsync(Expression<Func<Booking, object>> include = null)
        //{
        //    var allBookings =  _context.Set<Booking>();
        //    var result = await allBookings.ToListAsync();
        //    return allBookings;
        //    //return result;
        //}


    }
}
