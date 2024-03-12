using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        [Key]
        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; }

        // Navigation Property
        public ICollection<Place> Places { get; set; } = new HashSet<Place>();
    }
}
