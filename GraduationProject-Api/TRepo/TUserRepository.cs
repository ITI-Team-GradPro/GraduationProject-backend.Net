using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Identity;
using static GraduationProject_Api.TRepo.TUserRepository;

namespace GraduationProject_Api.TRepo
{
    public class TUserRepository : ITIUserRepository
    {

        private readonly UserManager<User> _userManager;

        public TUserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> CreateUserAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<User> GetUserByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);

        }
    }
}