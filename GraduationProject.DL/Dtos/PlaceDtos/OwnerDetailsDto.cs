using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraduationProject.Data.Models.User;

namespace GraduationProject.BL.Dtos.PlaceDtos
{
    public class OwnerDetailsDto
    {
        public string id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ImageUrl { get; set; }
    }
}
