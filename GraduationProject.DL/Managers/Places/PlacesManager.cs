using GraduationProject.Bl.Dtos.PlaceDtos;
using GraduationProject.BL.Dtos.PlaceDtos;
using GraduationProject.DAL;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Managers.Places
{
    public class PlacesManager : IPlacesManager
    {
        private readonly IPlacesRepo _placesRepo;

        public PlacesManager(IPlacesRepo placesRepo)
        {
            _placesRepo = placesRepo;
        }
        public int Add(AddPlaceDto place)
        {

            Place placedb = new Place()
            {
                Name = place.Name,
                Price = place.Price,
                Location = place.Location,
                Description = place.Description,
                PeopleCapacity = place.PeopleCapacity,
                OwnerId = place.OwnerId,
                CategoryId = place.CategoryId

            };
            _placesRepo.Add(placedb);
            _placesRepo.SaveChanges();

            return (placedb.PlaceId);
        }

        public bool Delete(int id)
        {
            Place? placedb = _placesRepo.GetPlaceById(id);
            if (placedb == null)
            {
                return false;
            }
            try
            {

                _placesRepo.Delete(placedb);
                _placesRepo.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<GetPlacesDtos> GetAll()
        {
            IEnumerable<Place> placesdb = _placesRepo.GetAllPlaces();
            var placedto = placesdb.Select(x => new GetPlacesDtos
            {
                Name = x.Name,
                Description = x.Description,
                PlaceId = x.PlaceId,
                Price = x.Price,
                Location = x.Location,
                OverAllRating = x.OverAllRating,
                PeopleCapacity = x.PeopleCapacity
            });
            return placedto;

        }

        //public IQueryable<GetPlacesDtos> GetPlacesByFilters()
        //{
        //    IQueryable<Place> placesdb = _placesRepo.GetPlacesByFilters();
        //    var placedto = placesdb.Select(x => new GetPlacesDtos
        //    {
        //        Name = x.Name,
        //        Description = x.Description,
        //        PlaceId = x.PlaceId,
        //        Price = x.Price,
        //        Location = x.Location,
        //        OverAllRating = x.OverAllRating,
        //        PeopleCapacity = x.PeopleCapacity
        //    });
        //    return placedto;
        //}

        public GetPlacesDtos GetPlacesById(int id)
        {
            Place? placesdb = _placesRepo.GetPlaceById(id);
            if (placesdb == null)
            {
                return null;
            }
            var placedto = new GetPlacesDtos
            {
                Name = placesdb.Name,
                Description = placesdb.Description,
                PlaceId = placesdb.PlaceId,
                Price = placesdb.Price,
                Location = placesdb.Location,
                OverAllRating = placesdb.OverAllRating,
                PeopleCapacity = placesdb.PeopleCapacity
            };
            return placedto;

        }
        public IQueryable<FilterSearchPlaceDto> FilterPlaces()
        {
            IQueryable<Place> filterPlacesDB = _placesRepo.FilterPlaces();
            var filterPlacesDto = filterPlacesDB.Select(x => new FilterSearchPlaceDto
            {
                Id = x.PlaceId,
                CategoryId = x.CategoryId,
                Rating = x.OverAllRating,
                Price = x.Price,
                PeopleCapacity = x.PeopleCapacity,
                Location = x.Location,
                ImgUrl = "",
                description = x.Description,
                Name = x.Name
            }) ;
            return filterPlacesDto;
        }

        public IQueryable<FilterSearchPlaceDto> SearchPlaces()
        {
            IQueryable<Place> searchPlacesDB = _placesRepo.SearchPlaces();
            var searchPlacesDto = searchPlacesDB.Select(x => new FilterSearchPlaceDto
            {
                Id = x.PlaceId,
                CategoryId = x.CategoryId,
                Rating = x.OverAllRating,
                Price = x.Price,
                PeopleCapacity = x.PeopleCapacity,
                Location = x.Location,
                ImgUrl = "",
                description = x.Description,
                Name = x.Name
            });
            return searchPlacesDto;

        }

        public IQueryable<CategoryPlacesDto> GetCategoryPlaces()
        {
            
        }
    }
}
