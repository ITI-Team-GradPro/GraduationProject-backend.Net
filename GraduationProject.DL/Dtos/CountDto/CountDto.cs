using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos;

public class CountDto
{
    public int AdminCount { get; set; }
    public int ClientCount { get; set; }

    public int HostCount { get; set; }
    public int CategoryCount { get; set; }
    public  int PlaceCount { get; set; }
    public int BookingCount { get; set; }

    public Dictionary<string , int> PlacePerCategory { get; set; }
}
