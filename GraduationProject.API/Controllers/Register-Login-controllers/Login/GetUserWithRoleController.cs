using GraduationProject.BL.Dtos.User;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.API.Controllers.Register_Login_controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUserWithRoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GetUserWithRoleController(ApplicationDbContext context)
        {
            _context = context;
            
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


        public async Task<ActionResult<IEnumerable<User>>> DeleteUser(string id)
        {

            var user = await _context.Users.Include(a => a.ClientBookings)
                .Include(a => a.WishListUserPlaces)
                .Include(a => a.OwnedPlaces)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (user == null)
                return NotFound();

            else
            {
                _context.Bookings.RemoveRange(user.ClientBookings);
                _context.Places.RemoveRange(user.OwnedPlaces);
                _context.WishList.RemoveRange(user.WishListUserPlaces);
                _context.Comments.RemoveRange(user.Comments);
                _context.Reviews.RemoveRange(user.Reviews);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok("User deleted successfully");
            }

        }













    }
}
