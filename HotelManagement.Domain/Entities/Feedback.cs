using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Domain.Entities
{
    public class Feedback
    {
    public int Id { get; set; }
    public string Comments { get; set; }
    public int Rating { get; set; } // e.g., 1 to 5 stars
    public DateTime DateSubmitted { get; set; }

    // Foreign Keys
    public string UserId { get; set; }
    public User User { get; set; }

    //public int BookingId { get; set; }
    //public Booking Booking { get; set; }
    }
}