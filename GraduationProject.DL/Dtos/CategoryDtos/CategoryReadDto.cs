using GraduationProject.BL.Dtos.PlaceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos;

public class CategoryReadDto
{
    public required string Name { get; set; } = string.Empty;

    //public ICollection<GetPlacesDtos> Places { get; set; } 
}
