using GraduationProject.BL.Dtos.User;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.API.Controllers.Register_Login_controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUserWithRoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public GetUserWithRoleController(ApplicationDbContext context , UserManager<User> userManager)
        {
            _context = context;
            _userManager=userManager;
        }

     [HttpGet("{UserRoleName}")]

        public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetByUserRole(string UserRoleName)
        {


            var user = await _context.Users.Where(a => a.RoleName == UserRoleName.ToLower())
                .Select(a => new UserRoleDto
                {

                    UserRoleId = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    UserEmail = a.Email,
                    Address = a.Address,
                    RoleName = a.RoleName,

                }).ToListAsync();

            return Ok(user);


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    // If deletion fails, return the error messages
                    return BadRequest(result.Errors);
                }

                return Ok("User deleted successfully.");
            }
            catch (DbUpdateException ex)
            {
                // Log the exception for troubleshooting
                Console.WriteLine($"Database error occurred while deleting user: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the user.");
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                Console.WriteLine($"Error occurred while deleting user: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred while deleting the user.");
            }
        }



    }
}
