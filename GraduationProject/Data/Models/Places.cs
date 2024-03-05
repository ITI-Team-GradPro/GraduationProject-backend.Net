using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public  class Places
{
        public int PlaceId { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public Double Price { get; set; }
        public double OverAllRating { get; set; }
        public String Location { get; set; }
        public String Description { get; set; }
        public int PeopleCapacity { get; set; }


    }
}
