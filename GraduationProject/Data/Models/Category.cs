using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class Category
{
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<PlacesCategory> PlacesCategory { get; set; } = new HashSet<PlacesCategory>();

    }
}
