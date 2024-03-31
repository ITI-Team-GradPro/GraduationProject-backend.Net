using CloudinaryDotNet.Actions;
using GraduationProject.Bl.Dtos.PlaceDtos;
using GraduationProject.BL.Dtos;
using GraduationProject.BL.Dtos.PlaceDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraduationProject.BL;

public interface IPlacesManager
{

    Task<GetPlacesDtos?> GetById(int id);


    Task< bool> Delete(int id);
    public Task<PlaceDetailsDto> GetByIdWithUser(int id);
    public Task<ImageUploadResult> UpdateImageAsync(IFormFile file);

    public Task<bool> Update(UpdatePlaceDto updatePlaceDto);

    Task<ImageUploadResult> AddPlaceAsync(AddPlaceDto addPlaceDto, IFormFile file);
    IQueryable<FilterSearchPlaceDto> FilterPlaces();
    IQueryable<FilterSearchPlaceDto> SearchPlaces();

    IQueryable<CategoryPlacesDto> GetCategoryPlaces();

}
