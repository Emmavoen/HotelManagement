using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Domain.Entities
{
    public class Refund
    {
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateIssued { get; set; }

    // Foreign Keys
    public int PaymentId { get; set; }
    public Payment Payment { get; set; }

    public int RefundMethodId { get; set; }
    public RefundMethod RefundMethod { get; set; }
    
    }
}