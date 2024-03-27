using CloudinaryDotNet.Actions;
using GraduationProject.Bl.Dtos.PlaceDtos;
using GraduationProject.BL.Dtos;
using GraduationProject.BL.Dtos.PlaceDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL;

public interface IPlacesManager
{
    IEnumerable<GetPlacesDtos> GetAll();

    GetPlacesDtos? GetById(int id);

    int Add(AddPlaceDto addPlaceDto);

    bool Delete(int id);

    Task<ImageUploadResult> AddPhotoAsync(AddPlaceDto addPlaceDto,IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string ImgsPlaceId);

    //IEnumerable<GetPlacesDtos> GetAll();
    //GetPlacesDtos? GetPlacesById(int id);

    //int Add(AddPlaceDto place);

    //bool Delete(int id);

}
