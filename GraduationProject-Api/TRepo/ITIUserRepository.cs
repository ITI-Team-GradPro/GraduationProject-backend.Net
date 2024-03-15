using GraduationProject.Data.Models;

namespace GraduationProject_Api;

public interface ITIUserRepository
{
   
        Task<User> GetUserByNameAsync(string username);
        Task<bool> CreateUserAsync(User user, string password);
   

}
