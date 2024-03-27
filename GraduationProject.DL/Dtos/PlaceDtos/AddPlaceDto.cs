using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationProject.Data.Models;

namespace GraduationProject.Bl.Dtos.PlaceDtos
{
    public class AddPlaceDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

     
        public string Location { get; set; }
     
        public string Description { get; set; }

        public int PeopleCapacity { get; set; }
        public string OwnerId { get; set; }

        public int CategoryId { get; set; }

    }
}
