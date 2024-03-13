using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        [Required]
        [StringLength(500)]
        public string CommentText { get; set; }


        // Make date time offset 
        [Required]
        public DateTimeOffset CommentDateTime { get; set; }

        // The date and the time come from the constractor
        [NotMapped]
        public DateTime CommentDate => CommentDateTime.Date;

        [NotMapped]
        public TimeSpan CommentTime => CommentDateTime.TimeOfDay;

        [ForeignKey("User")]
        public Guid UserId { get; set; }
       
        [ForeignKey("Place")]
        public Guid PlaceId { get; set; }

        //Navigation Properties
        public Place Place { get; set; }
        public User User { get; set; }


        public Comment()
        {
            CommentDateTime = DateTimeOffset.UtcNow;
        }

    }
}


