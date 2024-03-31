using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.PlaceDtos
{
    public class FilterSearchPlaceDto
    {
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public double? Rating { get; set; }
        public decimal? Price { get; set; }
        public int? PeopleCapacity { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string[] ImagesUrls { get; set; }
        public string description { get; set; }

    }
}
