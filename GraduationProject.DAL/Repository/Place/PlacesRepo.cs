using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL;

public class PlacesRepo : IPlacesRepo
{
    private readonly ApplicationDbContext _context;
    public PlacesRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    public void Add(Place place)
    {
        _context.Places.Add(place);
    }

    public void Delete(Place place)
    {
        _context.Places.Remove(place);
    }

    public Place GetPlaceById(int id)
    {
        return _context.Places.Find(id);
    }

    public IEnumerable<Place> GetAllPlaces()
    {
        return _context.Places;
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
    public IQueryable<Place> FilterPlaces()
    {
        var places = _context.Places.AsQueryable();
        return places;
    }

    public IQueryable<Place> SearchPlaces()
    {
        var places = _context.Places.AsQueryable();
        return places;
    }

    public IQueryable<Place> GetPlacesInCategory()
    {
        var places = _context.Places.AsQueryable();
        return places;
    }


    //public IQueryable<Place> MapPlaces()
    //{
    //    return _context.Places;
    //}
}
