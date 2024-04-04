using GraduationProject.BL.Dtos;
using GraduationProject.DAL.Data;
using GraduationProject.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Managers;

public class CountManager : ICountManager
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public CountManager(ApplicationDbContext context , IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }
    public async Task<CountDto> GetAllData()
    {
        var users = await _unitOfWork.Userrepo.GetAll();
        var categories = await _unitOfWork.Categoryrepo.GetAll();

        var Counts = new CountDto();

        Counts.PlacePerCategory = new Dictionary<string, int>();

        foreach(var category in categories)
        {
            int PlacesCount = await _unitOfWork.Placesrepo.GetPlaceCountInCategory(category.CategoryId);
            Counts.PlacePerCategory.Add(category.CategoryName, PlacesCount);
        }

        Counts.AdminCount = users.Count(a => a.RoleName == "Admin");
        Counts.ClientCount = users.Count(a => a.RoleName == "Client");
        Counts.HostCount = users.Count(a => a.RoleName == "Host");
        Counts.CategoryCount = await _context.Categories.CountAsync();
        Counts.PlaceCount = await _context.Places.CountAsync();
        Counts.BookingCount = await _context.Bookings.CountAsync();

        return Counts;
    }
}
