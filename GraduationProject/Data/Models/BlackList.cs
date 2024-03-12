using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models 
{
    public class BlackList
{
        public string Email { get; set; }
        public int AdminId { get; set; }
        public string BanReason { get; set; }
        public DateTime BanDate { get; set; }

    }
}
