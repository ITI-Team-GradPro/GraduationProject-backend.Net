using CloudinaryDotNet.Actions;
using GraduationProject.BL.Dtos;
using GraduationProject.BL.Dtos.PlaceDtos;
using GraduationProject.BL.Dtos.UserDto;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public interface IUserManager
    {
        public Task<ImageUploadResult> UploadImageToCloudinary(IFormFile file , string userId);
        Task<GetUserProfileDto> GetUserProfile(string id);

        Task<UpdateUserProfileDto> UpdateUserProfile(string userid, UpdateUserProfileDto profileDto);
        //Task<HostPlaceBookingDto> GetHostBookingDetails(string userid, int bookingid);

    }
}
