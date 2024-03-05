using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int RecipentId  { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        //Navigation Property
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
