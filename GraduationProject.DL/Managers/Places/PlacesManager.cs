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
        private readonly ApplicationDbContext _context;



        public PlacesManager(IUnitOfWork unitOfWork, IOptions<CloudinarySettings> config, ApplicationDbContext context)
        {
            _context = context;
            _UnitOfWork = unitOfWork;
            var acc = new Account
           (
              config.Value.CloudName,
              config.Value.APIKey,
              config.Value.APISecret

           );

            _Cloudinary = new Cloudinary(acc);
        }


        // Get Place with Image By Id 
        public async Task<GetPlacesDtos> GetById(int id)
        {
            Place place = await _context.Places.FindAsync(id);
           
            ImgsPlace imgs = await _context.ImagesPlaces.FirstOrDefaultAsync(c => c.PlaceId == place.PlaceId);


            GetPlacesDtos getPlacesDtos = new GetPlacesDtos
            {
                PlaceId = place.PlaceId,
                Name = place.Name,
                Price = place.Price,
                OverAllRating = place.OverAllRating,
                Location = place.Location,
                Description = place.Description,
                PeopleCapacity = place.PeopleCapacity,
                // Check if an image is associated with the place
                ImageUrl = imgs.ImageUrl
            };
            return getPlacesDtos;
        }

        // Get Place with Image and User By ID 
        public async Task<PlaceDetailsDto> GetByIdWithUser(int id)
        {
            var place = await _context.Places
                .Include(p => p.Owner).Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.PlaceId == id);

            if (place == null)
            {
                return null;
            }


            var imgUrls = await _context.ImagesPlaces
                .Where(i => i.PlaceId == place.PlaceId)
                .Select(i => i.ImageUrl)
                .ToListAsync();

            var ownerDto = new OwnerDetailsDto
            {
                id = place.OwnerId,
                FirstName = place.Owner.FirstName,
                LastName = place.Owner.LastName,
                Bio = place.Owner.Bio,
                ImageUrl = place.Owner.ImageUrl
            };



            var commentsDto = await _context.Comments
                .Where(c => c.PlaceId == place.PlaceId)
                .Select(c => new CommentDetailsDto
                {
                    CommentId = c.CommentID,
                    Comment = c.CommentText,
                    UserId = c.UserId,
                    User = new UserDetailsDto
                    {
                        Name = c.User.UserName,
                        Image = c.User.ImageUrl,
                        CommentDateTime = c.CommentDateTime
                    }

                })
                .ToListAsync();

            var reviewsDto = await _context.Reviews
               .Where(r => r.PlaceId == place.PlaceId)
               .Select(r => new ReviewDetailsDto
               {
                   ReviewId = r.ReviewID,
                   Review = r.ReviewText,
                   Rating = r.Rating,
                   UserId = r.UserId,
                   User = new UserDetailsDto
                   {
                       Name = r.User.UserName,
                       Image = r.User.ImageUrl,
                   }
               })
               .ToListAsync();

            var categoryDto = new CategoryDetailsDto
            {
                CategoryName = place.Category.CategoryName
            };


            var placeDto = new PlaceDetailsDto
            {
                PlaceId = place.PlaceId,
                Name = place.Name,
                Price = place.Price,
                OverAllRating = place.OverAllRating,
                Location = place.Location,
                Description = place.Description,
                PeopleCapacity = place.PeopleCapacity,
                ImageUrls = imgUrls,
                ownerDetailsDto = ownerDto,
                CommentDetailsDto = commentsDto,
                categoryDetailsDto = categoryDto,
                reviewDetailsDto = reviewsDto

            };

            return placeDto;
        }


        public async Task<bool> Update(UpdatePlaceDto updatePlaceDto)
        {
            Place? place = await _UnitOfWork.Placesrepo.GetById(updatePlaceDto.PlaceId);

            if (place == null) return false;

            place.Name = updatePlaceDto.Name;
            place.Description = updatePlaceDto.Description;
            place.Location = updatePlaceDto.Location;
            place.Price = updatePlaceDto.Price;
            place.PeopleCapacity = updatePlaceDto.PeopleCapacity;


            await _UnitOfWork.Placesrepo.Update(place);
            await _UnitOfWork.SaveChangesAsync();

            return true;
        }


        //Update Image Only
        public async Task<ImageUploadResult> UpdateImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stream),
                    // Set the public ID to update the existing image
                };

                uploadResult = await _Cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        // Add Place With Image
        public async Task<ImageUploadResult> AddPlaceAsync(AddPlaceDto addPlaceDto, IFormFile file)
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
                await _UnitOfWork.SaveChangesAsync();


            }
            return uploadResult;

        }

        // Delete Place With Image
        public async Task<bool> Delete(int id)
        {
            Place? placedb = await _UnitOfWork.Placesrepo.GetById(id);
            if (placedb == null)
            {
                return false;
            }
            try
            {

                await _UnitOfWork.Placesrepo.Delete(placedb);
                await _UnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { return  false; }

            }
        public IQueryable<FilterSearchPlaceDto> FilterPlaces()
        {
            IQueryable<Place> filterPlacesDB = _UnitOfWork.Placesrepo.FilterPlaces();
            var filterPlacesDto = filterPlacesDB.Select(x => new FilterSearchPlaceDto
            {
                Id = x.PlaceId,
                CategoryId = x.CategoryId,
                Rating = x.OverAllRating,
                Price = x.Price,
                PeopleCapacity = x.PeopleCapacity,
                Location = x.Location,
                ImagesUrls = x.Images.Select(i => i.ImageUrl).ToArray(),
                description = x.Description,
                Name = x.Name
            });
            return filterPlacesDto;
        }

        public IQueryable<FilterSearchPlaceDto> SearchPlaces()
        {
            IQueryable<Place> searchPlacesDB = _UnitOfWork.Placesrepo.SearchPlaces();
            var searchPlacesDto = searchPlacesDB.Select(x => new FilterSearchPlaceDto
            {
                Id = x.PlaceId,
                CategoryId = x.CategoryId,
                Rating = x.OverAllRating,
                Price = x.Price,
                PeopleCapacity = x.PeopleCapacity,
                Location = x.Location,
                ImagesUrls = x.Images.Select(i => i.ImageUrl).ToArray(),
                description = x.Description,
                Name = x.Name
            });
            return searchPlacesDto;

        }

        public IQueryable<CategoryPlacesDto> GetCategoryPlaces()
        {
            IQueryable<Place> places = _UnitOfWork.Placesrepo.GetPlacesInCategory();
            var searchPlacesDto = places.Select(x => new CategoryPlacesDto
            {
                Id = x.PlaceId,
                Rating = x.OverAllRating,
                Price = x.Price,
                Location = x.Location,
                Name = x.Name,
                Description = x.Description,
                ImagesUrls = x.Images.Select(i => i.ImageUrl).ToArray(),
                CategoryName = x.Category.CategoryName
            });
            return searchPlacesDto;
        }

        public async Task<IEnumerable<GetOwnerPlacesDto>> GetOwnerPlacesAsync(string ownerId)
        {
            var places = _UnitOfWork.Placesrepo.GetOwnerPlacesAsync(ownerId);
            var placesdto = places.Result.Select(x => new GetOwnerPlacesDto
             {
                 id = x.PlaceId,
                 OverAllRating = x.OverAllRating,
                 Price = x.Price,
                 Location = x.Location,
                 Name = x.Name,
                 Images = x.Images.Select(x => x.ImageUrl).ToArray(),
                 CategoryName = x.Category.CategoryName
             });
            return await Task.FromResult(placesdto);
        }
    }



} 

