using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraduationProject.Data.Models.Booking;

namespace GraduationProject.BL.Dtos.BookingDTOs
{
    public class GetBookingsByUserDTO
    {
        public int BookingId { get; set; }
        public decimal TotalPrice { get; set; }
        public Status BookingStatus { get; set; }
        public DateTime EventDate { get; set; }
    }
}
