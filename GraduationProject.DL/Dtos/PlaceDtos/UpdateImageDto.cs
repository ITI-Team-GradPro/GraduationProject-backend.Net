﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.PlaceDtos
{
    public class UpdateImageDto
    {
        public IFormFile ImageFile { get; set; }
    }
}
