using AutoMapper;
using GraduationProject.BL.Dtos.BookingDTOs;
using GraduationProject.DAL.Repository.Generics;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.API.Sevices.Booking_Services
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepo<Booking> _bookingRepo; // Replace Booking with your actual booking model class
        private readonly IMapper _mapper; // Inject AutoMapper instance
        public BookingService(IGenericRepo<Booking> bookingRepo,IMapper mapper)//inject the repo into the service ctor
        {
            _bookingRepo = bookingRepo;
            _mapper = mapper;//automapper injected
        }

        
        Task<IEnumerable<GetAllBookingsDTO>> IBookingService.GetAllBookings()
        {
            //I cannot use async/wait here becuase the GetAll method in generic repo isn't asynchronous
            var AllBookings =  _bookingRepo.GetAll();
            var AllBookingsDTO = _mapper.Map<IEnumerable<Booking>, IEnumerable<GetAllBookingsDTO>>((IEnumerable<Booking>)AllBookings);
            return Task.FromResult(AllBookingsDTO);
            //we are using Task.FormResult here because the method is not async and the compiler doesn't return task implicitly
        }

        Task<IEnumerable<GetBookingsByUserDTO>> IBookingService.GetBookingsByUser(string UserId)
        {
            var AllBookings = _bookingRepo.GetAllById(UserId, b => b.UserId == UserId);
            var UserBookingsDTO = _mapper.Map<Task<IEnumerable<Booking>>, IEnumerable<GetBookingsByUserDTO>>(AllBookings);
            return Task.FromResult(UserBookingsDTO);
        }

        Task IBookingService.AddNewBooking(AddNewBookingDTO bookingDTO)
        {
            throw new NotImplementedException();
        }

        Task IBookingService.DeleteBooking(int id)
        {
            throw new NotImplementedException();
        }
    }

}
