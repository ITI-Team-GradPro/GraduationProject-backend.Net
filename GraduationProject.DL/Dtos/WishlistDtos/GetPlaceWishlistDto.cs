using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos;

public class GetPlaceWishlistDto
{
    public int PlaceId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public double OverAllRating { get; set; }
    public string Description { get; set; }
    public string[] ImageUrls { get; set; }


}
