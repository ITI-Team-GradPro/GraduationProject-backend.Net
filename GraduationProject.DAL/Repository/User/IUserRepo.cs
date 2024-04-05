using GraduationProject.DAL.Repository.Generics;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL;

public interface IUserRepo : IGenericRepo<User>
{
    Task<User> GetUserProfile(string id);

  
}
