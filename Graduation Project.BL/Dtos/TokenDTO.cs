using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BL.Dtos
{
    public class TokenDTO
    {
            public string Token { get; set; } // JWT token string
            public DateTime ExpiryDate { get; set; } // Expiry date of the token
        

    }
}
