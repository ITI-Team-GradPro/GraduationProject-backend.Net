using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraduationProject.Data.Models.Booking;

namespace GraduationProject.BL.Dtos.BookingDTOs
{
    public class GetAllBookingsDTO
    {
        public string UserId { get; set; }
        public int PlaceId { get; set; }
        public int BookingId { get; set; }
        public decimal TotalPrice { get; set; }
        public Status BookingStatus { get; set; }
        public DateTime EventDate { get; set; }
        //public DateTime BookingDate { get; set; }
        //public BookingPeriod Period { get; set; }
    }
}
