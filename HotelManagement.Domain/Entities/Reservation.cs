using System;

namespace HotelManagement.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoomId {get; set;}
        public Room Room {get; set;} 
        // Constraint
    }
}