using System;

namespace HotelManagement.Domain.Entities
{
    public class Refund
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssued { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string PaymentReference { get; set; }

    }
}