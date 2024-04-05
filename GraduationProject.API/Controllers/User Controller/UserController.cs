using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GraduationProject.BL.Managers.Places;
using GraduationProject.DAL.Data;
using GraduationProject.Data.Context;
using Microsoft.Extensions.Options;
using GraduationProject.DAL;
using GraduationProject.BL;
using System;
using GraduationProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GraduationProject.BL.Dtos;

namespace GraduationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserManager _userManger;

        public UserController(IUserManager userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManger = userManager;

        }

        [HttpPost("{userId}/image")]
        public async Task<ActionResult> AddPhoto(IFormFile file, string userId)
        {
            var result = await _userManger.UploadImageToCloudinary(file, userId);
            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.ImageUrl = result.SecureUrl.AbsoluteUri;

            await _context.SaveChangesAsync();

            return StatusCode(200, "User image uploaded successfully");
        }
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserProfile(string id)
        {
            //var currentUserId = User.Identity.Name;
            //if (id != currentUserId)
            //{
            //    return Forbid("You can only access your own data.");
            //}
            var user = await _userManger.GetUserProfile(id);
            if (user == null)
            {
                return NotFound();
            }
            var userProfile = await _userManger.GetUserProfile(id);
            return Ok(userProfile);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateUserProfileDto>> UpdateUserProfile(string id, UpdateUserProfileDto profileDto)
        {
            try
            {
                var profile = await _userManger.UpdateUserProfile(id, profileDto);
                if (profile is null)
                {
                    return NotFound(new GeneralResponse { StatusCode = "Error", Message = "Can't update profile!" });
                }
                return Ok(profile);
            }
            catch (Exception)
            {
                return NotFound(new GeneralResponse { StatusCode = "Error", Message = "Can't update profile!" });
            }
        }
        
    }
}