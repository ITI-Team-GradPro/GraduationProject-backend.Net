using GraduationProject.DAL.Repository.Generics;
using GraduationProject.DAL.Repository;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL;

public class PlacesRepo : GenericRepo<Place>, IPlacesRepo
{
    private readonly ApplicationDbContext _context;
    public PlacesRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    


}
