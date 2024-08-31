using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Domain.Entities
{
    public class User : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AgeGroup { get; set; }
        public string Address { get; set; }
        public string StateId { get; set; }
        public State State { get; set; }
        //public string UserId { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public List<Feedback> Feedbacks { get; set; }
    }
}