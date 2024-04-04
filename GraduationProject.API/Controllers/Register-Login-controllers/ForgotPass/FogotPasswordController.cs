using GraduationProject.API.Services;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GraduationProject.BL.Dtos.SignDtos.ForgotPassword;
using Microsoft.AspNetCore.Identity.UI.Services;
using GraduationProject.BL.Dtos.SignDtos;
using GraduationProject.BL.Dtos.UserDto;
using System.Security.Claims;


namespace GraduationProject.API.Controllers.Register_Login_controllers.ForgotPass
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ForgotPasswordController> _logger;

        public ForgotPasswordController(UserManager<User> userManager, IEmailSender emailSender, ILogger<ForgotPasswordController> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpPost("check-email")]
        public async Task<IActionResult> CheckEmail(EmailModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return NotFound(new Response { Status = "Error", Message = "User not found!" });

                // Generate verification code and send it via email
                var verificationCode = GenerateVerificationCode();
                var emailSubject = "Verification Code";
                var emailBody = $"Your verification code is: {verificationCode}";

                await _emailSender.SendEmailAsync(model.Email, emailSubject, emailBody);

                // Store verification code in user data (assuming your User model has a property for verification code)
                user.VerificationCode = verificationCode;
                await _userManager.UpdateAsync(user);

                return Ok(new Response { Status = "Success", Message = "Verification code sent successfully!" });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                _logger.LogError(ex, "An error occurred while checking email.");

                // Return a generic error response
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"An unexpected error occurred. code {GenerateVerificationCode()}" });
            }
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerificationModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound(new Response { Status = "Error", Message = "User not found!" });

            // Check if verification code matches
            if (user.VerificationCode != model.VerificationCode)
                return BadRequest(new Response { Status = "Error", Message = "Invalid verification code!" });

            return Ok(new Response { Status = "Success", Message = "Verification code matched!" });
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound(new Response { Status = "Error", Message = "User not found!" });

            // Update passwordu
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
                return Ok(new Response { Status = "Success", Message = "Password updated successfully!" });
            else
                return BadRequest(new Response { Status = "Error", Message = "Failed to update password!" });
        }

        string GenerateVerificationCode()
        {
            // Generate a random verification code
            Random rnd = new Random();
            int verificationCode = rnd.Next(100000, 999999);
            return verificationCode.ToString();
        }

        

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(changePasswordDto.UserId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                string hashedPasswordFromDb = user.Password;

                string oldPasswordHash = PasswordHasherService.HashPassword(changePasswordDto.OldPassword);

                if (hashedPasswordFromDb != oldPasswordHash)
                {
                    return BadRequest("Failed to change password. The old password is incorrect.");
                }

                var newPasswordHash = PasswordHasherService.HashPassword(changePasswordDto.NewPassword);

                user.Password = newPasswordHash;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest("Failed to change password. Please try again later.");
                }

                return Ok("Password changed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $" Error: {ex.Message}");
            }
        }


    }
}