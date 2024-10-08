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
        public DateTime PaymentDate { get; set; }
        public Booking Booking { get; set; }
    }
}