using GraduationProject.API.Services;
using GraduationProject.Data.Models;
using GraduationProject.BL.Dtos;
using GraduationProject.BL.Dtos.SignDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.API.Controllers.Register_Login_controllers.Register
{
    [Route("api/Register/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AdminController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        #region Client Register
        [HttpPost("")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return Conflict(new Response { Status = "Error", Message = "User already exists!" });

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                RoleName = "Admin", // this the value of UserRole Enum
                Password = PasswordHasherService.HashPassword(model.Password) // Hash the password
            };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            else
                return BadRequest(new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        }
        #endregion
    }
}
