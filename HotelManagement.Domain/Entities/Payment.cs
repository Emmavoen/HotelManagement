using System;
using System.Collections.Generic;
using HotelManagement.Domain.Enum;

namespace HotelManagement.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }

        // Foreign Keys
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        // Relationships
        public ICollection<Refund> Refunds { get; set; } = new List<Refund>();
    }
}