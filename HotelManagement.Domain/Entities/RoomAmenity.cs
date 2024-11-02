using System.Collections.Generic;

namespace HotelManagement.Domain.Entities
{
    public class RoomAmenity{
        public int Id { get; set; }
        public ICollection<Amenity> Amenities { get; set; } // one to many
        public Room Room { get; set; } // one to one
    }
}