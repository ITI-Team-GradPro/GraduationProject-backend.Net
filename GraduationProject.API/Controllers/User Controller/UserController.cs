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
using GraduationProject.BL.Dtos.SignDtos;
using static GraduationProject.Data.Models.User;
using GraduationProject.BL.Dtos.User;
using Microsoft.AspNetCore.Http.HttpResults;

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

        //[HttpGet("{UserRoleName}")]

        //public async Task<ActionResult<IEnumerable<AdminRoleDto>>> GetByUserRole(string UserRoleName)
        //{

        //    //if (!Enum.TryParse<UserRole>(UserRoleName, true, out var role))
        //    //    return BadRequest();var var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower()) = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())

        //var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())

        //    var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())
        //        .Select(a => new AdminRoleDto
        //        {

        //            UserRoleId = a.Id,
        //            FirstName = a.FirstName,
        //            LastName = a.LastName,
        //            UserEmail = a.Email,
        //            Address = a.Address,
        //            RoleName = a.RoleName,

        //        }).ToListAsync();

        //    return Ok(user);


        //}







    }
}