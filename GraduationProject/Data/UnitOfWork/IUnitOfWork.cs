using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL;

public interface IUnitOfWork
{
    public IUserRepo UserRepo { get; }
    int SaveChanges();
}
