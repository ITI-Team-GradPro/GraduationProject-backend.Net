using GraduationProject.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Managers;

public interface IWishlistManager
{
    IEnumerable<GetWishListDto> GetAll(int userid);

    GetWishListDto? GetById(int userid, int placeid);

    WishlistDetailsDto? detailsDto(int userid);

    int Add(int userid, AddWishlistDto addWishlistDto);

    bool Update(int userid, UpdateWishlistDto updateWishlistDto);
    bool Delete(int userid);

}
