using GraduationProject.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Data.Models
{
    public class Place
    {

        [Key]
        public Guid PlaceId { get; set; }

        [Required]
        [StringLength(255)] 
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(6, 2)")] 
        public decimal Price { get; set; }

        [Range(0, 5)] 
        public double OverAllRating { get; set; }

        [Required]
        [StringLength(255)] 
        public string Location { get; set; }

        [StringLength(500)] 
        public string Description { get; set; }

        public int PeopleCapacity { get; set; }

        [Required]
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }

        [Required]
        [ForeignKey("Category")]
        public string CategoryName { get; set; }


        public bool ConfirmReq { get; set; } = false;


        // Navigation Property
        // for Places
        public User Owner { get; set; }

        public Category Category { get; set; }

        public ICollection<WishList> WishListPlaceUsers { get; set; } = new HashSet<WishList>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();

        public ICollection<ImgsPlace> Images { get; set; } = new HashSet<ImgsPlace>();


    }
}

