using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.PlaceDtos
{
    public class GetOwnerPlacesDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double OverAllRating { get; set; }
        public string Location { get; set; }
        public string[] Images { get; set; }
        public string CategoryName { get; set; }

    }
}
