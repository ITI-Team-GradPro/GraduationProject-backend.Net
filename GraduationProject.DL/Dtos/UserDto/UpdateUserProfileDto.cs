using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraduationProject.Data.Models.User;

namespace GraduationProject.BL.Dtos;

public class UpdateUserProfileDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GenderEnum Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; }
    public string Bio { get; set; }

    public string Address { get; set; }

}
