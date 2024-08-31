using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Application.Dtos.Request
{
    public class AmenityRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int RoomAmenityId { get; set; }
    }
}