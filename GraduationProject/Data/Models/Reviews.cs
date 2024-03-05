using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    internal class Reviews
    {
        [Key]
        public int ReviewID { get; set; }

        [ForeignKey]
        public int UserID { get; set; }

        [ForeignKey]
        public int PlaceID { get; set; }

        public DateTime ReviewDate { get; set; }

        public string ReviewText { get; set; }

        public double Rating { get; set; }
    }
}
