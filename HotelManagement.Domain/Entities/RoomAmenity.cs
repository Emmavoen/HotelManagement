using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Domain.Entities
{
    public class RoomAmenity
    {
        public int Id { get; set; }
        public ICollection<Amenity> Amenities { get; set; } // one to many
        public Room Room { get; set; } // one to one
    }
}