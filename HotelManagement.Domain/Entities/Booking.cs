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
        public int NumberOfOcupant { get; set; }
        //public string Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
         // one to one
                                             
        //public ICollection<Feedback> Feedbacks { get; set; }// user
                                            
    }
}