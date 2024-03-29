using GraduationProject.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace GraduationProject.DAL.Data;

public interface IUnitOfWork
{
    public ICategoryRepo Categoryrepo { get; }
    public IPlacesRepo Placesrepo { get; }

    void SaveChanges();
}
