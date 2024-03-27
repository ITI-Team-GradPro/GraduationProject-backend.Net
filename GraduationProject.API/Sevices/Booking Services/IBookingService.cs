using GraduationProject.BL.Dtos.BookingDTOs;

namespace GraduationProject.API.Sevices.Booking_Services
{
    public interface IBookingService
    {
        Task<IEnumerable<GetAllBookingsDTO>> GetAllBookings();
        Task<IEnumerable<GetBookingsByUserDTO>> GetBookingsByUser(string UserId);
        Task AddNewBooking(AddNewBookingDTO bookingDTO);
        Task DeleteBooking(int id);
    }
}
