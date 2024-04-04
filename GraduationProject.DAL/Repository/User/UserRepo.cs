using GraduationProject.DAL.Repository.Generics;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL;

public class UserRepo : GenericRepo<User>, IUserRepo
{
    private readonly ApplicationDbContext _context;
    public UserRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    //async Task<User> GetUserById(string id);
    public async Task<User> GetUserProfile(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        //await _context.Users.Entry(user).Collection(i => i.OwnedPlaces).LoadAsync();
       await _context.Users
                            .Include(user => user.OwnedPlaces)
                            .ThenInclude(place => place.Images)
                            .Where(user => user.Id == id)
                            .LoadAsync();
        return user;
    }


  
}
