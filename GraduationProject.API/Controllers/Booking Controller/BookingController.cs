using AutoMapper;
using GraduationProject.BL.Dtos.BookingDTOs;
using GraduationProject.BL.Managers;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using GraduationProject.BL.Dtos.SignDtos;


namespace GraduationProject.API.Controllers.Booking_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        //private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService/*, IMapper Mapper*/)
        {
            _bookingService = bookingService;
            //_mapper = Mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allBookingsDTO = await _bookingService.GetAllBookings();
                return Ok(allBookingsDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "An error occurred while retrieving bookings." });

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
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "An error occurred while retrieving user bookings." });

            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(AddNewBookingDTO newBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response { Status = "Error", Message = $" Error in {ModelState}" });

            }

            try
            {
                await _bookingService.AddNewBooking(newBooking);
                return Ok(new Response { Status = "Success", Message = "booking added successfully" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "An error occurred while creating the booking." });

            }
        }

        [HttpDelete("booking/{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            try
            {
                var allBookings = await _bookingService.GetAllBookings();
                var targetBooking = allBookings.Where(t => t.BookingId == bookingId);

                if (targetBooking == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Booking not found" });

                }

                await _bookingService.DeleteBooking(bookingId);
                return Ok(new Response { Status = "Success", Message = "Booking deleted successfully." });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "An error occurred while deleting the booking." });

            }
        }
        [HttpGet("{placeId:int}")]
        public async Task<IActionResult> GetUnavailableDates([FromRoute]int placeId, [FromHeader]string period)
        {
            if (string.IsNullOrEmpty(period))
            {
                return BadRequest("Period is required.");
            }
            if (placeId <= 0)
            {
                return BadRequest("Place ID is required.");
            }
            if (period != "Day" && period != "Night" && period != "AllDay")
            {
                return BadRequest("Invalid period.");
            }
            try
            {
                List<DateOnly> dates;
                dates = _bookingService.GetUnavailableDates(placeId, "AllDay").Result.ToList();
                if (period == "Day" || period == "AllDay")
                {
                    dates.AddRange(await _bookingService.GetUnavailableDates(placeId, "Day"));
                }
                if (period == "Night" || period == "AllDay")
                {
                    dates.AddRange(await _bookingService.GetUnavailableDates(placeId, "Night"));
                }
                return Ok(dates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving unavailable dates.");
            }
        }
    }
}
