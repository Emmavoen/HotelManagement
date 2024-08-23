using System;

namespace HotelManagement.Domain.Entities
{
    public class PaymentBatch
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        

    }
}