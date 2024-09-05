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
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public int RoomAmenityId { get; set; }
        public RoomAmenity RoomAmenity { get; set; } //Room Amenity
        public Booking Booking { get; set; }// is it possible
        //public List<string> Urls {get; set;}
    }
}