using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    internal class Comments
    {
        [Key]
        public int CommentID { get; set; }

        [ForeignKey]
        public int UserID { get; set; }

        [ForeignKey]
        public int PlaceID { get; set; }

        public string CommentText { get; set; }

        public DateTime CommentDate { get; set; }
    }
}
