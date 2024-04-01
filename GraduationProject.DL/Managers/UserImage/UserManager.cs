using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GraduationProject.Bl.Dtos.PlaceDtos;
using GraduationProject.BL.Managers.Places;
using GraduationProject.DAL.Data;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL;

public class UserManager : IUserManager
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _UnitOfWork;
    private readonly Cloudinary _Cloudinary;



    public UserManager(IUnitOfWork unitOfWork, IOptions<CloudinarySettings> config , ApplicationDbContext context)
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
    public async Task<ImageUploadResult> UploadImageToCloudinary(IFormFile file , string userId)
    {
        
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation()

                };

                uploadResult = await _Cloudinary.UploadAsync(uploadParams);
                await _UnitOfWork.SaveChangesAsync();


            }
            return uploadResult;

        }


    
    }
