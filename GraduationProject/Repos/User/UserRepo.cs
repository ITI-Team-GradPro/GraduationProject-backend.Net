
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL;

public class UserRepo : GenericRepo<User> , IUserRepo
{
    private readonly GP_Db _context;
    public UserRepo(GP_Db context) : base(context)
    {
        _context = context;
    }
}
