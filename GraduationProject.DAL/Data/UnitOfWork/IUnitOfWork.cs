using GraduationProject.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL.Data;

public interface IUnitOfWork
{
    public ICategoryRepo Categoryrepo { get; }
    public IPlacesRepo Placesrepo { get; }

    public IBookingRepo Bookingrepo { get; }
    
    public int SaveChanges ();
    public Task SaveChangesAsync();
}
