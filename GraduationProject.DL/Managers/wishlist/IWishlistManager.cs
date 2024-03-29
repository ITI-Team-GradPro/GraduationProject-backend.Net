using GraduationProject.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Managers;

public interface IWishlistManager
{
   Task<IEnumerable<GetWishListDto>> GetAll(int userid);

   Task<int> Add(int userid, AddWishlistDto addWishlistDto);
   Task<bool>Delete(int userid);

}
