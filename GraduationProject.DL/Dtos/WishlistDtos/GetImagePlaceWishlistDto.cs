using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos;

public class GetImagePlaceWishlistDto
{
  
    public int ImgsPlaceId { get; set; }
    public string ImageUrl { get; set; }
    public string publicId { get; set; }
}
