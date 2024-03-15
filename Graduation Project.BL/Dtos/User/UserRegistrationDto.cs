using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BL;

public class UserRegistrationDto 
{
    [Required(ErrorMessage = "User First Name must be at least 3 letters and maximum 30 letters.")]
    [StringLength(50, MinimumLength = 5)]
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "User Last Name must be at least 3 letters and maximum 30 letters.")]
    [StringLength(50, MinimumLength = 5)]
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, MinimumLength = 8)]
    public string? Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; } = string.Empty;

}
