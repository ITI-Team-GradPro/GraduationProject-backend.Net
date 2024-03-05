using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class Booking
{
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public double TotalPrice { get; set; }
        public enum Status { Confirmed, Canceled, Finished }
        public DateTime EventDate { get; set; }
        public DateTime BookingDate { get; set; }
        public enum BookingPeriod { Day, Night, AllDay } 


    }
}
