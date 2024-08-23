using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Domain.Entities
{
    public class User : IdentityUser
    {
        //guestRelationship
        public ICollection<Reservation> Reservations { get; set; }
    }
}