using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.PlaceDtos
{
    public class UpdatePlaceDto
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public int PeopleCapacity { get; set; }



    }
}
