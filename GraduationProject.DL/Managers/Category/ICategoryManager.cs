using GraduationProject.BL.Dtos;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Managers;

public interface ICategoryManager
{
   Task<IEnumerable<CategoryReadDto>> GetAll();

   Task< CategoryReadDto> GetByName(string name);

    Task <int> Add(CategoryAddDto categoryAddDto);

   Task< bool> Delete(int id);


}
