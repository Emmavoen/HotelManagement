using System;
using System.Collections.Generic;

namespace HotelManagement.Domain.Entities
{
    public class Booking
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
          
    public ICollection<Service> Services { get; set; } 
    public ICollection<Payment> Payments { get; set; } 
    public ICollection<Feedback> Feedbacks { get; set; }
        // Constraint
    }
}