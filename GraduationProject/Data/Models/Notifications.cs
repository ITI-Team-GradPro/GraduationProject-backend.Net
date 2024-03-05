using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class Notifications
    {
        public int NotificationId { get; set; }
        public int RecipentId  { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
