using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class WishList
{
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Places")]
        public int PlaceId { get; set; }
        public Place Places { get; set; }


    }
}
