using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraduationProject.Data.Models.User;

namespace GraduationProject.BL.Dtos.PlaceDtos
{
    public class GetUserDto
    {
        public string id {  get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public GenderEnum Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string Bio { get; set; }
        public string Address { get; set; }

        public string ImageUrl { get; set; }
    }
}
