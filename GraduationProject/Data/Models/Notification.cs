using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class Notification
    {

        [Key]
        public int NotificationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("Recipient")]
        public int RecipientId { get; set; }

        // Navigation Property
        public User Recipient { get; set; }

    }
}
