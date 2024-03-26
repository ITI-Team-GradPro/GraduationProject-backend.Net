using GraduationProject.DAL.Repository.Generics;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL.Repository;

public class CategoryRepo : GenericRepo<Category> , ICategoryRepo
{
    private readonly ApplicationDbContext _context;
    public CategoryRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
