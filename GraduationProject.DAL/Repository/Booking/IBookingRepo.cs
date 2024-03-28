using GraduationProject.DAL.Repository.Generics;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL
{
    public interface IBookingRepo: IGenericRepo<Booking>
    {
        //retrieve data based on an expression or something other than Id
        Task<IEnumerable<Booking>> GetAllById(string Id, Expression<Func<Booking, bool>> predicate);
    }
}
