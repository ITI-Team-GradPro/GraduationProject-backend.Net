using GraduationProject.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly GP_Db _context;
    public UnitOfWork(GP_Db context, IUserRepo userRepo)
    {
        _context = context;
        UserRepo = userRepo;
         
    }
    public IUserRepo UserRepo { get; }
 

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}
