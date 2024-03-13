using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Data.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }

        [Required]
        [StringLength(500)]
        public string ReviewText { get; set; }

        [Required]
        [Range(0, 5)] 
        public double Rating { get; set; }

        // Navigation Properties
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("Place")]
        public Guid PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
