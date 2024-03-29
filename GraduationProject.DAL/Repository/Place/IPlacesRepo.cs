using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL;

public interface IPlacesRepo
{
    IEnumerable<Place> GetAllPlaces();
    Place? GetPlaceById(int id);
    void Add (Place place);
    void Delete (Place place);
    int SaveChanges();
    IQueryable<Place> FilterPlaces();
    IQueryable<Place> SearchPlaces();
    IQueryable<Place> GetPlacesInCategory();



}
