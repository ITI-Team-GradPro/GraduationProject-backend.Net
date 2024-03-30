using AutoMapper;
using GraduationProject.Bl.Dtos.PlaceDtos;
using GraduationProject.BL.Dtos.BookingDTOs;
using GraduationProject.DAL;
using GraduationProject.DAL.Data;
using GraduationProject.DAL.Repository.Generics;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GraduationProject.BL.Managers;
public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; // Inject AutoMapper instance
    public BookingService(IUnitOfWork UnitOfWork, IMapper mapper)//inject the repo into the service ctor
    {
        _unitOfWork = UnitOfWork;
        _mapper = mapper;//automapper injected
    }


    public async Task<IEnumerable<GetAllBookingsDTO>> GetAllBookings()
    {
        var allBookings = await _unitOfWork.Bookingrepo.GetAll();
        foreach (var booking in allBookings)
        {
            booking.BookingDate = booking.BookingDate.Date;
            booking.EventDate = booking.EventDate.Date;
        }
        var allBookingsDTO = _mapper.Map<IEnumerable<Booking>, IEnumerable<GetAllBookingsDTO>>(allBookings);
        return allBookingsDTO;
    }


    public async Task<IEnumerable<GetBookingsByUserDTO>> GetBookingsByUser(string UserId)
    {
        var userBookings = await _unitOfWork.Bookingrepo.GetAllById(UserId, b => b.UserId == UserId);

        foreach(var booking in userBookings)
        {
            booking.Place = await _unitOfWork.Placesrepo.GetById(booking.PlaceId);
            booking.BookingDate = booking.BookingDate.Date;
            booking.EventDate = booking.EventDate.Date;
        }

        if (!userBookings.Any()) // Early return if no bookings found
        {
            return null;
        }

        var UserBookingsDTO = _mapper.Map<IEnumerable<Booking>, IEnumerable<GetBookingsByUserDTO>>(userBookings);
        return UserBookingsDTO;
    }

    public async Task AddNewBooking(AddNewBookingDTO bookingDTO)
    {
        //bookingDTO.TotalPrice = (decimal) bookingDTO.TotalPrice;
        
        var NewBooking = _mapper.Map<AddNewBookingDTO, Booking>(bookingDTO);
        await _unitOfWork.Bookingrepo.AddAsync(NewBooking);
    }

    public async Task DeleteBooking(int bookingId)
    {
        var targetBooking = await _unitOfWork.Bookingrepo.GetById(bookingId);

        if (targetBooking == null)
        {
            throw new KeyNotFoundException("Booking not found"); // Throw specific exception for better handling
        }

        await _unitOfWork.Bookingrepo.Delete(targetBooking);
    }

}