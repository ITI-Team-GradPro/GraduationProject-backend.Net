﻿using GraduationProject.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Data.Models
{
    public class User : IdentityUser
    {

        public enum UserRole
        {
            Admin = 1,
            Host = 2,
            Client = 3
        }

        public enum GenderEnum
        {
            Male = 1,
            Female = 2 
        }


        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        public GenderEnum Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Phone]
        public string Phone { get; set; }

        [StringLength(500)] 
        public string Bio { get; set; }

        [StringLength(100)]
        public string Address { get; set; } 

        public string ImageUrl { get; set; }

        [Required]
        public string RoleName { get; set; }

        public string VerificationCode { get; set; }

        // Navigation Property
        public ICollection<Place> OwnedPlaces { get; set; } = new List<Place>();
 
        public ICollection<WishList> WishListUserPlaces { get; set; } = new HashSet<WishList>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public ICollection<Booking> ClientBookings { get; set; } = new HashSet<Booking>();

    }

}



