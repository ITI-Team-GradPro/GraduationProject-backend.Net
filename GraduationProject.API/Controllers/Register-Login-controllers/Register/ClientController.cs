using Microsoft.AspNetCore.Mvc;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using static GraduationProject.Data.Models.User;
using GraduationProject.DL.Dtos;
using GraduationProject.API.Services;
using GraduationProject.DL.Dtos.SignDtos;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace GraduationProject.API.Controllers
{
    [Route("api/Register/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public ClientController(UserManager<User> userManager, IConfiguration configuration , IEmailSender emailSender)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
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
                RoleName = "Client" , // this the value of UserRole Enum
                Password = PasswordHasherService.HashPassword(model.Password) ,// Hash the password
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                // Send confirmation email
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Client", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);

                var emailSubject = "Confirm your email";
                var emailBody = $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.";

                await _emailSender.SendEmailAsync(user.Email, emailSubject, emailBody);

                return Ok(new Response { Status = "Success", Message = "User created successfully! Please check your email for verification." });
            }
            else
            {
                return BadRequest(new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

        }
        #endregion




    }
}
