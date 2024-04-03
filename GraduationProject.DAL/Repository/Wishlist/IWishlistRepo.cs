using GraduationProject.DAL.Repository.Generics;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.DAL.Repository;

public interface IWishlistRepo : IGenericRepo<WishList>
{
    //Task<IEnumerable<Place>> UserplaceList(string userid);

    Task <WishList> placedeleted(string userid, int placeid);

    Task<IEnumerable<WishList>> userwishlist(string userid);
}
