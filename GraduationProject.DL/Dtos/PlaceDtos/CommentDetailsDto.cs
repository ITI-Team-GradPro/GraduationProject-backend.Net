using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.PlaceDtos
{
    public class CommentDetailsDto
    {
    public int CommentId { get; set; }
    public string Comment { get; set; }
    public DateTimeOffset CommentDateTime { get; set; }

        public string UserId { get; set; }
    public UserDetailsDto User { get; set; }
        
    }
}
