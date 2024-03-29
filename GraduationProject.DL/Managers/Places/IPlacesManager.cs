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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace GraduationProject.BL;

public interface IPlacesManager
{
    IEnumerable<GetPlacesDtos> GetAll();

    GetPlacesDtos? GetById(int id);

    int Add(AddPlaceDto addPlaceDto);

    bool Delete(int id);

    public  Task<ImageUploadResult> UpdateImageAsync(IFormFile file);

     public bool Update(UpdatePlaceDto updatePlaceDto);

    Task<ImageUploadResult>AddPhotoAsync(AddPlaceDto addPlaceDto, IFormFile file);
  
}
