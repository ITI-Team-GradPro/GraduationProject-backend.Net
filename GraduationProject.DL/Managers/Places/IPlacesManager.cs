using GraduationProject.Bl.Dtos.PlaceDtos;
using GraduationProject.BL.Dtos.PlaceDtos;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL;

public interface IPlacesManager
{
    IEnumerable<GetPlacesDtos> GetAll();
    GetPlacesDtos? GetPlacesById(int id);

    int Add(AddPlaceDto place);

    bool Delete(int id);
    IQueryable<FilterSearchPlaceDto> FilterPlaces();
    IQueryable<FilterSearchPlaceDto> SearchPlaces();


}
