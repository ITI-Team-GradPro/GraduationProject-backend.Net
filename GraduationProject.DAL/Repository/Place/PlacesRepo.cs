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
    public async Task<IEnumerable<Place>> GetOwnerPlacesAsync(string ownerId)
    {
        var places = _context.Places.Where(p => p.OwnerId == ownerId).Include(p => p.Category).Include(p => p.Images).ToListAsync();
        return await places;
    }

    public async Task<int> GetPlaceCountInCategory(int categoryId)
    {
        var PlacePerCategory = await _context.Places.Where(d => d.CategoryId == categoryId).CountAsync();
        return PlacePerCategory;
    }


}
