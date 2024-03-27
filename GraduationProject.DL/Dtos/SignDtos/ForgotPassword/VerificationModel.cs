using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.SignDtos.ForgotPassword
{
    public class VerificationModel
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }
}
