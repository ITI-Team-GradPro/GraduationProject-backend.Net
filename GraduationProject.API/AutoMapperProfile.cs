using AutoMapper;
using GraduationProject.BL.Dtos;
using GraduationProject.BL.Dtos.BookingDTOs;
using GraduationProject.BL.Dtos.PlaceDtos;
using GraduationProject.Data.Models;

namespace GraduationProject.API
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Booking, AddNewBookingDTO>();

            //include nav property place in the mapping result
            CreateMap<Booking, GetAllBookingsDTO>();
            //include nav property place in the mapping result
            CreateMap<Booking, GetBookingsByUserDTO>().ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place));

            CreateMap<AddNewBookingDTO, Booking>();
            CreateMap<GetAllBookingsDTO, Booking>();
            CreateMap<GetBookingsByUserDTO, Booking>();
            CreateMap<Place, GetPlacesDtos>();
            CreateMap<GetPlacesDtos, Place>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryReadDto, Category>();

        }
    }
}
