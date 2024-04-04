using GraduationProject.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Managers;

public interface ICountManager
{
    Task<CountDto> GetAllData();
}
