using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public  class Place
{
        public int PlaceId { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public Double Price { get; set; }
        public double OverAllRating { get; set; }
        public String Location { get; set; }
        public String Description { get; set; }
        public int PeopleCapacity { get; set; }
        //Navigation Properties
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<WishList> WishListPlaceUsers { get; set; } = new HashSet<WishList>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<ImgsPlace> Images { get; set; } = new HashSet<ImgsPlace>();
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public ICollection<PlacesCategory> PlacesCategory { get; set; } = new HashSet<PlacesCategory>();
    }
}
