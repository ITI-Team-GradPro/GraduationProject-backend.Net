using AutoMapper;
using GraduationProject.BL.Dtos.BookingDTOs;
using GraduationProject.Data.Models;

namespace GraduationProject.API
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Booking, AddNewBookingDTO>();
            CreateMap<Booking, GetAllBookingsDTO>();
            CreateMap<Booking, GetBookingsByUserDTO>();
            CreateMap<AddNewBookingDTO, Booking>();
            CreateMap<GetAllBookingsDTO, Booking>();
            CreateMap<GetBookingsByUserDTO, Booking>();
        }
    }
}
