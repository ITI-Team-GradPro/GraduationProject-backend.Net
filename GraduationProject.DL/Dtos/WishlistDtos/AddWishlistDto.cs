using GraduationProject.BL.Dtos.PlaceDtos;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos;

public class AddWishlistDto
{
    public int UserId { get; set; }
   
    public int PlaceId { get; set; }
}
