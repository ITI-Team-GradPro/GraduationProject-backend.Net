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
        
       









    }
}
