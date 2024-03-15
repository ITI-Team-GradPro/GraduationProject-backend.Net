using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
//using System;
using GraduationProject.DAL.Data.Models;
//using Graduation_Project.BL;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Identity;
using GraduationProject.Data.Context;



namespace GraduationProject_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgetPasswordController : ControllerBase
    {
        private readonly GP_Db _context;

        public ForgetPasswordController(GP_Db context)
        {
            _context = context;
        }

        //private readonly ApplicationDbContext _context;
        //private readonly UserManager<User> _userManager;
        //private object _UserManager;

        //private ForgetPasswordController(UserManager<GP_Db> userManager)
        //{
        //    _userManager = userManager;
        //}

        [HttpPost("Forget-password")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Email == email);   
            //var user = await _context.FindByEmailAsync(email.Email);

            //var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            user.PaswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();
            return Ok("You can now reset your password");

        }

        [HttpPost("Reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            //var user = await _context.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token");
            }
            CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordsalt);

            user.PasswordHash = passwordHash;
            user.Passwordsalt = passwordsalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;


            await _context.SaveChangesAsync();
            return Ok("Password successfully reset");

        }

        private void CreatePasswordHash(object newPassword, out byte[] passwordHash, out byte[] passwordsalt)
        {
            throw new NotImplementedException();
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

    }



}
