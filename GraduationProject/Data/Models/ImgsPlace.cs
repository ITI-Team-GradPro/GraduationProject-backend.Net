using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class ImgsPlace
{
        public int ImgsPlaceId { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
