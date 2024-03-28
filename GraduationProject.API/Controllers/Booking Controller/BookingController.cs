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

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
        
                var allBookingsDTO = await _bookingService.GetAllBookings();
                return Ok(allBookingsDTO); // Return DTOs on success
            
           
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBookings(string userId)
        {
           
                var userBookingsDTO = await _bookingService.GetBookingsByUser(userId);
                if (userBookingsDTO == null || !userBookingsDTO.Any())
                {
                    return NotFound(); // User not found
                }
                return Ok(userBookingsDTO); // Return DTOs on success
            
           
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(AddNewBookingDTO newBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

                await _bookingService.AddNewBooking(newBooking);
                return Ok(); // Success
         
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            try
            {
                await _bookingService.DeleteBooking(bookingId);
                return Ok(); // Success
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException) // Handle specific exception (booking not found)
                {
                    return NotFound();
                }
                else return BadRequest(ex);
            }
        }
    }
}
