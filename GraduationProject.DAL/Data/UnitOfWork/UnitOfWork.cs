using GraduationProject.DAL.Repository;
using GraduationProject.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context , ICategoryRepo categoryRepo,IPlacesRepo placesRepo, IBookingRepo BookingRepo, IWishlistRepo wishlistRepo)
    {
        _context = context;
        Categoryrepo = categoryRepo;
        Placesrepo = placesRepo;
        Bookingrepo = BookingRepo;
        Wishlistrepo = wishlistRepo;
    }
    public ICategoryRepo Categoryrepo { get; }

    public IPlacesRepo Placesrepo {  get; }
    public IWishlistRepo Wishlistrepo { get; }

    public IBookingRepo Bookingrepo { get; }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public Task SaveChangesAsync()
    {
       return _context.SaveChangesAsync();
    }


    //ICategoryRepo IUnitOfWork.categoryRepo => throw new NotImplementedException();
}
