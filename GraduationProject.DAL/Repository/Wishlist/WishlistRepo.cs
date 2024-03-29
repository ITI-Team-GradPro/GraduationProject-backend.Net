using GraduationProject.DAL.Repository.Generics;
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL.Repository;

public class WishlistRepo : GenericRepo<WishList> , IWishlistRepo
{
    private readonly ApplicationDbContext _context;
    public WishlistRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
