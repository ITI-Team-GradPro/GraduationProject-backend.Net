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
    //IEnumerable <CategoryReadDto>>.GetAll()
    //{
    //    IEnumerable<Category> CategoryData = _UnitOfWork.Categoryrepo.GetAll();

    //    var categorydt = CategoryData.Select(x => new CategoryReadDto
    //    {
    //        Name = x.CategoryName
    //    });
    //    return categorydt;

    //}

    public async Task<IEnumerable<CategoryReadDto>> GetAll()
    {
        IEnumerable<Category> categoryData = await _UnitOfWork.Categoryrepo.GetAll();

        var categoryDtos = categoryData.Select(x => new CategoryReadDto
        {
            Name = x.CategoryName
        });

        return categoryDtos;
    }

    //public async Task<IEnumerable<CategoryReadDto>>ICategoryManager.GetAll()
    //{
    //    IEnumerable<Category> categoryData = await _UnitOfWork.Categoryrepo.GetAll();

    //    var categoryDtos = categoryData.Select(x => new CategoryReadDto
    //    {
    //        Name = x.CategoryName
    //    });

    //    return categoryDtos;
    //}

    //CategoryReadDto ICategoryManager.GetByName(string name)
    //{
    //    string? categorybyname = _UnitOfWork.Categoryrepo.GetByName(name);
    //    if(categorybyname is null)
    //    {
    //        return null;
    //    }
    //    return new CategoryReadDto
    //    {
    //        Name = categorybyname
    //    };
    //}

    public async Task<CategoryReadDto> GetByName(string name)
    {
        string? categoryByName = await Task.Run(() => _UnitOfWork.Categoryrepo.GetByName(name));

        if (categoryByName is null)
        {
            return null;
        }

        return new CategoryReadDto
        {
            Name = categoryByName
        };
    }


    //public async Task<CategoryReadDto> GetByNameAsync(string name)
    //{
    //    string? categoryByName = await Task.Run(() => _UnitOfWork.Categoryrepo.GetByName(name));

    //    if (categoryByName is null)
    //    {
    //        throw new InvalidOperationException("Category not found."); // Throwing an exception if category is not found
    //    }

    //    return new CategoryReadDto
    //    {
    //        Name = categoryByName
    //    };
    //}


   async  Task<int> ICategoryManager.AddAsync(CategoryAddDto categoryAddDto)
    {
        Category categoryadded = new Category
        {
            CategoryName = categoryAddDto.Name
        };
      await _UnitOfWork.Categoryrepo.AddAsync(categoryadded);
      await _UnitOfWork.SaveChangesAsync();

        int categoryid = categoryadded.CategoryId;
        return categoryid;
    }

   async Task< bool> ICategoryManager.Delete(int id)
    {
        Category categorydeleted = await _UnitOfWork.Categoryrepo.GetById(id);
        //Category categorydeleted = await _UnitOfWork.Categoryrepo.Delete(category);
        if(categorydeleted is null)
        {
            return false;
        }
       await _UnitOfWork.Categoryrepo.Delete(categorydeleted);
      await  _UnitOfWork.SaveChangesAsync();
        return true;
    }

}
