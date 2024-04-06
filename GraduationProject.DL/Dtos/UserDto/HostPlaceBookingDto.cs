using GraduationProject.BL.Dtos.BookingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos;

public class HostPlaceBookingDto
{
    public int PlaceId { get; set; }

    public string Clientid { get; set; }

    public string FirstName{ get; set; }
    public string LastName { get; set; }
    public int BookingId { get; set; }


}
