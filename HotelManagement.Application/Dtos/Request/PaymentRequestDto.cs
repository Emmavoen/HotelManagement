using System;
using HotelManagement.Domain.Enum;

namespace HotelManagement.Application.Dtos.Request
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}