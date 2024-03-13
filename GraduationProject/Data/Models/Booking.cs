using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class Booking
    {

        public enum Status
        {
            Confirmed,
            Canceled,
            Finished
        }

        public enum BookingPeriod
        {
            Day,
            Night,
            AllDay
        }

        [Key]
        public Guid BookingId { get; set; }

        [Required]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        public Status BookingStatus { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EventDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; }

        [Required]
        public BookingPeriod Period { get; set; }

        //Navigation Properties
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Place")]
        public Guid PlaceId { get; set; }
        public Place Place { get; set; }
    }


}
