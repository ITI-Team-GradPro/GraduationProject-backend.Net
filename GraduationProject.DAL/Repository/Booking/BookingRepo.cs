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
    }
}
