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

        public enum ReqMessages
        {
            accepted,
            rejected,
        }

        [Key]
        public int NotificationId { get; set; }

        [Required]
        [StringLength(255)]
        public ReqMessages Message { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("Sender")]
        public int SenderId { get; set; }

        [Required]
        [ForeignKey("Recipient")]
        public int RecipientId { get; set; }

        // Navigation Property
        public User Sender { get; set; }
        public User Recipient { get; set; }

    }
}
