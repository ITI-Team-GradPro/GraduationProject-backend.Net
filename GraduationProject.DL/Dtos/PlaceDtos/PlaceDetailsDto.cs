using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraduationProject.Data.Models.User;
using Microsoft.EntityFrameworkCore;
using GraduationProject.Data.Models;

namespace GraduationProject.BL.Dtos.PlaceDtos
{
    public class PlaceDetailsDto
    {

        public int PlaceId { get; set; }
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        [Range(0, 5)]
        public double OverAllRating { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int PeopleCapacity { get; set; }
        public bool ConfirmReq { get; set; } = false;
     
        public List<string> ImageUrls { get; set; }
        public OwnerDetailsDto ownerDetailsDto { get; set; }

        public List <CommentDetailsDto> CommentDetailsDto { get; set; }    
        public CategoryDetailsDto categoryDetailsDto { get; set; }

        public List< ReviewDetailsDto >reviewDetailsDto { get; set; }


    }
}
