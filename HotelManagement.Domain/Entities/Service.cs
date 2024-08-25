using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Domain.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public decimal Price { get; set; }

        // Foreign Key
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}