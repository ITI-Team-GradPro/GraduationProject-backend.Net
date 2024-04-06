using GraduationProject.BL.Dtos;
using GraduationProject.BL.Dtos.PlaceDtos;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Managers;

public interface IWishlistManager
{
    Task<IEnumerable<GetPlaceWishlistDto>> GetAll(string userid);

    Task<int> Add(AddWishlistDto addWishlistDto);
    Task<WishList> DeletePlaceFromWishlist(string userid, int placeid);

    Task<WishList> GetByUserIdAndPlaceId(string userid, int placeid);

}
