using Graduation_Project.BL.Dtos;
using GraduationProject.Data.Models;
using GraduationProject_Api.TRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraduationProject_Api.TController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TUserController : ControllerBase
    {
   
      
            private readonly IConfiguration _configuration;
            //private readonly ITIUserRepository _userRepository;
           private readonly UserManager<User> _userManager;


        public TUserController(IConfiguration configuration , UserManager<User> userManager)
            {
                _configuration = configuration;
            _userManager = userManager;
            }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO credentials)
        {
            //Using GetUserByNameAsync:to retrieve the user with the provided username from the database. 
            var user = await _userManager.FindByNameAsync(credentials.UserName);

            if (user == null)
            {
                //return BadRequest("User not found");
                return Unauthorized("User not found");

            }

            bool isAuthenticated = await _userManager.CheckPasswordAsync(user, credentials.Password);
            if (!isAuthenticated)
            {
                // to count the AccessFailed if >3 "go to locked "
                await _userManager.AccessFailedAsync(user);
                return Unauthorized("Wrong Credentials");
            }

            //Using IsLockedOutAsync:checks if the user account is locked out due to too many failed login attempts.
            bool isLocked = await _userManager.IsLockedOutAsync(user);
            if (isLocked)
            {
                return BadRequest("Try again");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            //var roles = await _userManager.GetRolesAsync(user);
            //if (!roles.Any())
            //{
            //    return Unauthorized("User has no role assigned");
            //}

            // Add role claims
            //var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));



            var secretKey = _configuration.GetValue<string>("SecretKey")!;
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(secretKeyInBytes);
            var methodUsedInGeneratingToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var exp = DateTime.Now.AddMinutes(15);

            var jwt = new JwtSecurityToken(
               //claims: userClaims.Concat(roleClaims).ToList(),
                 claims: userClaims,
                notBefore: DateTime.Now,
                issuer: "backendApplication", //url web api from swagger
                audience: "weather",// url consumer angular
                expires: exp,
                signingCredentials: methodUsedInGeneratingToken);

            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(jwt);

            return new TokenDTO
            {
                Token = tokenString,
                ExpiryDate = exp
            };
        }

    }

}
