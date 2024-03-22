using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraduationProject.Data.Models.User;

namespace GraduationProject.DL.Dtos.SignDtos
{
    public class RegisterModel
    {


        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string RoleName { get; set; }

        [NotMapped]
        public UserRole Role
        {
            get
            {
                return Enum.TryParse(RoleName, out UserRole role) ? role : UserRole.Client;
            }
            set
            {
                RoleName = value.ToString();
            }
        }





    }
}
