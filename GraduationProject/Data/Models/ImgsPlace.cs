using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class ImgsPlace
    {
        [Key]
        public int ImgsPlaceId { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; }

        [ForeignKey("Place")]
        public Guid PlaceId { get; set; }

        // Navigation Property
        public Place Place { get; set; }
    }
}
