using System.Collections.Generic;

namespace HotelManagement.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string status { get; set; }
        public string DateCreated { get; set; }
        public string RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}