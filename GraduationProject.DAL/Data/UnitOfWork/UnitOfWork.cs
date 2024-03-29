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

    public UnitOfWork(ApplicationDbContext context , ICategoryRepo categoryRepo,IPlacesRepo placesRepo)
    {
        _context = context;
        Categoryrepo = categoryRepo;
        Placesrepo = placesRepo;
    }
    public ICategoryRepo Categoryrepo { get; }

    public IPlacesRepo Placesrepo {  get; }

    

    public void SaveChanges()
    {
        _context.SaveChanges();

    }
    //ICategoryRepo IUnitOfWork.categoryRepo => throw new NotImplementedException();
}
