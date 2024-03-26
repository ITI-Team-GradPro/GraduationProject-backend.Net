using GraduationProject.BL.Dtos;
using GraduationProject.DAL.Data;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Managers;

public class CategoryManager : ICategoryManager
{
    private readonly IUnitOfWork _UnitOfWork;

    public CategoryManager(IUnitOfWork unitOfWork)
    {
        _UnitOfWork = unitOfWork;
    }
    IEnumerable<CategoryReadDto> ICategoryManager.GetAll()
    {
        IEnumerable<Category> CategoryData = _UnitOfWork.Categoryrepo.GetAll();
        return (IEnumerable<CategoryReadDto>)CategoryData.Select(x => new Category
        {
            CategoryId = x.CategoryId,
            CategoryName = x.CategoryName,
            Places = x.Places
        });
    }
    CategoryReadDto ICategoryManager.GetById(int id)
    {
        throw new NotImplementedException();
    }
    int ICategoryManager.Add(CategoryAddDto categoryAddDto)
    {
        throw new NotImplementedException();
    }

    bool ICategoryManager.Delete(int id)
    {
        throw new NotImplementedException();
    }

    

   
}
