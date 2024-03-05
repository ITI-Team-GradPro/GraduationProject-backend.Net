using System;

namespace GraduationProject.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public byte Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        

    }
}
