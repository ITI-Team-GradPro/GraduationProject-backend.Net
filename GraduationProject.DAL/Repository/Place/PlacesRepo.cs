using GraduationProject.DAL.Repository.Generics;
using GraduationProject.DAL.Repository;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL;

public class PlacesRepo : GenericRepo<Place>, IPlacesRepo
{
    private readonly ApplicationDbContext _context;
    public PlacesRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    

    //private readonly ApplicationDbContext _context;
    //public PlacesRepo(ApplicationDbContext context)
    //{
    //    _context = context;
    //}
    //public void Add(Place place)
    //{
    //   _context.Places.Add(place);
    //}

    //public void Delete(Place place)
    //{
    //    _context.Places.Remove(place);
    //}

    //public Place GetPlaceById(int id)
    //{
    //    return _context.Places.Find(id);
    //}

    //public IEnumerable<Place> GetAllPlaces()
    //{
    //    return _context.Places;
    //}

    //public int SaveChanges()
    //{
    //   return _context.SaveChanges();
    //}

}
