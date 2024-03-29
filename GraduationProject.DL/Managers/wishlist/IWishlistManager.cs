using GraduationProject.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Managers;

public interface IWishlistManager
{
   Task<IEnumerable<GetWishListDto>> GetAll(string userid);

   Task<int> Add(AddWishlistDto addWishlistDto);
   Task<bool>Delete(string userid);

}
