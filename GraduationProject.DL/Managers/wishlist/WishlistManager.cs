using GraduationProject.BL.Dtos;
using GraduationProject.DAL.Data;
using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Managers;

public class WishlistManager : IWishlistManager
{
    private readonly IUnitOfWork _UnitOfWork;

    public WishlistManager(IUnitOfWork unitOfWork)
    {
        _UnitOfWork = unitOfWork;
    }
      async Task<int> IWishlistManager.Add(AddWishlistDto addWishlistDto)
    {
        WishList wishList = new WishList
        {
            UserId = addWishlistDto.UserId,
            PlaceId = addWishlistDto.PlaceId
        };

        await _UnitOfWork.Wishlistrepo.AddAsync(wishList);
        await _UnitOfWork.SaveChangesAsync();
        return wishList.PlaceId;

    }

    Task<bool> IWishlistManager.Delete(string userid)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<GetWishListDto>> IWishlistManager.GetAll(string userid)
    {
        throw new NotImplementedException();
    }
}
