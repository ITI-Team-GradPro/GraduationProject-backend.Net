using AutoMapper;
using GraduationProject.BL.Dtos.BookingDTOs;
using GraduationProject.BL.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GraduationProject.API.Controllers.Booking_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IMapper Mapper)
        {
            _bookingService = bookingService;
            _mapper = Mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            try
            {
                var allBookingsDTO = await _bookingService.GetAllBookings();
                return Ok(allBookingsDTO);
            }
            catch (Exception ex)
            {
                // Implement more specific exception handling (e.g., database errors)
                return StatusCode(500, "An error occurred while retrieving bookings.");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBookings(string userId)
        {
            try
            {
                var userBookingsDTO = await _bookingService.GetBookingsByUser(userId);
                if (userBookingsDTO == null)
                {
                    return NotFound("No bookings found for this user.");
                }
                return Ok(userBookingsDTO);
            }
            catch (Exception ex)
            {
                // Implement more specific exception handling (e.g., database errors)
                return StatusCode(500, "An error occurred while retrieving user bookings.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(AddNewBookingDTO newBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            try
            {
                await _bookingService.AddNewBooking(newBooking);
                return Ok("booking added successfully");
            }
            catch (Exception ex)
            {
                // Implement more specific exception handling (e.g., validation errors)
                return StatusCode(500, "An error occurred while creating the booking.");
            }
        }

        [HttpDelete("booking/{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            try
            {
                var allBookings = await _bookingService.GetAllBookings();
                var targetBooking = allBookings.Where(t=> t.BookingId == bookingId);

                if (targetBooking == null)
                {
                    throw new KeyNotFoundException("Booking not found"); // Throw specific exception for better handling
                }

                await _bookingService.DeleteBooking(bookingId);
                return Ok("Booking deleted successfully."); // Informative message on success
            }
            catch (Exception ex)
            {
                // Implement more specific exception handling (e.g., database errors)
                return StatusCode(500, "An error occurred while deleting the booking.");
            }
        }
    }
}
