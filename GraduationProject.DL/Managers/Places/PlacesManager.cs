using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GraduationProject.Bl.Dtos.PlaceDtos;
using GraduationProject.BL.Dtos.PlaceDtos;
using GraduationProject.DAL;
using GraduationProject.DAL.Data;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Managers.Places
{
    public class PlacesManager : IPlacesManager
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly Cloudinary _Cloudinary;


        public PlacesManager(IUnitOfWork unitOfWork, IOptions<CloudinarySettings> config)
        {
            _UnitOfWork = unitOfWork;
            var acc = new Account
           (
              config.Value.CloudName,
              config.Value.APIKey,
              config.Value.APISecret

           );

            _Cloudinary = new Cloudinary(acc);
        }

        public int Add(AddPlaceDto addPlaceDto)
        {

            Place placedb = new Place()
            {
                Name = addPlaceDto.Name,
                Price = addPlaceDto.Price,
                Location = addPlaceDto.Location,
                Description = addPlaceDto.Description,
                PeopleCapacity = addPlaceDto.PeopleCapacity,
                OwnerId = addPlaceDto.OwnerId,
                CategoryId = addPlaceDto.CategoryId

            };

            _UnitOfWork.Placesrepo.AddAsync(placedb);
            //_UnitOfWork.Placesrepo.SaveChanges();
            return (placedb.PlaceId);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(AddPlaceDto addPlaceDto, IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation()/*.Height(500).Width(500).Crop("fill").Gravity("face")*/

                };

                uploadResult = await _Cloudinary.UploadAsync(uploadParams);
                //_UnitOfWork.Placesrepo.SaveChanges();


            }
            return uploadResult;

        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        //public bool Delete(int id)
        //{

        //    Place? placedb = _UnitOfWork.Placesrepo.GetById(id);
        //    if (placedb == null)
        //    {
        //        return false;
        //    }
        //    try
        //    {

        //        _UnitOfWork.Placesrepo.Delete(placedb);
        //        //_UnitOfWork.Placesrepo.SaveChanges();
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public async Task<DeletionResult> DeletePhotoAsync(string ImgsPlaceId)
        {
            var deleteParams = new DeletionParams(ImgsPlaceId);

            var result = await _Cloudinary.DestroyAsync(deleteParams);

            return result;
        }

        public IEnumerable<GetPlacesDtos> GetAll()
        {
            IEnumerable<Place> placesdb = (IEnumerable<Place>)_UnitOfWork.Placesrepo.GetAll();
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

        public GetPlacesDtos GetById(int id)
        {
            throw new NotImplementedException();
        }

        //public GetPlacesDtos GetById(int id)
        //{
        //    Place? placesdb = _UnitOfWork.Placesrepo.GetById(id);
        //    if (placesdb == null)
        //    {
        //        return null;
        //    }
        //    var placedto = new GetPlacesDtos
        //    {
        //        Name = placesdb.Name,
        //        Description = placesdb.Description,
        //        PlaceId = placesdb.PlaceId,
        //        Price = placesdb.Price,
        //        Location = placesdb.Location,
        //        OverAllRating = placesdb.OverAllRating,
        //        PeopleCapacity = placesdb.PeopleCapacity
        //    };
        //    return placedto;
        //}






        //private readonly IPlacesRepo _placesRepo;

        //public PlacesManager(IPlacesRepo placesRepo)
        //{
        //    _placesRepo = placesRepo;
        //}
        //public int Add(AddPlaceDto place)
        //{

        //    Place placedb = new Place()
        //    {
        //        Name = place.Name,
        //        Price = place.Price,
        //        Location = place.Location,
        //        Description = place.Description,
        //        PeopleCapacity = place.PeopleCapacity,
        //        OwnerId = place.OwnerId,
        //        CategoryId = place.CategoryId

        //    };
        //    _placesRepo.Add(placedb);
        //    _placesRepo.SaveChanges();

        //    return (placedb.PlaceId);
        //}

        //public bool Delete(int id)
        //{
        //    Place? placedb=_placesRepo.GetPlaceById(id);
        //    if(placedb == null)
        //    {
        //        return false;
        //    }
        //    try
        //    {

        //    _placesRepo.Delete(placedb);
        //    _placesRepo.SaveChanges();
        //    return true;

        //    }catch (Exception ex)
        //    {
        //      return false;
        //    }
        //}

        //public IEnumerable<GetPlacesDtos> GetAll()
        //{
        //    IEnumerable<Place> placesdb = _placesRepo.GetAllPlaces();
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

        //public GetPlacesDtos GetPlacesById(int id)
        //{
        //    Place? placesdb=_placesRepo.GetPlaceById(id);
        //    if (placesdb == null)
        //    {
        //        return null;
        //    }
        //    var placedto =  new GetPlacesDtos
        //    {
        //        Name = placesdb.Name,
        //        Description = placesdb.Description,
        //        PlaceId = placesdb.PlaceId,
        //        Price = placesdb.Price,
        //        Location = placesdb.Location,
        //        OverAllRating = placesdb.OverAllRating,
        //        PeopleCapacity = placesdb.PeopleCapacity
        //    };
        //    return placedto;

        //}
    }
}
