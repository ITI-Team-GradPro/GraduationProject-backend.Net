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
    IEnumerable<CategoryReadDto>ICategoryManager.GetAll()
    {
        IEnumerable<Category> CategoryData =  _UnitOfWork.Categoryrepo.GetAll();

        var categorydt = CategoryData.Select(x => new CategoryReadDto
        {
            Name = x.CategoryName
        });
        return categorydt;

        //return (IEnumerable<CategoryReadDto>)CategoryData.Select(x => new Category
        //{
        //    CategoryId = x.CategoryId,
        //    CategoryName = x.CategoryName,
        //    Places = x.Places
        //});
    }
    CategoryReadDto ICategoryManager.GetByName(string name)
    {
        string? categorybyname = _UnitOfWork.Categoryrepo.GetByName(name);
        if(categorybyname is null)
        {
            return null;
        }
        return new CategoryReadDto
        {
            Name = categorybyname
        };
    }
    int ICategoryManager.Add(CategoryAddDto categoryAddDto)
    {
        Category categoryadded = new Category
        {
            CategoryName = categoryAddDto.Name
        };
        _UnitOfWork.Categoryrepo.Add(categoryadded);
        _UnitOfWork.SaveChanges();
        return categoryadded.CategoryId;
    }

    bool ICategoryManager.Delete(int id)
    {
        Category categorydeleted = _UnitOfWork.Categoryrepo.GetById(id);
        if(categorydeleted is null)
        {
            return false;
        }
        _UnitOfWork.Categoryrepo.Delete(categorydeleted);
        _UnitOfWork.SaveChanges();
        return true;
    }

    

   
}
