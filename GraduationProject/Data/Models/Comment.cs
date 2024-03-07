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
        public string CommentText { get; set; }

        public DateTime CommentDate { get; set; }
        //Navigation Properties
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
