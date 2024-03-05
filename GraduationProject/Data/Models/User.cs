using System;
using System.ComponentModel.DataAnnotations.Schema;

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

        //Navigation Property
        [ForeignKey("UserRole")]
        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public ICollection<Place> HostPlaces { get; set; } = new HashSet<Place>();
        public ICollection<Booking> ClientBookings { get; set; } = new HashSet<Booking>();
        public ICollection<WishList> WishListUserPlaces { get; set; } = new HashSet<WishList>();



    }
}
