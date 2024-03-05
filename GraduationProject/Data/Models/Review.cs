using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        public int UserID { get; set; }
        public int PlaceID { get; set; }

        public DateTime ReviewDate { get; set; }

        public string ReviewText { get; set; }

        public double Rating { get; set; }
        //Navigation Properties
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
